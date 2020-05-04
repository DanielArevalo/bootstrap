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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReferenciaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            //toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboCalificacion(ddlCalificacion);
                if (Session[ReferenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ReferenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ReferenciaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    Navegar(Pagina.Nuevo);
    //}

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    //protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ReferenciaServicio.EliminarReferencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
    //        Navegar(Pagina.Lista);
    //    }
    //    catch (ExceptionBusiness ex)
    //    {
    //        VerError(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "btnEliminar_Click", ex);
    //    }
    //}

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ReferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
            vReferencia = ReferenciaServicio.ConsultarReferencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vReferencia.numero_radicacion.ToString()))
                txtNumero_radicacion.Text = HttpUtility.HtmlDecode(vReferencia.numero_radicacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.identificacion.ToString()))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vReferencia.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(vReferencia.nombres.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.nombre_verificador))
                txtVerificadorRef.Text = HttpUtility.HtmlDecode(vReferencia.nombre_verificador.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.detalle))
                txtDetalleRef.Text = HttpUtility.HtmlDecode(vReferencia.detalle.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.resultado))
                ddlCalificacion.SelectedValue = vReferencia.resultado;
            if (!string.IsNullOrEmpty(vReferencia.oficina))
                txtOficina.Text = HttpUtility.HtmlDecode(vReferencia.oficina.ToString().Trim());

            gvLista.DataSource = CrearDataTable(vReferencia);
            gvLista.DataBind();

            if (!string.IsNullOrEmpty(vReferencia.check_nombre.ToString()))
                if (vReferencia.check_nombre==1)
                    ((CheckBox)gvLista.Rows[0].Cells[3].FindControl("chkValidar")).Checked=true;

            if (!string.IsNullOrEmpty(vReferencia.check_cedula.ToString()))
                if (vReferencia.check_cedula == 1)
                    ((CheckBox)gvLista.Rows[1].Cells[3].FindControl("chkValidar")).Checked = true;

            if (!string.IsNullOrEmpty(vReferencia.check_direccion.ToString()))
                if (vReferencia.check_direccion == 1)
                    ((CheckBox)gvLista.Rows[2].Cells[3].FindControl("chkValidar")).Checked = true;

            if (!string.IsNullOrEmpty(vReferencia.check_parentesco.ToString()))
                if (vReferencia.check_parentesco == 1)
                    ((CheckBox)gvLista.Rows[3].Cells[3].FindControl("chkValidar")).Checked = true;

            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    public DataTable CrearDataTable(Referencia vReferencia)
    {
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("cod_referencia");
        table.Columns.Add("Columnas");
        table.Columns.Add("Valor");
        table.Columns.Add("Check");

        DataRow datarw;

        datarw = table.NewRow();
        datarw[0] = 1;
        datarw[1] = "Nombre";
        datarw[2] = vReferencia.nombre_referenciado;
        table.Rows.Add(datarw);

        datarw = table.NewRow();
        datarw[0] = 2;
        datarw[1] = "Cedula";
        datarw[2] = vReferencia.cedula_referenciado;
        table.Rows.Add(datarw);

        datarw = table.NewRow();
        datarw[0] = 3;
        datarw[1] = "Dirección";
        datarw[2] = vReferencia.direccion_referenciado;
        table.Rows.Add(datarw);

        datarw = table.NewRow();
        datarw[0] = 4;
        datarw[1] = "Parentesco";
        datarw[2] = vReferencia.vinculo;
        table.Rows.Add(datarw);


        return table;
    }

    protected void LlenarComboCalificacion(DropDownList ddlCalificacion)
    {
        CalificacionReferenciasService calificacionService = new CalificacionReferenciasService();
        CalificacionReferencias calificacion = new CalificacionReferencias();

        ddlCalificacion.DataSource = calificacionService.ListarCalificacionReferencias(calificacion, (Usuario)Session["usuario"]);
        ddlCalificacion.DataTextField = "nombre";
        ddlCalificacion.DataValueField = "tipocalificacionref";
        ddlCalificacion.DataBind();
        ddlCalificacion.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
}