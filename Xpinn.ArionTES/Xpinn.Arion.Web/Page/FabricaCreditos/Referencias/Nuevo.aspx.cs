using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ReferenciaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ReferenciaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ReferenciaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();

            //if (idObjeto != "")
                //vReferencia = ReferenciaServicio.ConsultarReferencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
                //vReferencia.identificacion = Convert.ToInt64(txtIdentificacion.Text.Trim());
                //vReferencia.primer_apellido = Convert.ToString(txtPrimer_apellido.Text.Trim());
                //vReferencia.segundo_apellido = Convert.ToString(txtSegundo_apellido.Text.Trim());
                //vReferencia.nombres = Convert.ToString(txtNombres.Text.Trim());
                //vReferencia.referenciado = Convert.ToString(txtDireccionRef.Text.Trim());
                //vReferencia.oficina = Convert.ToString(txtNombreRef.Text.Trim());
                //vReferencia.tipo_referencia = Convert.ToString(txtTipo_referencia.Text.Trim());
                //vReferencia.nombre_referenciado = Convert.ToString(txtTelefonoRef.Text.Trim());
                //vReferencia.estado = Convert.ToString(txtCiudadRef.Text.Trim());
                //vReferencia.linea_credito = Convert.ToString(txtResultadoRef.Text.Trim());
                vReferencia.cod_referencia = Convert.ToInt64(idObjeto);
            vReferencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);

            vReferencia.codigo_verificador = ((Usuario)Session["usuario"]).codusuario;
            //vReferencia.codigo_verificador = ((Usuario)Session["usuario"]).idUsuario;

            //vReferencia.resultado = Convert.ToString(txtResultadoRef.Text.Trim());
            vReferencia.detalle = Convert.ToString(txtDetalleRef.Text.Trim());
            vReferencia.check_nombre = 0;
            vReferencia.check_cedula = 0;
            vReferencia.check_direccion = 0;
            vReferencia.check_parentesco = 0;

            if (((CheckBox)gvLista.Rows[0].Cells[3].FindControl("chkValidar")).Checked)
                vReferencia.check_nombre = 1;
            if (((CheckBox)gvLista.Rows[1].Cells[3].FindControl("chkValidar")).Checked)
                vReferencia.check_cedula = 1;
            if (((CheckBox)gvLista.Rows[2].Cells[3].FindControl("chkValidar")).Checked)
                vReferencia.check_direccion = 1;
            if (((CheckBox)gvLista.Rows[3].Cells[3].FindControl("chkValidar")).Checked)
                vReferencia.check_parentesco = 1;

            vReferencia.resultado=ddlCalificacion.SelectedValue;
            
            if (idObjeto != "")
            {
                //vReferencia.numero_radicacion = Convert.ToInt64(idObjeto);
                ReferenciaServicio.ModificarReferencia(vReferencia, (Usuario)Session["usuario"]);
            }
            else
            {
                //vReferencia = ReferenciaServicio.CrearReferencia(vReferencia, (Usuario)Session["usuario"]);
                //idObjeto = vReferencia.numero_radicacion.ToString();
            }

            Session[ReferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ReferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
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
            if (!string.IsNullOrEmpty(vReferencia.oficina))
                txtOficina.Text = HttpUtility.HtmlDecode(vReferencia.oficina.ToString().Trim());
            //if (!string.IsNullOrEmpty(vReferencia.nombre_referenciado))
            //    txtNombreRef.Text = HttpUtility.HtmlDecode(vReferencia.nombre_referenciado.ToString().Trim());
            //if (!string.IsNullOrEmpty(vReferencia.direccion_referenciado))
            //    txtDireccionRef.Text = HttpUtility.HtmlDecode(vReferencia.direccion_referenciado.ToString().Trim());
            //if (!string.IsNullOrEmpty(vReferencia.telefono_referenciado.ToString()))
            //    txtTelefonoRef.Text = HttpUtility.HtmlDecode(vReferencia.telefono_referenciado.ToString().Trim());
            //if (!string.IsNullOrEmpty(vReferencia.tipo_referencia))
            //    ddlTipoReferencia.SelectedValue = vReferencia.tipo_referencia;
            //if (!string.IsNullOrEmpty(vReferencia.referenciado))
            //    ddlReferenciado.SelectedValue = vReferencia.referenciado;
            //if (!string.IsNullOrEmpty(vReferencia.vinculo))
            //    ddlVinculo.SelectedValue = vReferencia.vinculo;
            //if (!string.IsNullOrEmpty(vReferencia.resultado))
            //    txtResultadoRef.Text = HttpUtility.HtmlDecode(vReferencia.resultado.ToString().Trim());

            if (!string.IsNullOrEmpty(vReferencia.nombre_verificador))
                txtVerificadorRef.Text = HttpUtility.HtmlDecode(vReferencia.nombre_verificador.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.detalle))
                txtDetalleRef.Text = HttpUtility.HtmlDecode(vReferencia.detalle.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.resultado))
                ddlCalificacion.SelectedValue = vReferencia.resultado;
            //txtVerificadorRef.Text = ((Usuario)Session["usuario"]).nombre;

            //txtFechaRef.Text = DateTime.Today.ToString("dd-MM-yyyy"); 
            
            gvLista.DataSource = CrearDataTable(vReferencia);
            gvLista.DataBind();

            if (!string.IsNullOrEmpty(vReferencia.check_nombre.ToString()))
                if (vReferencia.check_nombre == 1)
                    ((CheckBox)gvLista.Rows[0].Cells[3].FindControl("chkValidar")).Checked = true;

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
}