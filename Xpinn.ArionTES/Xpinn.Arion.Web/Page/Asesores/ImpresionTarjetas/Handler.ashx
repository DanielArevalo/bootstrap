<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Common;
using Xpinn.Util;
using Fath;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Fath.BarcodeX b = new Fath.BarcodeX();
        b.Data = context.Request.QueryString["data"];
        string identific = context.Request.QueryString["identific"];
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
            g.Save(context.Response.OutputStream, ImageFormat.Png);
        }
        catch
        {
            w = 0;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}