namespace NotyAI.Photos;

internal class CurrentPhoto
{
    public string Base64 { get; private set; } = string.Empty;
    public string Text { get; private set; } = string.Empty;
    public string Summary { get; private set; } = string.Empty;
    
    public string PathToTextSynthesis { get; set; } = string.Empty;
    public string PathToSummarization { get; set; } = string.Empty; 

    public bool IsCaptured() => !string.IsNullOrEmpty(Base64);
    public string Src => $"data:image/jpeg;base64,{Base64}";
    
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
    
    public void TextSynthesized(string path)
    {
        PathToTextSynthesis = path;
    }
    
    public void SummarySynthesized(string path)
    {
        PathToSummarization = path;
    }
    
}