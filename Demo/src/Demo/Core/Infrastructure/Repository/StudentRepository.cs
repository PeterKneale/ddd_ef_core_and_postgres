using Demo.Core.Domain.Students;

namespace Demo.Core.Infrastructure.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly DatabaseContext _db;

    public StudentRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task Add(Student student)
    {
        await _db.Students.AddAsync(student);
    }

    public async Task<IEnumerable<Student>> List()
    {
        return await _db.Students.ToListAsync();
    }

    public async Task<Student?> Get(StudentId studentId)
    {
        return await _db.Students.FindAsync(studentId);
    }
}