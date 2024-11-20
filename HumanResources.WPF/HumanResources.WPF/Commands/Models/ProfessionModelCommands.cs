using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Models;

public class ProfessionModelCommands : CommandsDictionary
{
    public ProfessionModelCommands(ProfessionViewModel viewModel)
    {
        var editCommand = new RelayCommand(c =>
        {
            var professionEditDialog = new ProfessionWindow();
            var professionDialogContext = new ProfessionWindowViewModel(viewModel.Client, viewModel.Profession);
            professionEditDialog.DataContext = professionDialogContext;
            professionEditDialog.ShowDialog();
        });
        AddCommand("Edit", editCommand);

        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel
            .Client
            .Professions
            .DeleteAsync(viewModel.Profession.Id);

            var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
            mainWindowContext.PageManager.UpdateProfessions();
        });
        AddCommand("Delete", deleteCommand);
    }
}
