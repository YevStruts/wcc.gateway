namespace wcc.gateway.kernel.Models
{
    public class PlayerModelOld
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? AvatarUrl { get; set; }

        public int? CountryId { get; set; }

        public bool IsActive { get; set; }
    }

    public class PlayerGameListModel : PlayerModelOld
    {
        public int Score { get; set; }
    }

    public class PlayerRatingModel : PlayerModelOld
    {
        public string? Comment { get; set; }
        public int Score { get; set; }
    }
}
