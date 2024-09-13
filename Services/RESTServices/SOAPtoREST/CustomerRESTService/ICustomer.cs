using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Neuron.Samples.CustomerREST
{
    [ServiceContract(Namespace = "http://Neuron.Samples")]
    public interface ICustomerService
    {
        [OperationContract]
        [WebGet(UriTemplate = "", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(Result))]
        Customers GetAllCustomers();

        [OperationContract]
        [WebGet(UriTemplate = "/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(Result))]
        Customer GetCustomerByID(string id);

        [OperationContract]
        [WebGet(UriTemplate = "?Name={name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(Result))]
        Customer GetCustomerByName(string name);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(Result))]
        Result AddCustomer(Customer customer);
    }

    [CollectionDataContract(Namespace = "http://Neuron.Samples", Name = "customers")]
    public class Customers : List<Customer>
    {
    }

    [DataContract(Namespace = "http://Neuron.Samples", Name = "customer")]
    public class Customer
    {
        [DataMember(Name = "id")]
        public int ID;

        [DataMember(Name = "name")]
        public string Name;

        [DataMember(Name = "email")]
        public string Email;
    }

    [DataContract(Namespace = "http://Neuron.Samples", Name = "result")]
    public class Result
    {
        public Result(int result)
        {
            Success = result;
            ErrorMsg = string.Empty;
        }

        public Result(int result, string error)
        {
            Success = result;
            ErrorMsg = error;
        }

        [DataMember(Name = "success")]
        public int Success;

        [DataMember(Name = "errorMsg")]
        public string ErrorMsg;
    }
}
