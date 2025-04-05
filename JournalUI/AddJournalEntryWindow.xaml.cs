
using System.Collections.ObjectModel;
using System.Windows;
using JournalSystem.Core.DTOS.Currency;
using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.Services;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.DTOS.JournalLines;
using Microsoft.EntityFrameworkCore;
using JournalSystem.Core.DTOS.Account;
using JournalSystem.Core.DTOS.CostCentre;
using JournalSystem.Core.DTOS.Dimension;

namespace JournalUI
{
    public partial class AddJournalEntryWindow : Window
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly ICurrencyService _currencyService;
        private readonly IAccountService _accountService;
        private readonly ICostCenterService _costCenterService;
        private readonly IDimensionService _dimensionService;
        private readonly IJournalEntryRepository _journalEntryRepository;

        public ObservableCollection<string> JournalTypes { get; } = new();
        public ObservableCollection<string> Statuses { get; } = new() { "Draft", "Posted", "Approved", "Rejected" };
        public ObservableCollection<string> Periods { get; } = new();
        public ObservableCollection<CurrencyResponseDTO> Currencies { get; } = new();
        public ObservableCollection<AccountResponseDTO> Account { get; } = new();
        public ObservableCollection<CostCenterResponseDTO> CostCenter { get; } = new();
        public ObservableCollection<DimensionResponseDTO> Dimensions { get; } = new();
        public ObservableCollection<JournalLineDTO> JournalLines { get; } = new();

        public AddJournalEntryWindow(
            IJournalEntryService journalEntryService,
            ICurrencyService currencyService,
            IAccountService accountService,
            ICostCenterService costCenterService,
            IDimensionService dimensionService,
            IJournalEntryRepository journalEntryRepository)
        {
            InitializeComponent();
            DataContext = this;

            _journalEntryService = journalEntryService;
            _currencyService = currencyService;
            _accountService = accountService;
            _costCenterService = costCenterService;
            _dimensionService = dimensionService;
            _journalEntryRepository = journalEntryRepository;

            LoadDropdownData();

        }

        private async void LoadDropdownData()
        {
            try
            {
                var journalEntries = await _journalEntryRepository.GetAllAsync();

                JournalTypes.Clear();
                foreach (var type in journalEntries.Select(j => j.JournalType).Distinct())
                    JournalTypes.Add(type);

                Periods.Clear();
                foreach (var period in journalEntries.Select(j => j.Period).Distinct())
                    Periods.Add(period);


                var currencies = await _currencyService.GetAllAsync();
                Currencies.Clear();
                foreach (var currency in currencies)
                    Currencies.Add(currency);


                var accounts = await _accountService.GetAllAsync();
                var costCenters = await _costCenterService.GetAllAsync();
                var dimensions = await _dimensionService.GetAllAsync();

                Account.Clear();
                foreach (var account in accounts)
                    Account.Add(account);

                CostCenter.Clear();
                foreach (var cc in costCenters)
                    CostCenter.Add(cc);

                Dimensions.Clear();
                foreach (var dim in dimensions)
                    Dimensions.Add(dim);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dropdown data: {ex.Message}");
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtJournalNumber.Text, out int journalNumber))
                {
                    MessageBox.Show("Invalid Journal Number");
                    return;
                }


                if (cmbJournalType.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Journal Type");
                    return;
                }

                if (cmbPeriod.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Period");
                    return;
                }

                if (cmbCurrency.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Currency");
                    return;
                }

                if (!float.TryParse(txtExchangeRate.Text, out float exchangeRate))
                {
                    MessageBox.Show("Invalid Exchange Rate");
                    return;
                }

                // Convert JournalLines ObservableCollection to List<CreateJournalLineDTO>
                var journalLineDTOs = JournalLines.Select(jl => new CreateJournalLineDTO
                {
                    AccountId = jl.AccountId,
                    CostCenterId = jl.CostCenterId,
                    DimensionId = jl.DimensionId,
                    Description = jl.Description,
                    Reference = jl.Reference,
                    Debit = (float)jl.Debit,
                    Credit = (float)jl.Credit
                }).ToList();



                // Build main JournalEntry DTO
                var entry = new CreateJournalEntryDTO
                {
                    JournalNumber = journalNumber,
                    Status = cmbStatus.SelectedItem?.ToString() ?? "Draft",
                    JournalType = cmbJournalType.SelectedItem.ToString(),
                    Period = cmbPeriod.SelectedItem.ToString(),
                    PostingDate = dpPostingDate.SelectedDate ?? DateTime.Now,
                    DocumentDate = dpDocumentDate.SelectedDate ?? DateTime.Now,
                    CurrencyId = (cmbCurrency.SelectedItem as CurrencyResponseDTO)?.Id ?? 0,
                    ExchangeRate = exchangeRate,
                    JournalLines = journalLineDTOs
                };

                // Save to database
                await _journalEntryService.AddAsync(entry);

                MessageBox.Show("Journal Entry saved successfully!");
                Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}