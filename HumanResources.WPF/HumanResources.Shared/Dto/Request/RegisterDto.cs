namespace HumanResources.Shared.Dto.Request;

public record RegisterDto(
	string FirstName,
	string LastName,
	string UserName,
	string Password);
