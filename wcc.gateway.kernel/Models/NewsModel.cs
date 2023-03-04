using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models
{
    public class NewsModel
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image_url { get; set; }

        public void FromDto(News dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Description = dto.Description;
            this.Image_url = dto.ImageUrl;
        }
    }
}
