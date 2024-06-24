using Moq;

namespace ImageAPI.Models.Repositories.Tests
{
    [TestClass]
    public class ImageRepositoryTests
    {
        [TestMethod]
        public async Task GetImageById_ShouldReturnImage_WhenFound()
        {
            // Arrange
            var expectedImageId = 1;
            var expectedImageUrl = "test";
            var expectedImage = new Image(expectedImageId, expectedImageUrl);
            var mockContext = new Mock<IImageContext>();
            mockContext.Setup(context => context.Images.FindAsync(expectedImageId))
                .ReturnsAsync(expectedImage);

            var imageRepository = new ImageRepository(mockContext.Object);

            // Act
            var image = await imageRepository.GetImageById(expectedImageId);

            // Assert
            Assert.IsNotNull(image);
            Assert.AreEqual(expectedImageId, image.Id);
            Assert.AreEqual(expectedImageUrl, image.Url);
        }

        [TestMethod]
        public async Task GetImageById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var imageId = 1;
            var mockContext = new Mock<IImageContext>();
            mockContext.Setup(context => context.Images.FindAsync(imageId))
                .ReturnsAsync((Image?)null);

            var imageRepository = new ImageRepository(mockContext.Object);

            // Act
            var image = await imageRepository.GetImageById(imageId);

            // Assert
            Assert.IsNull(image);
        }
    }

}