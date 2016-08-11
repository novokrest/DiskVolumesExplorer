﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiskVolumesExplorer.Client.Services {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConnectionConfig", Namespace="http://schemas.datacontract.org/2004/07/DiskVolumesExplorer.Service")]
    [System.SerializableAttribute()]
    public partial class ConnectionConfig : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServerAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServerAddress {
            get {
                return this.ServerAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ServerAddressField, value) != true)) {
                    this.ServerAddressField = value;
                    this.RaisePropertyChanged("ServerAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.IHypervisorService")]
    public interface IHypervisorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/Connect", ReplyAction="http://tempuri.org/IHypervisorService/ConnectResponse")]
        void Connect(DiskVolumesExplorer.Client.Services.ConnectionConfig connectionConfig);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/Connect", ReplyAction="http://tempuri.org/IHypervisorService/ConnectResponse")]
        System.Threading.Tasks.Task ConnectAsync(DiskVolumesExplorer.Client.Services.ConnectionConfig connectionConfig);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/GetVirtualMachineNames", ReplyAction="http://tempuri.org/IHypervisorService/GetVirtualMachineNamesResponse")]
        string[] GetVirtualMachineNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/GetVirtualMachineNames", ReplyAction="http://tempuri.org/IHypervisorService/GetVirtualMachineNamesResponse")]
        System.Threading.Tasks.Task<string[]> GetVirtualMachineNamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/GetVirtualMachine", ReplyAction="http://tempuri.org/IHypervisorService/GetVirtualMachineResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DiskVolumesExplorer.Client.Services.ConnectionConfig))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        object GetVirtualMachine(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHypervisorService/GetVirtualMachine", ReplyAction="http://tempuri.org/IHypervisorService/GetVirtualMachineResponse")]
        System.Threading.Tasks.Task<object> GetVirtualMachineAsync(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHypervisorServiceChannel : DiskVolumesExplorer.Client.Services.IHypervisorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HypervisorServiceClient : System.ServiceModel.ClientBase<DiskVolumesExplorer.Client.Services.IHypervisorService>, DiskVolumesExplorer.Client.Services.IHypervisorService {
        
        public HypervisorServiceClient() {
        }
        
        public HypervisorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HypervisorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HypervisorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HypervisorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Connect(DiskVolumesExplorer.Client.Services.ConnectionConfig connectionConfig) {
            base.Channel.Connect(connectionConfig);
        }
        
        public System.Threading.Tasks.Task ConnectAsync(DiskVolumesExplorer.Client.Services.ConnectionConfig connectionConfig) {
            return base.Channel.ConnectAsync(connectionConfig);
        }
        
        public string[] GetVirtualMachineNames() {
            return base.Channel.GetVirtualMachineNames();
        }
        
        public System.Threading.Tasks.Task<string[]> GetVirtualMachineNamesAsync() {
            return base.Channel.GetVirtualMachineNamesAsync();
        }
        
        public object GetVirtualMachine(string name) {
            return base.Channel.GetVirtualMachine(name);
        }
        
        public System.Threading.Tasks.Task<object> GetVirtualMachineAsync(string name) {
            return base.Channel.GetVirtualMachineAsync(name);
        }
    }
}