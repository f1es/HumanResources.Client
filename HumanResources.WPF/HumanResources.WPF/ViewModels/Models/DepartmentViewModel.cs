using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class DepartmentViewModel : ObservableObject
{
	private DepartmentResponseDto _department = new DepartmentResponseDto(Guid.NewGuid(), "Sample Name", Guid.NewGuid());
	private HumanResourcesClient _client;
	public DepartmentModelCommands Commands { get; set; }
	
	public DepartmentResponseDto Department
	{
		get => _department;
		set
		{
			_department = value;
			OnPropertyChanged();
		}
	}
	public HumanResourcesClient Client => _client;

    public DepartmentViewModel(HumanResourcesClient client)
    {
		_client = client;
		Commands = new DepartmentModelCommands(this);
    }

    public DepartmentViewModel()
    { }
}
