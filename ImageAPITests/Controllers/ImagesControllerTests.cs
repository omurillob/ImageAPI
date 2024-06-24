using Microsoft.AspNetCore.Mvc;
using Moq;
using ImageAPI.DTOs;

namespace ImageAPI.Controllers.Tests
{
    [TestClass()]
    public class ImageControllerTests
    {
        private Mock<IImageService> _mockImageService;
        private ImagesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockImageService = new Mock<IImageService>();
            _controller = new ImagesController(_mockImageService.Object);
        }

        [TestMethod]
        public async Task Get_ShouldReturnBadRequest_ForNullOrEmptyId()
        {
            // Arrange
            string? id = null;

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Id cannot be null or empty.", badRequestResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOk_WithImageDTO_ForValidId()
        {
            // Arrange
            var imageId = "456";
            var expectedUrl = "https://example.com/image456.png";
            _mockImageService.Setup(service => service.GetImageById(imageId.ToString()))
                .ReturnsAsync(expectedUrl);

            // Act
            var result = await _controller.Get(imageId.ToString());

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(ImageDTO));
            var imageDto = okResult.Value as ImageDTO;
            Assert.IsNotNull(imageDto);
            Assert.AreEqual(imageId, imageDto.Id);
            Assert.AreEqual(expectedUrl, imageDto.Url);
        }
    }
}