using HumanResources.Client.Enums;
using HumanResources.Client.Methods;

namespace HumanResources.Client;

public class HumanResourcesClient
{
    private string _url; 
	private HttpClient _httpClient;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

    public Companies Companies { get; private set; }
    public Departmens Departmens { get; private set; }
    public Vacancies Vacancies { get; private set; }
    public Professions Professions { get; private set; }
    public Workers Workers { get; private set; }
    public HumanResourcesClient(string url)
    {
        _url = url;
        _httpClient = new HttpClient();
        _genericHttpMethods = new GenericHttpMethods(new HttpClient());
		_endpoints = new Dictionary<Endpoint, string>() 
        {
            { Endpoint.companies, "/api/companies" },
            { Endpoint.companiesWithId, "/api/companies/{id}" },

            { Endpoint.departments, "/api/companies/{companyId}/departments" },
            { Endpoint.departmentsWithId, "/api/companies/{companyId}/departments/{id}" },

            { Endpoint.vacancies, "/api/companies/{companyId}/vacancies" },
            { Endpoint.vacanciesWithId, "/api/companies/{companyId}/vacancies/{id}" },
            { Endpoint.vacanciesProfession, "/api/companies/{companyId}/vacancies/{id}/profession" },

            { Endpoint.professions, "/api/professions" },
            { Endpoint.professionsWithId, "/api/professions/{id}" },

			{ Endpoint.workers, "/api/companies/{companyId}/departments/{departmentId}/workers" },
			{ Endpoint.workersWithId, "/api/companies/{companyId}/departments/{departmentId}/workers/{id}" },
		};

        Companies = new Companies(_url, _genericHttpMethods, _endpoints);
        Departmens = new Departmens(_url, _genericHttpMethods, _endpoints);
        Vacancies = new Vacancies(_url, _genericHttpMethods, _endpoints);
        Professions = new Professions(_url, _genericHttpMethods, _endpoints);
        Workers = new Workers(_url, _genericHttpMethods, _endpoints);
    }
}
