namespace NotyAI.OpenAI;

public interface IOpenAiService
{
    Task<string> GetTextFromImageAsync(string base64);
}