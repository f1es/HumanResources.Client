using HumanResources.Client.Enums;
using HumanResources.Client.Shared.Dto.Request;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.Client.Methods;

public class Professions
{
	private string _domain;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

	public Professions(
		string domain,
		GenericHttpMethods genericHttpMethods,
		Dictionary<Endpoint, string> endpoints)
	{
		_domain = domain;
		_genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
	}

	public async Task<IEnumerable<ProfessionResponseDto>> GetAllAsync() =>
		await _genericHttpMethods
		.GetAsync<IEnumerable<ProfessionResponseDto>>(GetUri(Endpoint.professions));

	public async Task<ProfessionResponseDto> GetByIdAsync(Guid id)
	{
		var uri = GetUriWithId(Endpoint.professionsWithId, id);
		var response = await _genericHttpMethods.GetAsync<ProfessionResponseDto>(uri);
		return response;
	}

	public async Task DeleteAsync(Guid id)
	{
		var uri = GetUriWithId(Endpoint.professionsWithId, id);
		await _genericHttpMethods.DeleteAsync(uri);
	}

	public async Task<ProfessionResponseDto> PostAsync(ProfessionRequestDto professionDto)
	{
		var uri = GetUri(Endpoint.professions);
		var response = await _genericHttpMethods.PostAsync<ProfessionResponseDto>(uri, professionDto);
		return response;
	}

	public async Task PutAsync(Guid id, ProfessionRequestDto professionDto)
	{
		var uri = GetUriWithId(Endpoint.professionsWithId, id);
		await _genericHttpMethods.PutAsync(uri, professionDto);
	}

	private string GetUri(Endpoint endpoint) =>
		$"{_domain}{_endpoints[endpoint]}";

	private string GetUriWithId(Endpoint endpoint, Guid id, string idExpression = "{id}") =>
		GetUri(endpoint)
		.Replace(idExpression, id.ToString());
}
