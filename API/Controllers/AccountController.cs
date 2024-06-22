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

        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<UserOutputDTO>> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for existing user, email, phone number, etc.
            if (await UserExistsOnNameAsync(registerDTO.UserName))
            {
                ModelState.AddModelError(nameof(registerDTO.UserName), "User Name already exists.");
                return BadRequest(ModelState);
            }

            if (await UserExistsOnEmail(registerDTO.Email))
            {
                ModelState.AddModelError(nameof(registerDTO.Email), "Email already exists.");
                return BadRequest(ModelState);
            }

            if (await UserExistsOnPhoneNumber(registerDTO.MobileNumber))
            {
                ModelState.AddModelError(nameof(registerDTO.MobileNumber), "Phone Number already exists.");
                return BadRequest(ModelState);
            }

            // Continue with user creation logic
            var user = _mapper.Map<User>(registerDTO);
            user.UserName = registerDTO.UserName;

            var result = await _user.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }

            var roleResult = await _user.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }
            // Map the User entity to UserDTO
            var userDto = _mapper.Map<UserDTO>(user);

            // Return the UserOutputDTO with the mapped UserDTO and generated token
            return new UserOutputDTO
            {
                User = userDto,
                Token = await _tokenService.CreateToken(user)
            };
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserOutputDTO>> Login(LoginDTO loginDTO)
        {
            // Check if loginDTO or loginDTO.Email is null
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            // Attempt to find the user by email (case-insensitive), including UserRoles
            var user = await _user.Users
                //.Include(p=>p.Photos)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Email.ToUpper() == loginDTO.Email.ToUpper());

            // Check if user was found
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid Email." });
            }

            // Check if the password is correct
            var result = await _user.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
            {
                return Unauthorized(new { message = "Invalid Password." });
            }

            // Map the User entity to UserDTO
            var userDto = _mapper.Map<UserDTO>(user);

            // Return the UserOutputDTO with the mapped UserDTO and generated token
            return new UserOutputDTO
            {
                User = userDto,
                Token = await _tokenService.CreateToken(user)
            };
        }





        // Method to validate and adjust the mobile number
        private (bool isValid, string adjustedMobileNumber) ValidateMobileNumber(string mobileNumber)
        {
            // Remove any non-digit characters
            mobileNumber = new string(mobileNumber.Where(char.IsDigit).ToArray());

            // Check if the number is 10 digits long and doesn't start with "880"
            if (mobileNumber.Length == 10 && !mobileNumber.StartsWith("880"))
            {
                return (true, "880" + mobileNumber);
            }

            // If the number starts with "880", ensure the remaining digits are exactly 10 digits
            if (mobileNumber.StartsWith("880") && mobileNumber.Length == 13)
            {
                return (true, mobileNumber);
            }

            // Invalid mobile number
            return (false, mobileNumber);
        }

        // Method to validate and adjust the mobile number
        //private bool ValidateMobileNumber(ref string mobileNumber)
        //{
        //    // Remove any non-digit characters
        //    mobileNumber = new string(mobileNumber.Where(char.IsDigit).ToArray());

        //    // Check if the number is 10 digits long and doesn't start with "880"
        //    if (mobileNumber.Length == 10 && !mobileNumber.StartsWith("880"))
        //    {
        //        mobileNumber = "880" + mobileNumber;
        //        return true;
        //    }

        //    // If the number starts with "880", ensure the remaining digits are exactly 10 digits
        //    if (mobileNumber.StartsWith("880") && mobileNumber.Length == 13)
        //    {
        //        return true;
        //    }

        //    // Invalid mobile number
        //    return false;
        //}

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