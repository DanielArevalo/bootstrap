using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;


public partial class Nuevo : GlobalWeb
{
    GarantiaService _garantiasservicio = new GarantiaService();


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_garantiasservicio.CodigoPrograma2, "L");

            Site toolBar = (Site)Master;

            toolBar.eventoRegresar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_garantiasservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LlenarDDLLineaCredito();
            LlenarDDLOficina();
        }
    }

    #endregion


    #region EventoBotones


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("ListaGarantias.aspx");
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        LlenarCreditosSinGarantia();
    }


    #endregion


    #region Eventos Grilla


    protected void gvSinGarantias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string codPersona = gvSinGarantia.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_garantiasservicio.CodigoPrograma2 + ".codPersona"] = codPersona;

        string Nradicacion = gvSinGarantia.Rows[e.NewEditIndex].Cells[1].Text;
        Session[_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.Nuevo"] = Nradicacion;

        // Remuevo las sesiones de "Lista de Garantia" para evitar conflictos cuando entre a Detalle.aspx
        // Entro en modo CREACION
        Session.Remove(_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.ListaGarantia");
        Session.Remove("idGarantia");

        Navegar(Pagina.Detalle);
    }


    protected void gvSinGarantia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSinGarantia.PageIndex = e.NewPageIndex;
        LlenarCreditosSinGarantia();
    }


    #endregion


    #region Metodos de Llenado


    // Lleno el DDL de TIPO DE LINEA
    protected void LlenarDDLLineaCredito()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        List<Persona1> lstDatosSolicitud = new List<Persona1>(1);  //Lista de los menus desplegables
        string ListaSolicitada = "LineasCredito";

        try
        {
            lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, Usuario);
            ddlLineasCred.DataSource = lstDatosSolicitud;
            ddlLineasCred.DataTextField = "ListaDescripcion";
            ddlLineasCred.DataValueField = "ListaIdStr";
            ddlLineasCred.DataBind();
        }
        catch (Exception ex)
        {

            VerError("LlenarComboLineasCred:  " + ex.Message);
        }

        ddlLineasCred.Items.Insert(0, new ListItem("Todas las líneas", "0"));
    }


    // Llenar el DDL Oficina
    protected void LlenarDDLOficina()
    {
        OficinaService oficinaService = new OficinaService();
        List<Xpinn.FabricaCreditos.Entities.Oficina> lstOficina = new List<Xpinn.FabricaCreditos.Entities.Oficina>(1);

        try
        {
            int cod = Convert.ToInt32(Usuario.codusuario);
            int consulta = 0;
            consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, Usuario);
            ddlOficina.Visible = true;
            lbloficina.Visible = true;

            if (consulta >= 1)
            {
                ddlOficina.DataSource = oficinaService.ListarOficinas(new Xpinn.FabricaCreditos.Entities.Oficina(), Usuario);
                ddlOficina.DataTextField = "nombre";
                ddlOficina.DataValueField = "codigo";
                ddlOficina.DataBind();
                ddlOficina.Items.Insert(0, new ListItem("Todas las oficinas", "0"));
            }
            else
            {
                ddlOficina.Items.Insert(0, new ListItem(Convert.ToString(Usuario.nombre_oficina), Convert.ToString(Usuario.cod_oficina)));
                ddlOficina.DataBind();
                ddlOficina.Enabled = false;
            }
        }
        catch
        {
            ddlOficina.Visible = false;
            lbloficina.Visible = false;
        }
    }


    // Obtengo el filtro y hago el query de los Creditos CON GARANTIA
    private void LlenarCreditosSinGarantia()
    {
        string filtro = ObtenerFiltroToQuery();

        List<Garantia> lstConsulta = new List<Garantia>(1);

        try
        {
            lstConsulta = _garantiasservicio.ListarSinGarantias(filtro, Usuario);
        }
        catch (Exception ex)
        {
            VerError("LlenarCreditosConGarantia:  " + ex.Message);
        }

        if (lstConsulta.Count == 0)
        {
            lblAvisoNoResultadoGrilla.Visible = true;
            lblNumeroRegistros.Visible = false;
        }
        else
        {
            lblAvisoNoResultadoGrilla.Visible = false;
            lblNumeroRegistros.Text = "Número de registros encontrados: " + lstConsulta.Count.ToString();
            lblNumeroRegistros.Visible = true;
        }

        gvSinGarantia.DataSource = lstConsulta;
        gvSinGarantia.DataBind();
    }


    #endregion


    #region Metodo para obtener filtro de acuerdo a la información suministrada


    // Dependiendo de lo escrito en los campos armo el filtro para filtrar el query a realizar
    private string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;
        string radicacion = txtNumCredito.Text.Trim();
        string lineaCredito = ddlLineasCred.SelectedValue;
        string identificacion = txtIdentificacion.Text.Trim();
        string oficina = ddlOficina.SelectedValue;

        // Filtro radicacion
        if (radicacion != "")
        {
            filtro += " and c.NUMERO_RADICACION like '%" + radicacion + "%'";
        }

        //Filtro linea credito
        if (lineaCredito != "0")
        {
            filtro += " and c.COD_LINEA_CREDITO ='" + lineaCredito + "'";
        }

        //Filtro linea credito
        if (identificacion != "")
        {
            filtro += " and p.IDENTIFICACION like '%" + identificacion + "%'";
        }

        //Filtro linea credito
        if (oficina != "0")
        {
            filtro += " and  p.COD_OFICINA ='" + oficina + "'";
        }

        return filtro;
    }


    #endregion


}