using ImageAPI.Models.Repositories;

namespace ImageAPI
{
    public class ImageService : IImageService
    {
        public const string DEFAULT_URL = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        public const string CHARACTER_VOWEL_URL = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";
        public const string SEED_NUMBER_BASE_URL = "https://api.dicebear.com/8.x/pixel-art/png?seed={0}&size=150";

        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> GetImageById(string imageId)
        {
            if (int.TryParse(imageId.Last().ToString(), out int lastDigit))
            {
                if (lastDigit >= 6 && lastDigit <= 9)
                    return string.Format(SEED_NUMBER_BASE_URL, lastDigit);

                if (lastDigit >= 1 && lastDigit <= 5)
                {
                    var image = await _imageRepository.GetImageById(lastDigit);
                    if (image is not null)
                        return image.Url;
                }
            }

            if (imageId.IndexOfAny("aeiou".ToCharArray()) >= 0)
            {
                return CHARACTER_VOWEL_URL;
            }

            if (!imageId.All(Char.IsLetterOrDigit)) // Check for non-alphanumeric characters
            {
                var randomNumber = new Random().Next(1, 6);
                return string.Format(SEED_NUMBER_BASE_URL, randomNumber);
            }
                        
            return DEFAULT_URL;
        }
    }
}
