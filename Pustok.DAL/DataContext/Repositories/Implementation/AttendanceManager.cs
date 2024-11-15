using Pustok.DAL.DataContext.Entities;
using Pustok.DAL.DataContext.Repositories.Abstraction;
using Pustok.DAL.DataContext.Repositories.Implementation.Generic;

namespace Pustok.DAL.DataContext.Repositories.Implementation;

public class AttendanceManager : Repository<Attendance>, IAttendanceRepository
{
    public AttendanceManager(AppDBContext context) : base(context)
    {
        
    }
}
