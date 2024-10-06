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

var companyId = Guid.Parse(Console.ReadLine());

//var depResponse = await hrClient.Departmens.GetAllAsync(companyId);
//Console.WriteLine(JsonSerializer.Serialize(depResponse, serializerOptions));

var vacancies = await hrClient.Vacancies.GetAllAsync(companyId);
Console.WriteLine(JsonSerializer.Serialize(vacancies, serializerOptions));

//var departmentId = Guid.Parse(Console.ReadLine());
//var workers = await hrClient.Workers.GetAllAsync(companyId, departmentId);
//Console.WriteLine(JsonSerializer.Serialize(workers, serializerOptions));

//var professions = await hrClient.Professions.GetAllAsync();
//Console.WriteLine(JsonSerializer.Serialize(professions, serializerOptions));

var vacId = Guid.Parse(Console.ReadLine());
var prof = await hrClient.Vacancies.GetProfessionAsync(companyId, vacId);
Console.WriteLine(JsonSerializer.Serialize(prof, serializerOptions));



