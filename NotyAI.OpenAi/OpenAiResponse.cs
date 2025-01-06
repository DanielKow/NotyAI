namespace NotyAI.OpenAI;

internal class OpenAiResponse
{
    public Choice[] Choices { get; init; } = [];
}

internal class Choice
{
    public Message Message { get; init; } = new();
    
}

internal class Message
{
    public string Content { get; init; } = string.Empty;
}