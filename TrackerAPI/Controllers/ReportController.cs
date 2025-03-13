using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.ReportServiceContainer;
using Microsoft.AspNetCore.Mvc;
using TrackerUIWeb.Data.DataTransferObjects;

namespace TrackerAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
       private readonly IReportService _reportService;


        public ReportController(IReportService reportService)
        {
             _reportService = reportService;
        }


        [HttpGet("GetReportData")]
        public async Task<IActionResult> GetReportData(int shopId, DateTime start, DateTime end)
        {
            var data = await _reportService.GetReportData(shopId, start.Date, end.Date);
            if (data == null)
            {
                return Ok(data = new List<JournalEntryReportDTO> { } );
            }
            else
            {
                return Ok(data);
            }
        }


        [HttpGet("RunEOD")]
        public async Task<IActionResult> RunEOD(int shopid)
        {
            var data = await _reportService.RunEOD(shopid);
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }

[HttpGet("GetPieChartData")]
        public async Task<IActionResult> GetPieChartData()
        {
            var data = await _reportService.GetPieChart();
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }
    



[HttpGet("GetBarChartData")]
        public async Task<IActionResult> GetBarChartData()
        {
            var data = await _reportService.GetBarChart();
            if (data == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Something went wrong"
                });
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
