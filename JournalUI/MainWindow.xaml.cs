using JournalSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

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
                txtLoading.Visibility = Visibility.Visible;
                dgJournalList.IsEnabled = false;

                using (var scope = App.ServiceProvider.CreateScope())
                {
                    var journalEntryService = scope.ServiceProvider.GetRequiredService<IJournalEntryService>();
                    var journalEntries = await journalEntryService.GetAllAsync();

                    if (!journalEntries.Any())
                    {
                        MessageBox.Show("No journal entries found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    dgJournalList.ItemsSource = journalEntries;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading journal entries: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                txtLoading.Visibility = Visibility.Hidden;
                dgJournalList.IsEnabled = true;
            }
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = App.ServiceProvider.GetRequiredService<AddJournalEntryWindow>();
            addWindow.ShowDialog();
            LoadJournalEntries();
        }
    }
}
