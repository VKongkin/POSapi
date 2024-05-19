using POSapi.Model.Data;
using POSapi.Repository.Abstract;

namespace POSapi.Repository.Implement
{
    public class ProductRepository:IProductRepository
    {
        private readonly DemoDbContext _context;
        public ProductRepository(DemoDbContext context)
        {
            _context = context;
        }

        public bool Add(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool AddProImg(ProImage model)
        {
            try
            {
                _context.ProImages.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public bool AddProduct(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProduct(int id, Product model)
        {
            var existingProduct = _context.Products.Find(id);

            if (existingProduct != null)
            {
                if(model.ImagePath != null)
                {
                    existingProduct.ProductName = model.ProductName;
                    existingProduct.ImagePath = model.ImagePath;
                    existingProduct.Description = model.Description;
                    existingProduct.VendorID = model.VendorID;
                    existingProduct.CategoryID = model.CategoryID;
                    existingProduct.isActive = model.isActive;
                    existingProduct.skuID = model.skuID;
                    existingProduct.UnitPrice = model.UnitPrice;
                }
                else
                {
                    existingProduct.ProductName = model.ProductName;
                    //existingProduct.ImagePath = model.ImagePath;
                    existingProduct.Description = model.Description;
                    existingProduct.VendorID = model.VendorID;
                    existingProduct.CategoryID = model.CategoryID;
                    existingProduct.isActive = model.isActive;
                    existingProduct.skuID = model.skuID;
                    existingProduct.UnitPrice = model.UnitPrice;
                }

                
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        public bool UpdateProImg(int id, ProImage model)
        {
            var existingProduct = _context.ProImages.Find(id);

            if (existingProduct != null)
            {
                existingProduct.ProductName = model.ProductName;
                existingProduct.ImagePath = model.ImagePath;

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = _context.Products.Find(id);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Update(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
