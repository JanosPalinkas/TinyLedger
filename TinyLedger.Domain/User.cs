namespace TinyLedger.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public Guid AccountId { get; private set; }
    public string PasswordHash { get; private set; } = default!;
    public string Role { get; private set; } = "User";

    public User(Guid id, string email, string name, Guid accountId, string passwordHash, string role)
    {
        Id = id;
        Email = email;
        Name = name;
        AccountId = accountId;
        PasswordHash = passwordHash;
        Role = role;
    }

    // For EF Core
    private User() { }
}