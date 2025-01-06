namespace NotyAI.Photos;

internal class CurrentPhoto
{
    private string _base64 = string.Empty;
    public string Text { get; private set; } = string.Empty;

    public bool IsCaptured() => !string.IsNullOrEmpty(_base64);
    public string Source => $"data:image/jpeg;base64,{_base64}";
    
    public void Captured(string base64)
    {
        _base64 = base64;
        Text = string.Empty;
    }
    
    public void ReadText()
    {
        Text = "test";
    }
    
}