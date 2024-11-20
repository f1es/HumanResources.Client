using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.Client;
using HumanResources.Shared.Dto.Request;
using HumanResources.WPF.Views.Windows;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class LoginWindowViewModel : ObservableObject
{
	private HumanResourcesClient _client;
	private LoginDto _loginDto = new LoginDto("", "");

	public LoginDto LoginDto
	{
		get => _loginDto;
		set
		{
			_loginDto = value;
			OnPropertyChanged();
		}
	}

	public ICommand SignInCommand =>
		new RelayCommand(async c =>
		{
			try
			{
				await _client.GetAccessTokenAsync(_loginDto);

				if (!WindowsHandler.IsExist<MainWindow>())
				{
					var mainWindow = new MainWindow(_client);
					mainWindow.Show();
				}

				WindowsHandler.Close(this);
			}
			catch
			{
				MessageWindowViewModel.Show("Incorrect login or password");
			}
		});

	public ICommand SignUpCommand =>
		new RelayCommand(c =>
		{
			if (!WindowsHandler.IsExist<RegisterWindow>())
			{
				var registerWindowViewModel = new RegisterWindowViewModel(_client);
				var registerWindow = new RegisterWindow()
				{
					DataContext = registerWindowViewModel,
				};
				registerWindow.Show();
			}

			WindowsHandler.Close(this);
		});

    public LoginWindowViewModel()
    {
        _client = new HumanResourcesClient();
    }
}
