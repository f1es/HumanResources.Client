using HumanResources.Client;
using HumanResources.Client.Shared.Parameters;
using System.Text.Json;


var domain = "https://localhost:44327";
var hrClient = new HumanResourcesClient(domain);
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};

try
{

	await hrClient.GetCredentialsToken();

	var resp = await hrClient.Companies.GetAllAsync(new CompanyRequestParameters());
	Console.WriteLine(JsonSerializer.Serialize(resp, serializerOptions));
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

