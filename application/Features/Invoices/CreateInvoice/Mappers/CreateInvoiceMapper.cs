using Application.Features.Invoices.CreateInvoice.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Constants;

namespace Application.Features.Invoices.CreateInvoice.Mappers;

/// <summary>
/// Perfil de AutoMapper para la creación de facturas.
/// Mapea <see cref="CreateInvoiceRequestDto"/> a la entidad <see cref="Invoice"/>.
/// </summary>
public class CreateInvoiceMapper : Profile
{
    /// <summary>
    /// Define las reglas de mapeo, incluyendo valores por defecto
    /// para <c>Status</c>, <c>CreatedAt</c> y <c>UpdatedAt</c>.
    /// </summary>
    public CreateInvoiceMapper()
    {
        CreateMap<CreateInvoiceRequestDto, Invoice>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => InvoiceStatus.Pending))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}