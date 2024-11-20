using FlesLib.WPF.Commands;
using HumanResources.Core.Shared.Features;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Models;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Pages;

public class CompanyPageCommands : CommandsDictionary
{
    private PagingData _pagingData = new PagingData();
    public CompanyPageCommands(CompaniesPageViewModel viewModel)
    {
        var addCompanyCommand = new RelayCommand(c =>
        {
            var addCompanyDialog = new AddCompanyWindow();
            var addCompanyWindowContext = new AddCompanyWindowViewModel(viewModel.Client);
            addCompanyDialog.DataContext = addCompanyWindowContext;
            addCompanyDialog.ShowDialog();
        });
        AddCommand("Add", addCompanyCommand);

        var searchCommand = new RelayCommand(async c =>
        {
            var companies = await viewModel.Client.Companies.GetAllAsync(viewModel.RequestParameters);

            viewModel.Companies = new List<CompanyView>(companies.ToView(viewModel.Client));
            _pagingData = companies.PagingData;
        });
        AddCommand("Search", searchCommand);

        var nextPageCommand = new RelayCommand(c =>
        {
            if (!_pagingData.HasNext)
                return;

            viewModel.PageNumber += 1;
            searchCommand.Execute(null);
        });
        AddCommand("NextPage", nextPageCommand);

        var previousPageCommand = new RelayCommand(c =>
        {
            if (!_pagingData.HasPrevious)
                return;

            viewModel.PageNumber -= 1;
            searchCommand.Execute(null);
        });
        AddCommand("PreviousPage", previousPageCommand);
    }
}
