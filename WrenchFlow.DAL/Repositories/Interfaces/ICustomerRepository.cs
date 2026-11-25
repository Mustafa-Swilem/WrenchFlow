using WrenchFlow.DAL.Entities;

namespace WrenchFlow.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetByIdWithPersonAsync(int id);
    Task<List<Customer>> GetAllWithPersonAsync();
}