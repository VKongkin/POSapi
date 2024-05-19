using POSapi.Model.Data;

namespace POSapi.Repository.Abstract
{
    public interface IProductRepository
    {
        bool Add(Product product);
        IEnumerable<Product> GetAllProducts();
        bool Update(int id,Product product);
        bool Delete(int id);
        bool AddProduct(Product model);
        bool UpdateProduct(int id, Product model);
        bool DeleteProduct(int id);
        bool AddProImg(ProImage proImage);
        bool UpdateProImg(int id,ProImage proImage);
    }
}
