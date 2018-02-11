using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaceTime.API.Models;
using PaceTime.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaceTime.API.Controllers
{
    [Route("api/photos")]
    public class PhotosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDictionary<string, Uri> _remote = new Dictionary<string, Uri>();

        public PhotosController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            _remote.Add("instagram", new Uri($"https://api.instagram.com/v1/users/self/media/recent/"));
        }

        [HttpGet("{provider}")]
        public async Task<IActionResult> GetPhotos(string provider)
        {
            var token = await GetAccessTokenAsync(provider);

            var url = _remote[provider.ToLower()];
            if (!Uri.TryCreate(url, $"?access_token={token}", out url))
                return NotFound();

            var data = await GetRemoteDataAsync(url);

            return Json(data);
        }

        private static async Task<IEnumerable<InstagramMedia>> GetRemoteDataAsync(Uri url)
        {
            IEnumerable<InstagramMedia> data = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                    data = json["data"].ToObject<IList<InstagramMedia>>();
                }
            }

            return data;
        }

        private async Task<string> GetAccessTokenAsync(string provider)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var token = await _userManager.GetAuthenticationTokenAsync(user, provider, "access_token");
            return token;
        }
    }
}
