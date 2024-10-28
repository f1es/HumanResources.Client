using FlesLib.WPF.Commands;
using HumanResources.WPF.ViewModels.Models;

namespace HumanResources.WPF.Commands.Models;

public class CompanyModelCommands : CommandsDictionary
{
    public CompanyModelCommands(CompanyViewModel viewModel)
    {
        var deleteCommand = new RelayCommand(async c =>
        {
            await viewModel
            .Client
            .Companies
            .DeleteAsync(viewModel.Company.Id);
        });
        AddCommand("Delete", deleteCommand);
    }
}
