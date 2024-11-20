using FlesLib.WPF.Commands;
using HumanResources.Core.Shared.Features;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Pages;

public class WorkersPageCommands : CommandsDictionary
{
	private PagingData _pagingData = new PagingData();
    public WorkersPageCommands(WorkersPageViewModel viewModel, Guid companyId, Guid departmentId)
    {
		var addWorkerCommand = new RelayCommand(c =>
		{
			var addWorkerDialog = new WorkerWindow();
			var addWorkerWindowContext = new WorkerWindowViewModel(viewModel.Client, companyId, departmentId);
			addWorkerDialog.DataContext = addWorkerWindowContext;
			addWorkerDialog.ShowDialog();
		});
		AddCommand("Add", addWorkerCommand);

        var searchCommand = new RelayCommand(async c =>
        {
            var workers = await viewModel.Client.Workers.GetAllAsync(companyId, departmentId, viewModel.RequestParameters);

            viewModel.Workers = workers.ToView(viewModel.Client, companyId, departmentId).ToList();
			_pagingData = workers.PagingData;
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
