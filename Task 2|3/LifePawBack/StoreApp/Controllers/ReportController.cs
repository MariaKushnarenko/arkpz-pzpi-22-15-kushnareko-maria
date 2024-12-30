using Microsoft.AspNetCore.Mvc;
using StoreApp.Services.Interfaces;
using StoreApp.DatabaseProvider.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<List<Report>>> GetReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        // GET: api/Reports/shelter/5
        [HttpGet("shelter/{shelterId}")]
        public async Task<ActionResult<List<Report>>> GetReportsByShelter(int shelterId)
        {
            var reports = await _reportService.GetReportsByShelterIdAsync(shelterId);
            return Ok(reports);
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest("Invalid report data.");
            }

            var createdReport = await _reportService.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.Id }, createdReport);
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] Report report)
        {
            if (id != report.Id)
            {
                return BadRequest("Report ID mismatch.");
            }

            var updated = await _reportService.UpdateReportAsync(report);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var deleted = await _reportService.DeleteReportAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
