using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Mappers;

public static class DepartmentMapper
{
	public static DepartmentView ToView(this DepartmentResponseDto department, HumanResourcesClient client)
	{
		var departmentView = new DepartmentView();
		var dataContext = new DepartmentViewModel(client);

		dataContext.Department = department;
		departmentView.DataContext = dataContext;

		return departmentView;
	}

	public static IEnumerable<DepartmentView> ToView(this IEnumerable<DepartmentResponseDto> departments, HumanResourcesClient client)
	{
		var departmentViews = new List<DepartmentView>();
		foreach (var department in departments)
			departmentViews.Add(department.ToView(client));
		return departmentViews;
	}
}
