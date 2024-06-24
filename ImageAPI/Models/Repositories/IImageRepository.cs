namespace ImageAPI.Models.Repositories
{
    public interface IImageRepository
    {
        Task<Image?> GetImageById(int imageId);
    }
}