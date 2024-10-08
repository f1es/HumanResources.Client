using FlesLib.WPF;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.WPF.ViewModels.Models;

public class VacancyViewModel : ObservableObject
{
	private VacancyResponseDto _vacancy = new VacancyResponseDto(Guid.NewGuid(), DateTime.Now, "Sapmle description");

	public VacancyResponseDto Vacancy
	{
		get => _vacancy;
		set
		{
			_vacancy = value;
			OnPropertyChanged();
		}
	}
}
