using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class CompanyViewModel : ObservableObject
{
	private CompanyResponseDto _company = new CompanyResponseDto(Guid.NewGuid(), "Sample name", DateTime.Now);
	private HumanResourcesClient _humanResourcesClient;
	public CompanyModelCommands Commands { get; set; }
	public CompanyResponseDto Company
	{
		get => _company;
		set
		{
			_company = value;
			OnPropertyChanged();
		}
	}
	public HumanResourcesClient Client => _humanResourcesClient;

    public CompanyViewModel(HumanResourcesClient humanResourcesClient)
    {
        _humanResourcesClient = humanResourcesClient;
		Commands = new CompanyModelCommands(this);
    }

    public CompanyViewModel()
    { }
}
