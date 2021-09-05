using Flattener.Models;
using System.Collections.Generic;
using System.Linq;

namespace Flattener.Service
{
   public class FlattenService : IFlattenService
   {
      private readonly List<ResponseObject> _responseOjbects;
      public FlattenService()
      {
         _responseOjbects = new List<ResponseObject>();
      }

      public IEnumerable<ResponseObject> Flatten(List<Route> routes)
      {
         routes.ForEach(r => Flatten(r));

         return _responseOjbects;
      }

      private void Flatten(Route? route)
      {
         if (route == null) return;

         if(!route.HasStops())
         {
            AddRoute(route.RouteName, null);
            return;
         }

         foreach (var stop in route.Stops!)
         {
            if (stop == null)
            {
               AddRoute(route.RouteName, null);
               continue;
            }

            Flatten(route, stop);
         }
      }

      private void Flatten(Route route, Stop stop)
      {
         if (!stop.HasObjects())
         {
            AddRoute(route.RouteName!, stop.StopName);
            return;
         }

         stop.Objects!.ForEach(obj => Flatten(route, stop, obj));
      }

      private void Flatten(Route route, Stop stop, StopObject obj)
      {
         if (obj == null)
         {
            AddRoute(route.RouteName!, stop.StopName);
            return;
         }

         _responseOjbects.Add(new ResponseObject
         {
            RouteName = route.RouteName,
            StopName = stop.StopName,
            ObjectName = obj.ObjectName,
            ObjectType = obj.ObjectType
         });
      }

      private void AddRoute(string? routeName, string? stopName)
      {
         _responseOjbects.Add(new ResponseObject
         {
            RouteName = routeName,
            StopName = stopName
         });
      }
   }
}
