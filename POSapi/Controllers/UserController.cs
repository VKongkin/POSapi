using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DemoDbContext _context;

        public UserController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                var userList = await _context.Users.ToListAsync();
                return Ok(userList);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong!" + e.Message.ToString()
                });
            }
        }
        [HttpPost("/api/get_user_by_id")]
        public async Task<ActionResult> GetUserById([FromBody] UserGetById request)
        {
            try
            {
                var users = (
                   from u in await _context.Users.ToListAsync()
                   join d in await _context.UserDesc.ToListAsync() on u.UserId equals d.UserId
                   where u.UserId == request.UserId
                   select new
                   {
                       UserDescId = d.Id,
                       UserId = u.UserId,
                       UserName = d.UserName,
                       Status = u.Status,
                       CreateBy = u.CreateBy
                   }).ToList();
                return Ok(new
                {
                    status = "Success",
                    message = "",
                    users = users
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong!" + e.Message.ToString()
                });
            }
        }


        [HttpPost("/api/signup_user")]
        public async Task<ActionResult> UserCUD([FromBody] UserRequest request)
        {
            string status = "", message = "";

            if (request == null)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Invalid Request "
                });
            }

            DateTime CreateDate = DateTime.Now;

            var strategy = _context.Database.CreateExecutionStrategy();
            _ = strategy.Execute(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    #region Create: C
                    if (request.CUD == "C")
                    {
                        try
                        {
                            var user = new User()
                            {
                                UserName = request.UserName,
                                Password = request.Password,
                                Status = request.Status,
                                RoleId = request.RoleId,
                                CreateBy = request.CreateBy,
                                CreateDate = CreateDate,
                                LastUpdateBy = request.CreateBy,
                                LastUpdateDate = CreateDate,
                            };

                            await _context.Users.AddAsync(user);
                            await _context.SaveChangesAsync();

                            var userDes = new UserDesc()
                            {
                                UserId = user.UserId,
                                UserName = request.UserName,

                            };

                            await _context.UserDesc.AddAsync(userDes);
                            await _context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            status = "Success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "Success";
                            message = "Something when wrong" + e.Message;
                            await transaction.RollbackAsync();
                        }

                    }
                    #endregion
                    #region Update: U
                    else if (request.CUD == "U")
                    {
                        try
                        {
                            var user = new User()
                            {
                                UserId = request.UserId,
                                UserName = request.UserName,
                                Password = request.Password,
                                Status = request.Status,
                                RoleId = request.RoleId,
                                CreateBy = request.CreateBy,
                                CreateDate = CreateDate,
                                LastUpdateBy = request.CreateBy,
                                LastUpdateDate = CreateDate,
                            };

                            _context.Users.Update(user);
                            await _context.SaveChangesAsync();

                            var userDes = new UserDesc()
                            {
                                Id = request.UserDescId,
                                UserId = user.UserId,
                                UserName = request.UserName,

                            };

                            _context.UserDesc.Update(userDes);
                            await _context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            status = "Success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "Success";
                            message = "Something when wrong" + e.Message;
                            await transaction.RollbackAsync();
                        }

                    }
                    #endregion
                    #region Delete: D
                    else
                    {
                        try
                        {
                            var user = new User()
                            {
                                UserId = request.UserId,
                                UserName = request.UserName,
                                Password = request.Password,
                                Status = request.Status,
                                RoleId = request.RoleId,
                                CreateBy = request.CreateBy,
                                CreateDate = CreateDate,
                                LastUpdateBy = request.CreateBy,
                                LastUpdateDate = CreateDate,
                            };

                            _context.Users.Remove(user);
                            await _context.SaveChangesAsync();

                            var userDes = new UserDesc()
                            {
                                Id = request.UserDescId,
                                UserId = user.UserId,
                                UserName = request.UserName,

                            };

                            _context.UserDesc.Remove(userDes);
                            await _context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            status = "Success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "Success";
                            message = "Something when wrong" + e.Message;
                            await transaction.RollbackAsync();
                        }

                    }
                    #endregion
                }
            });

            return Ok(new
            {
                status = status,
                message = message
            });
        }

        [HttpPost("/api/user-login")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            try
            {
                var userObj = await _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
                if(userObj != null)
                {
                    if(userObj.Status == "true")
                    {
                        return Ok(new
                        {
                            status = "Success",
                            message = "Welcome to POS System"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = "Error",
                            message = "Your account is not activate. Please contact to Administrator!"
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        status = "Error",
                        message = "Incorrect Email or Password!" 
                    });
                }
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something When Wrong!" + e.Message.ToString()
                });
            }
        }
    }
}
