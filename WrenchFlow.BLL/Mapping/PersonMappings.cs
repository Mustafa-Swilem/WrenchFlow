using WrenchFlow.BLL.DTOs.People;
using WrenchFlow.DAL.Entities;

namespace WrenchFlow.BLL.Mapping;

public static class PersonMappings
{
    public static PersonDto ToDto(this Person person) => new()
    {
        PersonId = person.PersonId,
        FirstName = person.FirstName,
        LastName = person.LastName,
        Phone = person.Phone,
        Email = person.Email,
        Address = person.Address,
        NationalId = person.NationalId,
        IsActive = person.IsActive,
        CreatedDate = person.CreatedDate
    };
}