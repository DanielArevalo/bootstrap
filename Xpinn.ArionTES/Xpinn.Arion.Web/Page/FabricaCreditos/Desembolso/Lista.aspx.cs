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

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CreditoServicio.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                CargarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaoriginal);
                if (Session[CreditoServicio.CodigoProgramaoriginal + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaoriginal);
            Actualizar();
        }       
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        txtNombre.Text = "";
        txtIdentificacion.Text = "";
        txtNumero_radicacion.Text = "";
        txtLinea_credito.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaoriginal);
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session.Remove("Biometria");
        Session.Remove("ValidarBiometria");
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CreditoServicio.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Int64 tipolinea = Convert.ToInt64(gvLista.Rows[e.NewEditIndex].Cells[13].Text);
        Session.Remove("Biometria");
        Session.Remove("ValidarBiometria");
        Session[CreditoServicio.CodigoProgramaoriginal + ".id"] = id;
        if (tipolinea != 2)
        {
            Navegar(Pagina.Editar);
        }
        else
        {
            VerError("No puede Desembolsar. Este credito se genera por el modulo de Credito Rotativo");
            e.NewEditIndex = -1;        
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
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = CreditoServicio.ListarCredito(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CreditoServicio.CodigoProgramaoriginal + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        if (txtNumero_radicacion.Text.Trim() != "")
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            vCredito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if(txtNombre.Text.Trim() != "")
            vCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (ddlOficinas.SelectedIndex != 0)
            vCredito.oficina = ddlOficinas.SelectedValue;
        if(txtLinea_credito.Text.Trim() != "")
            vCredito.linea_credito = Convert.ToString(txtLinea_credito.Text.Trim());

        return vCredito;
    }

    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        ddlOficinas.AppendDataBoundItems = true;
        if (consulta >= 1)
        {
            ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "codigo";
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficinas.Enabled = true;
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficinas.Enabled = false;
        }
        
    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(Credito credito)
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion= " + credito.numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + credito.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and nombres like '%" + credito.nombre + "%'";
        if (txtLinea_credito.Text.Trim() != "")
            filtro += " and cod_linea_credito= '" + credito.linea_credito + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and cod_oficina= '" + credito.oficina + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        //Agregado para consultar proceso anterior a Desembolso según la parametrización de la entidad
        ControlTiempos control = new ControlTiempos();
        CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
        string estado = "Desembolsado";
        control = CreditoSolicitadoServicio.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
        if(control.estado != null && control.estado != "")
            filtro +=" and estado='" + control.estado + "'";
        else
            filtro += " and (estado = 'G')";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
        
    }
    
}