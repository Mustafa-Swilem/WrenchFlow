using WrenchFlow.BLL.DTOs.Customers;
using WrenchFlow.DAL.Entities;

namespace WrenchFlow.BLL.Mapping;

public static class CustomerMappings
{
    public static CustomerDto ToDto(this Customer customer) => new()
    {
        CustomerId = customer.CustomerId,
        FirstName = customer.Person.FirstName,
        LastName = customer.Person.LastName,
        Phone = customer.Person.Phone,
        Email = customer.Person.Email,
        Address = customer.Person.Address,
        NationalId = customer.Person.NationalId,
        IsActive = customer.IsActive,
        CreatedDate = customer.CreatedDate
    };

    public static Person ToPerson(this CreateCustomerDto dto) => new()
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Phone = dto.Phone,
        Email = dto.Email,
        Address = dto.Address,
        NationalId = dto.NationalId,
        IsActive = true
    };

    public static void ApplyTo(this UpdateCustomerDto dto, Customer customer)
    {
        customer.Person.FirstName = dto.FirstName;
        customer.Person.LastName = dto.LastName;
        customer.Person.Phone = dto.Phone;
        customer.Person.Email = dto.Email;
        customer.Person.Address = dto.Address;
        customer.Person.NationalId = dto.NationalId;
    }
}