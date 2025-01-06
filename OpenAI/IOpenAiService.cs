namespace NotyAI.OpenAI;

internal interface IOpenAiService
{
    Task<string> GetTextFromImageAsync(string base64);
}