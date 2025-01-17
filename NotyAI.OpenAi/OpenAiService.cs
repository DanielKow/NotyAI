using System.ClientModel;
using System.Text.Json;
using OpenAI.Audio;
using OpenAI.Chat;
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

    private readonly AudioClient _audioClient = new("tts-1", OpenAiConstants.ApiKey);
    
    public async Task<string> GetTextFromImage(string base64)
    {
        var messages = new object[]
        {
            new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text = "Proszę odczytaj tekst z załączonego zdjęcia (ręcznie napisany)."
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
                content = """
                          Jesteś zaawansowanym asystentem AI, wyspecjalizowanym w analizie i podsumowywaniu 
                          akademickich notatek. Twoim celem jest dostarczenie użytkownikowi maksymalnie 
                          przydatnych informacji w jak najbardziej zrozumiałej formie. Wykorzystuj klarowny język, 
                          a także stosuj strukturę, która wspiera zapamiętywanie treści. Podczas tworzenia podsumowań:
                          - Wybieraj tylko najważniejsze fakty, definicje, pojęcia i argumenty.
                          - Grupuj podobne tematy w logiczne sekcje.
                          - Upewnij się, że podsumowanie podkreśla związki między różnymi tematami.
                          """
            },
            new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text =  $"""
                                Proszę o podsumowanie poniższych notatek akademickich:
                                
                                {text}
                                
                                Podczas tworzenia podsumowania:
                                1. Wybierz najważniejsze punkty i wylistuj je w logicznej kolejności.
                                2. Wyodrębnij główną myśl przewodnią, która spaja wszystkie informacje w notatkach.
                                3. Używaj zwięzłych, ale precyzyjnych zdań – unikaj zbytniego rozwlekania, ale zachowaj 
                                kluczowe szczegóły.
                                4. Podziel podsumowanie na sekcje tematyczne (np. wprowadzenie, kluczowe argumenty, wnioski), 
                                jeśli notatki obejmują kilka obszarów tematycznych.
                                5. Wskaż, dlaczego dane zagadnienia są istotne i jak odnoszą się do głównego tematu.
                                6. Upewnij się, że podsumowanie jest czytelne i ustrukturyzowane tak, aby ułatwiało szybkie powtórki materiału.
                                """
                    }
                }
            }
        };

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages,
            temperature=0.2,
            max_tokens=1500,
            timeout=20
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

    public async Task SaveTextToSpeech(string path, string text)
    {
        BinaryData? speech = await _audioClient.GenerateSpeechAsync(text, GeneratedSpeechVoice.Onyx);

        if (speech is null)
        {
            return;
        }
        
        await using FileStream stream = File.OpenWrite(path);
        await speech.ToStream().CopyToAsync(stream);
    }
}