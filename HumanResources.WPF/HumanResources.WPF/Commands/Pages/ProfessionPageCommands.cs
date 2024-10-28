using FlesLib.WPF.Commands;
using HumanResources.Client.Shared.Dto.Response;
using HumanResources.Client.Shared.Parameters;
using HumanResources.WPF.ViewModels.Pages;
using HumanResources.WPF.Mappers;

namespace HumanResources.WPF.Commands.Pages;

public class ProfessionPageCommands : CommandsDictionary
{
    public ProfessionPageCommands(ProfessionsPageViewModel viewModel)
    {
        var searchCommand = new RelayCommand(async c =>
        {
            Dictionary<string, string> sortValues = new Dictionary<string, string>()
            {
                { "Name A-Z", $"{nameof(CompanyResponseDto.Name)} asc" },
                { "Name Z-A", $"{nameof(CompanyResponseDto.Name)} desc" },
            };

            var parameters = new ProfessionRequestParameters()
            {
                PageSize = 10,
                PageNumber = viewModel.RequestParameters.PageNumber,
                SearchTerm = viewModel.RequestParameters.SearchTerm,
                MaxSalary = viewModel.RequestParameters.MaxSalary,
                MinSalary = viewModel.RequestParameters.MinSalary,
                OrederByQuery = viewModel.SortTypes[viewModel.SortType],
            };
            var professions = await viewModel.Client.Professions.GetAllAsync(parameters);

            viewModel.Professions = professions.ToView().ToList();
        });
        AddCommand("Search", searchCommand);

    }
}
