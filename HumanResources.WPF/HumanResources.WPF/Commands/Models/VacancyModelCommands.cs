using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Models;

public class VacancyModelCommands : CommandsDictionary
{
    public VacancyModelCommands(VacancyViewModel viewModel)
    {
        var editCommand = new RelayCommand(c =>
        {
            var editVacancyContext = new VacancyWindowViewModel(viewModel.Client, viewModel.Vacancy.ComapnyId, viewModel.Vacancy);
            var editVacancyDialog = new VacancyWindow()
            { 
                DataContext = editVacancyContext,
            };
            editVacancyDialog.ShowDialog();
        });
        AddCommand("Edit", editCommand);

        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel.Client.Vacancies.DeleteAsync(viewModel.Vacancy.ComapnyId, viewModel.Vacancy.Id);

            var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
            mainWindowContext.PageManager.UpdateVacancies();
        });
        AddCommand("Delete", deleteCommand);
    }
}
