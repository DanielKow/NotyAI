namespace NotyAI.Notes;

internal class Note
{
    private readonly List<string> _photos = ["A", "B"];
    private readonly List<string> _texts =
    [
        "testtestetgsdsafads",
        "tegafsfgfdgdfsgdfsgdfgdfsgdfs",
        "dsfaisduf6kzhmp2lynozmp613kmvnsy2rqm6kzvnpkm2oq"
    ];

    public IEnumerable<string> Photos => _photos;
    public IEnumerable<string> Texts => _texts;
    public string Summary { get; private set; } = string.Empty;
    public string TextSpeechPath { get; private set; } = string.Empty;
    public string SummarySpeechPath { get; private set; } = string.Empty;

    public void AddPhoto(string photo)
    {
        _photos.Add(photo);
    }

    public void AddText(string text)
    {
        _texts.Add(text);
    }

    public void Summarized(string summary)
    {
        Summary = summary;
    }
    
    public void Clear()
    {
        _photos.Clear();
        _texts.Clear();
        Summary = string.Empty;
    }
}