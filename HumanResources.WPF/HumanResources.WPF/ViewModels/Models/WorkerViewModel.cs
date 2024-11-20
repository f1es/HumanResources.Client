using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.WPF.Commands.Models;

namespace HumanResources.WPF.ViewModels.Models;

public class WorkerViewModel : ObservableObject
{
	private WorkerResponseDto _worker = new WorkerResponseDto(Guid.NewGuid(), "", "", "", DateTime.Parse("01.01.2001"));
	private HumanResourcesClient _client;
	private Guid _companyId;
	private Guid _departmentId;
	private WorkerModelCommands _commands;
	public WorkerResponseDto Worker
	{
		get => _worker;
		set
		{
			_worker = value;
			OnPropertyChanged();
		}
	}
	public HumanResourcesClient Client => _client;
	public Guid CompanyId => _companyId;
	public Guid DepartmentId => _departmentId;
	public WorkerModelCommands Commands => _commands;
    public WorkerViewModel(HumanResourcesClient client, Guid companyId, Guid departmentId)
    {
		_client = client;
		_companyId = companyId;
		_departmentId = departmentId;
		_commands = new WorkerModelCommands(this);
    }

    public WorkerViewModel()
    { }
}
