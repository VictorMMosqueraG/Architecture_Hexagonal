namespace Infrastructure.Service.Email;

using Core.Entities;

internal static class EmailTemplates
{
    internal static string FirstReminder(Invoice invoice, string clientName) => $"""
        <html>
        <body style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;">
            <div style="background: #f59e0b; padding: 20px; border-radius: 8px 8px 0 0;">
                <h2 style="color: white; margin: 0;">⚠️ Recordatorio de Pago</h2>
            </div>
            <div style="padding: 24px; background: #fff; border: 1px solid #e5e7eb;">
                <p>Estimado/a <strong>{clientName}</strong>,</p>
                <p>Su factura ha pasado a <strong>segundo recordatorio de pago</strong>.</p>
                <div style="background: #fef3c7; padding: 16px; border-radius: 8px; margin: 20px 0;">
                    <p style="margin: 4px 0;"><strong>Factura:</strong> {invoice.InvoiceNumber}</p>
                    <p style="margin: 4px 0;"><strong>Monto:</strong> ${invoice.Amount:N2}</p>
                    <p style="margin: 4px 0;"><strong>Fecha límite:</strong> {invoice.DueDate:dd/MM/yyyy}</p>
                </div>
                <p>Si no regulariza su situación, su cuenta será <strong>desactivada</strong>.</p>
            </div>
        </body>
        </html>
        """;

    internal static string SecondReminder(Invoice invoice, string clientName) => $"""
        <html>
        <body style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;">
            <div style="background: #dc2626; padding: 20px; border-radius: 8px 8px 0 0;">
                <h2 style="color: white; margin: 0;">🚨 Cuenta Desactivada</h2>
            </div>
            <div style="padding: 24px; background: #fff; border: 1px solid #e5e7eb;">
                <p>Estimado/a <strong>{clientName}</strong>,</p>
                <p>Su cuenta ha sido <strong>desactivada</strong> por falta de pago.</p>
                <div style="background: #fee2e2; padding: 16px; border-radius: 8px; margin: 20px 0;">
                    <p style="margin: 4px 0;"><strong>Factura:</strong> {invoice.InvoiceNumber}</p>
                    <p style="margin: 4px 0;"><strong>Monto:</strong> ${invoice.Amount:N2}</p>
                    <p style="margin: 4px 0;"><strong>Fecha límite:</strong> {invoice.DueDate:dd/MM/yyyy}</p>
                </div>
                <p>Para reactivar su cuenta, comuníquese con nosotros de inmediato.</p>
            </div>
        </body>
        </html>
        """;
}