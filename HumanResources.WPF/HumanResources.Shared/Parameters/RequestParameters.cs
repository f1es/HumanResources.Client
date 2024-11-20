namespace HumanResources.Core.Shared.Parameters;

public class RequestParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OrederByQuery { get; set; }
}
