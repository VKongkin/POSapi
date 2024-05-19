using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Migrations;
using POSapi.Model.Data;
using POSapi.Model.Request;
using static POSapi.Model.Request.PORequest;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POController : ControllerBase
    {
        private readonly DemoDbContext _context;
        private readonly IMapper _mapper;
        public POController(DemoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetPO()
        {
            try
            {
                var po = await _context.PurchaseOrders.ToListAsync();
                return Ok(new
                {
                    status = "success",
                    message = "OK",
                    po = po
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Role List" + e.Message
                });
            }
        }

        [HttpGet("/api/get-po")]
        public async Task<IActionResult> Get()
        {
            var poDetail = await _context.PurchaseOrders.Include(_ => _.Details).ToListAsync();
            return Ok(new
            {
                status="success",
                message = "OK",
                poDetail = poDetail
            });
        }

        [HttpGet("/api/get-po-by-id")]
        public async Task<IActionResult> Get(int id)
        {
            var customerbyId = await _context.PurchaseOrders.Include(_ => _.Details).Where(_ => _.PoId == id).FirstOrDefaultAsync();
            return Ok(customerbyId);
        }



        [HttpPost("/api/add-new-po")]
        public async Task<IActionResult> Post([FromBody]PORequest poRequest)
        {
            var newPO = _mapper.Map<PurchaseOrder>(poRequest);
            _context.PurchaseOrders.Add(newPO);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "Success",
                message = "Successfully created PO",

            });
        }
        [HttpPost("/api/update-product")]
        public async Task<ActionResult> UpdateProduct([FromBody] RequestProductUpdate request)
        {
            try
            {
                var proExist = _context.Products.ToList().Where(x => x.ProductID == request.ProductID).FirstOrDefault();
                //var product = new Product();
                //var pro = _context.Products.ToList();
                _context.ChangeTracker.Clear();
                var updatePro = new Product()
                {
                    ProductID = request.ProductID,
                    Qty = proExist.Qty + request.Qty,
                    UnitCost = request.UnitCost,
                    CategoryID = proExist.CategoryID,
                    Description = proExist.Description,
                    ImagePath = proExist.ImagePath,
                    isActive = proExist.isActive,
                    ProductName = proExist.ProductName,
                    skuID = proExist.skuID,
                    UnitPrice = proExist.UnitPrice,
                    VendorID = proExist.VendorID, 
                };
                //int qtyAdd = proExist.Qty + updatePro.Qty;
                //pro.Add(product);
                _context.Products.Update(updatePro);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Product update successfully"
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

        [HttpPost("/api/update-products-new")]
        public async Task<ActionResult> UpdateProductsNew([FromBody] List<RequestProductUpdate> requests)
        {
            try
            {
                foreach (var request in requests)
                {
                    var proExist = _context.Products.FirstOrDefault(x => x.ProductID == request.ProductID);
                    if (proExist != null)
                    {
                        _context.ChangeTracker.Clear();
                        //proExist.Qty += request.Qty;
                        //proExist.UnitCost = request.UnitCost;
                        var updatePro = new Product()
                        {
                            ProductID = request.ProductID,
                            Qty = proExist.Qty + request.Qty,
                            UnitCost = request.UnitCost,
                            CategoryID = proExist.CategoryID,
                            Description = proExist.Description,
                            ImagePath = proExist.ImagePath,
                            isActive = proExist.isActive,
                            ProductName = proExist.ProductName,
                            skuID = proExist.skuID,
                            UnitPrice = proExist.UnitPrice,
                            VendorID = proExist.VendorID,
                        };
                        _context.Products.Update(updatePro);
                        await _context.SaveChangesAsync();
                    }
                }
                
                //await _context.SaveChangesAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Products updated successfully"
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something went wrong! " + e.Message,
                });
            }
        }


        [HttpPut("/api/update-po")]
        public async Task<IActionResult> Put(PORequest pORequest)
        {
            //var PODetailList = await _context.PODetails.ToListAsync();
            

            //    var delPO = new PODetail()
            //    {
            //        PoId = pORequest.PoId,
            //    };
            //    _context.PODetails.Remove(delPO);
            //await _context.SaveChangesAsync();
            

            var updatePO = _mapper.Map<PurchaseOrder>(pORequest);
            _context.PurchaseOrders.Update(updatePO);
            await _context.SaveChangesAsync();

            return Ok(updatePO);
        }

        [HttpPost("/api/delete-podetail")]
        public async Task<ActionResult> DeletePODetail([FromBody] PODetailRequest request)
        {
            //var existPODetail = await _context.PODetails.Where(x=>x.PoDetailId == request.PoDetailId).FirstOrDefaultAsync();
            _context.ChangeTracker.Clear();
            var PODetailList = new PODetail()
            {
                PoDetailId = request.PoDetailId,
                Amount = request.Amount,
                PoId = request.PoId,
                Price = request.Price,
                ProductId = request.ProductId,
                Qty = request.Qty,
            };
            _context.PODetails.Remove(PODetailList);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success",
                message = "Deleted successfull"
            });
        }

        [HttpGet("/api/get-po-detail")]
        public async Task<ActionResult> GetPODetail()
        {
            try
            {
                // Retrieve all Purchase Orders
                var poData = await _context.PurchaseOrders.ToListAsync();

                if (poData == null || !poData.Any())
                {
                    return NotFound(new { status = "Error", message = "Purchase Orders not found" });
                }

                // Retrieve all PODetails
                var poDetailData = await _context.PODetails.ToListAsync();

                // Map the data to a response model
                var response = poData.Select(po => new
                {
                    PoId = po.PoId,
                    PoDate = po.PoDate,
                    VendorId = po.VendorId,
                    UserId = po.UserId,
                    Reference = po.Reference,
                    PoAmount = po.PoAmount,
                    Details = poDetailData.Where(detail => detail.PoId == po.PoId)
                        .Select(detail => new
                        {
                            PoDetailId = detail.PoDetailId,
                            ProductId = detail.ProductId,
                            Qty = detail.Qty,
                            Price = detail.Price,
                            Amount = detail.Amount
                        }).ToList()
                }).ToList();

                return Ok(new
                {
                    status = "success",
                    message = "OK",
                    poDetail = response
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "Error", message = "Something went wrong" + e.Message });
            }
        }





        //[HttpPost("/api/add-po")]
        //public async Task<ActionResult> AddPO([FromBody] PORequest request)
        //{
        //    string status = "", message = "";

        //    var product = await _context.Products.ToListAsync();
        //    //var strategy = _context.Database.CreateExecutionStrategy();
        //    //_ = strategy.Execute(async () =>
        //    //{
        //    //    using (var transaction = _context.Database.BeginTransaction())
        //    //    {
        //            try
        //            {
        //                var poDate = DateTime.Now;
        //                var poList = new PurchaseOrder
        //                {
        //                    PoId = request.PoId,
        //                    PoDate = request.PoDate,
        //                    VendorId = request.VendorId,
        //                    UserId = request.UserId,
        //                    Reference = request.Reference,
        //                    PoAmount = request.PoAmount,
        //                };

        //                await _context.PurchaseOrders.AddAsync(poList);
        //                await _context.SaveChangesAsync();
        //                var productList = _context.Products.ToList();
        //                var proQty = _context.Products.ToList();
        //                var poDetailList = new List<PODetail>();

        //        var poDetail = new List<PODetail>();
        //                foreach (var po in poDetail)
        //                {

        //                    var podetail = new PODetail
        //                    {
        //                        PoId = poList.PoId,
        //                        ProductId = request.PODetails.ProductId,
        //                        Qty = request.PODetails.Qty,
        //                        Price = request.PODetails.Price,
        //                        Amount = request.PODetails.Amount,

        //                    };

        //                    var pro = new Product();
        //                    var proAv = _context.Products.ToList().Where(p => p.ProductID == podetail.ProductId).FirstOrDefault();
        //                    int qtyAdd = proAv.Qty + po.Qty;
        //                    float proCost = proAv.UnitCost;
        //                    proAv.Qty = qtyAdd;
        //                    proAv.UnitCost = po.Price;

        //                    proQty.Add(pro);
        //                    poDetailList.Add(podetail);
        //                }

        //                await _context.PODetails.AddRangeAsync(poDetailList);
        //                await _context.SaveChangesAsync();



        //                //await transaction.CommitAsync();

        //                status = "Success";
        //                message = "Your transaction completed";

        //            }
        //            catch (Exception e)
        //            {
        //                status = "Error";
        //                message = "Something went wrong" + e.Message;
        //            }
        //    //    }
        //    //});

        //   return Ok(new { status = status, message = message });
        //}


        //[HttpPost("/api/cud-po")]
        //public async Task<ActionResult> CUDPO([FromBody] PORequest request)
        //{
        //    string status = "", message = "";

        //    if (request == null)
        //    {

        //        return Ok(new
        //        {
        //            status = "Error",
        //            message = "Invalid Request or CUD value is missing"
        //        });


        //    }

        //    var product = await _context.Products.ToListAsync();
        //    var strategy = _context.Database.CreateExecutionStrategy();
        //    _ = strategy.Execute(async () =>
        //    {
        //        using (var transaction = _context.Database.BeginTransaction())
        //        {
        //            if(request.CUD == "C")
        //            {
        //                try
        //                {
        //                    var poDate = DateTime.Now;
        //                    var poList = new PurchaseOrder
        //                    {
        //                        PoId = request.PoId,
        //                        PoDate = poDate,
        //                        VendorId = request.VendorId,
        //                        UserId = request.UserId,
        //                        Reference = request.Reference,
        //                        PoAmount = request.PoAmount,
        //                    };

        //                    await _context.PurchaseOrders.AddAsync(poList);
        //                    await _context.SaveChangesAsync();

        //                    var productList = new List<Product>();
        //                    var proQty = productList.ToList();
        //                    var poDetailList = new List<PODetail>();

        //                    var poDetail = new List<PODetail>();
        //                    foreach (var po in poDetail)
        //                    {

        //                        var podetail = new PODetail
        //                        {
        //                            PoId = poList.PoId,
        //                            ProductId = request.PODetails.ProductId,
        //                            Qty = request.PODetails.Qty,
        //                            Price = request.PODetails.Price,
        //                            Amount = request.PODetails.Amount,

        //                        };

        //                        var pro = new Product();
        //                        var proAv = _context.Products.ToList().Where(p => p.ProductID == podetail.ProductId).FirstOrDefault();
        //                        int qtyAdd = proAv.Qty + po.Qty;
        //                        float proCost = proAv.UnitCost;
        //                        proAv.Qty = qtyAdd;
        //                        proAv.UnitCost = po.Price;

        //                        proQty.Add(pro);
        //                        poDetailList.Add(podetail);
        //                    }

        //                    await _context.PODetails.AddRangeAsync(poDetailList);
        //                    await _context.SaveChangesAsync();



        //                    await transaction.CommitAsync();

        //                    status = "Success";
        //                    message = "Your transaction completed";

        //                }
        //                catch (Exception e)
        //                {
        //                    status = "Error";
        //                    message = "Something went wrong" + e.Message;
        //                }
        //            }else if (request.CUD == "U")
        //            {
        //                try
        //                {
        //                    var existingPO = await _context.PurchaseOrders.FindAsync(request.PoId);

        //                    if (existingPO == null)
        //                    {
        //                        // Handle case where the Purchase Order with the specified ID doesn't exist.
        //                        status = "Error";
        //                        message = "Purchase Order not found";

        //                    }

        //                    // Update properties of the existing Purchase Order
        //                    existingPO.PoDate = DateTime.Now;
        //                    existingPO.VendorId = request.VendorId;
        //                    existingPO.UserId = request.UserId;
        //                    existingPO.Reference = request.Reference;
        //                    existingPO.PoAmount = request.PoAmount;

        //                    _context.PurchaseOrders.Update(existingPO);
        //                    await _context.SaveChangesAsync();

        //                    // Similar logic for updating PODetails
        //                    var poDetail = new List<PODetail>();
        //                    foreach (var po in poDetail)
        //                    {
        //                        var existingPODetail = await _context.PODetails
        //                            .Where(pd => pd.PoId == request.PoId && pd.ProductId == po.ProductId)
        //                            .FirstOrDefaultAsync();

        //                        if (existingPODetail != null)
        //                        {
        //                            // Update properties of the existing PODetail
        //                            existingPODetail.Qty = po.Qty;
        //                            existingPODetail.Price = po.Price;
        //                            existingPODetail.Amount = po.Amount;

        //                            _context.PODetails.Update(existingPODetail);
        //                        }
        //                        else
        //                        {
        //                            // Handle case where the PODetail with the specified IDs doesn't exist.
        //                            // You may choose to add it if required.
        //                            // status = "Error";
        //                            // message = "PODetail not found";
        //                            // return Ok(new { status = status, message = message });
        //                        }
        //                    }
        //                    await transaction.CommitAsync();

        //                    status = "Success";
        //                    message = "Your transaction completed";

        //                }
        //                catch (Exception e)
        //                {
        //                    status = "Error";
        //                    message = "Something went wrong" + e.Message;
        //                }
        //            }
        //            else if(request.CUD == "D")
        //            {
        //                try
        //                {
        //                    var existingPO = await _context.PurchaseOrders.FindAsync(request.PoId);

        //                    if (existingPO == null)
        //                    {
        //                        // Handle case where the Purchase Order with the specified ID doesn't exist.
        //                        status = "Error";
        //                        message = "Purchase Order not found";

        //                    }

        //                    // Update properties of the existing Purchase Order
        //                    existingPO.PoDate = DateTime.Now;
        //                    existingPO.VendorId = request.VendorId;
        //                    existingPO.UserId = request.UserId;
        //                    existingPO.Reference = request.Reference;
        //                    existingPO.PoAmount = request.PoAmount;

        //                    _context.PurchaseOrders.Remove(existingPO);
        //                    await _context.SaveChangesAsync();

        //                    // Similar logic for updating PODetails
        //                    var poDetail = new List<PODetail>();
        //                    foreach (var po in poDetail)
        //                    {
        //                        var existingPODetail = await _context.PODetails
        //                            .Where(pd => pd.PoId == request.PoId && pd.ProductId == po.ProductId)
        //                            .FirstOrDefaultAsync();

        //                        if (existingPODetail != null)
        //                        {
        //                            // Update properties of the existing PODetail
        //                            existingPODetail.Qty = po.Qty;
        //                            existingPODetail.Price = po.Price;
        //                            existingPODetail.Amount = po.Amount;

        //                            _context.PODetails.Remove(existingPODetail);
        //                        }
        //                        else
        //                        {
        //                            // Handle case where the PODetail with the specified IDs doesn't exist.
        //                            // You may choose to add it if required.
        //                            // status = "Error";
        //                            // message = "PODetail not found";
        //                            // return Ok(new { status = status, message = message });
        //                        }
        //                    }
        //                        await transaction.CommitAsync();

        //                    status = "Success";
        //                    message = "Your transaction completed";

        //                }
        //                catch (Exception e)
        //                {
        //                    status = "Error";
        //                    message = "Something went wrong" + e.Message;
        //                }
        //            }

        //        }
        //    });

        //    return Ok(new { status = status, message = message });
        //}

    }
}
