using HumanResources.Client.Enums;
using HumanResources.Client.Methods;

namespace HumanResources.Client;

public class HumanResourcesClient
{
    private string _domain; 
	private HttpClient _httpClient;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

    public Companies Companies { get; private set; }

    public HumanResourcesClient(string domain)
    {
        _domain = domain;
        _httpClient = new HttpClient();
        _genericHttpMethods = new GenericHttpMethods(new HttpClient());
		_endpoints = new Dictionary<Endpoint, string>() 
        {
            { Endpoint.companies, "/api/companies" },
            { Endpoint.companiesWithId, "/api/companies/{id}" },

            { Endpoint.departments, "/api/companies/{companyId}/departments" },
            { Endpoint.departmentsWithId, "/api/companies/{companyId}/departments/{id}" },

        };

        Companies = new Companies(_domain, _genericHttpMethods, _endpoints);
    }
}
