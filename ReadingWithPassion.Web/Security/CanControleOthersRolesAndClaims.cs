using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadingWithPassion.Web.Security
{
    public class CanControleOthersRolesAndClaims : AuthorizationHandler<ManageRolesAndClaimsRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageRolesAndClaimsRequirment requirement)
        {
            if (context==null)
                return Task.CompletedTask;

            var authFilterContext = context.Resource as AuthorizationFilterContext;
            if (authFilterContext==null)
                return Task.CompletedTask;

            var loggedInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            if (context.User.IsInRole("Admin") && context.User.HasClaim(claim=>claim.Type=="Edit Role" && claim.Value=="true")&&
                loggedInAdminId.ToLower() != adminIdBeingEdited.ToLower())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
