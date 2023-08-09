using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace B1_Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlContext context = new SqlContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadExcelButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                var excelDataLoader = new ExcelDataLoader();
                excelDataLoader.LoadDataFromExcelAsync(filePath);
                LoadDataEntries();
                CalculateAndDisplayTotals();
            }
        }
        private void LoadDataEntries()
        {
            dataGrid.ItemsSource = context.DataEntries.ToList();
        }

        private void CalculateAndDisplayTotals()
        {
            var groupedEntries = context.DataEntries
                .GroupBy(entry => entry.AccountCode.ToString().Substring(0,2))
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
