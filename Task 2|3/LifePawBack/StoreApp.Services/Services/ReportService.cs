using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly DataContext _dataContext;

        public ReportService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _dataContext.Report.Include(x => x.Shelter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _dataContext.Report.Include(x => x.Shelter).ToListAsync();
        }

        public async Task<List<Report>> GetReportsByShelterIdAsync(int shelterId)
        {
            return await _dataContext.Report.Include(x => x.Shelter).Where(x => x.ShelterId == shelterId).ToListAsync();
        }

        public async Task<Report> CreateReportAsync(Report report)
        {
            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == report.ShelterId);
            report.Shelter = shelter;
            await _dataContext.Report.AddAsync(report);
            await _dataContext.SaveChangesAsync();
            return report;
        }

        public async Task<bool> UpdateReportAsync(Report report)
        {
            var existingReport = await _dataContext.Report.FirstOrDefaultAsync(x => x.Id == report.Id);
            if (existingReport == null) return false;

            existingReport.Report_Type = report.Report_Type;
            existingReport.Date = report.Date;
            existingReport.CreationPeriod = report.CreationPeriod;
            existingReport.Description = report.Description;
            existingReport.File_path = report.File_path;
            existingReport.ShelterId = report.ShelterId;

            _dataContext.Report.Update(existingReport);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReportAsync(int id)
        {
            var report = await _dataContext.Report.FirstOrDefaultAsync(x => x.Id == id);
            if (report == null) return false;

            _dataContext.Report.Remove(report);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
