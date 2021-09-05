using Flattener.Models;
using Flattener.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Flattener.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class RouteController : Controller
   {
      private readonly IFlattenService _routeService;

      public RouteController(IFlattenService routeService)
      {
         _routeService = routeService;
      }

      [HttpPost]
      public ActionResult<IEnumerable<ResponseObject>> FlattenRoutes([FromBody] List<Route> routes)
      {
         try
         {
            return Ok(_routeService.Flatten(routes));
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error flattening routes");
         }
      }
   }
}
