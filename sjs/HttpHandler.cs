
namespace sjs
{
    public class HttpHandler : System.Web.IHttpHandler
    {
        public System.Web.Routing.RequestContext RequestContext { get; set; }

        public HttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            string jsonStr = "{}";
            using (var reader = new System.IO.StreamReader(context.Request.InputStream))
            {
                // This will equal to "charset = UTF-8 & param1 = val1 & param2 = val2 & param3 = val3 & param4 = val4"
                 jsonStr = reader.ReadToEnd();
            }
            var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            var post = json.Deserialize<System.Collections.Generic.Dictionary<string, object>>(jsonStr);

            string tableName = this.RequestContext.RouteData.GetRequiredString("storage");
            System.Data.DataTable dt = sjs.WQL.Select("Select * From " + tableName);


            int totalRowCount = dt.Rows.Count;
            int page = int.Parse(post["page"].ToString());
            int pageSize = int.Parse(post["pageSize"].ToString());
            dt = sjs.Data.SelectPagedDataRows(dt, page, pageSize);
            context.Response.ContentType = "application/json";
            context.Response.Write(sjs.Data.toJSON(dt, page, pageSize, totalRowCount));
        }
    }

    public class RouteHandler : System.Web.Routing.IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            return (System.Web.IHttpHandler)new HttpHandler(requestContext);
        }
    }
}
