<%@ Page language="c#" ContentType="image/gif" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="Fath" %>
<%    
	Response.Clear();
    Fath.BarcodeX b = new Fath.BarcodeX();
    b.Data = Request.QueryString["data"];
    string identific = Request.QueryString["identific"];
    b.Orientation = 0;
    b.Symbology = Fath.bcType.EAN128;
    b.ShowText = true;
    b.Font = new System.Drawing.Font("Arial", 8);
    b.BackColor = Color.White;
    b.ForeColor = Color.Black;
    int w = 300;
    int h = 80;
    try
    {
        System.Drawing.Image g = b.Image(w, h);
        g.Save(Server.MapPath("Imagenes\\") + identific + ".png", System.Drawing.Imaging.ImageFormat.Png);
        g.Save(Response.OutputStream, ImageFormat.Png);
    }
    catch
    {
        w = 0;
    }
%>
