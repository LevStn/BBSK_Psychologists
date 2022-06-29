using Microsoft.AspNetCore.Authorization;

namespace BBSK_Psycho
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }

}
