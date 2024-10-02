using HumanResources.Client.Enums;
using HumanResources.Client.Shared.Dto.Request;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.Client.Methods;

public class Departmens
{
	private string _url;
	private GenericHttpMethods _genericHttpMethods;
	private Dictionary<Endpoint, string> _endpoints;

	public Departmens(
		string url, 
		GenericHttpMethods genericHttpMethods, 
		Dictionary<Endpoint, string> endpoints)
	{
		_url = url;
		_genericHttpMethods = genericHttpMethods;
		_endpoints = endpoints;
	}

	public async Task<IEnumerable<DepartmentResponseDto>> GetAllAsync(Guid companyId) =>
		await _genericHttpMethods
		.GetAsync<IEnumerable<DepartmentResponseDto>>(GetUri(Endpoint.departments, companyId));

	public async Task<DepartmentResponseDto> GetByIdAsync(Guid companyId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.departmentsWithId, companyId, id);
		var response = await _genericHttpMethods.GetAsync<DepartmentResponseDto>(uri);
		return response;
	}

	public async Task DeleteAsync(Guid companyId, Guid id)
	{
		var uri = GetUriWithId(Endpoint.departmentsWithId, companyId, id);
		await _genericHttpMethods.DeleteAsync(uri);
	}

	public async Task<DepartmentResponseDto> PostAsync(Guid companyId, DepartmentRequestDto departmentDto)
	{
		var uri = GetUri(Endpoint.departments, companyId);
		var response = await _genericHttpMethods.PostAsync<DepartmentResponseDto>(uri, departmentDto);
		return response;
	}

	public async Task PutAsync(Guid companyId, Guid id, DepartmentRequestDto departmentDto)
	{
		var uri = GetUriWithId(Endpoint.departmentsWithId, companyId, id);
		await _genericHttpMethods.PutAsync(uri, departmentDto);
	}

	private string GetUri(
		Endpoint endpoint, 
		Guid companyId, 
		string companyIdExpression = "{companyId}") =>
		$"{_url}{_endpoints[endpoint]}"
		.Replace(companyIdExpression, companyId.ToString());

	private string GetUriWithId(
		Endpoint endpoint, 
		Guid companyId, 
		Guid id, 
		string idExpression = "{id}") => 
		GetUri(endpoint, companyId)
		.Replace(idExpression, id.ToString());
}
