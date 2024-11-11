using HumanResources.Client.Enums;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Features;
using HumanResources.Core.Shared.Parameters;

namespace HumanResources.Client.Methods;

public class Professions : BaseMethods
{
	private string _url;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

	public Professions(
		string url,
		GenericHttpMethods genericHttpMethods,
		Dictionary<Endpoint, string> endpoints)
	{
		_url = url;
		_genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
	}

	public async Task<PagedList<ProfessionResponseDto>> GetAllAsync(ProfessionRequestParameters parameters) =>
		await _genericHttpMethods
		.GetPagedListAsync<ProfessionResponseDto>(AddParametersToUri(GetUri(Endpoint.professions), parameters));

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
		$"{_url}{_endpoints[endpoint]}";

	private string GetUriWithId(Endpoint endpoint, Guid id, string idExpression = "{id}") =>
		GetUri(endpoint)
		.Replace(idExpression, id.ToString());
}
