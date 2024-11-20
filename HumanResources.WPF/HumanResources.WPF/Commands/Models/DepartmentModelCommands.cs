using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Models;

public class DepartmentModelCommands : CommandsDictionary
{
    public DepartmentModelCommands(DepartmentViewModel viewModel)
    {
        var editCommand = new RelayCommand(c =>
        {
            var editDepartmentDialog = new DepartmentWindow();
            var editDepartmentContext = new DepartmentWindowViewModel(viewModel.Client, viewModel.Department.CompanyId, viewModel.Department);
            editDepartmentDialog.DataContext = editDepartmentContext;
            editDepartmentDialog.ShowDialog();
        });
        AddCommand("Edit", editCommand);

        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel
            .Client
            .Departmens
            .DeleteAsync(viewModel.Department.CompanyId, viewModel.Department.Id);

			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
            mainWindowContext.PageManager.UpdateDepartments();
		});
        AddCommand("Delete", deleteCommand);

        var workersCommand = new RelayCommand(c =>
        {
			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;

            var workersPage = mainWindowContext.PageManager.WorkersPageView;
            var workersPageViewModel = new WorkersPageViewModel(viewModel.Client, viewModel.Department.CompanyId, viewModel.Department.Id);
            workersPage.DataContext = workersPageViewModel;
            mainWindowContext.PageManager.UpdateWorkers();

            mainWindowContext.PageManager.Page = mainWindowContext.PageManager.WorkersPageView;
		});
        AddCommand("Workers", workersCommand);
    }
}
