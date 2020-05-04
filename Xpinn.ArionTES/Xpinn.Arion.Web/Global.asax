<%@ Application Language="C#"  %>

<%@ Import Namespace="Xpinn.Util" %>
<%@ Import Namespace="Xpinn.Seguridad.Services" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        ConnectionDataBase.ObtenerInstanciaAuditoria = () => new AuditoriaStoredProceduresService();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        // Solo muestra el error si el codigo no se esta ejecutando remotamente
        //Usuario = (Usuario)Session["Usuario"]

        if (!HttpContext.Current.Request.IsLocal && Session["esSuperUsuario"] == null)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            var prevPage = Request.Url.ToString();
            Server.ClearError();
            Response.Clear();

            Session["errorAplicacionLogear"] = objErr;
            Session["paginaAnterior"] = prevPage;
            Response.Redirect("~/General/Global/error.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("X-Frame-Options");
        Response.AddHeader("X-Frame-Options", "AllowAll");
    }

</script>
