namespace NotyAI.Photos;

internal class PhotoService : IPhotoService
{
    public async Task<string> CaptureAsync()
    {
        try
        {
            if (!MediaPicker.Default.IsCaptureSupported)
            {
                return string.Empty;
            }
            
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            await using Stream stream = await photo.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            string base64 = Convert.ToBase64String(memoryStream.ToArray());
            return base64;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }
}