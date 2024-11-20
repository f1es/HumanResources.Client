using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.ViewModels.Models;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.Mappers;

public static class WorkerMapper
{
	public static WorkerView ToView(this WorkerResponseDto worker, HumanResourcesClient client, Guid companyId, Guid departmentId)
	{
		var workerView = new WorkerView();
		var dataContext = new WorkerViewModel(client, companyId, departmentId);

		dataContext.Worker = worker;
		workerView.DataContext = dataContext;

		return workerView;
	}

	public static IEnumerable<WorkerView> ToView(this IEnumerable<WorkerResponseDto> workers, HumanResourcesClient client, Guid companyId, Guid departmentId)
	{
		var workerViews = new List<WorkerView>();
		foreach (var worker in workers)
			workerViews.Add(worker.ToView(client, companyId, departmentId));
		return workerViews;
	}
}
