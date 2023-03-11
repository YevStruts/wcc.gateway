using System.Security.Claims;

namespace wcc.gateway.api.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public const string Anonymous = "Anonymous";

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            //string userid = User.GetUserId FindFirst("Id")?.Value;
            //string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //string useremail = User.FindFirst(ClaimTypes.Email)?.Value;

            return principal.FindFirstValue("Id") ?? Anonymous;
        }
    }
}
