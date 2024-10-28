using FlesLib.WPF;
using HumanResources.Client;
using HumanResources.WPF.Views.Models;

namespace HumanResources.WPF.ViewModels.Pages;

public class VacanciesPageViewModel : ObservableObject
{
	private HumanResourcesClient _client;
    private List<VacancyView> _vacancies = new List<VacancyView>();

	public HumanResourcesClient Client => _client;

    public VacanciesPageViewModel(HumanResourcesClient client)
    {
        _client = client;
    }
}
