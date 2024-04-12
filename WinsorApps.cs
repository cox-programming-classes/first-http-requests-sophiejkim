

namespace CS_First_HTTP_Client;

public readonly record struct Login(string email, string password);

public readonly record struct ErrorResponse(string email, string password);

public readonly record struct AuthResponse(string UserId, string jwt, DateTime expires, string refreshToken);

public readonly record struct UserInfo(string id, String FirstName, String nickname, string lastName);

public readonly record struct StudentInfo(string gradYear, string className, AdvisorInfo advisor);

public readonly record struct AdvisorInfo(string id, string firstName, string lastName, string email);

public readonly record struct CycleDay(DateOnly date, String cycleDay);

