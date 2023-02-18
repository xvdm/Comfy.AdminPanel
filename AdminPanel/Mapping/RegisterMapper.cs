using AdminPanel.Models.DTO;
using Mapster;
using AdminPanel.Models.Identity;

namespace AdminPanel.Mapping
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
