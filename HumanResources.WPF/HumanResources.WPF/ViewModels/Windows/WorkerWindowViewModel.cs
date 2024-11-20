using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class WorkerWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private WorkerRequestDto _workerRequestDto = new WorkerRequestDto("", "", "", DateTime.Now);
	private Guid _companyId;
	private Guid _departmentId;
	private bool _isEdit;
	private Guid _id;
	public WorkerRequestDto WorkerRequestDto
	{
		get => _workerRequestDto;
		set
		{
			_workerRequestDto = value;
			OnPropertyChanged();
		}
	}
	public ICommand ApplyCommand =>
		new RelayCommand(async c =>
		{
			if (_isEdit)
				await _client.Workers.PutAsync(_companyId, _departmentId, _id, _workerRequestDto);
			else
				await _client.Workers.PostAsync(_companyId, _departmentId, WorkerRequestDto);

			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
			mainWindowContext.PageManager.UpdateWorkers();

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});

	public WorkerWindowViewModel(HumanResourcesClient client, Guid companyId, Guid departmentId)
    {
        _client = client;
		_companyId = companyId;
		_departmentId = departmentId;
		_isEdit = false;
    }
	public WorkerWindowViewModel(HumanResourcesClient client, Guid companyId, Guid departmentId, WorkerResponseDto workerResponseDto)
	{
		_client = client;
		_companyId = companyId;
		_departmentId = departmentId;
		_isEdit = true;
		_workerRequestDto = new WorkerRequestDto(
			workerResponseDto.FirstName,
			workerResponseDto.LastName,
			workerResponseDto.Phone,
			workerResponseDto.Birthday);
		_id = workerResponseDto.Id;
	}
	public WorkerWindowViewModel()
    { }
}
