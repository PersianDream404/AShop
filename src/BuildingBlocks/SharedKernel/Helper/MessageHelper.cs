namespace SharedKernel.Helper;

public static class MessageHelper
{
    public static string Format(string messageTemplate, string entityName)
    {
        return string.Format(messageTemplate, entityName);
    }
}
public static class StringHelper
{
    public static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
