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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;

public partial class Nuevo : GlobalWeb
{
    private AperturaCDATService AperturaService = new AperturaCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AperturaService.codigoprogramaGarantia, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFechaGarantia2.ToDateTime = DateTime.Now;
                cargarDropdown();
                CargarValoresConsulta(pConsulta, AperturaService.codigoprogramaGarantia);
                if (Session[AperturaService.codigoprogramaGarantia + ".consulta"] != null)
                txtFechaAprobacion.ToDateTime = DateTime.Now;
                
               

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para consultar los datos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, AperturaService.codigoprogramaGarantia);
            Actualizar();
            
        }
    }

    /// <summary>
    /// Método para limpiar los datos en pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        LimpiarValoresConsulta(pConsulta, AperturaService.codigoprogramaGarantia);
    }

    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AperturaService.codigoprogramaGarantia + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Méotod para cuando se selecciona un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[AperturaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Método para cambio de página en la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            Site toolBar = (Site)this.Master;
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro();
            lstConsulta = AperturaService.ListarCredito((Usuario)Session["usuario"], filtro);

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
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                toolBar.MostrarGuardar(false);
            }

            Session.Add(AperturaService.codigoprogramaGarantia + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "Actualizar", ex);
        }
    }


    /// <summary>ird
    /// 
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    void cargarDropdown()
    {

        List<Cdat> lstOficina = new List<Cdat>();
        Cdat Data = new Cdat();

        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficinas.DataSource = lstOficina;
            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "cod_oficina";
            ddlOficinas.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficinas.SelectedIndex = 0;
            ddlOficinas.DataBind();
        }
    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion = " + txtNumero_radicacion.Text.Trim();
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (txtLinea_credito.Text.Trim() != "")
            filtro += " and cod_linea_credito = '" + txtLinea_credito.Text.Trim() + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and cod_oficina = '" + ddlOficinas.SelectedValue + "'";

        filtro += "and estado = 'C' and cod_linea_credito Not In (Select pl.cod_linea_credito from parametros_linea pl where pl.cod_parametro = 320)";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }

        return filtro;
    }

    protected void ObtenerDatoss()
    {
        try
        {
            String filtro = obtFiltro();
            Xpinn.FabricaCreditos.Entities.Credito vApe = new Xpinn.FabricaCreditos.Entities.Credito();

            vApe = AperturaService.ConsultarAperturas((Usuario)Session["usuario"], filtro);

                txtValorGarantia.Text += vApe.valor_cuota.ToString();

         

            if (vApe.estado != "0") txtEstadoCredito.Text = vApe.estado;

            Session["nroCDAT"] = vApe.estado;


            if (vApe.moneda != "0") txtMoneda.Text = vApe.moneda;

            if (vApe.identificacion != "0") txtTipoIdentif.Text = vApe.identificacion.ToString();

           

            //RECUPERAR GRILLA DETALLE 
            List<Xpinn.FabricaCreditos.Entities.Credito> lstDetalle = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            lstDetalle = AperturaService.ListarDetalles((Usuario)Session["usuario"], filtro);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
           
                if (lstDetalle.Count > 0)
                {
                   
                    GridView1.DataSource = lstDetalle;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    toolBar.MostrarGuardar(true);
                  
                }
                else
                {
                    InicializarDetalle();
                    toolBar.MostrarGuardar(true);
                   
                }

              
        
        }
           catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "btnContinuarMen_Click", ex);
        }  
    }


    protected void InicializarDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        for (int i = GridView1.Rows.Count; i < 3; i++)
        {
            Site toolBar = (Site)this.Master;
            Detalle_CDAT eApert = new Detalle_CDAT();
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
            toolBar.MostrarGuardar(true);
        }
       
        GridView1.DataSource = lstDetalle;
        GridView1.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
                    VerError("");
                    if (ValidarDatos())
                    {
                        string msj;

                        if (mvPrincipal.ActiveViewIndex == 0)
                        {
                            mvPrincipal.ActiveViewIndex = 1;
                            ObtenerDatos();
                            ObtenerDatoss();

                        }


                        else
                        {
                            foreach (GridViewRow rFila in GridView1.Rows)
                            {
                                CheckBox check = (CheckBox)rFila.FindControl("check");
                                if (check.Checked != false)
                                {


                            msj = "Guardar los datos de ";
                            ctlMensaje.MostrarMensaje("Desea " + msj + "Esta pantalla");
                        }
                    }

                }
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "btnGuardar_Click", ex);
        }
    }

    protected void CALCULAR()
    {
        foreach (GridViewRow rfila in GridView1.Rows)
        {
            txtValorGarantia.Text += rfila.Cells[5].Text;
        }

    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow rfila in GridView1.Rows)
            {
                // Datos de la operación
                Usuario vUsuario = new Usuario();
                vUsuario = (Usuario)Session["Usuario"];
                Xpinn.Tesoreria.Entities.Operacion poperacion = new Xpinn.Tesoreria.Entities.Operacion();
                poperacion.cod_ope = 0;
                poperacion.tipo_ope = 71;
                poperacion.cod_caja = 0;
                poperacion.cod_cajero = 0;
                poperacion.observacion = "operacion realizada";
                poperacion.cod_proceso = null;
                poperacion.fecha_oper = Convert.ToDateTime(txtFechaAprobacion.Text);
                poperacion.fecha_calc = DateTime.Now;

                // Datos del cierre del CDAT a renovar
                Xpinn.FabricaCreditos.Entities.Credito pAperturaCDAT = new Xpinn.FabricaCreditos.Entities.Credito();
                pAperturaCDAT.cod_deudor = Convert.ToInt64(txtIdentificacion.Text);
                pAperturaCDAT.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
                pAperturaCDAT.fecha_aprobacion = Convert.ToDateTime(txtFechaAprobacion.Text);
                pAperturaCDAT.valor_cuota = Convert.ToDecimal(txtValorGarantia.Text);
                pAperturaCDAT.estados = Convert.ToInt32(1); 
                pAperturaCDAT.Codeudor = "0";


                AperturaService.CrearAperturaCDAT(pAperturaCDAT, (Usuario)Session["usuario"], poperacion);

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarGuardar(true);

                mvPrincipal.ActiveViewIndex = 1;
                
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "btnContinuarMen_Click", ex);
        }
    } 


    protected void ObtenerDatos()
    {
        try
        {

            foreach (GridViewRow rfila in gvLista.Rows)
            {
                Site toolBar = (Site)this.Master;
                Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();
                txtNumCred.Text = txtNumero_radicacion.Text;
                txtIdentificacion2.Text = txtIdentificacion.Text;
                txtNombre2.Text = txtNombre.Text;
                txtFechaAprobacion.ToDateTime = DateTime.Now;
                txtLinea2.Text = rfila.Cells[3].Text;
                txtMonto.Text = rfila.Cells[7].Text;
                toolBar.MostrarGuardar(true);
               


               
            }
        }
catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramaGarantia, "obtenerdatos", ex);
        }
    }


    Boolean ValidarDatos()
    {
        if (txtIdentificacion2.Visible == true)
        {
            if (txtIdentificacion2.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }

        if (ddlOficinas.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Moneda");
            return false;
        }
        if (txtIdentificacion.Text == "")
        {
            VerError("Ingrese la identificacion");
            return false;
        }


     


        return true;
    }


    protected List<Detalle_CDAT> ObtenerListaDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Detalle_CDAT eDeta = new Detalle_CDAT();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eDeta.codigo_cdat = Convert.ToInt64(lblcodigo.Text);

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eDeta.identificacion = Convert.ToString(lblidentificacion.Text);

            TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eDeta.identificacion = txtIdentificacion.Text;

            TextBox lblcod_persona = (TextBox)rfila.FindControl("lblcod_persona");
            if (lblcod_persona.Text != "")
                eDeta.cod_persona = Convert.ToInt64(lblcod_persona.Text);

            TextBox lblNombre = (TextBox)rfila.FindControl("lblNombre");
            if (lblNombre.Text != "")
                eDeta.nombres = lblNombre.Text;

            TextBox lblApellidos = (TextBox)rfila.FindControl("lblApellidos");
            if (lblApellidos.Text != "")
                eDeta.apellidos = lblApellidos.Text;

            TextBox lblCiudad = (TextBox)rfila.FindControl("lblCiudad");
            if (lblCiudad.Text != "")
                eDeta.ciudad = lblCiudad.Text;

            TextBox lblDireccion = (TextBox)rfila.FindControl("lblDireccion");
            if (lblDireccion.Text != "")
                eDeta.direccion = lblDireccion.Text;

            TextBox lbltelefono = (TextBox)rfila.FindControl("lbltelefono");
            if (lbltelefono.Text != "")
                eDeta.telefono = lbltelefono.Text;

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                eDeta.principal = 1;
            else
                eDeta.principal = 0;





            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }


}