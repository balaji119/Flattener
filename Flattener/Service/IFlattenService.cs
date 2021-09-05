using Flattener.Models;
using System.Collections.Generic;

namespace Flattener.Service
{
   public interface IFlattenService
   {
      IEnumerable<ResponseObject> Flatten(List<Route> routes);
   }
}