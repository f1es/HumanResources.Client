namespace HumanResources.Client.Shared.Dto.Response;

public record ProfessionResponseDto(
    Guid Id,
    string Name,
    decimal Salary);
