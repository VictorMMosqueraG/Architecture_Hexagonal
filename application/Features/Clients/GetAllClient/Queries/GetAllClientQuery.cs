namespace Application.Features.Clients.GetAllClient.Queries;

using Application.Features.Clients.GetAllClient.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;
public class GetAllClientQuery : IRequest<ResultDto<IEnumerable<ClientResponseDto>>>;