namespace HumanResources.Client.Shared.Dto.Response;

public record VacancyResponseDto(
    Guid Id,
    DateTime ReceiptDate,
    string Description);
