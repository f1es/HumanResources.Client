using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Pages;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Models;

public class CompanyModelCommands : CommandsDictionary
{
    public CompanyModelCommands(CompanyViewModel viewModel)
    {
        var editCommand = new RelayCommand(c =>
        {
            var editCompanyDialog = new AddCompanyWindow();
            var editCompanyContext = new AddCompanyWindowViewModel(viewModel.Client, viewModel.Company);
            editCompanyDialog.DataContext = editCompanyContext;
            editCompanyDialog.ShowDialog();
        });
        AddCommand("Edit", editCommand);

        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel
            .Client
            .Companies
            .DeleteAsync(viewModel.Company.Id);

            var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
            mainWindowContext.PageManager.UpdateCompanies();
        });
        AddCommand("Delete", deleteCommand);

        var vacanciesCommand = new RelayCommand(c =>
        {
			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;

            var vacanciesPage = mainWindowContext.PageManager.VacanciesPageView;
            var vacanciesPageViewModel = new VacanciesPageViewModel(viewModel.Client, viewModel.Company.Id);
            vacanciesPage.DataContext = vacanciesPageViewModel;
            mainWindowContext.PageManager.UpdateVacancies();

            mainWindowContext.PageManager.Page = mainWindowContext.PageManager.VacanciesPageView;
		});
        AddCommand("Vacancies", vacanciesCommand);

        var departmentsCommand = new RelayCommand(c =>
        {
            var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;

            var departmentsPage = mainWindowContext.PageManager.DepartmentsPageView;
            var departmentPageViewModel = new DepartmentsPageViewModel(viewModel.Client, viewModel.Company.Id);
            departmentsPage.DataContext = departmentPageViewModel;
            mainWindowContext.PageManager.UpdateDepartments();

            mainWindowContext.PageManager.Page = mainWindowContext.PageManager.DepartmentsPageView;
        });
        AddCommand("Departments", departmentsCommand);
    }
}
