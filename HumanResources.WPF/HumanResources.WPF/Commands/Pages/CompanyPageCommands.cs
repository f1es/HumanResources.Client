using FlesLib.WPF.Commands;
using HumanResources.Client.Shared.Parameters;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Commands.Pages;

public class CompanyPageCommands : CommandsDictionary
{
    public CompanyPageCommands(CompaniesPageViewModel viewModel)
    {
        var addCompanyCommand = new RelayCommand(c =>
        {

        });
        AddCommand("Add", addCompanyCommand);

        var searchCommand = new RelayCommand(async c =>
        {
            var parameters = new CompanyRequestParameters()
            {
                PageSize = 10,
                PageNumber = viewModel.RequestParameters.PageNumber,
                SearchTerm = viewModel.RequestParameters.SearchTerm,
                OrederByQuery = viewModel.SortTypes[viewModel.SortType]
            };
            var companies = await viewModel.Client.Companies.GetAllAsync(parameters);

            viewModel.Companies = new List<CompanyView>(companies.ToView(viewModel.Client));
        });
        AddCommand("Search", searchCommand);

        var nextPageCommand = new RelayCommand(c =>
        {
            viewModel.PageNumber += 1;
            searchCommand.Execute(null);
        });
        AddCommand("NextPage", nextPageCommand);

        var previousPageCommand = new RelayCommand(c =>
        {
            viewModel.PageNumber -= 1;
            searchCommand.Execute(null);
        });
        AddCommand("PreviousPage", previousPageCommand);
    }
}
