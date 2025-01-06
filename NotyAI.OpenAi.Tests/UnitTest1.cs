using FluentAssertions;
using NotyAI.OpenAI;

namespace NotyAI.OpenAi.Tests;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        // Arrange
        var client = new OpenAiService();
        string base64 = await File.ReadAllTextAsync("image.base64");

        // Act
        string text = await client.GetTextFromImageAsync(base64);

        // Assert
        text.Should().NotBeEmpty();
    }
}