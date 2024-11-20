using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Core.Shared.Dto.Response;
using HumanResources.Core.Shared.Parameters;
using HumanResources.WPF.Commands.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class DepartmentsPageViewModel : ObservableObject
{
    private HumanResourcesClient _client;
    private List<DepartmentView> _departments = new List<DepartmentView>();
    private DepartmentRequestParameters _requestParameters = new DepartmentRequestParameters
    {
        PageNumber = 1,
        PageSize = 10,
    };
    private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
    {
		{ "Name A-Z", $"{nameof(DepartmentResponseDto.Name)} asc" },
		{ "Name Z-A", $"{nameof(DepartmentResponseDto.Name)} desc" },
	};
    private string _sortType;

    public HumanResourcesClient Client => _client;
	public DepartmentsPageCommands Commands { get; set; }
    public List<DepartmentView> Departments
    {
        get => _departments;
        set
        {
            _departments = value;
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
    public int PageNumber
    {
        get => _requestParameters.PageNumber;
        set
        {
            _requestParameters.PageNumber = value;
            OnPropertyChanged();
        }
    }
    public DepartmentRequestParameters RequestParameters
    {
        get => _requestParameters;
        set
        {
            _requestParameters = value;
            OnPropertyChanged();
        }
    }
    public DepartmentsPageViewModel(HumanResourcesClient client, Guid companyId)
    {
        Commands = new DepartmentsPageCommands(this, companyId);
        _sortType = _sortTypes.First().Key;

        _client = client;
    }

    public DepartmentsPageViewModel()
    { }
}
