namespace HumanResources.Core.Shared.Dto.Request;

public record VacancyRequestDto(
    DateTime ReceiptDate,
    string Description,
    Guid ProffesionId);
