using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Parameters;
using HumanResources.WPF.Commands.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class WorkersPageViewModel : ObservableObject
{
    private HumanResourcesClient _client;
    private List<WorkerView> _workers = new List<WorkerView>();
    private WorkerRequestParameters _requestParameters = new WorkerRequestParameters() 
    {
        PageNumber = 1,
        PageSize = 10,
    };
	private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
	{
		{ "Name A-Z", $"{nameof(WorkerResponseDto.FirstName)} asc" },
		{ "Name Z-A", $"{nameof(WorkerResponseDto.FirstName)} desc" },
		{ "Oldest", $"{nameof(WorkerResponseDto.Birthday)} desc" },
		{ "Youngest", $"{nameof(WorkerResponseDto.Birthday)} asc" },
	};
	private string _sortType;

    public HumanResourcesClient Client => _client;
    public WorkersPageCommands Commands { get; set; }
    public List<WorkerView> Workers
    {
        get => _workers;
        set
        {
            _workers = value;
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
    public WorkerRequestParameters RequestParameters
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
	public WorkersPageViewModel(HumanResourcesClient client, Guid companyId, Guid departmentId)
    {
        _client = client;
        Commands = new WorkersPageCommands(this, companyId, departmentId);
        _sortType = _sortTypes.First().Key;
        _requestParameters.OrederByQuery = _sortTypes[_sortType];

        Commands["Search"].Execute(null);
    }


    public WorkersPageViewModel()
    { }
}
