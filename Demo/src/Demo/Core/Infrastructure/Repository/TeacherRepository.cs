using Demo.Core.Domain.Teachers;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class TeacherRepository : ITeacherRepository
{
    private readonly DatabaseContext _db;

    public TeacherRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task Add(Teacher teacher)
    {
        await _db.Teachers.AddAsync(teacher);
    }

    public async Task<IEnumerable<Teacher>> List()
    {
        return await _db.Teachers
            .ToListAsync();
    }

    public Task<Teacher?> Get(TeacherId teacherId)
    {
        return _db
            .Teachers
            .SingleOrDefaultAsync(x => x.Id.Equals(teacherId));
    }
}