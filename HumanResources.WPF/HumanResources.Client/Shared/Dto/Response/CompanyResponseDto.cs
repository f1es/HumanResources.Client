namespace HumanResources.Client.Shared.Dto.Response;

public record CompanyResponseDto(
    Guid Id,
    string Name,
    DateTime BaseDate);
