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
    private CreditoGerencialService CredGerencialServicio = new CreditoGerencialService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CredGerencialServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CredGerencialServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //if (Session[CredGerencialServicio.CodigoPrograma + ".consulta"] != null)
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CredGerencialServicio.CodigoPrograma, "Page_Load", ex);
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
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    /// <summary>
    /// Método para limpiar los datos en pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

      
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();       
        Session[MOV_GRAL_CRED_PRODUC] = producto;
        VerError("");
        // Determinar los datos de la persona seleccionada
        GridView gvListaAFiliados = (GridView)sender;
        String cod_persona = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text;
        String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
        Session[CredGerencialServicio.CodigoPrograma + ".id"] = identificacion;
        producto.Persona.IdPersona = Convert.ToInt64(cod_persona);
        producto.Persona.identificacion = Convert.ToString(identificacion);
        Session["Origen"] = "CGL";// ORIGEN DE LA PAGINA CREDITO GERENCIAL LISTA
        // Ir a la siguiente página
        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
    }

    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            VerError("");
            // Determinar los datos de la persona seleccionada
            GridView gvListaAFiliados = (GridView)sender;
            String cod_persona = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text;
            String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
            Session[CredGerencialServicio.CodigoPrograma + ".id"] = identificacion;
            producto.Persona.IdPersona = Convert.ToInt64(cod_persona);
            producto.Persona.identificacion = Convert.ToString(identificacion);
        }
        catch
        {
            VerError("Se presento error al seleccionar la persona");
        }

        Session["Origen"] = "CGL";// ORIGEN DE LA PAGINA CREDITO GERENCIAL LISTA
        // Ir a la siguiente página
        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
        // Ir a la siguiente página
       // Navegar("~/Page/FabricaCreditos/CreditoGerencial/DatosBasicos.aspx");
    }    


    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {

        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }


    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
}