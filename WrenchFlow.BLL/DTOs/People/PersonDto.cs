namespace WrenchFlow.BLL.DTOs.People;

public class PersonDto
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? NationalId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}