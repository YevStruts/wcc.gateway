﻿namespace wcc.gateway.api.Models.Jwt
{
    public class JwtConfig
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Key { get; set; }
    }
}
