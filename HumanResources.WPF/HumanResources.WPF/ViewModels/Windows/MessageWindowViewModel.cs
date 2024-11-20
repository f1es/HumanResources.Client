using FlesLib.WPF;
using FlesLib.WPF.Commands;
using FlesLib.WPF.Windows;
using HumanResources.WPF.Views.Windows;
using System.Windows.Input;

namespace HumanResources.WPF.ViewModels.Windows;

public class MessageWindowViewModel : ObservableObject
{
	private string _message;

	public string Message
	{
		get => _message;
		set
		{
			_message = value;
			OnPropertyChanged();
		}
	}

	public ICommand OkCommand =>
		new RelayCommand(c =>
		{
			WindowsHandler.Close(this);
		});

    public MessageWindowViewModel(string message)
    {
        _message = message;
    }
    public MessageWindowViewModel()
    { }
    public static void Show(string message)
	{
		var messageDialogContext = new MessageWindowViewModel(message);
		var messageDialog = new MessageWindow()
		{
			DataContext = messageDialogContext,
		};
		messageDialog.ShowDialog();
	}
}
