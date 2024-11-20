using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class ProfessionWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private ProfessionRequestDto _professionRequestDto = new ProfessionRequestDto("", 0);
	private bool _isEdit;
	private Guid _id;

	public ProfessionRequestDto ProfessionRequestDto
	{
		get => _professionRequestDto;
		set
		{
			_professionRequestDto = value;
			OnPropertyChanged();
		}
	}

	public ICommand ApplyCommand =>
		new RelayCommand(async c =>
		{
			if (_isEdit)
				await _client.Professions.PutAsync(_id, ProfessionRequestDto);
			else
				await _client.Professions.PostAsync(ProfessionRequestDto);

			var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
			mainWindowContext.PageManager.UpdateProfessions();

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});

    public ProfessionWindowViewModel(HumanResourcesClient client)
    {
        _client = client;
		_isEdit = false;
    }
	public ProfessionWindowViewModel(HumanResourcesClient client, ProfessionResponseDto professionResponseDto)
	{
		_client = client;
		_isEdit = true;
		_professionRequestDto = new ProfessionRequestDto(
			professionResponseDto.Name,
			professionResponseDto.Salary);
		_id = professionResponseDto.Id;
	}
	public ProfessionWindowViewModel()
    { }
}
