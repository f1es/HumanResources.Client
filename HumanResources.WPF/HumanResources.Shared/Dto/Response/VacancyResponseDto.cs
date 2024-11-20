namespace HumanResources.Core.Shared.Dto.Response;

public record VacancyResponseDto(
    Guid Id,
    DateTime ReceiptDate,
    string Description,
	Guid ProfessionId,
    Guid ComapnyId);
