﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.integrations.Discord.Helpers
{
    public static class DiscordHelper
    {
        public static string GetAvatarUrl(string userId, string avatar)
        {
            if (string.IsNullOrEmpty(avatar))
                return string.Empty;
            return $"https://cdn.discordapp.com/avatars/{userId}/{avatar}.png";
        }
    }
}
