using Demo.Core.Domain.Students;

namespace Demo.Core.Application.Contracts;

public interface IStudentRepository
{
    Task Add(Student student);
    Task<IEnumerable<Student>> List();
    Task<Student?> Get(StudentId studentId);
}