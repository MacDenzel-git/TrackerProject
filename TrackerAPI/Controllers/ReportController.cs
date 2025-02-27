using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.ReportServiceContainer;
using Microsoft.AspNetCore.Mvc;

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
    }
}
