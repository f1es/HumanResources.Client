using FlesLib.WPF;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.WPF.ViewModels.Models;

public class DepartmentViewModel : ObservableObject
{
	private DepartmentResponseDto _department = new DepartmentResponseDto(Guid.NewGuid(), "Sample Name");

	public DepartmentResponseDto Department
	{
		get => _department;
		set
		{
			_department = value;
			OnPropertyChanged();
		}
	}
}
