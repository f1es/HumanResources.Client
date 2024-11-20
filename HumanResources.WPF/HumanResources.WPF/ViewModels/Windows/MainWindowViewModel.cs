using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.WPF.Commands.Windows;
using HumanResources.WPF.Services.Implementations;
using HumanResources.WPF.Services.Interfaces;
using System.Windows.Navigation;

namespace HumanResources.WPF.ViewModels.Windows;

public class MainWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private NavigationService _navigationService;

	private MainWindowCommands _commands;
	private IPageManager _pageManager;
	private string _pageSelector;

	public HumanResourcesClient HumanResourcesClient
	{
		get => _client;
		set => _client = value;
	}
	public NavigationService NavigationService
	{
		get => _navigationService;
	}
	public MainWindowCommands Commands
	{
		get => _commands;
		set => _commands = value;
	}
	public string PageSelector
	{
		get => _pageSelector;
		set
		{
			_pageSelector = value;
			OnPropertyChanged();
			PageManager.Page = PageManager.Pages[_pageSelector];
		}
	}
	public IPageManager PageManager => _pageManager;
	
    public MainWindowViewModel(HumanResourcesClient client, NavigationService navigationService)
    {
		_navigationService = navigationService;
		_client = client;

		Commands = new MainWindowCommands(this);
		_pageManager = new PageManager(_client);
    }

    public MainWindowViewModel()
    { }
}
