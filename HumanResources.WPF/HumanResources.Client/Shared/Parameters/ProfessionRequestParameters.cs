namespace HumanResources.Client.Shared.Parameters;

public class ProfessionRequestParameters : RequestParameters
{
	public string? SearchTerm { get; set; } = string.Empty;
	public uint MinSalary { get; set; } = 0;
	public uint MaxSalary { get; set; } = uint.MaxValue;
}
