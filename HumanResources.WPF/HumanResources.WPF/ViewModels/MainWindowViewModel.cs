using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.WPF.Views.Pages;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace HumanResources.WPF.ViewModels;

public class MainWindowViewModel : ObservableObject
{
	private ObservableCollection<CompanyResponseDto> _companies = new ObservableCollection<CompanyResponseDto>();
	private string _domain = "http://localhost:5000";
	private HumanResourcesClient _humanResourcesClient;
	private Page _page;
	public NotifyTaskCompletion<IEnumerable<CompanyResponseDto>> Companies { get; private set; }
	public Page Page
	{
		get => _page;
		set
		{
			_page = value;
			OnPropertyChanged();
		}
	}
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
