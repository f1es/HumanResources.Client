using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Shared.Dto.Request;
using HumanResources.WPF.Views.Windows;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class RegisterWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private RegisterDto _registerDto = new RegisterDto("", "", "", "");

	public RegisterDto RegisterDto
	{
		get => _registerDto;
		set
		{
			_registerDto = value;
			OnPropertyChanged();
		}
	}

	public ICommand AcceptCommand =>
		new RelayCommand(async c =>
		{
			await _client.RegisterUserAsync(_registerDto);

			if (!WindowsHandler.IsExist<LoginWindow>())
			{
				var loginWindow = new LoginWindow();
				loginWindow.Show();
			}

			WindowsHandler.Close(this);
		});

	public ICommand CancelCommand =>
		new RelayCommand(c =>
		{
			if (!WindowsHandler.IsExist<LoginWindow>())
			{
				var loginWindow = new LoginWindow();
				loginWindow.Show();
			}

			WindowsHandler.Close(this);
		});

    public RegisterWindowViewModel(HumanResourcesClient client)
    {
        _client = client;
    }

    public RegisterWindowViewModel()
    { }
}
