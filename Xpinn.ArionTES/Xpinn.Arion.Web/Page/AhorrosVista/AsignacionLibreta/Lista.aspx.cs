using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Configuration;

public partial  class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaLibreta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaLibreta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            if (!IsPostBack)
            {
                cargarddl();
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaLibreta);
                if (Session[AhorroVistaServicio.CodigoProgramaLibreta + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaLibreta, "Page_Load", ex);
        }
    }

    protected List<Xpinn.Comun.Entities.ListasFijas>ListaEstadosLibreta()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaEstadosLibreta();
    }
    private void cargarddl()
    {
      GlobalWeb glob = new GlobalWeb();
      ddlEstado.DataSource = glob.ListaEstadosLibreta();
      ddlEstado.DataTextField = "descripcion";
      ddlEstado.DataValueField = "codigo";
      ddlEstado.DataBind();
     ddlEstado.SelectedIndex = 1;
      


        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[AhorroVistaServicio.CodigoProgramaLibreta + ".id"] = null;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {        
        Actualizar();
    }



    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaLibreta + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        //Session[AhorroVistaServicio.CodigoProgramaLibreta + ".id"] = id;
        //Navegar(Pagina.Nuevo);
    }

    public String getFiltro() 
    {
        String filtro = "";
        if (txtNumeroLibreta.Text.Trim() != "")
            filtro += " and l.numero_libreta = " + txtNumeroLibreta.Text;
        if (ddlOficina.SelectedIndex > 0)
            filtro += " and a.cod_oficina= " + ddlOficina.SelectedValue;
        if (txtAsignacion.Text.Length != 0)
            filtro += " and l.fecha_asignacion = " + txtAsignacion.Text;
        if (txtNumeroCuenta.Text.Trim() != "")
            filtro += " and a.numero_cuenta = " + txtNumeroCuenta.Text;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion= "+txtIdentificacion.Text;
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.nombre =" + txtNombre.Text;
        if (this.ddlEstado.SelectedIndex !=0)
            filtro += " and l.estado = " + ddlEstado.SelectedValue;
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " AND  p.cod_nomina  = '" + txtCodigoNomina.Text + "'";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Int64 id = Convert.ToInt64(gvLista.DataKeys[e.NewEditIndex].Value.ToString());
        Session[AhorroVistaServicio.CodigoProgramaLibreta + ".id"] = id;
        String respuesta = AhorroVistaServicio.validarServices((Usuario)Session["usuario"],Convert.ToInt64(id));

        if (respuesta == "")
            Navegar(Pagina.Nuevo);
        else
            VerError("No puede Modificar Libretas con cupones en uso");
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 ide = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Value.ToString());
            string mensaje = AhorroVistaServicio.validarServices((Usuario)Session["usuario"], ide);

            if (mensaje == "PERMITIR")
            {
                AhorroVistaServicio.eliminarLibretaServices(ide, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                VerError("No se puede eliminar Esta libreta! tiene cupones usados");
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaLibreta, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            
            List<Xpinn.Ahorros.Entities.ELibretas> lista = new List<Xpinn.Ahorros.Entities.ELibretas>();
            lista = AhorroVistaServicio.getAllLibreta(getFiltro(),(Usuario)Session["usuario"]);
            

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lista;

            if (lista.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lista.Count.ToString();
                Session["DTAhorroVista"] = lista;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaLibreta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaLibreta, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
        
        return vAhorroVista;
    }

}