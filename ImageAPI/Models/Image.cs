namespace ImageAPI.Models
{
    public class Image(int id, string url)
    {
        public int Id { get; set; } = id;
        public string Url { get; set; } = url;
    }
}
