namespace HumanResources.Core.Shared.Parameters;

public class CompanyRequestParameters : RequestParameters
{
	public string? SearchTerm { get; set; } = string.Empty;
}
