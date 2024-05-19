using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public RoleController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/get-role")]
        [Authorize]
        public async Task<ActionResult> GetRole()
        {
            try
            {
                var role = await _context.Roles.ToListAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Role List",
                    role = role
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Role List"+e.Message
                });
            }
        }

        [HttpPost("/api/add-role")]
        [Authorize]
        public async Task<ActionResult> AddRole([FromBody] RoleRequest request)
        {
            try
            {
                var createDate = DateTime.Now;

                var role = new Role()
                {
                    RoleName = request.RoleName,
                    RoleDescription = request.RoleDescription,
                    IsActive = request.IsActive,
                    CreateBy = request.CreateBy,
                    CreateDate = createDate,
                    LastUpdateBy = request.CreateBy,
                    LastUpdateDate = createDate,
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "success",
                    message = "Your transaction completed",

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
