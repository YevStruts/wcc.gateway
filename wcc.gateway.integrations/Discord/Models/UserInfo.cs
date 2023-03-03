namespace wcc.gateway.integrations.Discord.Models
{
    public class UserInfo
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? avatar { get; set; }
        public string? avatar_decoration { get; set; }
        public string? discriminator { get; set; }
        public string? email { get; set; }
        public bool public_flags { get; set; }
        public bool flags { get; set; }
        public string? banner { get; set; }
        public string? banner_color { get; set; }
        public string? accent_color { get; set; }
        public string? locale { get; set; }
        public string? mfa_enabled { get; set; }
    }
}
