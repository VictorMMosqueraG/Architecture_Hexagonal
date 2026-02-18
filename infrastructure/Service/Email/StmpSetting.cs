namespace Infrastructure.Service.Email;

public class SmtpSettings
{
    public string Host      { get; init; } = string.Empty;
    public int    Port      { get; init; } = 587;
    public bool   EnableSsl { get; init; } = true;
    public string Username  { get; init; } = string.Empty;
    public string Password  { get; init; } = string.Empty;
    public string From      { get; init; } = string.Empty;
    public string FromName  { get; init; } = "Sistema de Facturación";
}