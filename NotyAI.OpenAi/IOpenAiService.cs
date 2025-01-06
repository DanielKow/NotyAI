namespace NotyAI.OpenAI;

public interface IOpenAiService
{
    Task<string> GetTextFromImage(string base64);
    Task<string> SummarizeTexts(IEnumerable<string> texts);
    Task SaveTextToSpeech(string path, string text);
}