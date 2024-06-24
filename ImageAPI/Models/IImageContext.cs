using Microsoft.EntityFrameworkCore;

namespace ImageAPI.Models
{
    public interface IImageContext
    {
        DbSet<Image> Images { get; }
    }
}
