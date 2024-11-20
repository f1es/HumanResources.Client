using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class DepartmentWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private DepartmentRequestDto _departmentRequestDto = new DepartmentRequestDto("");
	private Guid _companyId;
	private bool _isEdit;
	private Guid _id;

	public DepartmentRequestDto DepartmentRequestDto
	{
		get => _departmentRequestDto;
		set
		{
			_departmentRequestDto = value;
			OnPropertyChanged();
		}
	}
	public ICommand ApplyCommand =>
		new RelayCommand(async c =>
		{
			if (_isEdit)
				await _client.Departmens.PutAsync(_companyId, _id, _departmentRequestDto);
			else
				await _client.Departmens.PostAsync(_companyId, DepartmentRequestDto);

			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
			mainWindowContext.PageManager.UpdateDepartments();

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});

    public DepartmentWindowViewModel(HumanResourcesClient client, Guid companyId)
    {
        _client = client;
		_companyId = companyId;
		_isEdit = false;
    }
	public DepartmentWindowViewModel(HumanResourcesClient client, Guid companyId, DepartmentResponseDto departmentResponseDto)
	{
		_client = client;
		_companyId = companyId;
		_isEdit = true;
		_departmentRequestDto = new DepartmentRequestDto(departmentResponseDto.Name);
		_id = departmentResponseDto.Id;
	}
	public DepartmentWindowViewModel()
    { }
}
