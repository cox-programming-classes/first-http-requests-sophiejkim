

namespace CS_First_HTTP_Client;

public readonly record struct Login(string email, string password);

public readonly record struct ErrorResponse(string email, string password);

public readonly record struct AuthResponse(string UserId, string jwt, DateTime expires, string refreshToken);

public readonly record struct UserInfo(string id, String FirstName, String nickname, string lastName);


