using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace JournalSystemUI
{
    public partial class MainWindow : Window
    {
        private readonly IJournalEntryService _journalEntryService;

        public MainWindow()
        {
            InitializeComponent();

            
            _journalEntryService = App.ServiceProvider.GetRequiredService<IJournalEntryService>();

           
            LoadJournalEntries();
        }

        private async void LoadJournalEntries()
        {
            try
            {
                txtLoading.Visibility = Visibility.Visible; 
                dgJournalList.IsEnabled = false; 

                var journalEntries = await _journalEntryService.GetAllAsync();

                if (!journalEntries.Any())
                {
                    MessageBox.Show("No journal entries found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                dgJournalList.ItemsSource = journalEntries;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading journal entries: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

     

      
    }
}
