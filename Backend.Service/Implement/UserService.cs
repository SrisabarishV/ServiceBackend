using Backend.Common;
using Backend.Common.DTO;
using Backend.Repo.Interface;
using Backend.Service.Interface;
using Backend.SQLContext.Models;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Implement
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<UserResponseDto>> CreateAsync(CreateUserDto dto)
        {
            try
            {
                if (await _repo.EmailExistsAsync(dto.Emailid))
                    return ApiResponse<UserResponseDto>.FailResponse("Email already exists");

                var user = new User
                {
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Phonenumber = dto.Phonenumber,
                    Emailid = dto.Emailid,
                    Passwordhash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // hash recommended
                    Roleid = dto.Roleid,
                    Isactive = true,
                    Createdat = DateTime.UtcNow
                    
                    
                };

                var result = await _repo.CreateAsync(user);

                var response = new UserResponseDto
                {
                    Userid = result.Userid,
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    Emailid = result.Emailid,
                    Phonenumber = result.Phonenumber,
                    Roleid = result.Roleid,
                    Rolename = result.Role?.Rolename
                };

                return ApiResponse<UserResponseDto>.SuccessResponse(response, "User created successfully");
            }
            catch (Exception ex)
            {
               
                return ApiResponse<UserResponseDto>.FailResponse(ex.Message);
            }
        }


        public async Task<ApiResponse<UserResponseDto>> GetByIdAsync(long id)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);

                if (user == null)
                    return ApiResponse<UserResponseDto>.FailResponse("User not found");

                var dto = new UserResponseDto
                {
                    Userid = user.Userid,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Emailid = user.Emailid,
                    Phonenumber = user.Phonenumber,
                    Roleid = user.Roleid,
                    Rolename =user.Role?.Rolename
                    
                };

                return ApiResponse<UserResponseDto>.SuccessResponse(dto);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserResponseDto>.FailResponse(ex.Message);
            }
        }


        public async Task<ApiResponse<List<UserResponseDto>>> GetAllAsync()
        {
            try
            {
                var users = await _repo.GetAllAsync();

                var list = users.Select(x => new UserResponseDto
                {
                    Userid = x.Userid,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Emailid = x.Emailid,
                    Phonenumber = x.Phonenumber,
                    Roleid = x.Roleid,
                    Rolename = x.Role?.Rolename
                }).ToList();

                return ApiResponse<List<UserResponseDto>>.SuccessResponse(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<UserResponseDto>>.FailResponse(ex.Message);
            }
        }


        public async Task<ApiResponse<string>> UpdateAsync(long userids,UpdateUserDto dto, long modifiedBy)
        {
            try
            {
                var user = await _repo.GetByIdAsync(userids);

                if (user == null)
                    return ApiResponse<string>.FailResponse("User not found");

                user.Firstname = dto.Firstname;
                user.Lastname = dto.Lastname;
                user.Phonenumber = dto.Phonenumber;
                user.Roleid = dto.Roleid;
                user.Modifiedat = DateTime.UtcNow;
                user.Modifiedby = modifiedBy;

                await _repo.UpdateAsync(user);

                return ApiResponse<string>.SuccessResponse("Updated", "User updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse(ex.Message);
            }
        }


        public async Task<ApiResponse<string>> DeleteAsync(long id, long modifiedBy)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);

                if (user == null)
                    return ApiResponse<string>.FailResponse("User not found");


                user.Isactive = false;
                user.Modifiedby = modifiedBy;
                user.Modifiedat = DateTime.UtcNow;

                await _repo.SoftDeleteAsync(user);

                return ApiResponse<string>.SuccessResponse("Deleted", "User deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse(ex.Message);
            }
        }
    }


}
