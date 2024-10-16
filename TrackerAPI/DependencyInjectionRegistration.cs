using BusinessLogicLayer.Services.CategoryServiceContainer;
using BusinessLogicLayer.Services.InventoryTransactionServiceContainer;
using BusinessLogicLayer.Services.InventoryTransactionsServiceContainer;
using BusinessLogicLayer.Services.OrderDetailServiceContainer;
using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.ProductServiceContainer;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using BusinessLogicLayer.Services.SupplierServiceContainer;
using BusinessLogicLayer.Services.TransactionTypeServiceContainer;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;

namespace TrackerAPI
{
    public static class DependencyInjectionRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IOrderDetailService, OrderDetailService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ISupplierService, SupplierService>();
            service.AddScoped<ITransactionTypeService, TransactionTypeService>();
            service.AddScoped<ICategoryService, CategoryService>();
            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<GenericRepository<Category>>();
            service.AddScoped<GenericRepository<InventoryTransaction>>();
            service.AddScoped<GenericRepository<Order>>();
            service.AddScoped<GenericRepository<OrderDetail>>();
            service.AddScoped<GenericRepository<Product>>();
            service.AddScoped<GenericRepository<Supplier>>();
            service.AddScoped<GenericRepository<TransactionType>>();
            return service;
        }
    }
}
