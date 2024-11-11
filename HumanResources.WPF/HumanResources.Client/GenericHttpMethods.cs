using HumanResources.Core.Shared.Features;
using System.Net.Http.Json;
using System.Text.Json;

namespace HumanResources.Client;

public class GenericHttpMethods
{
	private HttpClient _httpClient;

    public GenericHttpMethods(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

	public async Task<T> GetAsync<T>(string uri)
	{
		var response = await _httpClient.GetAsync(uri);
		var responseObject = await GetResponseObjectFromJsonAsync<T>(response);
		return responseObject;
	}

	public async Task<PagedList<T>> GetPagedListAsync<T>(string uri)
	{
		var response = await _httpClient.GetAsync(uri);
		var responseObject = await GetResponseObjectFromJsonAsync<IEnumerable<T>>(response);
		var responseObjectList = responseObject.ToList();

		var pagedDataJson = response.Headers.GetValues("pagination").Single();
		var pagedData = JsonSerializer.Deserialize<PagingData>(pagedDataJson);

		var pagedList = new PagedList<T>(responseObjectList, pagedData);
		return pagedList;
	}

	public async Task<T> PostAsync<T>(string uri, object obj)
	{
		var content = JsonContent.Create(obj);
		var response = await _httpClient.PostAsync(uri, content);
		var responseObject = await GetResponseObjectFromJsonAsync<T>(response);
		return responseObject;
	}
	
	public async Task DeleteAsync(string uri) =>
		await _httpClient.DeleteAsync(uri);

	public async Task PutAsync(string uri, object obj)
	{
		var content = JsonContent.Create(obj);
		await _httpClient.PutAsync(uri, content);
	}

	private async Task<T> GetResponseObjectFromJsonAsync<T>(HttpResponseMessage response)
	{
		var responseContent = await response.Content.ReadAsStringAsync();
		var responseObject = JsonSerializer.Deserialize<T>(responseContent);
		return responseObject;
	}
}
