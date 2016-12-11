namespace sjs
{
    public static class Web
    {
        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Add(new System.Web.Routing.Route("api/{storage}/", new sjs.RouteHandler()));
        }
    }
}
