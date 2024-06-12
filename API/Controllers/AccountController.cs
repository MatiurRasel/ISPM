using API.DTOs;
using API.Entities.Identity;
using API.Services;
using API.Utility.Enums;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await UserExistsOnNameAsync(registerDTO.UserName)) return BadRequest("User Name is exists.");
            if (await UserExistsOnEmail(registerDTO.Email)) return BadRequest("Email is exists.");
            if (await UserExistsOnPhoneNumber(registerDTO.MobileNumber)) return BadRequest("Phone Number is exists.");

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
            //var user = await _user.Users
            //.FirstOrDefaultAsync(x => x.UserName.ToUpper() == loginDTO.UserName.ToUpper());

            //if (user == null) return Unauthorized("Invalid User Name.");

            var user = await _user.Users
            .FirstOrDefaultAsync(x => x.Email.ToUpper() == loginDTO.Email.ToUpper());

            if (user == null) return Unauthorized("Invalid Email.");

            var result = await _user.CheckPasswordAsync(user,loginDTO.Password);

            if(!result) return Unauthorized("Invalid Password.");

            return new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
            
        }


        private async Task<bool> UserExistsOnNameAsync(string userName)
        {
            return await UserExistsAsync(UserCheckType.UserName, userName);
        }
        private async Task<bool> UserExistsOnEmail(string email)
        {
            return await UserExistsAsync(UserCheckType.Email, email);
        }

        private async Task<bool> UserExistsOnPhoneNumber(string phoneNumber)
        {
            return await UserExistsAsync(UserCheckType.PhoneNumber, phoneNumber);
        }

        private async Task<bool> UserExistsAsync(UserCheckType checkType, string value)
        {
            value = value.ToUpper();

            return checkType switch
            {
                UserCheckType.UserName => await _user.Users.AnyAsync(x => x.UserName.ToUpper() == value),
                UserCheckType.Email => await _user.Users.AnyAsync(x => x.Email.ToUpper() == value),
                UserCheckType.PhoneNumber => await _user.Users.AnyAsync(x => x.PhoneNumber.ToUpper() == value),
                _ => throw new ArgumentException("Invalid check type")
            };
        }
    }
}