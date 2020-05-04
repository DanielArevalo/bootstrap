using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;

public partial class Lista : GlobalWeb
{
    CajaService Cajaservicio = new CajaService();



    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Cajaservicio.CodigoPrograma2, "L");

            Site toolBar = (Site)this.Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Cajaservicio.CodigoPrograma2, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Cajaservicio.CodigoPrograma2, "Page_Load", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[Cajaservicio.CodigoPrograma2 + ".id"] = id;

        String cod_radica = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String nombre = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[3].Text;

        string[] ListCaja = new string[4];
        ListCaja[0] = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        ListCaja[1] = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        ListCaja[2] = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        ListCaja[3] = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        
     
        Session["ListCaja"] = ListCaja;
        //Session["Cod_caja"] = id;
        //Session["Cod_ofi"] = cod_radica;
        Navegar(Pagina.Nuevo);
    }

    private void Actualizar()
    {
        try
        {
            gvLista.EmptyDataText = emptyQuery;
            Caja caja = new Caja();
            List<Caja> lstCaja = new List<Caja>();
            lstCaja = Cajaservicio.ListarCajaAllOficinas(caja, (Usuario)Session["Usuario"]);

            if (lstCaja.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataSource = lstCaja;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
           

            Session.Add(Cajaservicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Cajaservicio.CodigoPrograma2, "Actualizar", ex);
        }
    }
}