namespace ImageAPI.DTOs
{
    public class ImageDTO(string id, string url)
    {
        public string Id { get; set; } = id;
        public string Url { get; set; } = url;
    }
}
