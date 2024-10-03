using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using System.Collections.ObjectModel;

namespace HumanResources.WPF.ViewModels;

public class MainWindowViewModel : ObservableObject
{
	private ObservableCollection<CompanyResponseDto> _companies = new ObservableCollection<CompanyResponseDto>();
	private string _domain = "http://localhost:5000";
	private HumanResourcesClient _humanResourcesClient;
	public NotifyTaskCompletion<IEnumerable<CompanyResponseDto>> Companies { get; private set; }

    public MainWindowViewModel()
    {
		_humanResourcesClient = new HumanResourcesClient(_domain);
		Companies = new NotifyTaskCompletion<IEnumerable<CompanyResponseDto>>(GetCompanies());
    }

	public async Task<IEnumerable<CompanyResponseDto>> GetCompanies()
	{
		throw new NotImplementedException();
		//var companies = await _humanResourcesClient.Companies.GetAllAsync();
		//return companies;
	}
}
