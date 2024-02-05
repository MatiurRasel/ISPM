using API.DTOs;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _user;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<User> user,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _user = user;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]//POST: api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(await UserExists(registerDTO.UserName)) return BadRequest("User Name is exists.");
            
            var user = _mapper.Map<User>(registerDTO);

            user.UserName = registerDTO.UserName;

            var result = await _user.CreateAsync(user,registerDTO.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _user.AddToRoleAsync(user,"User");
            if(!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _user.Users
            .FirstOrDefaultAsync(x => x.UserName.ToUpper() == loginDTO.UserName.ToUpper());

            if (user == null) return Unauthorized("Invalid User Name.");

            var result = await _user.CheckPasswordAsync(user,loginDTO.Password);

            if(!result) return Unauthorized("Invalid Password.");

            return new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
            
        }


        private async Task<bool> UserExists(string userName)
        {
            return await _user.Users.AnyAsync(x => x.UserName.ToUpper() == userName.ToUpper());
        }
    }
}