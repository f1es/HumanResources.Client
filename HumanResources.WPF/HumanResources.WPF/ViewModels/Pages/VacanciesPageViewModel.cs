using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Parameters;
using HumanResources.WPF.Commands.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class VacanciesPageViewModel : ObservableObject
{
	private HumanResourcesClient _client;
    private List<VacancyView> _vacancies = new List<VacancyView>();
    private VacancyRequestParameters _requestParameters = new VacancyRequestParameters()
    {
        PageNumber = 1,
        PageSize = 10,
    };
    private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
    {
        { "Description A-Z", $"{nameof(VacancyResponseDto.Description)} asc" },
        { "Description Z-A", $"{nameof(VacancyResponseDto.Description)} desc" },
        { "Oldest", $"{nameof(VacancyResponseDto.Description)} desc" },
        { "Newest", $"{nameof(VacancyResponseDto.Description)} asc" },
    };
    private string _sortType;
    private Guid _companyId;

	public HumanResourcesClient Client => _client;
    public VacanciesPageCommands Commands { get; set; }
    public List<VacancyView> Vacancies
    {
        get => _vacancies;
        set
        {
            _vacancies = value;
            OnPropertyChanged();
        }
    }
    public Dictionary<string, string> SortTypes => _sortTypes;
    public string SortType
    {
        get => _sortType;
        set
        {
            _sortType = value;
            _requestParameters.OrederByQuery = _sortTypes[_sortType];
            OnPropertyChanged();
            Commands["Search"].Execute(null);
        }
    }
    public VacancyRequestParameters RequestParameters
    {
        get => _requestParameters;
        set
        {
            _requestParameters = value;
            OnPropertyChanged();
        }
    }
    public int PageNumber
    {
        get => _requestParameters.PageNumber;
        set
        {
            _requestParameters.PageNumber = value;
            OnPropertyChanged();
        }
    }
    public VacanciesPageViewModel(HumanResourcesClient client, Guid companyId)
    {
        _client = client;
        Commands = new VacanciesPageCommands(this, companyId);
        _companyId = companyId;
        _sortType = _sortTypes.First().Key;
        _requestParameters.OrederByQuery = _sortTypes[_sortType];

        Commands["Search"].Execute(null);
    }
    public VacanciesPageViewModel()
    { }
}
