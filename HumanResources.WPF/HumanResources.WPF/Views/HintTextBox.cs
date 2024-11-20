using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HumanResources.WPF.Views;
public class HintTextBox : Grid, INotifyPropertyChanged
{
	private readonly TextBox _textBox;
	private readonly TextBlock _hintTextBlock;

	public static readonly DependencyProperty HintProperty =
		DependencyProperty.Register("Hint", typeof(string), typeof(HintTextBox), new PropertyMetadata(string.Empty, OnHintPropertyChanged));

	public static readonly DependencyProperty TextProperty =
		DependencyProperty.Register("Text", typeof(string), typeof(HintTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged));

	public static readonly DependencyProperty TextBoxStyleProperty =
		DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(HintTextBox), new PropertyMetadata(null, OnTextBoxStylePropertyChanged));

	public static readonly DependencyProperty HintTextBlockStyleProperty =
		DependencyProperty.Register("HintTextBlockStyle", typeof(Style), typeof(HintTextBox), new PropertyMetadata(null, OnHintTextBlockStylePropertyChanged));

	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") 
	{ 
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
	}

	public string Hint
	{
		get { return (string)GetValue(HintProperty); }
		set 
		{ 
			SetValue(HintProperty, value);
			NotifyPropertyChanged();
		}
	}

	public string Text
	{
		get { return (string)GetValue(TextProperty); }
		set 
		{ 
			SetValue(TextProperty, value);
			NotifyPropertyChanged(nameof(Text));
		}
	}

	public Style TextBoxStyle
	{
		get { return (Style)GetValue(TextBoxStyleProperty); }
		set { SetValue(TextBoxStyleProperty, value); }
	}

	public Style HintTextBlockStyle
	{
		get { return (Style)GetValue(HintTextBlockStyleProperty); }
		set { SetValue(HintTextBlockStyleProperty, value); }
	}

	public HintTextBox()
	{
		_hintTextBlock = new TextBlock
		{
			Foreground = Brushes.LightGray,
			VerticalAlignment = VerticalAlignment.Center,
			Margin = new Thickness(5, 2, 0, 0),
			IsHitTestVisible = false
		};

		_textBox = new TextBox
		{
			Background = Brushes.Transparent,
			MinWidth = 50
		};

		_textBox.TextChanged += TextBox_TextChanged;
		_textBox.GotFocus += TextBox_GotFocus;
		_textBox.LostFocus += TextBox_LostFocus;

		this.Children.Add(_hintTextBlock);
		this.Children.Add(_textBox);
	}

	private static void OnHintPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var hintTextBox = (HintTextBox)d;
		hintTextBox._hintTextBlock.Text = (string)e.NewValue;
	}

	private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var hintTextBox = (HintTextBox)d;
		hintTextBox._textBox.Text = (string)e.NewValue;
	}

	private static void OnTextBoxStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var hintTextBox = (HintTextBox)d;
		hintTextBox._textBox.Style = (Style)e.NewValue;
	}

	private static void OnHintTextBlockStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var hintTextBox = (HintTextBox)d;
		hintTextBox._hintTextBlock.Style = (Style)e.NewValue;
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		this.Text = _textBox.Text;
		_hintTextBlock.Visibility = string.IsNullOrEmpty(_textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
	}

	private void TextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		_hintTextBlock.Visibility = Visibility.Collapsed;
	}

	private void TextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		_hintTextBlock.Visibility = string.IsNullOrEmpty(_textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
	}
}
