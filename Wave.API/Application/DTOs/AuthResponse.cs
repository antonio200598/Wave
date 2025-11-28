namespace Wave.API.Application.DTOs;

public class AuthResponse
{
    public string Token { get; }
    public string ErrorMessage { get; }

    public bool Success => ErrorMessage == null;

    public AuthResponse(string token, string errorMessage)
    {
      Token = token;
      ErrorMessage = errorMessage;
    }
}
