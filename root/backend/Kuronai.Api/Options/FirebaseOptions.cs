using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Kuronai.Api.Options;

public class FirebaseOptions
{
    public string GoogleCredentialFilePath { get; set; } = null!;

    public AppOptions ToAppOptions() => new()
    {
        Credential = GoogleCredential.FromFile(GoogleCredentialFilePath),
        ProjectId = "meal-generator-68891"
    };
}
