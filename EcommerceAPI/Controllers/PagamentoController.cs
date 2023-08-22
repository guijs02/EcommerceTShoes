using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using static System.Net.WebRequestMethods;


namespace EcommerceAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        //private NavigationManager _navigationManager;
        public PagamentoController()
        {
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetPagePagamento()
        {
            return Ok();
        }
    }
}
