using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using POSapi.Model.Data;
using POSapi.Model.Request;
using POSapi.Model.Response;
using POSapi.Repository.Abstract;

namespace POSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DemoDbContext _context;
        private IFileService _fileService;
        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _environment;
        public ProductController(IFileService fs, IProductRepository productRepo, IWebHostEnvironment environment, DemoDbContext context)
        {
            this._fileService = fs;
            this._productRepo = productRepo;
            this._environment = environment;
            _context = context;
        }

        [NonAction]
        private string GetFilePath()
        {
            return this._environment.WebRootPath + "\\Uploads\\Product\\";
        }

        [HttpPost("/api/add-product-with-image")]
        public IActionResult AddProImage([FromForm] ProImage model)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Product is invalid"
                });
            }

            if(model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if(fileResult.Item1 == 1)
                {
                    var _uploadedfiles = Request.Form.Files;
                    foreach(IFormFile source in _uploadedfiles)
                    {
                        string Filename = source.FileName;
                        string Filepath = GetFilePath();
                        if(!System.IO.Directory.Exists(Filepath))
                        {
                            System.IO.Directory.CreateDirectory(Filepath);
                        }
                        string imagepath = Filepath + fileResult.Item2;
                        model.ImagePath = fileResult.Item2;
                        if(System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                        using(FileStream stram = System.IO.File.Create(imagepath))
                        {
                            source.CopyToAsync(stram);
                        }
                    }
                }
                var productResult = _productRepo.AddProImg(model);
                if (productResult)
                {
                    return Ok(new
                    {
                        status = "Success",
                        message = "Product added successfully"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = "Error",
                        message = "Product added failed"
                    });
                }
            }
            return Ok();
        }

        [HttpPost("/api/addProductImage")]
        public IActionResult AddProductWithImage([FromForm] Product model)
        {
            var status = "";
            var message = "";

            if (!ModelState.IsValid)
            {
                status = "Error";
                message = "Cannot create product. ";
                return Ok(status);
            }
            if(model.ProductID == 0)
            {
                if (model.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(model.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        var _uploadedfiles = Request.Form.Files;
                        foreach (IFormFile source in _uploadedfiles)
                        {
                            string Filename = source.FileName;
                            string Filepath = GetFilePath();

                            if (!System.IO.Directory.Exists(Filepath))
                            {
                                System.IO.Directory.CreateDirectory(Filepath);
                            }

                            string imagepath = Filepath + fileResult.Item2;
                            model.ImagePath = fileResult.Item2;

                            if (System.IO.File.Exists(imagepath))
                            {
                                System.IO.File.Delete(imagepath);
                            }

                            using (FileStream stream = System.IO.File.Create(imagepath))
                            {
                                source.CopyToAsync(stream);

                            }
                        }
                        //model.ImagePath = GetFilePath(Filename) + fileResult.Item2; // getting name of image
                    }

                    var productResult = _productRepo.Add(model);
                    if (productResult)
                    {
                        status = "success";
                        message = "Product Add Successfully";
                    }
                    else
                    {
                        status = "Error";
                        message = "Cannot add product";

                    }
                    

                }
            }
            else
            {
                if (model.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(model.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        var _uploadedfiles = Request.Form.Files;
                        foreach (IFormFile source in _uploadedfiles)
                        {
                            string Filename = source.FileName;
                            string Filepath = GetFilePath();

                            if (!System.IO.Directory.Exists(Filepath))
                            {
                                System.IO.Directory.CreateDirectory(Filepath);
                            }

                            string imagepath = Filepath + fileResult.Item2;
                            model.ImagePath = fileResult.Item2;

                            if (System.IO.File.Exists(imagepath))
                            {
                                System.IO.File.Delete(imagepath);
                            }

                            using (FileStream stream = System.IO.File.Create(imagepath))
                            {
                                source.CopyToAsync(stream);

                            }
                        }
                        //model.ImagePath = GetFilePath(Filename) + fileResult.Item2; // getting name of image
                    }


                    var productResult = _productRepo.UpdateProduct(model.ProductID, model);
                    if (productResult)
                    {
                        status = "success";
                        message = "Update Successfully";
                    }
                    else
                    {
                        status = "Error";
                        message = "Cannot update product";

                    }
                }
                else
                {
                    var fileResult = _fileService.SaveImage(model.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        var _uploadedfiles = Request.Form.Files;
                        foreach (IFormFile source in _uploadedfiles)
                        {
                            string Filename = source.FileName;
                            string Filepath = GetFilePath();

                            if (!System.IO.Directory.Exists(Filepath))
                            {
                                System.IO.Directory.CreateDirectory(Filepath);
                            }

                            string imagepath = Filepath + fileResult.Item2;
                            model.ImagePath = fileResult.Item2;

                            if (System.IO.File.Exists(imagepath))
                            {
                                System.IO.File.Delete(imagepath);
                            }

                            using (FileStream stream = System.IO.File.Create(imagepath))
                            {
                                source.CopyToAsync(stream);

                            }
                        }
                    }


                    var productResult = _productRepo.UpdateProduct(model.ProductID, model);
                    if (productResult)
                    {
                        status = "success";
                        message = "Update Successfully";
                    }
                    else
                    {
                        status = "Error";
                        message = "Cannot update product";

                    }
                }

                
            }
            
            return Ok(new
            {
                status,
                message,

            });
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            try
            {
                var product = await _context.Products.ToListAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Data List",
                    product = product
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "success",
                    message = "Something when wrong! "+e.Message,
                });
            }
        }

        // GET: api/Product/GetByCategory/categoryName
        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }


        [HttpPost("/api/ProductGetByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryID([FromBody] GetCategoryById request )
        {
            var products = await _context.Products
                .Where(p => p.CategoryID == request.CategoryID)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return Ok(new
                {
                    status = "Error",
                    message = "Invalid category"
                });
            }
            try
            {
                return Ok(new
                {
                    status = "success",
                    message = "Product list",
                    products = products
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



        [HttpGet("/api/get-product-list")]
        public async Task<ActionResult> GetProductList()
        {
            try
            {
                var product = await _context.Products.ToArrayAsync();
                var proList = new List<ProductListResponse>();
                foreach (var item in product)
                {
                    var pro = new ProductListResponse() {
                        Id = item.ProductID,
                        Name = item.ProductName,
                        skuID = item.skuID,
                        Price = item.UnitPrice
                        
                    };
                    proList.Add(pro);

                }

                return Ok(new
                {
                    status = "success",
                    message = "Data List",
                    product = proList
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "success",
                    message = "Something when wrong! " + e.Message,
                });
            }
        }


        [HttpPost("/api/get_product_by_id")]
        public async Task<ActionResult> GetProductById([FromBody] GetProductById request)
        {
            try
            {
                var productId = await _context.Products.Where(p => p.ProductID == request.ProductID).FirstOrDefaultAsync();
                return Ok(new
                {
                    status = "success",
                    message = "Data List",
                    product = productId
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "success",
                    message = "Something when wrong! " + e.Message,
                });
            }
        }

        [HttpPost("/api/product-cud")]
        public async Task<ActionResult> CUDProduct([FromBody] ProductRequest request)
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
                            _context.ChangeTracker.Clear();
                            var product = new Product
                            {
                                ProductName = request.ProductName,
                                Description = request.Description,
                                CategoryID = request.CategoryID,
                                skuID = request.skuID,
                                VendorID = request.VendorID,
                                UnitPrice = request.UnitPrice,
                                isActive = request.isActive
                            };

                            await _context.Products.AddAsync(product);
                            await _context.SaveChangesAsync();

                            

                            await transaction.CommitAsync();

                            status = "success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "Error";
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
                            _context.ChangeTracker.Clear();
                            var product = new Product
                            {
                                ProductID = request.ProductID,
                                ProductName = request.ProductName,
                                Description = request.Description,
                                CategoryID = request.CategoryID,
                                skuID = request.skuID,
                                VendorID = request.VendorID,
                                UnitPrice = request.UnitPrice,
                                isActive = request.isActive
                            };

                            _context.Products.Update(product);
                            await _context.SaveChangesAsync();

                            

                            await transaction.CommitAsync();

                            status = "success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "success";
                            message = "Something when wrong" + e.Message;
                            await transaction.RollbackAsync();
                        }

                    }
                    #endregion
                    #region Delete: D
                    else if(request.CUD == "D")
                    {
                        try
                        {
                            _context.ChangeTracker.Clear();
                            var product = new Product
                            {
                                ProductID = request.ProductID,
                                ProductName = request.ProductName,
                                Description = request.Description,
                                CategoryID = request.CategoryID,
                                skuID = request.skuID,
                                VendorID = request.VendorID,
                                UnitPrice = request.UnitPrice,
                                isActive = request.isActive
                            };

                            _context.Products.Remove(product);
                            await _context.SaveChangesAsync();



                            await transaction.CommitAsync();

                            status = "success";
                            message = "Your transaction completed";
                        }
                        catch (Exception e)
                        {
                            status = "Error";
                            message = "Something when wrong" + e.Message;
                            await transaction.RollbackAsync();
                        }

                    }
                    else
                    {
                        status = "success";
                        message = request.CUD.ToString();
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

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> SearchProducts(string searchTerm)
        {
            searchTerm = searchTerm?.ToLower();  // Convert searchTerm to lowercase

            // Check if the searchTerm is blank, and adjust the query accordingly
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Handle the case where searchTerm is blank (e.g., return all products)
                var allProducts = _context.Products.ToList();
                return Ok(null);
            }
            else
            {
                // Use the searchTerm in the WHERE clause of the query with case-insensitive comparison
                var searchResults = _context.Products
                    .Where(p => p.ProductName.ToLower().Contains(searchTerm) || p.ProductID.ToString().ToLower() == searchTerm)
                    .ToList();

                return Ok(searchResults);
            }
        }



    }
}
