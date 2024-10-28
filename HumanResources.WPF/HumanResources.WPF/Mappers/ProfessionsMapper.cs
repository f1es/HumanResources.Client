using HumanResources.Client.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Mappers;

public static class ProfessionsMapper
{
	public static ProfessionView ToView(this ProfessionResponseDto profession)
	{
		var professionView = new ProfessionView();
		var dataContext = new ProfessionViewModel();

		dataContext.Profession = profession;
		professionView.DataContext = dataContext;

		return professionView;
	}

	public static IEnumerable<ProfessionView> ToView(this IEnumerable<ProfessionResponseDto> professions)
	{
		var professionsList = new List<ProfessionView>();
		foreach(var profession in professions) 
			professionsList.Add(profession.ToView());
		return professionsList;
	}
}
