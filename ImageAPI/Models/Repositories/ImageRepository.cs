namespace ImageAPI.Models.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IImageContext _context;
        public ImageRepository(IImageContext context)
        {
            _context = context;
        }

        public async Task<Image?> GetImageById(int imageId)
        {
            return await _context.Images.FindAsync(imageId);
        }
    }
}
