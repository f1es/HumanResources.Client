using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Request;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Parameters;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class VacancyWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private List<ProfessionResponseDto> _professions;
	private VacancyRequestDto _vacancyRequestDto = new VacancyRequestDto(DateTime.Now, "", Guid.Empty);
	private ProfessionResponseDto _profession;
	private Guid _companyId;
	private bool _isEdit;
	private Guid _id;

	private string _description = "";

    public ICommand ApplyCommand => new RelayCommand(async c =>
	{
		WindowsHandler.Close(this);

		_vacancyRequestDto = new VacancyRequestDto(DateTime.Now, Description, Profession.Id);

		if (_isEdit)
			await _client.Vacancies.PutAsync(_companyId, _id, _vacancyRequestDto);
		else
			await _client.Vacancies.PostAsync(_companyId, _vacancyRequestDto);

		var mainWindowContext = WindowsHandler.GetContextOfUniqueWindow<MainWindow>() as MainWindowViewModel;
		mainWindowContext.PageManager.UpdateVacancies();
	});
	public ICommand CancelCommand => new RelayCommand(c =>
	{
		WindowsHandler.Close(this);
	});
	public ICommand LoadProfessions => new RelayCommand(async c =>
	{
		var requestParameters = new ProfessionRequestParameters()
		{
			PageSize = 10,
			PageNumber = 1,
		};

		Professions = await _client.Professions.GetAllAsync(requestParameters);
		Profession = Professions.FirstOrDefault();
	});
	public ICommand LoadProfession => new RelayCommand(async id =>
	{
		var guid = (Guid)id;
		Profession = await _client.Professions.GetByIdAsync(guid);
	});
	public string Description
	{
		get => _description;
		set
		{
			_description = value;
			OnPropertyChanged();
		}
	}
	public ProfessionResponseDto Profession
	{
		get => _profession;
		set
		{
			_profession = value;
			OnPropertyChanged();
		}
	}
	public List<ProfessionResponseDto> Professions
	{
		get => _professions;
		set
		{
			_professions = value;
			OnPropertyChanged();
		}
	}
    public VacancyWindowViewModel(HumanResourcesClient client, Guid companyId)
    {
		_client = client;
		_companyId = companyId;
		_professions = new List<ProfessionResponseDto>();
		_isEdit = false;

		LoadProfessions.Execute(null);
    }

    public VacancyWindowViewModel(HumanResourcesClient client, Guid companyId, VacancyResponseDto vacancyResponseDto)
    {
		_client = client;
		_companyId = companyId;
		_professions = new List<ProfessionResponseDto>();
		_isEdit = true;
		_id = vacancyResponseDto.Id;

		LoadProfessions.Execute(null);

		Description = vacancyResponseDto.Description;
		LoadProfession.Execute(vacancyResponseDto.ProfessionId);
    }

    public VacancyWindowViewModel()
    { }

}
