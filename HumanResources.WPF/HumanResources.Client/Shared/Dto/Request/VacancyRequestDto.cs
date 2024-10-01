namespace HumanResources.Client.Shared.Dto.Request;

public record VacancyRequestDto(
    DateTime ReceiptDate,
    string Description,
    Guid ProffesionId);
