using FlesLib.WPF;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.WPF.ViewModels.Models;

public class CompanyViewModel : ObservableObject
{
	private CompanyResponseDto _company = new CompanyResponseDto(Guid.NewGuid(), "Sample name", DateTime.Now);

	public CompanyResponseDto Company
	{
		get => _company;
		set
		{
			_company = value;
			OnPropertyChanged();
		}
	}
}
