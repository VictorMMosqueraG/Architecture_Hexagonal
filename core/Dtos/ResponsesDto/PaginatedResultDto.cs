namespace Core.Dtos.ResponsesDto;

public class PaginatedResultDto<T> : ResultDto<T>
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrev { get; set; }

    public PaginatedResultDto() : base() { }

    private PaginatedResultDto(int total, int page, int pageSize, T Result) : base(Result)
    {
        Total = Math.Max(0, total);
        Page = page;
        PageSize = pageSize;
        HasNext = page < (Total > 0 ? (int)Math.Ceiling((double)Total / pageSize) : 0);
        HasPrev = page > 1;
    }

    public static PaginatedResultDto<T> Success(int total, int page, int pageSize, T result) => new(total, page, pageSize, result);
}