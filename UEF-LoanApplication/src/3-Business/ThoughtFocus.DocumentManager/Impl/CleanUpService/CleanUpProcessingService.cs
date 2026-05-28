using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Workflow;

namespace ThoughtFocus.DocumentManager.Impl.CleanUpService
{
    public class CleanUpProcessingService 
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICleanUpApplicationDocumentRepository _cleanUpApplicationDocumentRepository;
        public CleanUpProcessingService(IServiceProvider serviceProvider, ICleanUpApplicationDocumentRepository cleanUpApplicationDocumentRepository)
        {
            _serviceProvider = serviceProvider;
            this._cleanUpApplicationDocumentRepository = cleanUpApplicationDocumentRepository;
        }
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    // This method is called when the background service starts.
        //    // You can implement any long-running tasks here if needed.
        //    //while (!stoppingToken.IsCancellationRequested)
        //    //{
        //    //    // You can add logic here if you want to perform periodic tasks.
        //    //    await Task.Delay(1000, stoppingToken); // Example: wait for 1 second
        //    //}
        //}
        public async Task<long> ProcessExcelDataAsync(IFormFile file)
        {
            long rslt = 0;
            using (var stream = file.OpenReadStream())
            {
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var lastRow = worksheet.LastRowUsed().RowNumber();
                    var lastColumn = worksheet.LastColumnUsed().ColumnNumber();
                    for (int row = 2; row <= lastRow; row++) // Start from the second row
                    {
                        var name = worksheet.Cell(row, 1).GetString(); // Get value from the first column
                        var description = worksheet.Cell(row, 2).GetString(); // Get value from the second column

                        // Process the data as needed
                        // For example, you can create a model and save it to the database
                        var grantApplication = new GrantApplication
                        {
                            GrantNumber = worksheet.Cell(row, 1).GetString(),
                            DateApplied = DateTime.Parse(worksheet.Cell(row, 2).GetString()),
                            DisbursedAmount = Convert.ToDecimal(worksheet.Cell(row, 7).GetString()),
                        };
                        rslt =  await this._cleanUpApplicationDocumentRepository.UpdateApplicationDocument(grantApplication);
                        if(rslt > 0)
                        {
                            rslt = WorkflowInit.UpdateWorkFlow(rslt);
                        }
                       
                        // Save dataModel to the database
                    }
                    //for (int row = 1; row <= lastRow; row++)
                    //{
                    //    for (int col = 1; col <= lastColumn; col++)
                    //    {
                    //        var cellValue = worksheet.Cell(row, col).GetString();
                    //        var grantApplication = new GrantApplication
                    //        {
                    //             GrantNumber = 
                    //        };

                    //        // Process cellValue as needed
                    //    }
                    //}
                }
            }
            return rslt;
            //using (var package = new ExcelPackage(new MemoryStream(excelData)))
            //{
            //    var worksheet = package.Workbook.Worksheets.First();
            //    var rowCount = worksheet.Dimension.Rows;

            //    using (var scope = _serviceProvider.CreateScope())
            //    {
            //       // var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //        for (int row = 2; row <= rowCount; row++) // Assuming the first row is the header
            //        {
            //            //var dataModel = new MyDataModel
            //            //{
            //            //    Name = worksheet.Cells[row, 1].Text,
            //            //    Description = worksheet.Cells[row, 2].Text
            //            //};

            //            //dbContext.MyDataModels.Add(dataModel);
            //        }

            //       // await dbContext.SaveChangesAsync();
            //    }
            //}
        }
    }
}
