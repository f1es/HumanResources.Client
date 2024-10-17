using FlesLib.WPF;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.WPF.ViewModels.Models;

public class WorkerViewModel : ObservableObject
{
	private WorkerResponseDto _worker = new WorkerResponseDto(Guid.NewGuid(), "Назар", "Нарейко", "Русланович", DateTime.Parse("06.05.2004"));

	public WorkerResponseDto Worker
	{
		get => _worker;
		set
		{
			_worker = value;
			OnPropertyChanged();
		}
	}
}
