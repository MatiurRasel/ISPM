namespace API.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string DateOfBirth { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string LastActive { get; set; }
    public string PhoneNumber { get; set; }
    public List<UserRoleDTO> UserRoles { get; set; } = new List<UserRoleDTO>();

}
public class UserRoleDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }


}
public class UserOutputDTO
{
    public UserDTO User { get; set; }
    public string Token { get; set; }
}