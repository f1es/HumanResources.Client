using HumanResources.Client;
using HumanResources.WPF.ViewModels.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HumanResources.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(HumanResourcesClient client)
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel(client, MainFrame.NavigationService);
		}
	}
}