using HumanResources.Client.Enums;
using HumanResources.Client.Shared.Dto.Request;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.Client.Methods;

public class Vacancies
{
	private string _url;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

	public Vacancies(
		string url,
		GenericHttpMethods genericHttpMethods,
		Dictionary<Endpoint, string> endpoints)
	{
		_url = url;
		_genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
	}

	public async Task<IEnumerable<VacancyResponseDto>> GetAllAsync(Guid companyId)
	{
		var uri = GetUri(Endpoint.vacancies, companyId);
		var response = await _genericHttpMethods.GetAsync<IEnumerable<VacancyResponseDto>>(uri);
		return response;
	} 

	public async Task<VacancyResponseDto> GetByIdAsync(Guid companyId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.companiesWithId, companyId, id);
		var resonse = await _genericHttpMethods.GetAsync<VacancyResponseDto>(uri);
		return resonse;
	}

	public async Task DeleteAsync(Guid companyId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.vacanciesWithId, companyId, id);
		await _genericHttpMethods.DeleteAsync(uri);
	}

	public async Task<VacancyResponseDto> PostAsync(Guid companyId, VacancyRequestDto vacancyDto)
	{
		var uri = GetUri(Endpoint.vacancies, companyId);
		var response = await _genericHttpMethods.PostAsync<VacancyResponseDto>(uri, vacancyDto);
		return response;
	}

	public async Task PutAsync(Guid companyId, Guid id, VacancyRequestDto vacancyDto)
	{
		var uri = GetUriWithId(Endpoint.vacanciesWithId, companyId, id);
		await _genericHttpMethods.PutAsync(uri, vacancyDto);
	}

	public async Task<ProfessionResponseDto> GetProfessionAsync(Guid companyId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.vacanciesProfession, companyId, id);
		var response = await _genericHttpMethods.GetAsync<ProfessionResponseDto>(uri);
		return response;
	}

	private string GetUri(
		Endpoint endpoint, 
		Guid companyId, 
		string companyIdExpression = "{companyId}") =>
		$"{_url}{_endpoints[endpoint]}"
		.Replace(companyIdExpression, companyId.ToString());

	private string GetUriWithId(Endpoint endpoint, Guid companyId, Guid id, string idExpression = "{id}") =>
		GetUri(endpoint, companyId)
		.Replace(idExpression, id.ToString());
}
