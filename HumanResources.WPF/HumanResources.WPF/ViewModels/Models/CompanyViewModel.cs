using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class CompanyViewModel : ObservableObject
{
	private CompanyResponseDto _company = new CompanyResponseDto(Guid.NewGuid(), "Sample name", DateTime.Now);
	private HumanResourcesClient _client;
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
	public HumanResourcesClient Client => _client;
    public CompanyViewModel(HumanResourcesClient client)
    {
        _client = client;
		Commands = new CompanyModelCommands(this);
    }

    public CompanyViewModel()
    { }
}
