using AdminPanel.Models;
using AdminPanel.Models.DTO;
using Mapster;

namespace AdminPanel.Mappers
{
    public class RegisterMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, UserDTO>().RequireDestinationMemberSource(true);
        }
    }
}
