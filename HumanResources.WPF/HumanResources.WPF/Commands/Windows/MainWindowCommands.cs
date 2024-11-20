using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.ViewModels.Windows;
using HumanResources.WPF.Views.Windows;
using System.Windows.Controls;

namespace HumanResources.WPF.Commands.Windows;

public class MainWindowCommands : CommandsDictionary
{
    public MainWindowCommands(MainWindowViewModel viewModel)
    {
		var selectCommand = new RelayCommand(param =>
		{
			var radioButton = param as RadioButton;
			var content = radioButton.Content as string;
			viewModel.PageManager.Page = viewModel.PageManager.Pages[content];
		});
		AddCommand("Select", selectCommand);

		var forwardCommand = new RelayCommand(c =>
		{
			if (!viewModel.NavigationService.CanGoForward)
				return;

			viewModel.NavigationService.GoForward();
		});
		AddCommand("Forward", forwardCommand);

		var backCommand = new RelayCommand(c =>
		{
			if (!viewModel.NavigationService.CanGoBack)
				return;

			viewModel.NavigationService.GoBack();
		});
		AddCommand("Back", backCommand);

		var logOutCommand = new RelayCommand(c =>
		{
			var loginWindow = new LoginWindow();
			loginWindow.Show();
			WindowsHandler.Close(viewModel);
		});
		AddCommand("LogOut", logOutCommand);
    }
}
