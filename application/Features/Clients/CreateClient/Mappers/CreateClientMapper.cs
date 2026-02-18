namespace Application.Features.Clients.CreateClient.Mappers;

using Application.Features.Clients.CreateClient.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Constants;


public class CreateClientMapper : Profile
{
    public CreateClientMapper()
    {
        CreateMap<CreateClientRequestDto, Client>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ClientStatus.Active))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}