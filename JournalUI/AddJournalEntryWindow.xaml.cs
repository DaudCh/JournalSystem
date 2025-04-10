
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
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

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
        private bool _isEditMode = false;
        private int _existingEntryId = 0;

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

                await Dispatcher.InvokeAsync(() =>
                {
                    JournalTypes.Clear();
                    Periods.Clear();
                    foreach (var type in journalEntries.Select(j => j.JournalType).Distinct())
                        JournalTypes.Add(type);

                    foreach (var period in journalEntries.Select(j => j.Period).Distinct())
                        Periods.Add(period);
                });

                var currencies = await _currencyService.GetAllAsync();
                await Dispatcher.InvokeAsync(() =>
                {
                    Currencies.Clear();
                    foreach (var currency in currencies)
                        Currencies.Add(currency);
                });

                var accounts = await _accountService.GetAllAsync();
                await Dispatcher.InvokeAsync(() =>
                {
                    Account.Clear();
                    foreach (var account in accounts)
                        Account.Add(account);
                });

                var costCenters = await _costCenterService.GetAllAsync();
                await Dispatcher.InvokeAsync(() =>
                {
                    CostCenter.Clear();
                    foreach (var cc in costCenters)
                        CostCenter.Add(cc);
                });

                var dimensions = await _dimensionService.GetAllAsync();
                await Dispatcher.InvokeAsync(() =>
                {
                    Dimensions.Clear();
                    foreach (var dim in dimensions)
                        Dimensions.Add(dim);
                });
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error loading dropdown data: {ex.Message}");
                });
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

                if (cmbJournalType.SelectedItem == null || cmbPeriod.SelectedItem == null ||
                    cmbCurrency.SelectedItem == null || !float.TryParse(txtExchangeRate.Text, out float exchangeRate))
                {
                    MessageBox.Show("Please ensure all fields are filled correctly.");
                    return;
                }

                if (JournalLines.Count == 0)
                {
                    MessageBox.Show("Please add at least one journal line before saving.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                foreach (var line in JournalLines)
                {
                    if (!line.AccountId.HasValue ||
                        !line.CostCenterId.HasValue ||
                        !line.DimensionId.HasValue ||
                        string.IsNullOrWhiteSpace(line.Description) ||
                        string.IsNullOrWhiteSpace(line.Reference) ||
                        (line.Debit == 0 && line.Credit == 0))
                    {
                        MessageBox.Show("Please complete all fields for each journal line. Debit or Credit must also be filled.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                var totalDebit = JournalLines.Sum(jl => jl.Debit);
                var totalCredit = JournalLines.Sum(jl => jl.Credit);

                if (totalDebit != totalCredit)
                {
                    MessageBox.Show("Total Debit must be equal to Total Credit.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var journalLineDTOs =JournalLines.Select(jl => new CreateJournalLineDTO
                {
                    AccountId = jl.AccountId ?? 0,
                    CostCenterId = jl.CostCenterId ?? 0,
                    DimensionId = jl.DimensionId ?? 0,
                    Description = jl.Description,
                    Reference = jl.Reference,
                    Debit = jl.Debit,
                    Credit = jl.Credit
                }).ToList();
                if (_isEditMode && _existingEntryId > 0)
                {
                    // Update existing entry
                    var updateEntry = new UpdateJournalEntryDTO
                    {
                        Id = _existingEntryId,
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

                    await _journalEntryService.UpdateAsync(updateEntry);
                }
                else
                {
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

                    await _journalEntryService.AddAsync(entry);
                }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JournalLines.Add(new JournalLineDTO());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is JournalLineDTO lineToRemove)
            {
                JournalLines.Remove(lineToRemove);
            }
        }



        public async void LoadExistingJournalEntry(int id)
        {
            try
            {
                _isEditMode = true;
                _existingEntryId = id;

                using (var scope = App.ServiceProvider.CreateScope())
                {
                    var journalService = scope.ServiceProvider.GetRequiredService<IJournalEntryService>();
                    var existingEntry = await journalService.GetByIdAsync(id);

                    await Dispatcher.InvokeAsync(() =>
                    {
                        txtJournalNumber.Text = existingEntry.JournalNumber.ToString();
                        cmbJournalType.SelectedItem = existingEntry.JournalType;
                        cmbStatus.SelectedItem = existingEntry.Status;
                        cmbPeriod.SelectedItem = existingEntry.Period;
                        dpPostingDate.SelectedDate = existingEntry.PostingDate;
                        dpDocumentDate.SelectedDate = existingEntry.DocumentDate;

                        // Ensure Currencies are populated before selecting the currency
                        cmbCurrency.SelectedItem = Currencies.FirstOrDefault(c => c.Id == existingEntry.CurrencyId);

                        txtExchangeRate.Text = existingEntry.ExchangeRate.ToString();

                        JournalLines.Clear();
                        foreach (var line in existingEntry.JournalLines)
                        {
                            JournalLines.Add(new JournalLineDTO
                            {
                                AccountId = line.AccountId,
                                CostCenterId = line.CostCenterId,
                                DimensionId = line.DimensionId,
                                Description = line.Description,
                                Reference = line.Reference,
                                Debit = line.Debit,
                                Credit = line.Credit
                            });
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error loading journal entry: {ex.Message}");
                });
            }
        }


    }
}