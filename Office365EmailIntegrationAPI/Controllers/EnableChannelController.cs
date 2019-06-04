using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.BotService;
using Microsoft.Azure.Management.BotService.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Office365EmailIntegrationAPI.Interfaces;
using Office365EmailIntegrationAPI.Model;

namespace Office365EmailIntegrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnableChannelController : ControllerBase
    {
        IConfiguration configuration;
        private readonly IEnableChannelService _enableChannelService;
        public EnableChannelController(IConfiguration configuration, IEnableChannelService enableChannelService)
        {
            this.configuration = configuration;
            _enableChannelService = enableChannelService;
        }

        /// <summary>
        /// GET api/ChannelInformation
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetChannelInformation")]
        public BotChannel ChannelInformation()
        {
            var result = _enableChannelService.GetChannelInformation();
            return result;
        }

        /// <summary>
        /// POST: api/EnableChannel
        /// </summary>
        /// <param name="channelInformation"></param>
        [HttpPost]
        [Route("CreateChannel")]
        public IActionResult EnableChannel([FromBody] EmailChannelInformation channelInformation)
        {
            _enableChannelService.EnableChannel(channelInformation);
            return Ok();
        }

        ///// <summary>
        ///// POST: api/SendEmail
        ///// </summary>
        ///// <param name="emailChannelInformation"></param>
        //[HttpPost]
        //[Route("SendEmail")]
        //public IActionResult SendEmail([FromBody] EmailChannelInformation emailChannelInformation)
        //{
        //    _enableChannelService.SendEmail(emailChannelInformation);
        //    return Ok();
        //}

        ///// <summary>
        ///// DELETE api/Delete
        ///// </summary>
        //[HttpDelete]
        //[Route("DeleteChannel")]
        //public IActionResult Delete()
        //{
        //    _enableChannelService.DeleteChannel();
        //    return Ok();
        //}
    }

}