using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class AddCompanyWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private CompanyRequestDto _companyRequestDto = new CompanyRequestDto("", DateTime.Now);
	private bool _isEdit;
	private Guid _id;
	public CompanyRequestDto CompanyRequestDto
	{
		get => _companyRequestDto;
		set
		{
			_companyRequestDto = value;
			OnPropertyChanged();
		}
	}
	public ICommand ApplyCommand =>
		new RelayCommand(async c =>
		{
			if (_isEdit)
				await _client.Companies.PutAsync(_id, CompanyRequestDto);
			else
				await _client.Companies.PostAsync(CompanyRequestDto);

			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
			mainWindowContext.PageManager.UpdateCompanies();

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});

    public AddCompanyWindowViewModel(HumanResourcesClient client)
    {
        _client = client;
		_isEdit = false;
    }
    public AddCompanyWindowViewModel(HumanResourcesClient client, CompanyResponseDto companyResponseDto)
    {
		_client = client;
		_id = companyResponseDto.Id;
		_companyRequestDto = new CompanyRequestDto(
			companyResponseDto.Name,
			companyResponseDto.BaseDate);
		_isEdit = true;
    }
    public AddCompanyWindowViewModel()
    { }
}
