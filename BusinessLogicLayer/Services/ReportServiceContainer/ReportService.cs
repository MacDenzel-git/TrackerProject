using AllinOne.DataHandlers;
using AllinOne.DataHandlers.ErrorHandler;
using AllinOne.DataHandlers.MailHandler;
using AllinOne.OpenXMLHandler;
using Blazored.SessionStorage;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using TrackerUIWeb.Data.DataTransferObjects;

namespace BusinessLogicLayer.Services.ReportServiceContainer
{
    public class ReportService : IReportService
    {
        private readonly GenericRepository<JournalEntry> _journalEntries;
        private readonly GenericRepository<Shop> _shop;
        private readonly IMailService _mailService;
        TrackerDbContext _context;
        private readonly IConfiguration _configuration;

        public ReportService(IMailService mailService, TrackerDbContext context, GenericRepository<JournalEntry> journalEntries, GenericRepository<Shop> shop, IConfiguration configuration)
        {

            _mailService = mailService;
            _context = context;
            _journalEntries = journalEntries;
            _shop = shop;
            _configuration = configuration;
        }


        #region
        //public async Task<DashboardDTO> SetupDashboard()
        //{
        //    DateTime date = DateTime.Now.Date;
        //    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


        //    int day = (int)date.DayOfWeek;
        //    var today = date;
        //    var sameDayNextWeek = today.AddDays(7);

        //    var rooms = await _roomService.GetAvailableAllRooms();
        //    var incentives = await _incetivesService.GetAllFilteredIncentives();
        //    var bookings = await _reportService.GetAllBookings( );
        //     var checkingOutThisWeek = bookings.Where(x=>x.IsCheckedOut== false && x.EndDate.Date <= sameDayNextWeek && x.EndDate.Date >= today);
        //     var checkingOutToday = bookings.Where(x=>x.IsCheckedOut== false && x.EndDate.Date == DateTime.Now.Date);
        //    var dashboardItems = new DashboardDTO
        //    {
        //        Bookings = bookings.ToList(),
        //        AvailableRooms = rooms.Count(),
        //        BookingsToday = checkingOutToday.Count(),
        //        Monthly = bookings.Sum(x => x.Price).ToString(),
        //        Weekly = checkingOutThisWeek.Count().ToString(),
        //        //Rooms = rooms.ToList(),
        //        //Incentives = incentives.ToList()



        //    };

        //    return dashboardItems;
        //}

        #endregion

        public async Task<IEnumerable<ReportBookingDTO>> GetTransactions(DateTime start, DateTime end, int shopId = 0)
        {



            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("startDate", start);
            parameters.Add("startDate", end);

            if (shopId == 0)
            {
                var output = await _journalEntries.FromSprocAsync<ReportBookingDTO>("spGetAllTransactionsForReport", parameters);

                return output;
            }
            else
            {
                parameters.Add("shopId", shopId);
                var output = await _journalEntries.FromSprocAsync<ReportBookingDTO>("spGetTransactionsForReport", parameters);
                return output;
            }

        }





