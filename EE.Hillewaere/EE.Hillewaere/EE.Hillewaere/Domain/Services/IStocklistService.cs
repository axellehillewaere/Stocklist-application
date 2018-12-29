using EE.Hillewaere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EE.Hillewaere.Domain.Services
{
    public interface IStocklistService
    {
        Task<IEnumerable<Order>> GetOrderList();
        Task<IEnumerable<Category>> GetCategoryList();
        Task<IEnumerable<SubCategory>> GetSubCategoryListById(Guid subCategoryid);
        Task<IEnumerable<Product>> GetProductListById(Guid productId);
        Task<IEnumerable<Product>> GetProductListBySub(string name);
        Task SaveToOrder(Order order);
        Task SaveProduct(Product product);
        Task DeleteCategory(Guid categoryId);
        Task DeleteSubCategory(Guid subCategoryId);
        Task DeleteProduct(Guid productId);
        decimal CalculateTotalPrice();
    }
}
