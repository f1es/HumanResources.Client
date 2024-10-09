using HumanResources.Client.Enums;

namespace HumanResources.Client.Methods;

public abstract class BaseMethods
{
	private ParametersBuilder _parametersBuilder;
    protected BaseMethods()
    {
        _parametersBuilder = new ParametersBuilder();
    }
    protected string AddParametersToUri<T>(string uri, T parameters)
	{
		var parametersString = _parametersBuilder.BuildParameters(parameters);
		return uri + parametersString;
	}
}
