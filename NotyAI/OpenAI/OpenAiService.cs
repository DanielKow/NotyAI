using System.ClientModel;
using OpenAI.Chat;

namespace NotyAI.OpenAI;

internal class OpenAiService : IOpenAiService
{
    private readonly ChatClient _client = new(model: "gpt-4o-mini", OpenAiConstants.ApiKey);

    
    public string GetTextFromImageAsync(string base64)
    {
        SystemChatMessage? systemMessage = ChatMessage.CreateSystemMessage("Jesteś automatem odczytującym treść odręcznych notatek ze zdjęcia");
        UserChatMessage? userMessage = ChatMessage.CreateUserMessage();
        userMessage.Content.Add("Odczytaj tekst ze zdjęcia notatek");
        userMessage.Content.Add(ChatMessageContentPart.CreateImagePart(new Uri(base64)));
        
        ClientResult<ChatCompletion>? completion = _client.CompleteChat(systemMessage, userMessage);
        
        if (completion is null)
        {
            return "Nie udało się odczytać tekstu z obrazu";
        }

        return "test";
    }
}