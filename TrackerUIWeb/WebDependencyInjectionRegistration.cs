using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using TrackerUIWeb.Data.ApiGateway;
using TrackerUIWeb.Utilities;

namespace TrackerUIWeb
{
    public static class WebDependencyInjectionRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUtilService, UtilService>();
            service.AddScoped<HttpHandlerService>();
            return service;
        }
    }
}
