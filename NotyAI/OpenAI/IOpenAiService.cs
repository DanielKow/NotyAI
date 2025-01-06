namespace NotyAI.OpenAI;

internal interface IOpenAiService
{
    string GetTextFromImageAsync(string base64);
}