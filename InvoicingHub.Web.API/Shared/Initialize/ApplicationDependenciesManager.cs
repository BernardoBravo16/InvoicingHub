using InvoicingHub.Application.UseCases.Interfaces;
using InvoicingHub.Application.UseCases.Invoices;

namespace InvoicingHub.Web.API.Shared.Initialize
{
    public static class ApplicationDependenciesManager
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IInvoicesUseCases, InvoicesUseCases>();

            return services;
        }
    }
}