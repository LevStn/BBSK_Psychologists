using BBSK_Psycho.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BBSK_Psycho
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(Role roles)
        {


            Roles = string.Concat(roles.ToString(), ",");
            Roles += nameof(Role.Manager);
           
        }
    }

}
