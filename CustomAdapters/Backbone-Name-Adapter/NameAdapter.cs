﻿using System.ComponentModel;
using System.Transactions;
using System;
using System.Globalization;
using Neuron.NetX.Adapters;
using Neuron.NetX.Esb.Adapters;
using Neuron.NetX;

/// <summary>
/// Namespace for adapter assembly. 
/// </summary>
/// <remarks>
/// The default namespace for the adapter assembly is defined in the project property's application tab. This can be changed.
/// The name of the assembly, also defined on the application tab of the project property page is the concatonation of the 
/// namespace + the name of the adapter class.  The adapter class name, "NameAdapter", should be changed to reflect the nature of the adapter. 
/// For instance, if this adapter was to communicate with SalesForce, "NameAdapter" could be changed to 
/// "SalesForceAdapter". Hence, in project property's application tab the user would also change the assembly name so that the last
/// segment of the name would be identical to the class name adopted. The full assembly name would be namespace + "." + class name.
/// </remarks>
namespace Neuron.NetX.Sample.Adapters
{
    /// <summary>
    /// Generally the developer would change the name of the class to reflect the nature of the adapter. The class name in the 
    /// Adapter.cs file would also have to be changed to ensure that both class names are identical since they are both marked
    /// as partial classes.
    /// </summary>
    [AdapterConnector(EConnectorCategory.CustomConnectors, "Name Adapter", "Name Adapter " + "Connector")]
    public partial class NameAdapter
    {
        #region Constants and types
        /// <summary>
        /// These 2 constants are important.  They are used within the Peregrine Design Studio at runtime and design time for 
        /// reporting and changing any properties that the custom adapter implementation may need. 
        /// 
        /// For example, _metadataOutPrefix constant, is the prefix used for querying or editing any custom properties that the 
        /// custom adapter may wish to allow users to modify or access at runtime through the context.Data.GetProperty() or 
        /// context.Data.SetProperty() methods. Also all properties "registered" using this prefix in the adapter class's constructor
        /// are displayed in the Set Property Process Step.
        /// 
        /// For instance, if this class used a connection string to access a resource, that connection string could be provided at 
        /// design time by defining it as a property of the class.  However, if the developer wanted to grant users the ability to 
        /// provide that property at runtime (as it may change for each message), the user could register the property in the class's 
        /// constructor and then with each incoming message, inspect the message for the presence of that property, extract the value 
        /// and use it at runtime to access the resource.
        /// 
        /// The _adapterName constant defines a "user friendly name" that is used to in the Adapter Registration screen within the 
        /// Peregrine Design Studio. Its the name listed in the drop down adapter list when registrating an adapter. 
        /// </summary>
        #endregion

        /// <summary>
        /// There are a number of sample propeties defined. All properties defined will be displayed at design time
        /// in a property grid within the Adapter Endpoint configuration screen in the Peregrine Design Studio. 
        /// These samples demonstrate how to make properties visible/invisible (PropertyAttributesProvider attribute) 
        /// based on the state of other properties. These samples also demonstrate how to use type converters (TypeConverter attribute)
        /// , ordering (PropertyOrder attribute) of properties, categories (Category attribute), etc.
        /// 
        /// By default, all properties will also be displayed in the Bindings Dialog form for binding to Environmental Variables.
        /// If a property should not be "bindable", i.e. should not be used with Environmental Variables, add the attribute, 
        /// ,Bindable(false),  to the property.  The sample Password property has this applied.
        /// </summary>
        #region SAMPLE: Public Properties to Expose and Control in UI

        /// <summary>
        /// Used by the "PropertyAttributesProvider" attribute used to decorate properties
        /// where the user can control visiblitiy of the property based on the value of other 
        /// properties
        /// </summary>
        /// <param name="attributes"></param>
        //public void DynamicCredentialsAttributesProvider(PropertyAttributes attributes)
        //{
        //    attributes.IsBrowsable = (Anonymous == false);
        //}

