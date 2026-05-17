using SQLite;

namespace MariVshApp.Models;

public class UserAccount
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Pin { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UserTypeId { get; set; }
}
