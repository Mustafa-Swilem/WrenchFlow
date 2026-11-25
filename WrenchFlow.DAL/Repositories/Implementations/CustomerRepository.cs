using Microsoft.EntityFrameworkCore;
using WrenchFlow.DAL.Data;
using WrenchFlow.DAL.Entities;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.DAL.Repositories.Implementations;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetByIdWithPersonAsync(int id)
        => await _dbSet.Include(c => c.Person)
                       .FirstOrDefaultAsync(c => c.CustomerId == id);

    public async Task<List<Customer>> GetAllWithPersonAsync()
        => await _dbSet.Include(c => c.Person).ToListAsync();
}