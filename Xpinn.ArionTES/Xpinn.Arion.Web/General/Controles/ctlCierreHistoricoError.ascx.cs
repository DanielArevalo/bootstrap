using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Services;
using Xpinn.Comun.Entities;

public partial class ctlCierreHistoricoError : System.Web.UI.UserControl
{
    private AhorroVistaServices ahorroservice_ = new AhorroVistaServices();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }
    public void Actualizar(string Tipo)
    {

        try
        {
            List<Cierea> lstConsulta = new List<Cierea>();
            lstConsulta = ahorroservice_.ListarErrorCierre(Tipo, (Usuario)Session["usuario"]);
            Session["lstErrores"] = lstConsulta;
            gvLista.DataSource = lstConsulta;
            hdf.Value = Tipo;
            
            if (lstConsulta.Count > 0)
            {
                lblTitulo.Visible = true;
                gvLista.Visible = true;
                Count.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();               
                gvLista.DataBind();
            }
            else
            {
                lblTitulo.Visible = false;
                gvLista.Visible = false;
                Count.Text = "<br/> No se encuentron errores en el cierre ";
            }
        }
        catch (Exception ex)
        {
            Count.Visible = true;
            Count.Text = ex.ToString();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {       
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(hdf.Value);
    }

    public GridViewRowCollection Rows
    {
        get { return gvLista.Rows; }
    }

}