using System.Collections.Generic;

namespace Flattener.Models
{
   public class Stop
   {
      public string? StopName { get; set; }

      public List<StopObject>? Objects { get; set; }

      public bool HasObjects()
      {
         return Objects != null && Objects.Count > 0;
      }
   }
}
