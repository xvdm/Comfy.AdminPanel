using AdminPanel.Data;
using AdminPanel.Helpers;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Services
{
    [Authorize]
    public class DTOService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DTOService(IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = RolesNames.Manager)]
        public IEnumerable<UserDTO> GetDTOUsers()
        {
            var users = _mapper.From(_context.Users).ProjectToType<UserDTO>().ToList();
            foreach(var user in users)
            {
                var claim = _context.UserClaims.FirstOrDefault(x => x.UserId == user.Id);
                if(claim is not null)
                {
                    user.Position = claim.ClaimValue;
                }
            }
            return users;
        }
    }
}
