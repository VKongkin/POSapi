using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkuController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public SkuController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetSKU()
        {
            try
            {
                var sku = await _context.Skus.ToListAsync();
                return Ok(new
                {
                    status ="success",
                    message = "SKU List",
                    sku = sku
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong! "+e.Message
                });
            }
        }

        [HttpPost("/api/get_sku_by_id")]
        public async Task<ActionResult> GetSKUById([FromBody] GetSKUById request)
        {
            var skuList = await _context.Skus.Where(s=>s.skuID==request.skuID).FirstOrDefaultAsync();
            try
            {
                return Ok(new
                {
                    status = "success",
                    message = "SKU ID",
                    sku = skuList
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong! "+e.Message
                });
            }
            
        }

        [HttpPost("/api/sku_CUD")]
        public async Task<ActionResult> CUDSku([FromBody] SKURequest request)
        {
            string status = "", message = "";

            if (request == null)
            {

                return Ok(new
                {
                    status = "Error",
                    message = "Invalid Request or CUD value is missing"
                });
            }

            if (request.CUD == "C")
            {
                try
                {
                    var sku = new SKU
                    {
                        skuID = request.skuID,
                        skuName = request.skuName,
                        skuDescription = request.skuDescription
                    };
                    await _context.Skus.AddAsync(sku);
                    await _context.SaveChangesAsync();

                    status = "success";
                    message = "Your transaction completed";
                }
                catch (Exception e)
                {
                    status = "Error";
                    message = "Something when wrong" + e.Message;
                }
            }else if (request.CUD == "U")
            {
                try
                {
                    var sku = new SKU
                    {
                        skuID = request.skuID,
                        skuName = request.skuName,
                        skuDescription = request.skuDescription
                    };
                    _context.Skus.Update(sku);
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
            else
            {
                try
                {
                    var sku = new SKU
                    {
                        skuID = request.skuID,
                        skuName = request.skuName,
                        skuDescription = request.skuDescription
                    };
                    _context.Skus.Remove(sku);
                    await _context.SaveChangesAsync();

                    status = "success";
                    message = "Your transaction completed";
                }
                catch (Exception e)
                {
                    status = "Error";
                    message = "Something when wrong" + e.Message;
                }
            };

            return Ok(new
            {
                status = status,
                message = message
            });
        }
    }
}
