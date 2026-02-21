namespace Application.Features.Clients.GetByIdClient.Queries;

using Application.Features.Clients.GetByIdClient.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Query para obtener un cliente por su ID.
/// Hereda el parámetro <c>Id</c> de <see cref="ParamIdDto"/>.
/// </summary>
public class GetByIdClientQuery : ParamIdDto, IRequest<ResultDto<ClientByIdResponseDto>>;