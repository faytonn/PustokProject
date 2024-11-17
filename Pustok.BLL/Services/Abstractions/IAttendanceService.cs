using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Services.Abstractions;

public interface IAttendanceService
{
    Task<List<AttendanceViewModel>> GetAllAttendancesAsync();
    Task<AttendanceViewModel> CreateAttendanceAsync(CreateAttendanceViewModel model);
    Task<AttendanceViewModel> UpdateAttendanceAsync(int id, UpdateAttendanceViewModel model);
    Task<AttendanceViewModel> DeleteAttendanceAsync(int id);
}
