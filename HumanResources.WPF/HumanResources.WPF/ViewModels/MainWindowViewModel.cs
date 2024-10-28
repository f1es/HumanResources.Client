using FlesLib.WPF;
using FlesLib.WPF.Commands;
using HumanResources.Client;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.Views.Pages;
using System.Windows.Controls;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels;

public class MainWindowViewModel : ObservableObject
{
	private string _url = "http://localhost:5000";
	private HumanResourcesClient _humanResourcesClient;

	private Page _page;
	private Dictionary<string, Page> _pages;
	private string _pageSelector;

	public Page Page
	{
		get => _page;
		set
		{
			_page = value;
			OnPropertyChanged();
		}
	}

	public string PageSelector
	{
		get => _pageSelector;
		set
		{
			_pageSelector = value;
			OnPropertyChanged();
			Page = _pages[_pageSelector];
		}
	}

	public ICommand SelectCommand => 
		new RelayCommand(ChangeSelector);

	private void ChangeSelector(object parameter)
	{
		var radioButton = parameter as RadioButton;
		var content = radioButton.Content as string;
		Page = _pages[content];
	}

    public MainWindowViewModel()
    {
		_humanResourcesClient = new HumanResourcesClient(_url);

		var companiesPageView = new CompaniesPageView();
		companiesPageView.DataContext = new CompaniesPageViewModel(_humanResourcesClient);

		var professionsPageView = new ProfessionsPageView();
		professionsPageView.DataContext = new ProfessionsPageViewModel(_humanResourcesClient);

		var vacanciesPageView = new VacanciesPageView();

		Page = companiesPageView;

		_pages = new Dictionary<string, Page>()
		{
			{ "Companies", companiesPageView },
			{ "Professions", professionsPageView },
			{ "Vacancies", vacanciesPageView},
		};
    }


}
