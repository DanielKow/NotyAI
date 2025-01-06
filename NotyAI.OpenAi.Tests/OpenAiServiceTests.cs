using FluentAssertions;
using NotyAI.OpenAI;

namespace NotyAI.OpenAi.Tests;

public class Tests
{
    [Test]
    public async Task GetTextFromImageAsync_should_return_text_from_image()
    {
        // Arrange
        var client = new OpenAiService();
        string base64 = await File.ReadAllTextAsync("image.base64");

        // Act
        string text = await client.GetTextFromImage(base64);

        // Assert
        text.Should().NotBeEmpty();
    }
    
    [Test]
    public async Task SummarizeTextsAsync_should_return_summarized_text()
    {
        // Arrange
        var client = new OpenAiService();
        IEnumerable<string> texts = new List<string>
        {
            "This is a test text",
            "This is another test text"
        };

        // Act
        string summary = await client.SummarizeTexts(texts);

        // Assert
        summary.Should().NotBeEmpty();
    }
}