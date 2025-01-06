namespace NotyAI.Notes;

internal class Note
{
    private readonly List<string> _photos = [];
    private readonly List<string> _texts = [];
    public IEnumerable<string> Photos => _photos;
    public IEnumerable<string> Texts => _texts;
    public string Summary { get; private set; } = string.Empty;
    public static string TextSpeechPath => Path.Combine(FileSystem.AppDataDirectory, "text.wav");
    public static string SummarySpeechPath => Path.Combine(FileSystem.AppDataDirectory, "summary.wav");

    public void AddPhoto(string photo)
    {
        _photos.Add(photo);
    }

    public void AddText(string text)
    {
        _texts.Add(text);
    }

    public void Summarize()
    {
        Summary = string.Join("\n", _texts);
    }
    
    public void Clear()
    {
        _photos.Clear();
        _texts.Clear();
        Summary = string.Empty;
    }
}