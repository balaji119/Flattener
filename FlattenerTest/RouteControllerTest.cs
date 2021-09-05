using Flattener.Controllers;
using Flattener.Models;
using Flattener.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace FlattenerTest
{
   public class RouteControllerTest
   {
      private readonly RouteController _controller;
      public RouteControllerTest()
      {
         _controller = new RouteController(new FlattenService());
      }

      [Fact]
      public void FlattenRoutes_WithValidRoutes_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>
         {
            new Route
            {
               RouteName = "Route 1",
               Stops = new List<Stop>
               {
                  new Stop
                  {
                     StopName = "Stop 1",
                     Objects = new List<StopObject>
                     {
                        new StopObject
                        {
                           ObjectType = "tank",
                           ObjectName = "MT ACE UNIT 3H WATER TANK"
                        },
                        new StopObject
                        {
                           ObjectType = "meter",
                           ObjectName = "MT ACE UNIT 3H WATER METER"
                        }
                     }
                  },
                  new Stop
                  {
                     StopName = "Stop 12",
                     Objects = new List<StopObject>
                     {
                        new StopObject
                        {
                           ObjectType = "tank",
                           ObjectName = "MT ACE UNIT 5H WATER TANK"
                        },
                        new StopObject
                        {
                           ObjectType = "meter",
                           ObjectName = "MT ACE UNIT 5H WATER METER"
                        }
                     }
                  }
               }
            }
         };

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Equal(4, responseObjList.Count);
         Assert.Equal("tank", responseObjList[0].ObjectType);
         Assert.Equal("MT ACE UNIT 3H WATER TANK", responseObjList[0].ObjectName);
         Assert.Equal("Stop 1", responseObjList[0].StopName);
         Assert.Equal("Route 1", responseObjList[0].RouteName);
      }

      [Fact]
      public void FlattenRoutes_WithEmptyObject_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>
         {
            new Route
            {
               RouteName = "Route 1",
               Stops = new List<Stop>
               {
                  new Stop
                  {
                     StopName = "Stop 1",
                     Objects = new List<StopObject>
                     {
                        new StopObject
                        {
                        }
                     }
                  }
               }
            }
         };

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Single(responseObjList);
         Assert.Null(responseObjList[0].ObjectType);
         Assert.Null(responseObjList[0].ObjectName);
         Assert.Equal("Stop 1", responseObjList[0].StopName);
         Assert.Equal("Route 1", responseObjList[0].RouteName);
      }

      [Fact]
      public void FlattenRoutes_WithNoObject_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>
         {
            new Route
            {
               RouteName = "Route 1",
               Stops = new List<Stop>
               {
                  new Stop
                  {
                     StopName = "Stop 1"
                  }
               }
            }
         };

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Single(responseObjList);
         Assert.Null(responseObjList[0].ObjectType);
         Assert.Null(responseObjList[0].ObjectName);
         Assert.Equal("Stop 1", responseObjList[0].StopName);
         Assert.Equal("Route 1", responseObjList[0].RouteName);
      }

      [Fact]
      public void FlattenRoutes_WithEmptyStops_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>
         {
            new Route
            {
               RouteName = "Route 1",
               Stops = new List<Stop>
               {
               }
            }
         };

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Single(responseObjList);
         Assert.Null(responseObjList[0].ObjectType);
         Assert.Null(responseObjList[0].ObjectName);
         Assert.Null(responseObjList[0].StopName);
         Assert.Equal("Route 1", responseObjList[0].RouteName);
      }

      [Fact]
      public void FlattenRoutes_WithNoStops_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>
         {
            new Route
            {
               RouteName = "Route 1"
            }
         };

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Single(responseObjList);
         Assert.Null(responseObjList[0].ObjectType);
         Assert.Null(responseObjList[0].ObjectName);
         Assert.Null(responseObjList[0].StopName);
         Assert.Equal("Route 1", responseObjList[0].RouteName);
      }

      [Fact]
      public void FlattenRoutes_WithEmptyRoutes_ReturnsFlattenedRoutes()
      {
         var routes = new List<Route>();

         var response = _controller.FlattenRoutes(routes);
         var responseObjList = (response.Result as OkObjectResult).Value as List<ResponseObject>;

         Assert.Empty(responseObjList);
      }
   }
}
