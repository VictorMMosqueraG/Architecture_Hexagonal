namespace Tests.Builders;

using Core.Constants;
using Core.Entities;

public class ClientBuilder
{
    private string _id = "6994d1917bd3c08095284d0c";
    private string _name = "Carlos Mendoza";
    private string _email = "carlos@example.com";
    private string _documentNumber = "123456789";
    private string? _phone = "3001234567";
    private string _status = ClientStatus.Active;
    private DateTime _createdAt = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private DateTime _updatedAt = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public ClientBuilder WithId(string id) { _id = id; return this; }
    public ClientBuilder WithName(string name) { _name = name; return this; }
    public ClientBuilder WithEmail(string email) { _email = email; return this; }
    public ClientBuilder WithDocumentNumber(string documentNumber) { _documentNumber = documentNumber; return this; }
    public ClientBuilder WithPhone(string? phone) { _phone = phone; return this; }
    public ClientBuilder WithStatus(string status) { _status = status; return this; }

    public Client Build() => new()
    {
        Id = _id,
        Name = _name,
        Email = _email,
        DocumentNumber = _documentNumber,
        Phone = _phone,
        Status = _status,
        CreatedAt = _createdAt,
        UpdatedAt = _updatedAt
    };

    public static Client Default() => new ClientBuilder().Build();
}