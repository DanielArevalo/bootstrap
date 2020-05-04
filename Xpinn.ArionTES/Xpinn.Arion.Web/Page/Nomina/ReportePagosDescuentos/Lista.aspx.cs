using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    PagosDescuentosFijosService _pagosDescuentosServices = new PagosDescuentosFijosService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_pagosDescuentosServices.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
      
        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        string filtro = ObtenerFiltro2();
        ddlConcepto.DataSource = conceptoService.ListarConceptosNomina(filtro, (Usuario)Session["usuario"]);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "consecutivo";
        ddlConcepto.DataBind();
    
        ddlProveedor.DataSource = conceptoService.ListarProveedorDescuentos( (Usuario)Session["usuario"]);
        ddlProveedor.DataTextField = "nombre_tercero";
        ddlProveedor.DataValueField = "identificacion_tercero";
        ddlProveedor.DataBind();

        txtFechaFin.Attributes.Add("readonly", "readonly");
        txtFechaIni.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
      
        txtFechaIni.Text = string.Empty;
        txtFechaFin.Text = string.Empty;
        ddlConcepto.SelectedIndex = 0;
        ddlProveedor.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvPagos.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvPagos, "Pagos");

        gvPagos.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPagos.PageIndex = e.NewPageIndex;
        Actualizar();
    }

  


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<PagosDescuentosFijos> lstConsulta = _pagosDescuentosServices.ListarDescuentosFijosReporte(filtro, Usuario);

            gvPagos.DataSource = lstConsulta;
            gvPagos.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Actualizar", ex);
        }
    }
    string ObtenerFiltro2()
    {
        string filtro = string.Empty;
        filtro += " and tipo=2 and clase=3  and tipoconcepto not in(1,2)";


        return filtro;
    }
    string ObtenerFiltro()
    {
        string filtro = string.Empty;

       

        if (!string.IsNullOrWhiteSpace(txtFechaIni.Text))
        {
            filtro += " and TRUNC(pago.FECHA) >= to_date('" + txtFechaIni.Text + "', 'dd/MM/yyyy') ";
        }

       

        if (!string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue))
        {
            filtro += " and pago.CODIGOCONCEPTONOMINA = " + ddlConcepto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlProveedor.SelectedValue))
        {
            filtro += " and identificacion_proveedor = " + ddlProveedor.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
