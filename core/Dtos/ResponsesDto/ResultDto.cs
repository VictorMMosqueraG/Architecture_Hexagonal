namespace Core.Dtos.ResponsesDto;

public class ResultDto
{
    public string? Message { get; set; }

    public ResultDto() : base() { }

    protected ResultDto(string message)
    {
        Message = message;
    }

    public static ResultDto Success(string message) => new(message);
}

public class ResultDto<T> : ResultDto
{
    public T? Results { get; set; }

    public ResultDto() : base() { }

    protected ResultDto(T results) : base(string.Empty)
    {
        Results = results;
    }

    protected ResultDto(string message) : base(message) { }

    public static ResultDto<T> Success(T Result) => new(Result);
}