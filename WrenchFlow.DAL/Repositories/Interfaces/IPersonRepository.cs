using WrenchFlow.DAL.Entities;

namespace WrenchFlow.DAL.Repositories.Interfaces;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<Person?> GetByPhoneOrNationalIdAsync(string phone, string? nationalId);
}