using FlesLib.WPF.Commands;
using HumanResources.Core.Shared.Features;
using HumanResources.WPF.Mappers;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;

namespace HumanResources.WPF.Commands.Pages;

public class DepartmentsPageCommands : CommandsDictionary
{
	private PagingData _pagingData = new PagingData();
    public DepartmentsPageCommands(DepartmentsPageViewModel viewModel, Guid companyId)
    {
		var addCommand = new RelayCommand(c =>
		{
			var addDepartmentDialog = new DepartmentWindow();
			var addDepartmentContext = new DepartmentWindowViewModel(viewModel.Client, companyId);
			addDepartmentDialog.DataContext = addDepartmentContext;
			addDepartmentDialog.ShowDialog();
		});
		AddCommand("Add", addCommand);

        var searchCommand = new RelayCommand(async C =>
        {
            var departments = await viewModel.Client.Departmens.GetAllAsync(companyId, viewModel.RequestParameters);

            viewModel.Departments = departments.ToView(viewModel.Client).ToList();
			_pagingData = departments.PagingData;
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
