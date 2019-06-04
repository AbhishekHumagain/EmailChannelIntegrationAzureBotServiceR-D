using EASendMail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.BotService;
using Microsoft.Azure.Management.BotService.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Options;
using Office365EmailIntegrationAPI.Interfaces;
using Office365EmailIntegrationAPI.Model;
using Office365EmailIntegrationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365EmailIntegrationAPI.Services
{
    public class EnableChannelService : IEnableChannelService
    {
        private readonly IOptions<ResourceInformation> _resourceInformation;
        private readonly IOptions<EmailInformation> _emailInformation;
        public EnableChannelService(IOptions<ResourceInformation> resourceInformation, IOptions<EmailInformation> emailInformation)
        {
            _resourceInformation = resourceInformation;
            _emailInformation = emailInformation;
        }

        /// <summary>
        /// gets all the information of bot
        /// </summary>
        /// <returns>Bot Information</returns>
        public BotChannel GetChannelInformation()
        {
            try
            {
                AzureBotServiceClient botServiceClient = GetBotServiceClient();
                var getChannelInfo = botServiceClient.Channels.Get(_resourceInformation.Value.ResourceGroupName, _resourceInformation.Value.BotName, ChannelName.EmailChannel.ToString());
                return getChannelInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// it enables the email channel
        /// </summary>
        /// <param name="channelInformation"></param>
        /// <returns>Channel information</returns>
        public void EnableChannel(EmailChannelInformation channelInformation)
        {
            try
            {
                EmailChannel channel = new EmailChannel()
                {
                    Properties = new EmailChannelProperties
                    {
                        IsEnabled = true,
                        EmailAddress = channelInformation.EmailAddress,
                        Password = channelInformation.Password
                    }
                };
                BotChannel botChannel = new BotChannel { Location = "global", Properties = channel };

                AzureBotServiceClient botServiceClient = GetBotServiceClient();
                var test = botServiceClient.Channels.Create(_resourceInformation.Value.ResourceGroupName, _resourceInformation.Value.BotName, ChannelName.EmailChannel, botChannel);
                SendEmail(channelInformation.EmailAddress, "Email integrated successfully!");
            }
            catch (Exception ex)
            {
                SendEmail(channelInformation.EmailAddress, "Email integrated failed!");
            }
        }

        ///// <summary>
        ///// deletes the channel
        ///// </summary>
        ///// <returns></returns>
        //public void DeleteChannel()
        //{
        //    try
        //    {
        //        AzureBotServiceClient botServiceClient = GetBotServiceClient();
        //        botServiceClient.Channels.Delete(_resourceInformation.Value.ResourceGroupName, _resourceInformation.Value.BotName, ChannelName.EmailChannel.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// authenticate the user to enable the channel
        /// </summary>
        /// <returns></returns>
        private AzureBotServiceClient GetBotServiceClient()
        {
            var clientId = _resourceInformation.Value.ClientId;
            var clientSecret = _resourceInformation.Value.ClientSecret;
            var tenantId = _resourceInformation.Value.TenantId;
            var subscriptionId = _resourceInformation.Value.SubscriptionId;

            try
            {
                var cred = SdkContext.AzureCredentialsFactory.FromServicePrincipal(clientId, clientSecret, tenantId,
            AzureEnvironment.AzureGlobalCloud);

                AzureBotServiceClient azureBotServiceClient = new AzureBotServiceClient(cred);

                var azure = new AzureBotServiceClient(cred);
                azure.SubscriptionId = subscriptionId;
                return azure;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// send email when then channel is integrated or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        private void SendEmail(string email, string message)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            // Your Offic 365 email address
            oMail.From = _emailInformation.Value.EmailAddress;

            // Set recipient email address
            oMail.To = email;

            // Set email subject
            oMail.Subject = "Email Channel Integration";

            // Set email body
            oMail.TextBody = message;

            // Your Office 365 SMTP server address,
            // You should get it from outlook web access.
            SmtpServer oServer = new SmtpServer(_emailInformation.Value.SmtpServer);

            // user authentication should use your
            // email address as the user name.
            oServer.User = _emailInformation.Value.EmailAddress;
            oServer.Password = _emailInformation.Value.Password;

            // Set 587 port
            oServer.Port = _emailInformation.Value.Port;

            // detect SSL/TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            try
            {
                oSmtp.SendMail(oServer, oMail);
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

        }

    }
}
