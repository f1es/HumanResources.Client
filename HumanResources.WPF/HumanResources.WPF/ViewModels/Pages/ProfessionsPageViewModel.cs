using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.Client.Shared.Parameters;
using HumanResources.WPF.Commands.Pages;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class ProfessionsPageViewModel : ObservableObject
{
    private HumanResourcesClient _client;

    private List<ProfessionView> _professions = new List<ProfessionView>();
    private ProfessionRequestParameters _requestParameters = new ProfessionRequestParameters()
    {
        PageNumber = 1,
    };

    private Dictionary<string, string> _sortTypes = new Dictionary<string, string>()
    {
		{ "Name A-Z", $"{nameof(ProfessionResponseDto.Name)} asc" },
		{ "Name Z-A", $"{nameof(ProfessionResponseDto.Name)} desc" },
		{ "Huge salary", $"{nameof(ProfessionResponseDto.Salary)} desc"},
        { "Small salary", $"{nameof(ProfessionResponseDto.Salary)} asc" },
    };
    private string _sortType;
    public HumanResourcesClient Client => _client;
    public ProfessionPageCommands Commands { get; set; }
    public List<ProfessionView> Professions
    {
        get => _professions;
        set
        {
            _professions = value;
            OnPropertyChanged();
        }
    }
    public ProfessionRequestParameters RequestParameters
    {
        get => _requestParameters;
        set
        {
            _requestParameters = value;
            OnPropertyChanged();
        }
    }
    public Dictionary<string, string> SortTypes
    {
        get => _sortTypes;
    }
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
    public int PageNumber
    {
        get => _requestParameters.PageNumber;
        set
        {
            _requestParameters.PageNumber = value;
            OnPropertyChanged();
        }
    }
    public ProfessionsPageViewModel(HumanResourcesClient client)
    {
        _client = client;
        Commands = new ProfessionPageCommands(this);
        _sortType = _sortTypes.First().Key;

        Commands["Search"].Execute(null);
    }

    public ProfessionsPageViewModel()
    { }
}
