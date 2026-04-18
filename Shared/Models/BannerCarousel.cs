using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.BannerCarousel.Models
{
    [Table("GIBSBannerCarousel")]
    public class BannerCarousel : ModelBase
    {
        [Key]
        public int BannerCarouselId { get; set; }
        public int ModuleId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string ImageUrl { get; set; }

        public int OrderBy { get; set; } = 1;
        public bool ShowCaption { get; set; } = true;
        public bool IsActive { get; set; } = true;

    }
}
