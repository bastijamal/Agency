using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class Portfolios
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
