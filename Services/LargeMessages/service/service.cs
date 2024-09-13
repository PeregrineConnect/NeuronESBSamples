using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace Neuron.EnterpriseServiceBus.Samples
{
    // Define a service contract.
    [ServiceContract(Namespace = "http://Neuron.EnterpriseServiceBus.Samples")]
    public interface ICalculator
    {
        [OperationContract(IsOneWay = true)]
        void Notify(string text);
    }

    // Service class which implements the service contract.

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalculatorService : ICalculator
    {
        public void Notify(string text)
        {
            Console.WriteLine("Received message of size " + text.Length.ToString() + " characters");
        }

        // Host the service in this console application.

        public static void Main()
        {
            Console.WriteLine("Initializing service");

            // Create a ServiceHost for the CalculatorService type
            /*  using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService)))
              {
                  serviceHost.Open(); */

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddServiceModelServices().AddServiceModelMetadata(); ;
            builder.WebHost.UseNetTcp(8001);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080);
            });
            var app = builder.Build();

            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<CalculatorService>
                (serviceOptions =>
                {
                    serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
                }
                );
                var netTCPbinading = new NetTcpBinding();
                netTCPbinading.Security = new NetTcpSecurity() { Mode = SecurityMode.None };
                netTCPbinading.MaxReceivedMessageSize = 100000000;
                netTCPbinading.ReaderQuotas.MaxStringContentLength = 100000000;

                serviceBuilder.AddServiceEndpoint<CalculatorService, ICalculator>(netTCPbinading, "/samples/patterns/oneway");
            });
            // Enable getting metadata/wsdl
            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpGetUrl = new Uri("http://localhost:8080/CalculatorSample/metadata");
            app.Start();


            // The service can now be accessed. */
            Console.WriteLine("The service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            Console.ReadLine();
            app.StopAsync().Wait();
            //}
        }
    }

}
