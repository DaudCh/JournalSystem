using JournalSystem.Core.Services;
using System.Windows;

namespace JournalUI
{
    public partial class AddJournalEntryWindow : Window
    {
        private readonly IJournalEntryService _journalEntryService;

        public AddJournalEntryWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Journal Entry Saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
