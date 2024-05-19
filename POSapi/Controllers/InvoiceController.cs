using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSapi.Model.Data;
using POSapi.Model.Request;
using POSapi.Model.Service;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DemoDbContext _context;
        private readonly IMapper _mapper;
        public InvoiceController(DemoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetINV()
        {
            try
            {
                var invoice = await _context.Invoices.ToListAsync();
                return Ok(new
                {
                    status = "success",
                    message = "OK",
                    invoice = invoice
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Something when wrong! " + e.Message
                });
            }
        }

        [HttpGet("/api/get-invoice")]
        public async Task<IActionResult> GetInvoice()
        {
            var invoices = await _context.Invoices.Include(_ => _.InvoiceItems).ToListAsync();
            return Ok(new
            {
                status = "success",
                message = "OK",
                invoices = invoices
            });
        }

        [HttpGet("/api/get-invoiec-by-id")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoieId = await _context.Invoices.Include(_ => _.InvoiceItems).Where(_ => _.InvoiceId == id).FirstOrDefaultAsync();
            return Ok(invoieId);
        }



        [HttpPost("/api/add-new-invoice")]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceRequest request)
        {
            var newInvoice = _mapper.Map<Invoice>(request);
            _context.Invoices.Add(newInvoice);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "Success",
                message = "Successfully created PO",

            });
        }

        [HttpPost("/api/update-product-invoice")]
        public async Task<ActionResult> UpdateProduct([FromBody] List<RequestProductUpdate>requests)
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
                            Qty = proExist.Qty - request.Qty,
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
                    message = "Something when wrong! " + e.Message,
                });
            }
        }

        [HttpGet("/api/lastinvoiceID")]
        public async Task<ActionResult> GetInvoiceID()
        {
            try
            {
                var lastID = _context.Invoices.OrderByDescending(i => i.InvoiceId).FirstOrDefault();
                int id = 0;
                if (lastID != null)
                {
                    id = lastID.InvoiceId;
                }
         
                

                var invoiceID = IDGeneratorService.IDGenerator("INV-", id, ("000000"));

                var RequestNewInvoiceId = new RequestNewInvoiceId
                {
                    InvoiceId = invoiceID,
                };

                return Ok(new 
                {
                    status = "success",
                    message = "Invoice ID",
                    InvoiceId = RequestNewInvoiceId,
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

        //[HttpPost("/api/create-inv")]
        //public async Task<ActionResult> CreateInvoice([FromBody] InvoiceRequest request)
        //{
        //    string status = "", message = "";

        //    var product = await _context.Invoices.ToListAsync();
        //    var strategy = _context.Database.CreateExecutionStrategy();

        //    _ = strategy.Execute(async () =>
        //    {
        //        using (var transaction = _context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var invDate = DateTime.Now;
        //                var invList = new Invoice
        //                {
        //                    InvoiceId = request.InvoiceId,
        //                    InvoiceNumber = request.InvoiceNumber,
        //                    InvoiceDate = invDate,
        //                    CustomerId = request.CustomerId,
        //                    InvoiceAmount = request.InvoiceAmount,
        //                    InvoiceDiscount = request.InvoiceDiscount,
        //                    InvoiceDeposit = request.InvoiceDeposit,
        //                    InvoiceBalance = request.InvoiceBalance,
        //                    CreateBy = request.CreateBy,
        //                    CreateDate = invDate,
        //                    LastUpdateBy = request.CreateBy,
        //                    LastUpdateDate = invDate,
        //                    InvoiceItems = request.InvoiceItems,

        //                };

        //                await _context.Invoices.AddAsync(invList);
        //                await _context.SaveChangesAsync();

        //                var proQty = _context.Products.ToList();
        //                var invItemList = new List<InvoiceItem>();

        //                foreach (var inv in request.InvoiceItems)
        //                {

        //                    var invItem = new InvoiceItem
        //                    {
        //                        InvoiceId = invList.InvoiceId,
        //                        ProductId = inv.ProductId,
        //                        Qty = inv.Qty,
        //                        Price = inv.Price,
        //                        Amount = inv.Amount

        //                    };

        //                    var pro = new Product();
        //                    var proAv = _context.Products.ToList().Where(p => p.ProductID == invItem.ProductId).FirstOrDefault();
        //                    int qtyEnd= proAv.Qty - inv.Qty;
        //                    proAv.Qty = qtyEnd;

        //                    proQty.Add(proAv);
        //                    invItemList.Add(invItem);
        //                }

        //                await _context.InvoicesItem.AddRangeAsync(invItemList);
        //                await _context.SaveChangesAsync();



        //                await transaction.CommitAsync();

        //                status = "Success";
        //                message = "Your transaction completed";

        //            }
        //            catch (Exception e)
        //            {
        //                status = "Error";
        //                message = "Something went wrong" + e.Message;
        //            }
        //        }
        //    });

        //    return Ok(new { 
        //        status, 
        //        message 
        //    });
        //}
    }
}
