using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public CategoryController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategory()
        {
            var category = await _context.Categories.ToListAsync();
            return Ok(new
            {
                status = "success",
                message = "Category List",
                category = category
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }



        [HttpPost("/api/get_category_by_id")]
        public async Task<ActionResult> GetCategoryById([FromBody] GetCategoryById request)
        {
            var category = await _context.Categories.Where(c => c.CategoryID == request.CategoryID).FirstOrDefaultAsync();
            if (request == null)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Invalid category or ID "
                });
            }
            try
            {
                
                return Ok(new
                {
                    status = "success",
                    message = "Category List",
                    category = category
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong! " + e.Message,
                });
            }

        }



        [HttpPost("/api/category_cud")]
        public async Task<ActionResult> CUDCategory([FromBody] CategoryRequest request)
        {
            string status = "", message = "";
            if (request == null)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Invalid category or ID "
                });
            }

            if(request.CUD == "C")
            {
                
                try
                {
                    var category = new Category
                    {
                        CategoryID = request.CategoryID,
                        CategoryName = request.CategoryName,
                        CategoryDescription = request.CategoryDescription,
                    };
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();

                    status = "success";
                    message = "Your transaction completed";

                }
                catch (Exception e)
                {
                    status = "Error";
                    message = "Something when wrong" + e.Message;
                }
            }else if(request.CUD == "U")
            {
                try
                {
                    var category = new Category
                    {
                        CategoryID = request.CategoryID,
                        CategoryName = request.CategoryName,
                        CategoryDescription = request.CategoryDescription,
                    };
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();

                    status = "success";
                    message = "Your transaction completed";

                }
                catch (Exception e)
                {
                    status = "Error";
                    message = "Something when wrong" + e.Message;
                }
            }else{
                try
                {
                    var category = new Category
                    {
                        CategoryID = request.CategoryID,
                        CategoryName = request.CategoryName,
                        CategoryDescription = request.CategoryDescription,
                    };
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();

                    status = "success";
                    message = "Your transaction completed";

                }
                catch (Exception e)
                {
                    status = "Error";
                    message = "Something when wrong" + e.Message;
                }
            }

            return Ok(new
            {
                status = status,
                message = message
            });

        }
    }
}
