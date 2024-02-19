namespace Infrastructure.Security;

public class AuthorizationSettings
{
    public const string Section = "AuthorizationSettings";

    public string ApiToken { get; set; } = null!;
}
