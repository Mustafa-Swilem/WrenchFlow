using Microsoft.EntityFrameworkCore;
using WrenchFlow.DAL.Data;
using WrenchFlow.DAL.Entities;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.DAL.Repositories.Implementations;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context) { }

    public async Task<Person?> GetByPhoneOrNationalIdAsync(string phone, string? nationalId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Phone == phone
                || (nationalId != null && p.NationalId == nationalId));
    }
}