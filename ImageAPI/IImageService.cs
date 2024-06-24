using ImageAPI.Models;

namespace ImageAPI
{
    public interface IImageService
    {
        Task<string> GetImageById(string imageId);
    }
}