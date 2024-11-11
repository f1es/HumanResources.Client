using HumanResources.Client.Enums;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Features;
using HumanResources.Core.Shared.Parameters;

namespace HumanResources.Client.Methods;

public class Workers : BaseMethods
{
	private string _url;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

	public Workers(
		string url,
		GenericHttpMethods genericHttpMethods,
		Dictionary<Endpoint, string> endpoints)
	{
		_url = url;
		_genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
	}

	public async Task<PagedList<WorkerResponseDto>> GetAllAsync(Guid companyId, Guid departmentId, WorkerRequestParameters parameters)
	{
		var uri = GetUri(Endpoint.workers, companyId, departmentId);
		uri = AddParametersToUri(uri, parameters);
		var response = await _genericHttpMethods.GetPagedListAsync<WorkerResponseDto>(uri);
		return response;
	}

	public async Task<WorkerResponseDto> GetByIdAsync(Guid companyId, Guid departmentId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.workersWithId, companyId, departmentId, id);
		var response = await _genericHttpMethods.GetAsync<WorkerResponseDto>(uri);
		return response;
	}

	public async Task DeleteAsync(Guid companyId, Guid departmentId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.companiesWithId, companyId, departmentId, id);
		await _genericHttpMethods.DeleteAsync(uri);
	}

	public async Task<WorkerResponseDto> PostAsync(Guid companyId, Guid departmentId, WorkerRequestDto workerDto)
	{
		var uri = GetUri(Endpoint.workers, companyId, departmentId);
		var reponse = await _genericHttpMethods.PostAsync<WorkerResponseDto>(uri, workerDto);
		return reponse;
	}

	public async Task PutAsync(Guid companyId, Guid departmentId, Guid id, WorkerRequestDto workerDto)
	{
		var uri = GetUriWithId(Endpoint.workersWithId, companyId, departmentId, id);
		await _genericHttpMethods.PutAsync(uri, workerDto);
	}

	private string GetUri(
		Endpoint endpoint,
		Guid companyId,
		Guid departmentId,
		string companyIdExpression = "{companyId}",
		string departmentIdExpression = "{departmentId}") =>
		$"{_url}{_endpoints[endpoint]}"
		.Replace(companyIdExpression, companyId.ToString())
		.Replace(departmentIdExpression, departmentId.ToString());

	private string GetUriWithId(
		Endpoint endpoint,
		Guid companyId,
		Guid departmentId,
		Guid id,
		string idExpression = "{id}") =>
		GetUri(Endpoint.workersWithId, companyId, departmentId)
		.Replace(idExpression, id.ToString());
}
