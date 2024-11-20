using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.WPF.Services.Interfaces;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.Views.Pages;
using System.Windows.Controls;

namespace HumanResources.WPF.Services.Implementations;

public class PageManager : ObservableObject, IPageManager
{
    private Page _page;
    private Dictionary<string, Page> _pages;
    private Lazy<CompaniesPageView> _companiesPageView;
    private Lazy<ProfessionsPageView> _professionsPageView;
    private Lazy<VacanciesPageView> _vacanciesPageView;
    private Lazy<WorkersPageView> _workersPageView;
    private Lazy<DepartmentsPageView> _departmentsPageView;
    public PageManager(HumanResourcesClient client)
    {
        _companiesPageView = new Lazy<CompaniesPageView>(() =>
        {
            var viewModel = new CompaniesPageViewModel(client);
            var view = new CompaniesPageView();
            view.DataContext = viewModel;
            return view;
        });

        _professionsPageView = new Lazy<ProfessionsPageView>(() =>
        {
            var viewModel = new ProfessionsPageViewModel(client);
            var view = new ProfessionsPageView();
            view.DataContext = viewModel;
            return view;
        });

        _vacanciesPageView = new Lazy<VacanciesPageView>(() =>
        {
            var viewModel = new VacanciesPageViewModel();
            var view = new VacanciesPageView();
            view.DataContext = viewModel;
            return view;
        });

        _workersPageView = new Lazy<WorkersPageView>(() =>
        {
            var viewModel = new WorkersPageViewModel();
            var view = new WorkersPageView();
            view.DataContext = viewModel;
            return view;
        });

        _departmentsPageView = new Lazy<DepartmentsPageView>(() =>
        {
            var viewModel = new DepartmentsPageViewModel();
            var view = new DepartmentsPageView();
            view.DataContext = viewModel;
            return view;
        });

        _page = _companiesPageView.Value;

        _pages = new Dictionary<string, Page>()
        {
			{ "Companies", CompaniesPageView },
			{ "Professions", ProfessionsPageView },
			{ "Vacancies", VacanciesPageView},
			{ "Departments", DepartmentsPageView },
			{ "Workers", WorkersPageView }
		};
    }

    public Page Page
    {
        get => _page;
        set
        {
            _page = value;
            OnPropertyChanged();
        }
    }
    public Dictionary<string, Page> Pages => _pages;
    public CompaniesPageView CompaniesPageView => _companiesPageView.Value;
    public ProfessionsPageView ProfessionsPageView => _professionsPageView.Value;
    public VacanciesPageView VacanciesPageView => _vacanciesPageView.Value;
    public WorkersPageView WorkersPageView => _workersPageView.Value;
    public DepartmentsPageView DepartmentsPageView => _departmentsPageView.Value;

	public void UpdateCompanies()
	{
		var viewModel = CompaniesPageView.DataContext as CompaniesPageViewModel;
        viewModel.Commands["Search"].Execute(null);
	}

	public void UpdateDepartments()
	{
        var viewModel = DepartmentsPageView.DataContext as DepartmentsPageViewModel;
        viewModel.Commands["Search"].Execute(null);
	}

	public void UpdateProfessions()
	{
		var viewModel = ProfessionsPageView.DataContext as ProfessionsPageViewModel;
        viewModel.Commands["Search"].Execute(null);
	}

	public void UpdateVacancies()
	{
		var viewModel = VacanciesPageView.DataContext as VacanciesPageViewModel;
        viewModel.Commands["Search"].Execute(null);
	}

	public void UpdateWorkers()
	{
		var viewModel = WorkersPageView.DataContext as WorkersPageViewModel;
        viewModel.Commands["Search"].Execute(null);
	}
}
