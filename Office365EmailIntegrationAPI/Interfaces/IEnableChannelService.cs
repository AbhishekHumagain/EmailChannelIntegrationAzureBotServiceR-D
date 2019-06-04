using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.BotService.Models;
using Office365EmailIntegrationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365EmailIntegrationAPI.Interfaces
{
    public interface IEnableChannelService
    {
        /// <summary>
        /// get bot channel information
        /// </summary>
        /// <returns></returns>
        BotChannel GetChannelInformation();

        /// <summary>
        /// enable email channel
        /// </summary>
        /// <param name="channelInformation"></param>
        /// <returns></returns>
        void EnableChannel(EmailChannelInformation channelInformation);

        //void SendEmail(EmailChannelInformation channelInformation);

        ///// <summary>
        ///// delete the channel
        ///// </summary>
        //void DeleteChannel();
    }
}
