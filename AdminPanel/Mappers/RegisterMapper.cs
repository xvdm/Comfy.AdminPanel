using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdminPanel.Mappers
{
    public class RegisterMapper : IRegister
    {
        public RegisterMapper()
        {
        }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, UserDTO>()
                .Map(udto => udto.Position, _ => String.Empty)
                .RequireDestinationMemberSource(true);
        }
    }
}
