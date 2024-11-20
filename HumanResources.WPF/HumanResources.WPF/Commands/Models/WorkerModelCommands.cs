using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Models;

public class WorkerModelCommands : CommandsDictionary
{
    public WorkerModelCommands(WorkerViewModel viewModel)
    {
        var editCommand = new RelayCommand(c =>
        {
            var editWorkerContext = new WorkerWindowViewModel(viewModel.Client, viewModel.CompanyId, viewModel.DepartmentId, viewModel.Worker);
            var editWorkerDialog = new WorkerWindow();
            editWorkerDialog.DataContext = editWorkerContext;
            editWorkerDialog.ShowDialog();
        });
        AddCommand("Edit", editCommand);

        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel.Client.Workers.DeleteAsync(viewModel.CompanyId, viewModel.DepartmentId, viewModel.Worker.Id);

            var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
            mainWindowContext.PageManager.UpdateWorkers();
        });
        AddCommand("Delete", deleteCommand);
    }
}
