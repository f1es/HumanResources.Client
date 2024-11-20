using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Mappers;

public static class ProfessionsMapper
{
	public static ProfessionView ToView(this ProfessionResponseDto profession, HumanResourcesClient client)
	{
		var professionView = new ProfessionView();
		var dataContext = new ProfessionViewModel(client);

		dataContext.Profession = profession;
		professionView.DataContext = dataContext;

		return professionView;
	}

	public static IEnumerable<ProfessionView> ToView(this IEnumerable<ProfessionResponseDto> professions, HumanResourcesClient client)
	{
		var professionsList = new List<ProfessionView>();
		foreach(var profession in professions) 
			professionsList.Add(profession.ToView(client));
		return professionsList;
	}
}
