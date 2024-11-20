using FlesLib.WPF.Commands;
using HumanResources.Core.Shared.Features;
using HumanResources.Core.Shared.Parameters;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Pages;

public class ProfessionPageCommands : CommandsDictionary
{
    private PagingData _pagingData = new PagingData();
    public ProfessionPageCommands(ProfessionsPageViewModel viewModel)
    {
        var addCommand = new RelayCommand(c =>
        {
            var addProfessionDialog = new ProfessionWindow();
            var addProfessionContext = new ProfessionWindowViewModel(viewModel.Client);
            addProfessionDialog.DataContext = addProfessionContext;
            addProfessionDialog.ShowDialog();
        });
        AddCommand("Add", addCommand);

        var searchCommand = new RelayCommand(async c =>
        {
            var parameters = new ProfessionRequestParameters()
            {
                PageSize = 10,
                PageNumber = viewModel.RequestParameters.PageNumber,
                SearchTerm = viewModel.RequestParameters.SearchTerm,
                MaxSalary = viewModel.RequestParameters.MaxSalary,
                MinSalary = viewModel.RequestParameters.MinSalary,
                OrederByQuery = viewModel.SortTypes[viewModel.SortType],
            };
            var professions = await viewModel.Client.Professions.GetAllAsync(parameters);

            viewModel.Professions = professions.ToView(viewModel.Client).ToList();
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
