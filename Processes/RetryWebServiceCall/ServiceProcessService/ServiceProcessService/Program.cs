using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Builder;
using CoreWCF.Configuration;
using Microsoft.Extensions.Hosting;
using CoreWCF;
using CoreWCF.Description;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

//This sample service is intended to be run in conjunction with
//the Neuron Process sample: Retry Service that can be found in the 
//Neuron sample configuration file RetryWebSerivceCall.esb

namespace Neuron.NetX.Samples.Processes
{
    [ServiceContract(Namespace="http://neuron.netx.samples.processes/")]
    public interface IServiceProcessService
    {
        [OperationContract]
        Person GetPerson(string name);
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public string name;
        [DataMember]
        public int age;
    }

    public class ServiceProcessService : IServiceProcessService
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddServiceModelServices().AddServiceModelMetadata();
            builder.WebHost.UseUrls("http://localhost:8003");
            var app = builder.Build();

            app.UseServiceModel(serviceBuilder =>
            {
                 serviceBuilder.AddService<ServiceProcessService>
                 (serviceOptions => 
                     {
                           serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true; 
                           serviceOptions.BaseAddresses.Add(new Uri("http://localhost:8003"));
                     }
                );

                var basicHttpBinding = new BasicHttpBinding();

                serviceBuilder.AddServiceEndpoint<ServiceProcessService, IServiceProcessService>
                (basicHttpBinding, "/samples/processes/serviceprocess");
             });

			// Enable getting metadata/wsdl
			var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpGetUrl = new Uri("http://localhost:8003/ServiceProcess/metadata");
			app.Start();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadLine();
            app.StopAsync().Wait();
        }


        public ServiceProcessService()
        {
            CreatePersonList();
        }

        static List<Person> people = new List<Person>();
        //create some default content
        private void CreatePersonList()
        {
            Person p = new Person();
            p.name = "Johny Neuron";
            p.age = 16;
            people.Add(p);

            p = new Person();
            p.name = "Bobby Neuron";
            p.age = 44;
            people.Add(p);

            p = new Person();
            p.name = "Betty Neuron";
            p.age = 36;
            people.Add(p);

        }

        public Person GetPerson(string name)
        {
            Person person = people.Find((p) => (p.name.Equals(name)));
            if (person == null)
            {
                Thread.Sleep(5000); 
            }

            return person;
        }
    }
}