        /// <summary>
        /// Used by the "PropertyAttributesProvider" attribute used to decorate properties
        /// where the user can control visiblitiy of the property based on the value of other 
        /// properties
        /// </summary>
        /// <param name="attributes"></param>
        //public void DynamicHttpProxyAttributesProvider(PropertyAttributes attributes)
        //{
        //    attributes.IsBrowsable = (HttpProxy == true);
        //}

        private string _server = "localhost";
        [DisplayName("Server Name")]
        [Category("(General)")]
        [Description("The name or IP address of the FTP Server to connect to.")]
        [DefaultValue("localhost")]
        [PropertyOrder(0)]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private int _port = 21;
        [DisplayName("Port")]
        [Category("(General)")]
        [Description("The port number of the FTP Server. This is usually 21")]
        [DefaultValue(21)]
        [PropertyOrder(1)]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        [DisplayName("Certificate")]
        [Category("(General)")]
        [Description("Select a Certificate configured in the Security section of the Peregrine Design Studio to secure communication with the FTP Server IF the FTP Server requests a Certificate.")]
        [PropertyOrder(8)]
        [TypeConverter(typeof(CertificateConverter))]
        public string SecureCertificate { get; set; }
        

        private bool _anonymous = true;
        [DisplayName("Anonymous Connection")]
        [Category("(General)")]
        [Description("Connect to the FTP Server using Anonymous logon")]
        [DefaultValue(true)]
        [PropertyOrder(9)]
        public bool Anonymous
        {
            get { return _anonymous; }
            set { _anonymous = value; }
        }

