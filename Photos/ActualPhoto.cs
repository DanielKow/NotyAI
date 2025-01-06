namespace NotyAI.Photos;

internal class ActualPhoto
{
    public string Base64 { get; private set; } = string.Empty;
    public string Text { get; private set; } = string.Empty;
    public string Summary { get; private set; } = string.Empty;

    public void Captured(string base64)
    {
        Base64 = base64;
        Text = string.Empty;
        Summary = string.Empty;
    }
    
    public void TextRead(string text)
    {
        Text = text;
        Summary = string.Empty;
    }
    
    public void Summarized(string summary)
    {
        Summary = summary;
    }
}