        public async Task<OutputHandler> RunEOD(int shopId)
        {
            try
            {
                List<string> attachmentFiles = new List<string>();

                var ReportOutput = await GenerateReport(shopId);
                if (!ReportOutput.IsErrorOccured)
                {
                    if (!string.IsNullOrEmpty(ReportOutput.Result.ToString()))
                    {
                        attachmentFiles.Add(ReportOutput.Result.ToString());
                    }

                }
                else
                {
                    return ReportOutput;
                }

                //var reversalReportOutput = await GenerateReversalReport();
                //if (!reversalReportOutput.IsErrorOccured)
                //{
                //    if (!string.IsNullOrEmpty(reversalReportOutput.Result.ToString()))
                //    {
                //        attachmentFiles.Add(reversalReportOutput.Result.ToString());
                //    }

                //}


                //email report 
                AdminEmailConfiguration emailConfig = new AdminEmailConfiguration
                {
                    AdminEmail = "Denzelmac8@gmail.com",
                    Password = "jbbb jmcf glzy tboa",
                    EnableSSL = true,
                    Port = 587,
                    Host = "smtp.gmail.com"
                };
                string emails = "Denzelmachowa@gmail.com,denzelmac8@gmail.com,yammymac8@gmail.com";
                EmailProperties emailProperties = new EmailProperties
                {
                    RecepientEmail = emails,
                    Subject = "Reports for 11_01_2023",
                    EmailBody = "Please find Reports for 11_01_2023",
                    CC = emailConfig.AdminEmail
                };

                var mailOutput = await EmailMember(emailConfig, emailProperties, attachmentFiles);



                //archive data


                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "EOD Ran Successfully"
                };
            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<OutputHandler> GenerateReport(int shopId)
        {
            try
            {
                var shop = await _shop.GetSingleItem(x => x.ShopId == shopId);

                JournalEntryReportDTO ReportDTO = new JournalEntryReportDTO();

                //generate report
                var output = OpenXMLGenericProcessor<JournalEntryReportDTO>.PrepareDocumentForProcessing(ReportDTO);

                //get data 
                var reportData = await GetReportData(shopId, DateTime.Now, DateTime.Now);
                string path = "";
                if (shop is null)
                {
                    path = "AllShops";


                }
                else
                {
                    path = $"{DateTime.Now.ToString("MMMM")}\\{DateTime.Now.Day}\\{shop.ShopName}";

                }


                var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Reports", "EOD", "Report", path);

                //jan/1/
                //Filename 
                var shopName = shop is null ? "AllShops" : shop.ShopName;
                string filename = $"{DateTime.Now.ToString("dd_mm_yy")}_{shopName}_AgroLightReport.xlsx";
                if (reportData is null)
                {
                    reportData = new List<JournalEntryReportDTO>() { new JournalEntryReportDTO { TransactionDate = DateTime.Now } };
                }
                OpenXMLGenericProcessor<JournalEntryReportDTO>.CreateExcelFile(reportData.ToList(), outputFolder, output, filename);

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "",
                    Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Reports", "EOD", "AgroLightReport", filename)
                };

            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
            }
        }
        public async Task<IEnumerable<JournalEntryReportDTO>> GetTodaysReportData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            var output = await _journalEntries.FromSprocAsync<JournalEntryReportDTO>("spGetTodayTransactions");
            return output;

        }

        public async Task<SalesPieChartDTO> GetPieChart()
        {
            SalesPieChartDTO pieChartData = new SalesPieChartDTO
            {
                Sales = new List<ShopSales> 
                {

                    new ShopSales {Amount = 300000,ShopName = "mzuzu",TransactionCount = 50  },
                    new ShopSales {Amount = 1200000,ShopName = "Blantyre",TransactionCount = 20  },
                    new ShopSales {Amount = 1000000,ShopName = "Lilongwe",TransactionCount = 90 }
                },
                Shops = new List<string> { }
            };


            //pieChartData.Sales = await _journalEntries.FromSprocAsync<ShopSales>("spGetPieChartData");
             foreach (var item in pieChartData.Sales)
            {
                pieChartData.Shops.Add(item.ShopName);
            }
            return pieChartData;

        }



        public async Task<SalesBarChartDTO> GetBarChart()
        {
            SalesBarChartDTO salesBarChart = new SalesBarChartDTO
            {
                MonthNames = new List<string> { },
                Shops = new List<string> { }
            };
            var output = await _journalEntries.FromSprocAsync<BarChartData>("spGetBarChartData");
            if (output is null)
            {
                return null;
            }
            foreach (var item in output)
            {

                item.MonthName = new DateTime(DateTime.Now.Year, item.Month, DateTime.Now.Day).ToString("MMMM");

                var isExist = salesBarChart.MonthNames.Find(x => x == item.MonthName);
                if (isExist is null)
                {
                    salesBarChart.MonthNames.Add(item.MonthName);
                }


                var isShopExist = salesBarChart.Shops.Find(x => x == item.ShopName);
                if (isShopExist is null)
                {
                    salesBarChart.Shops.Add(item.ShopName);
                }
            }
            salesBarChart.Data = output.OrderBy(x => x.Month);
            return salesBarChart;

        }
        public async Task<IEnumerable<JournalEntryReportDTO>> GetReportData(int shopId, DateTime start, DateTime end)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("startDate", start.Date);
            parameters.Add("EndDate", end.Date);

