using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class ProfessionViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private ProfessionResponseDto _profession = new ProfessionResponseDto(Guid.NewGuid(), "Sample name", 1000);
	private ProfessionModelCommands _commands;
	public HumanResourcesClient Client => _client;
	public ProfessionResponseDto Profession
	{
		get => _profession;
		set
		{
			_profession = value;
			OnPropertyChanged();
		}
	}
	public ProfessionModelCommands Commands => _commands;
    public ProfessionViewModel(HumanResourcesClient client)
    {
        _client = client;
		_commands = new ProfessionModelCommands(this);
    }
    public ProfessionViewModel()
    { }
}