        [DisplayName("  User Name")]
        [Category("(General)")]
        [Description("User Id for FTP Server logon.")]
        [DefaultValue("")]
        [PropertyOrder(10)]
        //[PropertyAttributesProvider("DynamicCredentialsAttributesProvider")]
        // PropertyVisibility is used by the Design studio to decorate properties
        // where the user can control visiblitiy of the property based on the value of other 
        // properties
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(Anonymous), ERelationalOperator.Equals, "false", ELogicalOperator.None, ERightParenthesis.None)]
        public string UserName { get; set; }

        [PasswordPropertyText(true)]
        [DisplayName("  Password")]
        [Category("(General)")]
        [Description("The password for FTP Server logon.")]
        [DefaultValue("guest")]
        [PropertyOrder(11)]
        //[PropertyAttributesProvider("DynamicCredentialsAttributesProvider")]
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(Anonymous), ERelationalOperator.Equals, "false", ELogicalOperator.None, ERightParenthesis.None)]
        [Bindable(false)]
        public string Password { get; set; }


        [DisplayName("HTTP Proxy")]
        [Category("(General)")]
        [Description("Connect to the FTP Server using an HTTP Proxy Server")]
        [DefaultValue(false)]
        [PropertyOrder(12)]
        public bool HttpProxy { get; set; }

        private string _httpProxyaddress = "http://localhost";
        [DisplayName("  Address")]
        [Category("(General)")]
        [Description("Url address to HTTP Proxy.")]
        [DefaultValue("http://localhost")]
        [PropertyOrder(13)]
        //[PropertyAttributesProvider("DynamicHttpProxyAttributesProvider")]
        // PropertyVisibility is used by the Design studio to decorate properties
        // where the user can control visiblitiy of the property based on the value of other 
        // properties
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(HttpProxy), ERelationalOperator.Equals, "true", ELogicalOperator.None, ERightParenthesis.None)]
        public string HttpProxyAddress
        {
            get { return _httpProxyaddress; }
            set { _httpProxyaddress = value; }
        }

        private int _httpPort = 8080;
        [DisplayName("  Port")]
        [Category("(General)")]
        [Description("The port for the HTTP Proxy Server address")]
        [DefaultValue(8080)]
        [PropertyOrder(14)]
        //[PropertyAttributesProvider("DynamicHttpProxyAttributesProvider")]
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(HttpProxy), ERelationalOperator.Equals, "true", ELogicalOperator.None, ERightParenthesis.None)]
        public int HttpProxyPort
        {
            get { return _httpPort; }
            set { _httpPort = value; }
        }

        [DisplayName("  User Name")]
        [Category("(General)")]
        [Description("User Id for HTTP Proxy Server logon.")]
        [DefaultValue("")]
        [PropertyOrder(15)]
        //[PropertyAttributesProvider("DynamicHttpProxyAttributesProvider")]
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(HttpProxy), ERelationalOperator.Equals, "true", ELogicalOperator.None, ERightParenthesis.None)]
        public string HttpProxyUserName { get; set; }

        [DisplayName("  Password")]
        [Category("(General)")]
        [Description("The password for HTTP Proxy Server logon.")]
        [DefaultValue("password")]
        [PropertyOrder(16)]
        //[PropertyAttributesProvider("DynamicHttpProxyAttributesProvider")]
        [PropertyVisibility(0, ELeftParenthesis.None, nameof(HttpProxy), ERelationalOperator.Equals, "true", ELogicalOperator.None, ERightParenthesis.None)]
        [PasswordPropertyText(true)]
        public string HttpProxyPassword { get; set; }


        private bool _deleteOnGet = true;
        [DisplayName("Delete After Download")]
        [Category("Publish Mode Properties")]
        [Description("Deletes the file on the FTP site after it is retreived. Default is True")]
        [DefaultValue(true)]
        [PropertyOrder(4)]
        public bool DeleteOnGet
        {
            get { return _deleteOnGet; }
            set { _deleteOnGet = value; }
        }


        #endregion

        /// <summary>
        /// Create an initialized instance of the adapter
        /// </summary>
        /// <remarks>
        /// AdapterModes and ESBAdapterCapabilities must always be initialized so that the ESB
        /// framework and Peregrine Design Studio UI can correctly interact with the adapter. Modes that are supported in the UI must listed.
        /// meta data i.e. esb message custom propeties that will be supported must be added, each one preceeded by a period (.), followed by the
        /// name of the property (no spaces), a semicolon, followed by the description of the property i.e.
        ///     .[NameOfProperty].[Description of property]
        /// These properties will be displayed in the Set Properties Process Step as well.
        /// </remarks>
        public NameAdapter() : base("Name Adapter", "prefix")
        {
            AdapterModes = new AdapterMode[]
            { 
                /// To control what is displayed and supporte in the neuron explorer UI, comment out the modes
                /// that you do not wish to support
                new AdapterMode(AdapterModeStringsEnum.Subscribe.ToString(), MessageDirection.DatagramSender),      // subscribe mode - send messages to an FTP server
                new AdapterMode(AdapterModeStringsEnum.Publish.ToString(), MessageDirection.DatagramReceiver),     // Receive mode - new files are obtained from ftp server and published to the bus
                new AdapterMode(AdapterModeStringsEnum.SolicitResponse.ToString(), MessageDirection.RequestReplySender),      // SolicitResponse/Query mode - adapter writes message to outbound system, waits for reply, returns reply message from outbound system back to bus
                new AdapterMode(AdapterModeStringsEnum.RequestReply.ToString(), MessageDirection.RequestReplyReceiver)    // RequestReply mode - Client submits request to the listening adapter endpoint, adapter picks up, sends to bus, returns message from bus back to client

            };

            ESBAdapterCapabilities caps = new ESBAdapterCapabilities();
            caps.AdapterName = AdapterName;
            caps.Prefix = base.AdapterPrefix;


            // SAMPLE: Sample context properties that will be exposed within Neuron. These can be viewed within the Set Properties Process Step 
            // **************************************************************
            caps.MetadataFieldInfo =
                base.AdapterPrefix + ".Server:Name of FTP Server data is sent to or retreived from," +
                base.AdapterPrefix + ".Port:FTP Server port," +
                base.AdapterPrefix + ".Address:FTP Server address," +
                base.AdapterPrefix + ".Username:User name for FTP Server," +
                base.AdapterPrefix + ".Folder: Name of Ftp folder which file was retrieved," +
                base.AdapterPrefix + ".Filename:Name of file received from, or sent to FTP Server," +
                base.AdapterPrefix + ".Length: Lengeth of the file in bytes," + 
                base.AdapterPrefix + ".Mode:Represents the configured Adapter Mode for the endpoint";

            // **************************************************************
            Capabilities = caps;
        }

        #region Base Class call outs for the Adapter Interfaces

        /// <summary>
        /// This is called by the base adapter class's Connect() method. This should contain all application specific validation
        /// logic and initialization of resources used by the custom adapter.
        /// if an exception is thrown, it will be caught by the runtime and the adapter will fail to start up. It will appear as 
        /// error in the Backbone Event log and will be in a stopped state within Endpoint Health.
        /// </summary>
        private void ConnectAdapter()
        {
            // SAMPLE:  Validation logic for basic properties should be done here
            // ***********************************
            if (HttpProxy)
            {
                if (string.IsNullOrEmpty(HttpProxyAddress)) throw new ArgumentNullException();
                if (HttpProxyPort < 0) throw new ArgumentOutOfRangeException("HTTP Proxy - Port","You must enter a valid Port number (greater than zero) for the HTTP Proxy server");
                if (string.IsNullOrEmpty(HttpProxyAddress)) throw new ArgumentNullException("HTTP Proxy - Address", "You must enter the url address of the HTTP Proxy Server");
            }
            if (Port < 0) throw new ArgumentOutOfRangeException("FTP Port", "You must enter a valid Port number (greater than zero) for the FTP server");
            if (string.IsNullOrEmpty(Server)) throw new ArgumentNullException("FTP Server", "You must enter the name or IP address of the FTP Server to connect to");
            if (!Anonymous & string.IsNullOrEmpty(UserName)) throw new ArgumentNullException("Username", "The Username property to use for logging into the FTP Server cannot be null.");


            // SAMPLE: Custom set metadata properties that will be provided with every message sent or received from Neuron
            // The IncludeMetadata flag is set by the "Include Metadata Properties" checkbox located on the General tab of an 
            // Adapter Endpoint within the Peregrine Design Studio.
            // ***********************************
            if (IncludeMetadata)
            {
                MessageProperties.Add(new NameValuePair("Server", Server));
                MessageProperties.Add(new NameValuePair("Port", Port.ToString()));
                MessageProperties.Add(new NameValuePair("Username", UserName ));
            }

        }

        /// <summary>
        /// This is called by the base adapter class's Disconnect() method
        /// All resources that the custom adapter uses should be cleaned up here.
        /// </summary>
        private void DisconnectResources()
        {
            RaiseAdapterInfo(ErrorLevel.Info, "Disconnecting all resources");

            try
            {
                // *** place clean up work here

            }
            catch (Exception ex)
            {
                RaiseAdapterError(ErrorLevel.Error,ex.Message, ex);
            }
        }


        /// <summary>
        /// Called from the base class's SendToEndpoint().  This be called by the Backbone runtime for Subscription mode adapters
        /// 
        /// </summary>
        /// <param name="message">ESB Message handed to adapter from runtime</param>
        /// <param name="tx">Deprecated Property - This is no longer used and IS ALWAYS NULL</param>
        private void SendToDataSource(ESBMessage message, CommittableTransaction tx)
        {
            RaiseAdapterInfo(ErrorLevel.Verbose, "Received Message. MessageId " + message.Header.MessageId + " Topic " + message.Header.Topic);
            try
            {
                // if mode is solicit/Response, return a reply to the bus
                switch (this._adapterMode.ModeName)
                {
                    case AdapterModeStringConstants.Subscribe:
                        SendToDataSource(message);
                        break;

                    case AdapterModeStringConstants.SolicitResponse:
                        QueryDataSource(message);
                        break;

                    default:
                        throw new InvalidOperationException(string.Format("Error in {0}, mode {1} not supported", AdapterName, this._adapterMode.ModeName));
                }
 
            }
            catch (Exception ex)
            {
                // Enrich exception
                var msg = string.Format(CultureInfo.CurrentCulture, "The {0} endpoint, '{1}', failed to send the message. {2}", AdapterName, base.Name, ex.Message);
                var adapterEx = new AdapterSubscribeException(msg, ex);

                // throw the error to ensure the runtime enforces any adapter policies. Once an a policy executes
                throw adapterEx;
            }
        }

        /// <summary>
        /// Called by the TryReceive(), which is executed within a polling function within the framework's Adapter class.  
        /// This will be called on each poll interval defined by the PollingInterval property.
        /// All errors are handled pursuant to the error handling propeties defined by the "Error Reporting" and "Error on Polling" properties
        /// located in the framework's Adapter class.
        /// The core of the work is done by calling out to PublishMessageFromSource()
        /// </summary>
        private void ReceiveFromSource()
        {
            try
            {
               //*****************************************************************************

                PublishMessageFromSource();

           
            }
            finally
            {

            }
        }

        private void InitializeRequestReplyListener()
        {
            ESBMessage message;

            // Do work here to setup a listener

            // DO WORK HERE TO GET DATA FROM Listerner

            // CREATE ESB MESSAGE FROM DATA RETREIVED FROM Listerner
            //message = CreateEsbMessage(new byte[1],MessageProperties,MetadataPrefix,PublishTopic);

            // OR
            message = CreateEsbMessage("<xml>some data</xml>",MessageProperties,base.AdapterPrefix,PublishTopic);

            // Once a message is received, publish to bus and await a reply
            ESBMessage reply = PublishEsbMessage(message, 60);

            // once reply is received, return reply to calling client
        }

        #endregion

        #region Send Functions

        /// <summary>
        /// called from SendToDataSource(). 
        /// This is a one way send to a back end system, protocol, transport, etc
        /// </summary>
        private void SendToDataSource(ESBMessage message)
        {
            // do work here

        }

        /// <summary>
        /// called from SendToDataSource(). 
        /// This is a one way send to a back end system, protocol, transport, etc
        /// </summary>
        private void QueryDataSource(ESBMessage message)
        {
            try
            {
                // do work here
                PublishReplyMessage(message, "some kind of response from previous query to back end system of choice",MessageProperties,base.AdapterPrefix);

            }
            catch (Exception ex)
            {
                /// This is where you can optionally, catch the exception and publish the exception as the Reply message
                /// and send that reply messsage back to the caller.  Otherwise, if the developer wants to simply report
                /// the error and have the client timeout, have the adapter policy execute retries,etc then this 
                /// Try/Catch should be commented out.
                PublishReplyFromException(message, ex,MessageProperties,base.AdapterPrefix);
            }
        }

        #endregion

        #region Publish Functions

        /// <summary>
        /// called from ReceiveFromSource(). 
        /// If an error is raised, the error is reported to the event log, and an attempt is made to audit the 
        /// message to the Neuron Audit database.
        /// Demonstrates creating an ESB Message from either Text or Bytes and then publishing that message to the bus
        /// </summary>
        private void PublishMessageFromSource()
        {
            ESBMessage message = null;

            try
            {
                // DO WORK HERE TO GET DATA FROM SOURCE

                // CREATE ESB MESSAGE FROM DATA RETREIVED FROM SOURCE
                //message = CreateEsbMessage(new byte[1], MessageProperties, MetadataPrefix, PublishTopic);

                // OR
                message = CreateEsbMessage("<xml>some data</xml>", MessageProperties, base.AdapterPrefix, PublishTopic);

                // PUBLISH ESB MESSAG TO BUS
                PublishEsbMessage(message);

            }
            catch (Exception ex)
            {
                // Enrich exception, log to failed message table and rethrow
                var msg = string.Format(CultureInfo.CurrentCulture, "The {0} endpoint, '{1}', encountered an error while publishing to the bus. {2}", AdapterName, base.Name, ex.Message);
                var adapterEx = new AdapterPublishException(msg, ex);
                if (message != null) RaiseReceiveFailedFromEndpoint(message, adapterEx);
                throw adapterEx;
            }
        }
        #endregion 
    }
}
