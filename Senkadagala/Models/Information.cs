using Senkadagala.Models;

namespace Senkadagala.Models
{
    public class Information : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
