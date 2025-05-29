using InvoicingHub.Application.UseCases.Customers;
using InvoicingHub.Application.UseCases.Interfaces;
using InvoicingHub.Application.UseCases.Invoices;
using InvoicingHub.Application.UseCases.Products;

namespace InvoicingHub.Web.API.Shared.Initialize
{
    public static class ApplicationDependenciesManager
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IInvoicesUseCases, InvoicesUseCases>();
            services.AddTransient<IProductUseCases, ProductUseCases>();
            services.AddTransient<ICustomerUseCases, CustomerUseCases>();

            return services;
        }
    }
}