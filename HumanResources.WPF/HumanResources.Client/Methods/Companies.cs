using HumanResources.Client.Enums;
using HumanResources.Client.Shared.Dto.Request;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.Client.Shared.Parameters;

namespace HumanResources.Client.Methods;

public class Companies
{
    private string _url;
    private GenericHttpMethods _genericHttpMethods;
    private Dictionary<Endpoint, string> _endpoints;
    private ParametersBuilder _parametersBuilder;

    public Companies(
        string url,
		GenericHttpMethods genericHttpMethods,
        Dictionary<Endpoint, string> endpoints
        )
    {
        _url = url;
        _genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
        _parametersBuilder = new ParametersBuilder();
    }

    public async Task<IEnumerable<CompanyResponseDto>> GetAllAsync(CompanyRequestParameters parameters) =>
        await _genericHttpMethods
        .GetAsync<IEnumerable<CompanyResponseDto>>(GetUriWithParameters(Endpoint.companies, parameters));

    public async Task<CompanyResponseDto> GetByIdAsync(Guid id)
    {
		var uri = GetUriWithId(Endpoint.companiesWithId, id);
		var response = await _genericHttpMethods.GetAsync<CompanyResponseDto>(uri);
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        var uri = GetUriWithId(Endpoint.companiesWithId, id);
        await _genericHttpMethods.DeleteAsync(uri);
    }

    public async Task<CompanyResponseDto> PostAsync(CompanyRequestDto companyDto) =>
        await _genericHttpMethods
        .PostAsync<CompanyResponseDto>(GetUri(Endpoint.companies), companyDto);

    public async Task PutAsync(Guid id, CompanyRequestDto companyDto)
    {
		var uri = GetUriWithId(Endpoint.companiesWithId, id);
		await _genericHttpMethods.PutAsync(uri, companyDto);
    }

    private string GetUri(Endpoint endpoint) =>
        $"{_url}{_endpoints[endpoint]}";

    private string GetUriWithId(Endpoint endpoint, Guid id, string idExpression = "{id}") =>
        GetUri(endpoint).Replace(idExpression, id.ToString());

    private string GetUriWithParameters(Endpoint endpoint, CompanyRequestParameters parameters)
    {
        var uri = GetUri(endpoint);
        var parametersString = _parametersBuilder.BuildParameters(parameters);
        return uri + parametersString;
    }
}
