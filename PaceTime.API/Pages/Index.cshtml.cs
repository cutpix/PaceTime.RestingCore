using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PaceTime.API.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; }
            = $"PaceTime.RestingCore - Server time is { DateTime.Now }";

        public void OnGet()
        {
        }
    }
}