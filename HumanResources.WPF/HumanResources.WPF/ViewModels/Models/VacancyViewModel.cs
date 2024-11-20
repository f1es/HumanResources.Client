using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class VacancyViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private VacancyResponseDto _vacancy = new VacancyResponseDto(
		Guid.NewGuid(),
		DateTime.Now,
		"Sapmle description",
		Guid.NewGuid(),
		Guid.NewGuid());
	private VacancyModelCommands _commands;

	public HumanResourcesClient Client => _client;
	public VacancyResponseDto Vacancy
	{
		get => _vacancy;
		set
		{
			_vacancy = value;
			OnPropertyChanged();
		}
	}
	public VacancyModelCommands Commands => _commands;

    public VacancyViewModel(HumanResourcesClient client)
    {
		_client = client;
		_commands = new VacancyModelCommands(this);
    }

    public VacancyViewModel()
    { }
}
