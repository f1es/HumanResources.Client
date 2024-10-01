using HumanResources.Client;
using System.Text.Json;


var domain = "http://localhost:5000";
var hrClient = new HumanResourcesClient(domain);
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};

var compResponse = await hrClient.Companies.GetAllAsync();
Console.WriteLine(JsonSerializer.Serialize(compResponse, serializerOptions));

var compId = Guid.Parse(Console.ReadLine());
var depResponse = await hrClient.Departmens.GetAllAsync(compId);
Console.WriteLine(JsonSerializer.Serialize(depResponse, serializerOptions));

var id = Guid.Parse(Console.ReadLine());
var depResp = await hrClient.Departmens.GetByIdAsync(compId, id);
Console.WriteLine(JsonSerializer.Serialize(depResp, serializerOptions));

