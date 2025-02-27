using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerUIWeb.Data.DataTransferObjects;

namespace BusinessLogicLayer.Services.ReportServiceContainer
{
    public interface IReportService
    {

        Task<IEnumerable<JournalEntryReportDTO>> GetTodaysReportData();
        Task<IEnumerable<JournalEntryReportDTO>> GetReportData(int shopId, DateTime start, DateTime end);
        Task<OutputHandler> RunEOD(int shopId);
        //Task<DashboardDTO> SetupDashboard();
        Task<IEnumerable<ReportBookingDTO>> GetTransactions( DateTime start, DateTime end,int shopid=0);
    }
}
