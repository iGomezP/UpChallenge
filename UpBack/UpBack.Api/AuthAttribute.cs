using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UpBack.Api
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _requiredPermissions;

        public AuthAttribute(string requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var hasPermission = false;

                var servicePermissionsList = _requiredPermissions.Replace(" ", "").Split(',').ToList();

                var userPermissionsList = context.HttpContext.User.Claims
                    .Where(c => c.Type == "Permissions")
                    .Select(c => c.Value)
                    .ToList();

                foreach (var servicePermission in servicePermissionsList)
                {
                    hasPermission = hasPermission = userPermissionsList
                        .Any(up =>
                            up.Substring(0, up.IndexOf('.')) == servicePermission.Substring(0, servicePermission.IndexOf('.')) &&
                            up.Substring(up.IndexOf('.') + 1).Contains(servicePermission.Substring(servicePermission.IndexOf('.') + 1)));

                    if (hasPermission) break;
                }

                if (!hasPermission)
                    context.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
