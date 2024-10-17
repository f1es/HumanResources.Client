using FlesLib.WPF;
using HumanResources.Client.Shared.Dto.Response;

namespace HumanResources.WPF.ViewModels.Models;

public class ProfessionViewModel : ObservableObject
{
	private ProfessionResponseDto _profession = new ProfessionResponseDto(Guid.NewGuid(), "Sample name", 1000);

	public ProfessionResponseDto Profession
	{
		get => _profession;
		set
		{
			_profession = value;
			OnPropertyChanged();
		}
	}
}
