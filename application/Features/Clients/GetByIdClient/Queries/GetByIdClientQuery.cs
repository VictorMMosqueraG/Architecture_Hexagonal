namespace Application.Features.Clients.GetByIdClient.Queries;

using Application.Features.Clients.GetByIdClient.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;
public class GetByIdClientQuery: ParamIdDto, IRequest<ResultDto<ClientByIdResponseDto>>;