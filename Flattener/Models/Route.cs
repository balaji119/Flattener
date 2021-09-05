using System.Collections.Generic;

namespace Flattener.Models
{
   public class Route
   {
      public string? RouteName { get; set; }

      public List<Stop>? Stops { get; set; }

      public bool HasStops()
      {
         return Stops != null && Stops.Count > 0;
      }
   }
}
