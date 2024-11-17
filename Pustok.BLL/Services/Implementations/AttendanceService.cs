using AutoMapper;
using Pustok.BLL.Services.Abstractions;
using Pustok.BLL.ViewModels;
using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction.Generic;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<Attendance> _attendanceRepository;
    private readonly IMapper _mapper;

    public AttendanceService(IRepository<Attendance> attendanceRepository, IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceViewModel>> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync(a => true);
        return _mapper.Map<List<AttendanceViewModel>>(attendances);
    }

    public async Task<AttendanceViewModel> CreateAttendanceAsync(CreateAttendanceViewModel model)
    {
        var attendance = _mapper.Map<Attendance>(model);
        var createdAttendance = await _attendanceRepository.CreateAsync(attendance);

        return _mapper.Map<AttendanceViewModel>(createdAttendance);
    }

    public async Task<AttendanceViewModel> UpdateAttendanceAsync(int id, UpdateAttendanceViewModel model)
    {
        var existingAttendance = await _attendanceRepository.GetAsync(id);
        if (existingAttendance == null) throw new Exception("Attendance not found.");

        _mapper.Map(model, existingAttendance);
        var updatedAttendance = await _attendanceRepository.UpdateAsync(existingAttendance);

        return _mapper.Map<AttendanceViewModel>(updatedAttendance);
    }

    public async Task<AttendanceViewModel> DeleteAttendanceAsync(int id)
    {
        var attendance = await _attendanceRepository.GetAsync(id);
        if (attendance == null) throw new Exception("Attendance not found.");

        var deletedAttendance = await _attendanceRepository.DeleteAsync(attendance);
        return _mapper.Map<AttendanceViewModel>(deletedAttendance);
    }
}
