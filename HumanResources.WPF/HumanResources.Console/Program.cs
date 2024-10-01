using HumanResources.Client;
using System.Text.Json;


var domain = "http://localhost:5000";
var hrClient = new HumanResourcesClient(domain);
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};

var response = await hrClient.Companies.GetAllAsync();
Console.WriteLine(JsonSerializer.Serialize(response, serializerOptions));



