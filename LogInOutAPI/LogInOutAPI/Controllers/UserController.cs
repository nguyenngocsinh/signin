using LogInOutAPI.DB;
using LogInOutAPI.Models;
using LogInOutAPI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using BC = BCrypt.Net.BCrypt;


namespace LogInOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _myDb;

        public UserController(MyDbContext myDbContext)
        {
            _myDb = myDbContext;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterViewModel userObject)
        {
            if (userObject == null)
            {
                return BadRequest();
            }
            var existingUsername = await _myDb.Users.AnyAsync(u => u.UserName == userObject.UserName);
            if (existingUsername)
            {
                return BadRequest("Username Already Exists");
            }
            else
            {
                if (!Regex.IsMatch(userObject.Password, @"((?=.*\d)(?=.*\W)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).{8,})") || !Regex.IsMatch(userObject.UserName, @"(.{3,20x})"))
                {
                    return BadRequest("Your username at least 8 long\nAnd your password must be at least 8 characters including a lowercase letter, an uppercase letter, a number, and a special character");
                }
                else if ( userObject.confirmPassword != userObject.Password )
                {
                    return BadRequest("Password is not match");
                }
                else
                {
                    int workFactor = 10;
                    userObject.Password = BC.HashPassword(userObject.Password, workFactor);
                    userObject.IsActive = 0;
                    var newUser = new Users
                    {
                        UserName = userObject.UserName,
                        Password = userObject.Password,
                        IsActive = userObject.IsActive
                    };
                    await _myDb.Users.AddAsync(newUser);
                    await _myDb.SaveChangesAsync();

                    return Ok(new
                    {
                        Message = "Registered!"
                    });
                }
            }
        }

        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel logInObject)
        {

            if (logInObject == null)
            {
                return BadRequest();
            }

            var user = await _myDb.Users.FirstOrDefaultAsync(x => x.UserName == logInObject.UserName);

            if (user == null || !BC.Verify(logInObject.Password, user.Password))
            {
                return NotFound(new { Message = "Invalid username or password!" });
            }
            else
            {
                user.IsActive = 1;
                await _myDb.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Login Success!"
                });
            }


        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout([FromBody] LogoutViewModel userName)
        {
            try
            {
                var user = _myDb.Users.FirstOrDefault(u => u.UserName == userName.UserName);

                if (user == null)
                {
                    return NotFound($"Người dùng với tên đăng nhập '{userName}' không tồn tại.");
                }

                user.IsActive = 0;
                _myDb.SaveChanges();

                return Ok(new
                {
                    msg = "logout",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi khi thực hiện đăng xuất: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Showcompany")]

        public IActionResult Showcompany([FromBody] string userName) 
        {
            var userCompany = _myDb.Users
                              .FirstOrDefault(u => u.UserName == userName);

            if (userCompany != null)
            {
                var companies = userCompany.UserCompanies.Select(uc => uc.Company).ToList();
                return Ok(companies);
            }
            else
            {
                return Ok(new List<string>());
            }
        }

        [HttpPost]
        [Route("ShowUsername")]

        public IActionResult ShowUsername([FromBody] string CompanyName)
        {
            var company = _myDb.UserCompany
                          .FirstOrDefault(u => u.Company == CompanyName);

           

            if (company != null)
            {
                var users = _myDb.Users
                     .Where(u => u.UserCompanies.Any(uc => uc.Company == CompanyName))
                     .Select(u => u.UserName)
                     .ToList();
                return Ok(users);
            }
            else
            {
                return Ok(new List<string>());
            }

        }

        [HttpPost]
        [Route("AddCompany")]

        public IActionResult AddCompany([FromBody] string CompanyName, string userName)
        {
            var user = _myDb.Users
                .Include(u => u.UserCompanies) 
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return BadRequest("Người dùng không tồn tại");
            }
            else
            {
                var userCompanyExists = user.UserCompanies.Any(uc => uc.Company == CompanyName);
                if (userCompanyExists)
                {
                    return BadRequest("Người dùng đã được liên kết với công ty này");
                }else
                {
                    var newUserCompany = new UserCompany { Company = CompanyName };
                    user.UserCompanies.Add(newUserCompany);
                    _myDb.SaveChanges();
                }


                

                return Ok("Công ty đã được thêm thành công cho người dùng");
            }
        }

        [HttpPost]
        [Route("DeleteCompany")]

        public IActionResult DelCompany([FromBody] string CompanyName, string userName)
        {
            var userCompany = _myDb.Users
                .Include(u => u.UserCompanies)
                .FirstOrDefault(u => u.UserName == userName);

            if (userCompany == null)
            {
                return BadRequest("Người dùng không tồn tại");
            }

            
            bool companyExistsInUserCompanies = userCompany.UserCompanies.Any(uc => uc.Company == CompanyName);

            if (companyExistsInUserCompanies)
            {
                var companyRemove = userCompany.UserCompanies.FirstOrDefault(uc => uc.Company == CompanyName);
                userCompany.UserCompanies.Remove(companyRemove);
                _myDb.SaveChanges();

                return Ok("Xoá thành công");
            }
            else
            {
                return Ok(new List<string>());
            }
        }

        [HttpPost]
        [Route("ShowUser")]
        public IActionResult GetUsers()
        {
            var users = _myDb.Users.Select(u => u.UserName).ToList();
            return Ok(users);
        }

    }
}
