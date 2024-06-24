using Moq;
using ImageAPI.Models;
using ImageAPI.Models.Repositories;

namespace ImageAPI.Tests
{
    [TestClass]
    public class ImageServiceTests
    {
        private Mock<IImageRepository> _mockRepository;
        private ImageService _imageService;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IImageRepository>();
            _imageService = new ImageService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnUrlForLastDigit_6_to_9()
        {
            // Arrange
            var imageId = "asdf7";

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            Assert.AreEqual(string.Format(ImageService.SEED_NUMBER_BASE_URL,imageId.Last()) , url);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnUrlFromImageRepository_ForLastDigit_1_to_5()
        {
            // Arrange
            var imageId = "asdf233";
            var expectedImage = new Image( 3, "https://example.com/image3.png" );
            _mockRepository.Setup(repo => repo.GetImageById(3)).ReturnsAsync(expectedImage);

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            Assert.AreEqual(expectedImage.Url, url);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnUrlFromImageRepository_ForLastDigit_1_to_5_WhenNull()
        {
            // Arrange
            var imageId = "2";
            _mockRepository.Setup(repo => repo.GetImageById(2)).ReturnsAsync((Image?)null);

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            Assert.AreEqual(ImageService.DEFAULT_URL, url);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnVowelUrl_IfImageIdContainsVowel()
        {
            // Arrange
            var imageId = "ThisHasVowels";

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            Assert.AreEqual(ImageService.CHARACTER_VOWEL_URL, url);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnRandomSeedUrl_IfImageIdContainsSpecialCharacter()
        {
            // Arrange
            var imageId = "Sp1c1l!Ch1r1ct1rs";

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            var urls = new string[5];
            for(var i = 0; i < urls.Length; i++)
            {
                urls[i] = string.Format(ImageService.SEED_NUMBER_BASE_URL, i + 1);
            }

            Assert.IsTrue(urls.Contains(url), "Url not found");
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnDefaultUrl_IfNoMatch()
        {
            // Arrange
            var imageId = "d0f0l00t0";

            // Act
            var url = await _imageService.GetImageById(imageId);

            // Assert
            Assert.IsNotNull(url);
            Assert.AreEqual(ImageService.DEFAULT_URL, url);
        }
    }
}