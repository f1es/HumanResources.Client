using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.Client.Shared.Parameters;
using HumanResources.WPF.Commands.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class CompaniesPageViewModel : ObservableObject
{
    private HumanResourcesClient _client;

    private List<CompanyView> _companies = new List<CompanyView>();
    private CompanyRequestParameters _requestParameters = new CompanyRequestParameters() 
    { 
        PageNumber = 1,
    };

	private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
	{
		{ "Name A-Z", $"{nameof(CompanyResponseDto.Name)} asc" },
		{ "Name Z-A", $"{nameof(CompanyResponseDto.Name)} desc" },
		{ "Oldest", $"{nameof(CompanyResponseDto.BaseDate)} desc" },
		{ "Newest", $"{nameof(CompanyResponseDto.BaseDate)} asc" },
	};
	private string _sortType;

    public HumanResourcesClient Client => _client;
    public CompanyPageCommands Commands { get; set; }
    public List<CompanyView> Companies
    {
        get => _companies;
        set
        {
            _companies = value;
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
            OnPropertyChanged();
			Commands["Search"].Execute(null);
		}
    }
    
	public CompanyRequestParameters RequestParameters
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
    public CompaniesPageViewModel(HumanResourcesClient client)
    {
        _client = client;
        Commands = new CompanyPageCommands(this);
        _sortType = _sortTypes.First().Key;

        Commands["Search"].Execute(null);
    }

    public CompaniesPageViewModel()
    { }
}
