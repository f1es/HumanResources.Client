using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Mappers;

public static class CompanyMapper
{
	public static CompanyView ToView(this CompanyResponseDto company, HumanResourcesClient client)
	{
		var companyView = new CompanyView();
		var dataContext = new CompanyViewModel(client);

		dataContext.Company = company;
		companyView.DataContext = dataContext;

		return companyView;
	}

	public static IEnumerable<CompanyView> ToView(this IEnumerable<CompanyResponseDto> companies, HumanResourcesClient client)
	{
		var viewList = new List<CompanyView>();	

		foreach(var company in companies)
			viewList.Add(company.ToView(client));
		
		return viewList;
	}
}
