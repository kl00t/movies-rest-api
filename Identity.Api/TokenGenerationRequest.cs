namespace Identity.Api;

public class TokenGenerationRequest
{
    public Guid UserId { get; set; }

    public string Email { get; init; } = null!;

    public Dictionary<string, object> CustomClaims { get; set; } = new();
}
