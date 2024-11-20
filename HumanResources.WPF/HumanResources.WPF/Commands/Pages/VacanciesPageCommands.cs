using FlesLib.WPF.Commands;
using HumanResources.Core.Shared.Features;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Pages;

public class VacanciesPageCommands : CommandsDictionary
{
	private PagingData _pagingData = new PagingData();
    public VacanciesPageCommands(VacanciesPageViewModel viewModel, Guid companyId)
    {
		var addCommand = new RelayCommand(c =>
		{
			var addVacancyDialog = new VacancyWindow();
			var addVacancyContext = new VacancyWindowViewModel(viewModel.Client, companyId);
			addVacancyDialog.DataContext = addVacancyContext;
			addVacancyDialog.ShowDialog();
		});
		AddCommand("Add", addCommand);

        var searchCommand = new RelayCommand(async c =>
        {
            var vacancies = await viewModel.Client.Vacancies.GetAllAsync(companyId, viewModel.RequestParameters);

            viewModel.Vacancies = vacancies.ToView(viewModel.Client).ToList();
			_pagingData = vacancies.PagingData;
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
