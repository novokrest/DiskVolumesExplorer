using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.Linq;
using DiskVolumesExplorer.Service.Configs.Users;

namespace DiskVolumesExplorer.Service
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6)))
                                       .Split(':');
                var user = new
                {
                    Name = credentials[0], Password = credentials[1]
                };

                return ValidateUser(user.Name, user.Password);
            }

            WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"HypervisorService\"");
            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }

        private bool ValidateUser(string userName, string password)
        {
            var userConfigs = UserConfigsLoader.LoadUserConfigs();

            return userConfigs.Any(userConfig => userConfig.Name == userName && userConfig.Password == password);
        }
    }
}
