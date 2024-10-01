using HumanResources.Client.Enums;
using HumanResources.Client.Shared.Dto.Request;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.Client.Methods;

public class Companies
{
    private string _domain;
    private GenericHttpMethods _genericHttpMethods;
    private Dictionary<Endpoint, string> _endpoints;

    public Companies(
        string domain,
		GenericHttpMethods genericHttpMethods,
        Dictionary<Endpoint, string> endpoints
        )
    {
        _domain = domain;
        _genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
    }

    public async Task<IEnumerable<CompanyResponseDto>> GetAllAsync() =>
        await _genericHttpMethods
        .GetAsync<IEnumerable<CompanyResponseDto>>(GetUri(Endpoint.companies));

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
        $"{_domain}{_endpoints[endpoint]}";

    private string GetUriWithId(Endpoint endpoint, Guid id, string idExpression = "{id}") =>
        GetUri(endpoint).Replace(idExpression, id.ToString());
}
