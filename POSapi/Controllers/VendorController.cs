using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public VendorController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetVendor()
        {
            try
            {
                var vendor = await _context.Vendors.ToListAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Vendor List",
                    vendor = vendor
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong! "+e.Message,
                });
            }
        }

        [HttpPost("/api/get_vendor_by_id")]
        public async Task<ActionResult> GetVendorById([FromBody] GetVendorById request)
        {
            var vendorList = await _context.Vendors.Where(v => v.VendorID == request.VendorID).FirstOrDefaultAsync();
            if (request == null)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Vendor not found"
                });
            }
            try
            {
                
                return Ok(new
                {
                    status = "success",
                    message = "Vendor List",
                    vendor = vendorList
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

        [HttpPost("/api/CUDVendor")]
        public async Task<ActionResult> CUDVendor([FromBody] VendorRequest request)
        {
            string status = "", message = "";
            if (request == null)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Vendor not found"
                });
            }

            if(request.CUD == "C")
            {
                try
                {
                    var vendorList = new Vendor
                    {
                        VendorID = request.VendorID,
                        VendorName = request.VendorName,
                        VendorDescription = request.VendorDescription,
                        VendorPhone = request.VendorPhone,
                        VendorEmail = request.VendorEmail,
                        VendorAddress = request.VendorAddress,
                        isActive = request.isActive
                    };
                    await _context.Vendors.AddAsync(vendorList);
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
                    var vendorList = new Vendor
                    {
                        VendorID = request.VendorID,
                        VendorName = request.VendorName,
                        VendorDescription = request.VendorDescription,
                        VendorPhone = request.VendorPhone,
                        VendorEmail = request.VendorEmail,
                        VendorAddress = request.VendorAddress,
                        isActive = request.isActive
                    };

                    _context.Vendors.Update(vendorList);
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
                    var vendorList = new Vendor
                    {
                        VendorID = request.VendorID,
                        VendorName = request.VendorName,
                        VendorDescription = request.VendorDescription,
                        VendorPhone = request.VendorPhone,
                        VendorEmail = request.VendorEmail,
                        VendorAddress = request.VendorAddress,
                        isActive = request.isActive
                    };

                    _context.Vendors.Remove(vendorList);
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
