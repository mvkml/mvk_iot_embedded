using SQLite;

namespace MariVshApp.Models;

/// <summary>
/// Represents a user account stored in the SQLite database.
/// Maps to the "UserAccount" table.
/// </summary>
public class UserAccount
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key referencing UserType.TypeId.
    /// Identifies the type of user (e.g., 1 = Admin, 2 = User).
    /// </summary>
    public int TypeId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}
