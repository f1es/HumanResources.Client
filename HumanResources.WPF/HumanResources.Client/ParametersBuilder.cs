using System.Text;

namespace HumanResources.Client;

public class ParametersBuilder
{
	public string BuildParameters<T>(T parametersObj)
	{
		var objectProperties = parametersObj.GetType().GetProperties();
		var propertyBuilder = new StringBuilder("?");

		foreach (var property in objectProperties)
		{
			if (property.GetValue(parametersObj) != null)
			{
				propertyBuilder.Append($"{property.Name}={property.GetValue(parametersObj).ToString()}&");
			}
		}

		propertyBuilder = propertyBuilder.Remove(propertyBuilder.Length - 1, 1);

		return propertyBuilder.ToString();
	}
}
