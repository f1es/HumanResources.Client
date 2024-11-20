using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HumanResources.WPF.Views;

public class WatermarkTextBox : TextBox
{
	private Brush _foreground;

	public static readonly DependencyProperty WatermarkProperty =
		DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(string.Empty));

	public string Watermark
	{
		get { return (string)GetValue(WatermarkProperty); }
		set { SetValue(WatermarkProperty, value); }
	}

	public WatermarkTextBox()
	{
		Loaded += WatermarkTextBox_Loaded;
		GotFocus += WatermarkTextBox_GotFocus;
		LostFocus += WatermarkTextBox_LostFocus;
		TextChanged += WatermarkTextBox_TextChanged;

		_foreground = Foreground;
	}

	private void WatermarkTextBox_Loaded(object sender, RoutedEventArgs e)
	{
		_foreground = Foreground;
		ShowWatermark();
	}

	private void WatermarkTextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		HideWatermark();
	}

	private void WatermarkTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		ShowWatermark();
	}

	private void WatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (IsFocused)
		{
			Foreground = _foreground;
			HideWatermark();
		}
		else
		{
			ShowWatermark();
		}
	}

	private void ShowWatermark()
	{
		if (string.IsNullOrEmpty(Text))
		{
			Text = Watermark;
			Foreground = Brushes.LightGray;
		}
	}

	private void HideWatermark()
	{
		if (Text == Watermark)
		{
			Text = string.Empty;
			Foreground = _foreground;
		}
	}
}
