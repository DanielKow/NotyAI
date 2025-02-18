using NotyAI.OpenAI;

namespace NotyAI.Notes;

internal class Note(IOpenAiService openAiService)
{
    private readonly List<string> _photos = [];
    private readonly List<string> _texts = [];
    public ICollection<string> Photos => _photos;
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

    public async Task Summarize()
    {
        Summary = await openAiService.SummarizeTexts(_texts);
    }
    
    public async Task TextToSpeech()
    {
        await openAiService.SaveTextToSpeech(TextSpeechPath, string.Join(" ", _texts));
        await openAiService.SaveTextToSpeech(SummarySpeechPath, Summary);
    }
    
    public void Clear()
    {
        _photos.Clear();
        _texts.Clear();
        Summary = string.Empty;
        
        File.Delete(TextSpeechPath);
        File.Delete(SummarySpeechPath);
    }
}