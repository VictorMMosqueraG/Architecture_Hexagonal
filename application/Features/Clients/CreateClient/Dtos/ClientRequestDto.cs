
namespace Application.Features.Clients.CreateClient.Dtos;

public class CreateClientRequestDto
{
    public required string Name {get;set;}
    public required string Email {get;set;}
    public required string DocumentNumber {get;set;}
    public string? Phone {get;set;}

}