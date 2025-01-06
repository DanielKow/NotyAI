namespace NotyAI.Photos;

internal interface IPhotoService
{
    Task<string> CaptureAsync();
}