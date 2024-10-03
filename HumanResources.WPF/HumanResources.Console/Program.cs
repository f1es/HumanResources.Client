using HumanResources.Client;
using HumanResources.Client.Shared.Parameters;
using System.Text.Json;


var domain = "http://localhost:5000";
var hrClient = new HumanResourcesClient(domain);
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};

var companyParameters = new CompanyRequestParameters() 
{
	SearchTerm = "",
	PageNumber = 1,
	PageSize = 4
};

var compResponse = await hrClient.Companies.GetAllAsync(companyParameters);
Console.WriteLine(JsonSerializer.Serialize(compResponse, serializerOptions));

var professionParameters = new ProfessionRequestParameters()
{
	SearchTerm = "chel",
};
var professions = await hrClient.Professions.GetAllAsync(professionParameters);
Console.WriteLine(JsonSerializer.Serialize(professions, serializerOptions));