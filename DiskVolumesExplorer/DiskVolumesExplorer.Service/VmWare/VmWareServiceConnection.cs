using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Web.Services3.Design;
using Vim25Api;

namespace DiskVolumesExplorer.Service.VmWare
{
    public class VmWareServiceConnection
    {
        private const string ServiceReferenceValue = "ServiceInstance";

        public enum ConnectionState
        {
            Connected,
            Disconnected,
        }

        private VimService _service;
        private ConnectionState _state;
        private ServiceContent _serviceContent;
        private readonly ManagedObjectReference _serviceRef;

        public event ConnectionEventHandler AfterConnect;
        public event ConnectionEventHandler AfterDisconnect;
        public event ConnectionEventHandler BeforeDisconnect;

        /// <summary>
        ///  This method is used to validate remote certificate 
        /// </summary>
        /// <param name="sender">string Array</param>
        /// <param name="certificate">X509Certificate certificate</param>
        /// <param name="chain">X509Chain chain</param>
        /// <param name="policyErrors">SslPolicyErrors policyErrors</param>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        public VmWareServiceConnection()
        {
            _serviceRef = new ManagedObjectReference();
            _serviceRef.type = ServiceReferenceValue;
            _serviceRef.Value = ServiceReferenceValue;
            _state = ConnectionState.Disconnected;
            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
        }


        /// <summary>
        /// Creates an instance of the VMA proxy and establishes a connection
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Connect(string url, string username, string password)
        {
            if (_service != null)
            {
                Disconnect();
            }

            _service = new VimService();
            _service.Url = url;
            _service.Timeout = 600000; //The value can be set to some higher value also.
            _service.CookieContainer = new System.Net.CookieContainer();

            _serviceContent = _service.RetrieveServiceContent(_serviceRef);

            if (_serviceContent.sessionManager != null)
            {
                _service.Login(_serviceContent.sessionManager, username, password, null);
            }

            _state = ConnectionState.Connected;
            if (AfterConnect != null)
            {
                AfterConnect(this, new ConnectionEventArgs());
            }
        }

        public void Connect(string url, Cookie cookie)
        {
            if (_service != null)
            {
                Disconnect();
            }
            _service = new VimService();
            _service.Url = url;
            _service.Timeout = 600000; //The value can be set to some higher value also.
            _service.CookieContainer = new System.Net.CookieContainer();
            _service.CookieContainer.Add(cookie);
            _serviceContent = _service.RetrieveServiceContent(_serviceRef);

            _state = ConnectionState.Connected;
            if (AfterConnect != null)
            {
                AfterConnect(this, new ConnectionEventArgs());
            }
        }

        public void SaveSession(String fileName, String urlString)
        {
            Cookie cookie = _service.CookieContainer.GetCookies(
                            new Uri(urlString))[0];
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(fileName, FileMode.Create);
            bf.Serialize(s, cookie);
            s.Close();
        }




        public void LoadSession(String fileName, String urlString)
        {
            if (_service != null)
            {
                Disconnect();
            }
            _service = new VimService();
            _service.Url = urlString;
            _service.Timeout = 600000;
            _service.CookieContainer = new System.Net.CookieContainer();

            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(fileName, FileMode.Open);
            Cookie c = bf.Deserialize(s) as Cookie;
            s.Close();
            _service.CookieContainer.Add(c);
            _serviceContent = _service.RetrieveServiceContent(_serviceRef);
            _state = ConnectionState.Connected;
            if (AfterConnect != null)
            {
                AfterConnect(this, new ConnectionEventArgs());
            }
        }

        public VimService Service
        {
            get
            {
                return _service;
            }
        }

        public ManagedObjectReference ServiceRef
        {
            get
            {
                return _serviceRef;
            }
        }

        public ServiceContent ServiceContent
        {
            get
            {
                return _serviceContent;
            }
        }

        public ManagedObjectReference PropertyCollector
        {
            get
            {
                return _serviceContent.propertyCollector;
            }
        }

        public ManagedObjectReference RootFolder
        {
            get
            {
                return _serviceContent.rootFolder;
            }
        }

        public ConnectionState State
        {
            get
            {
                return _state;
            }
        }

        /// <summary>
        /// Disconnects the Connection
        /// </summary>
        public void Disconnect()
        {
            if (_service != null)
            {
                if (BeforeDisconnect != null)
                {
                    BeforeDisconnect(this, new ConnectionEventArgs());
                }

                if (_serviceContent != null)
                    _service.Logout(_serviceContent.sessionManager);

                _service.Dispose();
                _service = null;
                _serviceContent = null;

                _state = ConnectionState.Disconnected;
                if (AfterDisconnect != null)
                {
                    AfterDisconnect(this, new ConnectionEventArgs());
                }
            }
        }

        public void SSOConnect(XmlElement token, string url)
        {
            if (_service != null)
            {
                Disconnect();
            }

            _service = new VimService();
            _service.Url = url;
            _service.Timeout = 600000; //The value can be set to some higher value also.
            _service.CookieContainer = new System.Net.CookieContainer();

            //...
            //When this property is set to true, client requests that use the POST method 
            //expect to receive a 100-Continue response from the server to indicate that 
            //the client should send the data to be posted. This mechanism allows clients 
            //to avoid sending large amounts of data over the network when the server, 
            //based on the request headers, intends to reject the request
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var customSecurityAssertion = new CustomSecurityAssertionBearer();
            customSecurityAssertion.BinaryToken = token;

            //Setting up the security policy for the request
            Policy policySAML = new Policy();
            policySAML.Assertions.Add(customSecurityAssertion);

            // Setting policy of the service
            _service.SetPolicy(policySAML);

            _serviceContent = _service.RetrieveServiceContent(_serviceRef);

            if (_serviceContent.sessionManager != null)
            {
                _service.LoginByToken(_serviceContent.sessionManager, null);
            }

            _state = ConnectionState.Connected;
            if (AfterConnect != null)
            {
                AfterConnect(this, new ConnectionEventArgs());
            }
        }
    }

    public class ConnectionEventArgs : System.EventArgs
    {
    }

    public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);
}
