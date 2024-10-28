using System.Globalization;
using System.Windows.Data;

namespace HumanResources.WPF.Convertors;

public class UintToStringConvertor : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var uintValue = (uint)value;

		if (uintValue == 0)
			return string.Empty;

		if (uintValue == uint.MaxValue) 
			return string.Empty;

		return value.ToString();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (uint.TryParse(value as string, out uint result))
		{
			return result;
		}
		return 0;
	}
}
