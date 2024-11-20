namespace HumanResources.Core.Shared.Dto.Response;

public record DepartmentResponseDto(
    Guid Id,
    string Name,
    Guid CompanyId);
