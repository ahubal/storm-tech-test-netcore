using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Todo.Models.TodoLists;

namespace Todo.Services
{
    public static class UserConvenience
    {
        public static string Id(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static async Task ResolveDisplayNamesAsync(TodoListDetailViewmodel model)
        {
            var names = new Dictionary<string, string>();
            foreach (var item in model.Items)
            {
                if (!names.ContainsKey(item.ResponsibleParty.Email))
                {
                    var displayName = (await Gravatar.GetUserName(item.ResponsibleParty.Email)) ?? item.ResponsibleParty.UserName;
                    names.Add(item.ResponsibleParty.Email, displayName);
                }
                item.ResponsibleParty.DisplayName = names[item.ResponsibleParty.Email];
            }
        }
    }
}