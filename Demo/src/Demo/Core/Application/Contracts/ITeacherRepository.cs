using Demo.Core.Domain.Teachers;

namespace Demo.Core.Application.Contracts;

public interface ITeacherRepository
{
    Task Add(Teacher teacher);
    Task<IEnumerable<Teacher>> List();
    Task<Teacher?> Get(TeacherId teacherId);
}