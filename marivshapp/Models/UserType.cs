using SQLite;

namespace MariVshApp.Models;

/// <summary>
/// Represents a user type stored in the SQLite database.
/// Maps to the "UserType" table.
/// Used to categorize users (e.g., User, Admin).
/// </summary>
public class UserType
{
    [PrimaryKey]
    public int TypeId { get; set; }

    public string TypeName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}
