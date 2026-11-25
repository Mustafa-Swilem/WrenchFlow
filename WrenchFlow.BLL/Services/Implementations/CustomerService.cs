using WrenchFlow.BLL.DTOs.Customers;
using WrenchFlow.BLL.Exceptions;
using WrenchFlow.BLL.Mapping;
using WrenchFlow.BLL.Services.Interfaces;
using WrenchFlow.DAL.Entities;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.BLL.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customers;
    private readonly IPersonRepository _people;

    public CustomerService(ICustomerRepository customers, IPersonRepository people)
    {
        _customers = customers;
        _people = people;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var list = await _customers.GetAllWithPersonAsync();
        return list.Select(c => c.ToDto()).ToList();
    }

    public async Task<CustomerDto> GetByIdAsync(int id)
    {
        var customer = await _customers.GetByIdWithPersonAsync(id)
            ?? throw new NotFoundException($"Customer {id} not found.");
        return customer.ToDto();
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        var person = await _people.GetByPhoneOrNationalIdAsync(dto.Phone, dto.NationalId);

        if (person?.Customer != null)
            throw new BusinessRuleException("This person is already registered as a customer.");

        person ??= dto.ToPerson();

        var customer = new Customer { Person = person, IsActive = true };
        await _customers.AddAsync(customer);
        await _customers.SaveChangesAsync();

        return customer.ToDto();
    }

    public async Task<CustomerDto> UpdateAsync(int id, UpdateCustomerDto dto)
    {
        var customer = await _customers.GetByIdWithPersonAsync(id)
            ?? throw new NotFoundException($"Customer {id} not found.");

        var existing = await _people.GetByPhoneOrNationalIdAsync(dto.Phone, dto.NationalId);
        if (existing != null && existing.PersonId != customer.PersonId)
            throw new BusinessRuleException("This phone or national ID belongs to another person.");

        dto.ApplyTo(customer);
        await _customers.SaveChangesAsync();

        return customer.ToDto();
    }

    public async Task DeactivateAsync(int id)
    {
        var customer = await _customers.GetByIdWithPersonAsync(id)
            ?? throw new NotFoundException($"Customer {id} not found.");

        customer.IsActive = false;
        await _customers.SaveChangesAsync();
    }
}