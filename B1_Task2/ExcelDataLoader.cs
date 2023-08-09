using System;
using System.IO;

using B1_Task2.Model;

using Microsoft.EntityFrameworkCore;

using OfficeOpenXml;

namespace B1_Task2
{
    public class ExcelDataLoader
    {
        public async void LoadDataFromExcelAsync(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var worksheet = package.Workbook.Worksheets[0];

                var context = new SqlContext();

                var file = new Model.File();

                file.FileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                var temp = worksheet.Cells[3, 1].Value.ToString();
                file.PeriodStart = DateTime.Parse(temp.Substring(11, 11));
                file.PeriodEnd = DateTime.Parse(temp.Substring(25, 11));

                var bankName = worksheet.Cells[1,1].Value.ToString();

                if (!await context.Banks.AnyAsync(b => b.Name == bankName))
                {
                    context.Banks.Add(new Model.Bank
                    {
                        Name = bankName
                    });
                }

                await context.SaveChangesAsync();

                var bank = await context.Banks.FirstOrDefaultAsync(b => b.Name == bankName);

                file.BankId = bank.Id;

                context.Files.Add(file);

                await context.SaveChangesAsync();

                file = await context.Files.FirstOrDefaultAsync(x => x == file);

                for (int row = 10; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value is null || worksheet.Cells[row, 1].Value.ToString().Length != 4)
                    {
                        continue;
                    }

                    var accountCode = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                    var beginningDebitBalance = decimal.Parse(worksheet.Cells[row, 2].Value.ToString());
                    var beginningCreditBalance = decimal.Parse(worksheet.Cells[row, 3].Value.ToString());
                    var debitTurnover = decimal.Parse(worksheet.Cells[row, 4].Value.ToString());
                    var creditTurnover = decimal.Parse(worksheet.Cells[row, 5].Value.ToString());
                    var endingDebitBalance = decimal.Parse(worksheet.Cells[row, 6].Value.ToString());
                    var endingCreditBalance = decimal.Parse(worksheet.Cells[row, 7].Value.ToString());

                    var dataEntry = new DataEntry
                    {
                        FileId = file.Id,
                        AccountCode = accountCode,
                        BeginningDebitBalance = beginningDebitBalance,
                        BeginningCreditBalance = beginningCreditBalance,
                        DebitTurnover = debitTurnover,
                        CreditTurnover = creditTurnover,
                        EndingDebitBalance = endingDebitBalance,
                        EndingCreditBalance = endingCreditBalance
                    };

                    context.DataEntries.Add(dataEntry);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
