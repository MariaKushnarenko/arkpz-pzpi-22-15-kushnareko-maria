using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<Report> GetReportByIdAsync(int id);
        Task<List<Report>> GetAllReportsAsync();
        Task<List<Report>> GetReportsByShelterIdAsync(int shelterId);
        Task<Report> CreateReportAsync(Report report);
        Task<bool> UpdateReportAsync(Report report);
        Task<bool> DeleteReportAsync(int id);
    }
}
