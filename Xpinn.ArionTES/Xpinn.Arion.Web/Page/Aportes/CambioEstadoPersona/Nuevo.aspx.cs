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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{    
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.Aportes.Services.AporteServices objAhorraServi = new Xpinn.Aportes.Services.AporteServices();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[objAhorraServi.codigoProgramaCambio + ".id"] != null)
                VisualizarOpciones(objAhorraServi.codigoProgramaCambio, "E");
            else
                VisualizarOpciones(objAhorraServi.codigoProgramaCambio, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;            
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.MostrarConsultar(false);
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.codigoProgramaCambio, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            panelPersona.Enabled = false;
            if (!IsPostBack)
            {
                VerError("");
                ctlBusquedaPersonas.Collapsed(false);
                mvCambioEstado.ActiveViewIndex = 0;
                Poblar.PoblarListaDesplegable("MOTIVO_CAMBIO_ESTADO", ddlCambio, (Usuario)Session["usuario"]);
                cargarddl();
                if (Session[objAhorraServi.codigoProgramaCambio + ".id"] != null)
                {
                    mvCambioEstado.ActiveViewIndex = 1;
                    pDatos.Visible = false;
                    idObjeto = Session[objAhorraServi.codigoProgramaCambio + ".id"].ToString();
                    Session.Remove(objAhorraServi.codigoProgramaCambio + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarConsultar(true);
                    toolBar.MostrarGuardar(false);
                    mvCambioEstado.ActiveViewIndex = 0;
                    Usuario pusuario = (Usuario)Session["usuario"];
                    // Inicializar variables
                    pDatos.Visible = true;
                    txtFechaApertura.ToDateTime = System.DateTime.Now;
                    // Ocultar datos
                    pDatos.Visible = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.codigoProgramaCambio, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected Boolean ValidarDatos()
    {
        String estad = txtEstadoActual.Text;
        if (txtFechaApertura.Text == "")
        {
            VerError("Ingrese la Fecha de Apertura");
            txtFechaApertura.Focus();
            return false;
        }
        if (ddlEstado.SelectedItem.Text==estad)
        {
            VerError("El Usuario tiene el estado " + ddlEstado.SelectedItem.Text);
            return false;
        }
        if (ddlCambio.SelectedIndex ==0)
        {
            VerError("seleccione Motivo de Cambio");
            return false;
        }
        if (ddlEstado.SelectedIndex==0)
        {
            VerError("Cambie el Estado ");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea Cambiar el estado?");
    }

    protected void cargarddl() 
    {
        ddlEstado.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlEstado.Items.Insert(1, new ListItem("HABIL", "A"));
        ddlEstado.Items.Insert(2, new ListItem("INHABIL", "I"));
        ddlEstado.Items.Insert(3, new ListItem("RETIRADO", "R"));
        ddlEstado.DataBind();
    }

    protected void limpiarCampos() 
    {
        ddlCambio.ClearSelection();
        txtFechaApertura.Text = string.Empty;
        txtFechaAfiliacion.Text = "";
        txtEstadoActual.Text = String.Empty;
        ctlPersona.AdicionarPersona("", 0,"",1);
        txtObservaciones.Text = "";
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //String estado  ="";
            //if (ddlEstado.SelectedIndex == 1)
            //    estado = "A";
            //else if (ddlEstado.SelectedIndex == 1)
            //    estado = "I";
            //else estado = "R";
            string estado = ddlEstado.SelectedValue;

            Int64 idAf =0;
            Int64 idco=0;

            Usuario pUsuario = (Usuario)Session["usuario"];

            if(Session["idAfil"]!=null)
                idAf = Convert.ToInt64(Session["idAfil"].ToString());
            string pObservacion = txtObservaciones.Text.Trim() != "" ? txtObservaciones.Text.Trim() : null;
            if (Session["idPersona"] != null)
                objAhorraServi.updateInsertServices(idco,
                    idAf,
                    (Int64)Session["idPersona"],
                    txtEstadoActual.Text,
                    DateTime.ParseExact(txtFechaApertura.Texto, gFormatoFecha, null),
                    estado,
                    int.Parse(ddlCambio.SelectedValue),
                    pObservacion,
                    pUsuario);
            else
                VerError("No se ha determinado el código de la persona");
            limpiarCampos();
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(false);
            mvCambioEstado.ActiveViewIndex = 2;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.codigoProgramaCambio, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(false);
        mvCambioEstado.ActiveViewIndex = 0;
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new AhorroVistaServices();
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(pIdObjeto, (Usuario)Session["usuario"]);

            if (vAhorroVista.observaciones != null)
                txtObservaciones.Text = HttpUtility.HtmlDecode(vAhorroVista.observaciones.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                txtFechaApertura.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString().Trim()));
        }
        //Generar Consulta de la Linea Seleccionada
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.codigoProgramaCambio, "ObtenerDatos", ex);
        }
    }



    protected void cargarCampos(String idCodigo) 
    {
        var arrayDatos = objAhorraServi.getRegistroServices((Usuario)Session["usuario"], idCodigo);

        if (arrayDatos[0].Equals("No se encontraron Registros"))
        {
            VerError("No se encontraron Registros");
            Site toolbar = (Site)Master;
            toolbar.MostrarGuardar(false);
        }
        else
        {
            txtFechaAfiliacion.Text = arrayDatos[0].ToString();
            txtEstadoActual.Text = arrayDatos[1].ToString() =="A" ? "HABIL" : arrayDatos[1].ToString() == "R" ? "RETIRADO" : "INHABIL";
            txtFechaRetiro.Text = arrayDatos[2].ToString();
            Session["idAfil"] = arrayDatos[3].ToString();
            Session["idPersona"] =Convert.ToInt64(arrayDatos[4].ToString());
            ddlEstado.SelectedValue = arrayDatos[1].ToString() != null ? arrayDatos[1] : "0";
        }
    }

    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");

        // Determinar la identificacion
        GridView gvListaAFiliados = (GridView)sender;
        Int64 cod_persona = Convert.ToInt64(gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text);
        String tipo_persona = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[2].Text;
        String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
        String nombre = "";
        Int32 TipoIdent = Convert.ToInt32(gvListaAFiliados.DataKeys[gvListaAFiliados.SelectedRow.RowIndex].Values[1].ToString());

        if (tipo_persona == "Natural")
        {
            nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[5].Text + " " +
                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[6].Text + " " +
                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[7].Text + " " +
                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[8].Text;
        }
        else
        {
            nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[9].Text;
        }
        ctlPersona.AdicionarPersona(identificacion, cod_persona, nombre, TipoIdent);
        if (ctlPersona.DatosPersona(cod_persona) == true)
        {
            // Habilitar la barra de herramientas
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarConsultar(false);
            cargarCampos(identificacion);
            // Ir a la siguiente página
            mvCambioEstado.ActiveViewIndex = 1;
        }
        else
        {
            VerError("No se encontraron datos de las persona");
        }
    }

}