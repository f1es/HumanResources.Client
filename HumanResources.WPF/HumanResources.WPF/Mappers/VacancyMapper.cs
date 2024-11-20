using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;
using System.Runtime.CompilerServices;

namespace HumanResources.WPF.Mappers;

public static class VacancyMapper
{
	public static VacancyView ToView(this VacancyResponseDto vacancy, HumanResourcesClient client)
	{
		var vacancyView = new VacancyView();
		var dataContext = new VacancyViewModel(client);

		dataContext.Vacancy = vacancy;
		vacancyView.DataContext = dataContext;
		return vacancyView;
	}

	public static IEnumerable<VacancyView> ToView(this IEnumerable<VacancyResponseDto> vacancies, HumanResourcesClient client)
	{
		var vacanciesList = new List<VacancyView>();
		foreach (var vacancy in vacancies)
			vacanciesList.Add(vacancy.ToView(client));
		return vacanciesList;
	}
}
