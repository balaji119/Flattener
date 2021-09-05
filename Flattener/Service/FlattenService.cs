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

         if(route.Stops == null || route.Stops?.Count == 0)
         {
            AddRoute(route.RouteName);
            return;
         }

         foreach (var stop in route.Stops!)
         {
            if (stop == null)
            {
               AddRoute(route.RouteName);
               continue;
            }

            Flatten(stop.Objects, route, stop);
         }
      }

      private void Flatten(List<StopObject> objects, Route route, Stop stop)
      {
         if (objects == null || objects.Count == 0)
         {
            AddRoute(route.RouteName!, stop.StopName);
            return;
         }

         objects.ForEach(obj => Flatten(obj, route, stop));
      }

      private void Flatten(StopObject obj, Route route, Stop stop)
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

      private void AddRoute(string? routeName)
      {
         _responseOjbects.Add(new ResponseObject
         {
            RouteName = routeName
         });
      }

      private void AddRoute(string routeName, string stopName)
      {
         _responseOjbects.Add(new ResponseObject
         {
            RouteName = routeName,
            StopName = stopName
         });
      }
   }
}
