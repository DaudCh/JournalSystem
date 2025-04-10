using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Documents;

namespace JournalUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadJournalEntries();
        }

        private async void LoadJournalEntries()
        {
            try
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    txtLoading.Visibility = Visibility.Visible;
                    dgJournalList.IsEnabled = false;
                });

                using (var scope = App.ServiceProvider.CreateScope())
                {
                    var journalEntryService = scope.ServiceProvider.GetRequiredService<IJournalEntryService>();
                    var journalEntries = await journalEntryService.GetAllAsync();

                    await Dispatcher.InvokeAsync(() =>
                    {
                        if (!journalEntries.Any())
                        {
                            MessageBox.Show("No journal entries found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        dgJournalList.ItemsSource = journalEntries;
                    });
                }
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error loading journal entries: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
            finally
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    txtLoading.Visibility = Visibility.Hidden;
                    dgJournalList.IsEnabled = true;
                });
            }
        }


        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = App.ServiceProvider.GetRequiredService<AddJournalEntryWindow>();
            addWindow.ShowDialog();
             LoadJournalEntries();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            var journalEntry = (JournalEntryDTO)hyperlink.DataContext;

            var editWindow = App.ServiceProvider.GetRequiredService<AddJournalEntryWindow>();
            editWindow.LoadExistingJournalEntry(journalEntry.Id); 
            editWindow.ShowDialog();
            LoadJournalEntries(); 
        }
    }
}
