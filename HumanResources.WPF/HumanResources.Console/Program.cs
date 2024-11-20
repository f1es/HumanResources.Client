using HumanResources.Client;
using HumanResources.Core.Shared.Parameters;
using HumanResources.Shared.Dto.Request;
using System.Text.Json;

var hrClient = new HumanResourcesClient();
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};

try
{
	var loginDto = new LoginDto("string", "string");

	//await hrClient.GetAccessToken(loginDto);

	var resp = await hrClient.Companies.GetAllAsync(new CompanyRequestParameters());
	Console.WriteLine(JsonSerializer.Serialize(resp, serializerOptions));
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

