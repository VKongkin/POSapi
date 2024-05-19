using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstLoginController : ControllerBase
    {
        private readonly JwtTokenHandler _tokenHandler;
        private readonly DemoDbContext _context;
        public FirstLoginController(JwtTokenHandler tokenHandler, DemoDbContext request) { 
         _tokenHandler = tokenHandler;
            _context = request;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetTesting()
        {
            return Ok(new
            {
                status = "OK",
                message = "OK"
            });
        }

        [HttpPost("/api/first-login")]
        public async Task<ActionResult> doFirstLogin([FromBody] LoginRequest request)
        {
            try
            {
                var userList = await _context.Users.Where(x=>x.UserName == request.UserName && x.Password == request.Password).ToListAsync();
                if (userList.FirstOrDefault() == null)
                {
                    return Ok(new
                    {
                        status = "Error",
                        message = "Invalid username or password"
                    });
                }

                var token = _tokenHandler.GenrateToken(request, userList);

                return Ok(new
                {
                    status = "success",
                    message = "OK",
                    token = token
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong" + e.Message
                });
            }
        }

    }
}
