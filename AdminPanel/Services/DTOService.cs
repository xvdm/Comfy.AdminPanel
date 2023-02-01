using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models.DTO;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace AdminPanel.Services
{
    [Authorize]
    public class DTOService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public DTOService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [Authorize(Roles = RolesNames.Manager)]
        public IEnumerable<UserDTO> GetDTOUsers()
        {
            return _mapper.From(_context.Users).ProjectToType<UserDTO>();
        }
    }
}