            if (shopId == 0)
            {
                var output = await _journalEntries.FromSprocAsync<JournalEntryReportDTO>("spGetAllTransactionsForReport", parameters);
                return output;
            }
            else
            {
                parameters.Add("shopId", shopId);
                var output = await _journalEntries.FromSprocAsync<JournalEntryReportDTO>("GetTransactionsForReport", parameters);
                return output;
            }


        }


        #region
        //public async Task<OutputHandler> GenerateReversalReport()
        //{
        //    try
        //    {
        //        ReportBookingDTO ReportDTO = new ReportBookingDTO();

        //        //generate report
        //        var output = OpenXMLGenericProcessor<ReportBookingDTO>.PrepareDocumentForProcessing(ReportDTO);

        //        //get data 
        //        var reportData = await _reportService.GetReversalReportData(DateTime.Now, DateTime.Now);


        //        var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),"Reports", "EOD", "Reversals");

        //        //Filename 
        //        string filename = $"{DateTime.Now.ToString("dd_mm_yy")}_ReversalReport.xlsx";

        //        OpenXMLGenericProcessor<ReportBookingDTO>.CreateExcelFile(reportData.ToList(), outputFolder, output,filename);

        //        return new OutputHandler
        //        {
        //            IsErrorOccured = false,
        //            Message = "",
        //            Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Reports", "EOD", "Reversals", filename)
        //        };

        //    }
        //    catch (Exception ex)
        //    {

        //        return StandardMessages.getExceptionMessage(ex);
        //    }
        //}

        #endregion
        public async Task<OutputHandler> EmailMember(
     AdminEmailConfiguration adminEmailConfiguration,
     EmailProperties emailProperties, List<string> attachmentFiles
      )
        {
            try
            {
                var message = new MailMessage();

                message.Subject = emailProperties.Subject;
                message.Body = string.Format(emailProperties.EmailBody);
                message.IsBodyHtml = true;
                message.From = new MailAddress(adminEmailConfiguration.AdminEmail);



                if (attachmentFiles.Count > 0) //loop through attachment 
                {
                    foreach (var item in attachmentFiles)
                    {
                        Attachment attachment;
                        attachment = new Attachment(item);
                        message.Attachments.Add(attachment);
                    }
                }
                else
                {
                    message.Body = "Either System Met an error or they were no transactions found for today, Try Generate Manual Report, under reports";
                }


                if (!string.IsNullOrEmpty(emailProperties.RecepientEmail))
                {

                    string[] receipientAddresses = emailProperties.RecepientEmail.Split(',');

                    foreach (var recepientAddress in receipientAddresses)
                    {
                        message.To.Add(new MailAddress(recepientAddress));
                    }
                }
                else
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Please add at least one add recepient"
                    };
                }

                if (!string.IsNullOrEmpty(emailProperties.CC))
                {

                    string[] ccAddresses = emailProperties.CC.Split(',');
                    foreach (var ccAddress in ccAddresses)
                    {
                        message.CC.Add(new MailAddress(ccAddress));
                    }
                }
                else
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Please add at least one add recepient"
                    };
                }


                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = adminEmailConfiguration.AdminEmail,  // replace with valid value
                        Password = adminEmailConfiguration.Password // "kncdwnolobrlptnc"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = adminEmailConfiguration.Host;
                    smtp.Port = adminEmailConfiguration.Port;
                    smtp.EnableSsl = adminEmailConfiguration.EnableSSL;
                    await smtp.SendMailAsync(message);

                    return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = "Email Sent Succesfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new OutputHandler { IsErrorOccured = true, Message = "Something went wrong, Please Ensure that your email is valid and try gain" };
            }
        }
    }
}
