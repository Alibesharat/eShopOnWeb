using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parbad.Builder;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Storage.EntityFrameworkCore.Builder;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public static class ServiceExtentsion
    {
        public static void AddMellatPayment(this IServiceCollection services, string ConnectionString)
        {


            services.AddParbad().SetMellat().ConfigStorage(ConnectionString).ConfigureHttpContext(builder => builder.UseDefaultAspNetCore())
                .ConfigureAutoTrackingNumber(opt =>
          opt.MinimumValue = 20000);
        }



        public static void AddVirtualPayment(this IServiceCollection services,string ConnectionString)
        {
            services.AddParbad().SetMockGatWay().ConfigStorage(ConnectionString).ConfigureHttpContext(builder => builder.UseDefaultAspNetCore())
                .ConfigureAutoTrackingNumber(opt =>
           opt.MinimumValue = 20000);
        }


        public static IParbadBuilder SetMockGatWay(this IParbadBuilder builder)
        {
            builder.ConfigureGateways(gateways =>
            {
                gateways
                      .AddParbadVirtual()
                      .WithOptions(options => options.GatewayPath = "/MyVirtualGateway");
            });


            return builder;
        }
        public static IParbadBuilder SetMellat(this IParbadBuilder builder)
        {
            builder.ConfigureGateways(gateways =>
            {
                gateways
                     .AddMellat()
                     .WithAccounts(source =>
                     {
                         source.AddInMemory(account =>
                         {
                             account.TerminalId = 5372244;
                             account.UserName = "vokala1399";
                             account.UserPassword = "75479599";
                         });
                     });
            });



            return builder;
        }



        public static IParbadBuilder ConfigStorage(this IParbadBuilder builder, string ConnectionString)
        {
            builder.ConfigureStorage(builder =>
            {
                builder.UseEfCore(options =>
                {
                    // Using SQL Server
                    var assemblyName = typeof(Startup).Assembly.GetName().Name;

                    // If you prefer to have a separate MigrationHistory table for Parbad, you can change the above line to this:
                    options.ConfigureDbContext = db => db.UseSqlServer(ConnectionString, sql =>
                    {
                        sql.MigrationsAssembly(assemblyName);
                        sql.MigrationsHistoryTable("Parbad_History");
                    });

                });

            });
            return builder;
        }
    }
}
