using HumanResources.Client;
using HumanResources.Client.Shared.Parameters;
using System.Text.Json;


var domain = "http://localhost:5000";
var hrClient = new HumanResourcesClient(domain);
var serializerOptions = new JsonSerializerOptions()
{
	WriteIndented = true
};
