﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Movi.Extension.MvcXmlRouting
{
    public static class RouteCollectionExtention
    {
        public static void MapRoute(
            this System.Web.Routing.RouteCollection routes, 
            MvcRouteConfigurationSection section )
        {
            // Manipulate the Ignore List
            foreach(IgnoreItem ignoreItem in section.Ignore)
            {
                RouteValueDictionary ignoreConstraints = new RouteValueDictionary();

                foreach (Constraint constraint in ignoreItem.Constraints)
                    ignoreConstraints.Add(constraint.Name, constraint.Value);

                IgnoreRoute(routes, ignoreItem.Url, ignoreConstraints);
            }

            // Manipluate the Routing Table
            foreach (RoutingItem routingItem in section.Map)
            {
                RouteValueDictionary defaults = new RouteValueDictionary();
                RouteValueDictionary constraints = new RouteValueDictionary();

                if (!string.IsNullOrEmpty(routingItem.Controller))
                    defaults.Add("controller", routingItem.Controller);

                if (!string.IsNullOrEmpty(routingItem.Action))
                    defaults.Add("action", routingItem.Action);

                foreach (Parameter param in routingItem.Paramaters)
                {
                    //防止重复添加controller和action
                    if ((param.Name.Equals("controller",StringComparison.OrdinalIgnoreCase) || param.Name.Equals("action",StringComparison.OrdinalIgnoreCase)) && defaults.ContainsKey(param.Name) && !string.IsNullOrEmpty(param.Value))
                    {
                        defaults.Remove(param.Name);
                    }

                    if (!string.IsNullOrEmpty(param.Name) && !string.IsNullOrEmpty(param.Value))
                    {
                        defaults.Add(param.Name, param.Value);
                    }
                    if (!string.IsNullOrEmpty(param.Constraint))
                    {
                        constraints.Add(param.Name, param.Constraint);
                    }
                }

                MapRoute(routes, routingItem.Name, routingItem.Url, defaults, constraints);
            }
        }

        public static void IgnoreRoute
            (RouteCollection routes, string url, RouteValueDictionary constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            IgnoreRoute ignore = new IgnoreRoute(url);
            ignore.Constraints = constraints;
            routes.Add(ignore);
        }



        public static void MapRoute(
            RouteCollection routes, 
            string name, 
            string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints )
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            Route route = new Route(url, new MvcRouteHandler());
            route.Defaults = defaults;
            route.Constraints = constraints;
            routes.Add(name, route);
        }
    }


    sealed class IgnoreRoute : Route
    {
        // Methods
        public IgnoreRoute(string url)
            : base(url, new StopRoutingHandler())
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
