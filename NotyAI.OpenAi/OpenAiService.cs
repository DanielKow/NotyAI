using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;

namespace NotyAI.OpenAI;

internal class OpenAiService : IOpenAiService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private readonly RestClient _client = new(new RestClientOptions("https://api.openai.com/v1/chat/completions")
        {
            Authenticator = new JwtAuthenticator(OpenAiConstants.ApiKey)
        },
        configureSerialization: config => config.UseSystemTextJson(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        }));

    public async Task<string> GetTextFromImage(string base64)
    {
        var messages = new object[]
        {
            new
            {
                role = "system",
                content = "Jesteś automatem odczytującym treść odręcznych notatek ze zdjęcia"
            },
            new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text = "Odczytaj tekst ze zdjęcia notatek"
                    },
                    new
                    {
                        type = "image_url",
                        image_url = new
                        {
                            url = base64
                        }
                    }
                }
            }
        };

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages
        };

        string requestBodyJson = JsonSerializer.Serialize(requestBody, _jsonSerializerOptions);

        RestRequest request = new RestRequest().AddJsonBody(requestBodyJson);
        var response = await _client.PostAsync<OpenAiResponse>(request);

        if (response is null)
        {
            return "Nie udało się odczytać tekstu ze zdjęcia";
        }

        if (response.Choices.Length == 0)
        {
            return "Nie udało się odczytać tekstu ze zdjęcia";
        }

        return response.Choices[0].Message.Content;
    }

    public async Task<string> SummarizeTexts(IEnumerable<string> texts)
    {
        string text = string.Join('\n', texts);
        
        var messages = new object[]
        {
            new
            {
                role = "system",
                content = "Jesteś automatem podsumowującym notatki"
            },
            new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text = $"Podsumuj następujące notatki:\n{text}"
                    }
                }
            }
        };

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages
        };

        string requestBodyJson = JsonSerializer.Serialize(requestBody, _jsonSerializerOptions);

        RestRequest request = new RestRequest().AddJsonBody(requestBodyJson);
        var response = await _client.PostAsync<OpenAiResponse>(request);

        if (response is null)
        {
            return text;
        }

        if (response.Choices.Length == 0)
        {
            return text;
        }

        return response.Choices[0].Message.Content;
    }
}