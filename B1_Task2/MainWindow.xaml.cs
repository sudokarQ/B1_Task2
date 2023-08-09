using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using Microsoft.Win32;

namespace B1_Task2
{
    public partial class MainWindow : Window
    {
        private SqlContext context = new SqlContext();
        private ObservableCollection<string> loadedFiles = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            loadedFiles.CollectionChanged += LoadedFiles_CollectionChanged;
            fileListView.ItemsSource = context.Files.Select(x => x.Id).ToList();
        }

        private async void LoadExcelButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                var excelDataLoader = new ExcelDataLoader();
                var fileId = await excelDataLoader.LoadDataFromExcelAsync(filePath);
                loadedFiles.Add(fileId.ToString());
            }
        }

        private void LoadedFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            fileListView.ItemsSource = context.Files.Select(x => x.Id).ToList(); ; // Подключаем обновленный источник данных
        }

        private void FileListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fileListView.SelectedIndex >= 0)
            {
                int selectedFileId = int.Parse(fileListView.SelectedItem.ToString());
                LoadDataEntries(selectedFileId);
                CalculateAndDisplayTotals(selectedFileId);
            }
        }

        private void LoadDataEntries(int fileId)
        {
            var data = context.DataEntries.Where(x => x.FileId == fileId).ToList();
            dataGrid.ItemsSource = data;
        }

        private void CalculateAndDisplayTotals(int fileId)
        {
            var groupedEntries = context.DataEntries
                .Where(x => x.FileId == fileId)
                .GroupBy(entry => entry.AccountCode.ToString().Substring(0, 2))
                .Select(group => new
                {
                    Class = group.Key,
                    BeginningDebitTotal = group.Sum(entry => entry.BeginningDebitBalance),
                    BeginningCreditTotal = group.Sum(entry => entry.BeginningCreditBalance),
                    DebitTurnoverTotal = group.Sum(entry => entry.DebitTurnover),
                    CreditTurnoverTotal = group.Sum(entry => entry.CreditTurnover),
                    EndingDebitTotal = group.Sum(entry => entry.EndingDebitBalance),
                    EndingCreditTotal = group.Sum(entry => entry.EndingCreditBalance)
                })
                .OrderBy(group => group.Class);

            dataGridTotals.ItemsSource = groupedEntries.ToList();
        }
    }
}
