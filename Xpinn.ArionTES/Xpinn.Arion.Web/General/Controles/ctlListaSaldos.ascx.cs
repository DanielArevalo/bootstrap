using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Entities;


public partial class General_Controles_ctlListaSaldos : System.Web.UI.UserControl
{
    private AporteServices aporte_service = new AporteServices();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }
    public void Actualizar(string Persona)
    {

        try
        {
            Aporte Entidad = new Aporte();
            Entidad.cod_persona = Convert.ToInt64(Persona);
            List<Aporte> lstConsulta = new List<Aporte>();
            lstConsulta = aporte_service.ListarSaldos(Entidad, (Usuario)Session["usuario"]);
            gvLista.DataSource = lstConsulta;
            hdf.Value = Persona;

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