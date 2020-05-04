using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;


public class ListarCreditosPorFiltroArgs : EventArgs
{
    public ListarCreditosPorFiltroArgs(string codPersona, string noRadicacion, string noLinea, string error)
    {
        CodPersona = codPersona;
        Nradicacion = noRadicacion;
        Error = error;
        NrLinea = noLinea;
    }

    public ListarCreditosPorFiltroArgs(string codPersona, string noRadicacion, string noLinea) : this(codPersona, noRadicacion, noLinea, "") { }

    public ListarCreditosPorFiltroArgs(string codPersona, string noRadicacion) : this(codPersona, noRadicacion, "", "") { }

    public ListarCreditosPorFiltroArgs() { }

    public string CodPersona { get; private set; }
    public string Nradicacion { get; private set; }
    public string NrLinea { get; private set; }
    public string Error { get; private set; }
}

public partial class ctlListarCreditosPorFiltro : UserControl
{
    public event EventHandler<ListarCreditosPorFiltroArgs> NuevaPagina;
    public event EventHandler<ListarCreditosPorFiltroArgs> Error;

    CreditoService _creditoServicio = new CreditoService();
    Usuario _usuario;
    string _filtroDefinido;
    bool _exigirFiltro;

    // Se usa ViewState para evitar que se me pierdan las variables globales tras postbacks y que no me haga cosas raras si dejo sessiones regada, 
    // Por eso mejor usar ViewState
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (ViewState["ViewStateDeFiltroParaNoPerderlosEnPostBacks"] != null)
                _filtroDefinido = (string)ViewState["ViewStateDeFiltroParaNoPerderlosEnPostBacks"];
            if (ViewState["ViewStateDeEXIGIRFILTROParaNoPerderlosEnPostBacks"] != null)
                _exigirFiltro = (bool)ViewState["ViewStateDeEXIGIRFILTROParaNoPerderlosEnPostBacks"];
            _usuario = (Usuario)Session["Usuario"];
        }
    }


    #region  Metodo para Inicializar Control


    // Se puede definir un filtro permanente para los creditos en el primer parametro
    // Definir el filtro como un T-SQL query comenzando desde el WHERE
    // El segundo parametro obliga a que filtres usando los filtros de la grilla:D
    public void CargaInicial(string filtro, bool exigirFiltro = false)
    {
        _usuario = (Usuario)Session["Usuario"];
        LlenarDDLLineaCredito();
        LlenarDDLOficina();
        _filtroDefinido = filtro;
        _exigirFiltro = exigirFiltro;
        ViewState.Add("ViewStateDeFiltroParaNoPerderlosEnPostBacks", _filtroDefinido);
        ViewState.Add("ViewStateDeEXIGIRFILTROParaNoPerderlosEnPostBacks", _exigirFiltro);
    }


    #endregion


    #region Evento para Consultar con filtros y limpiar


    public void Consultar(object sender, EventArgs e)
    {
        if (Error != null)
        {
            Error(this, new ListarCreditosPorFiltroArgs("", ""));
        }

        LlenarCreditosPorFiltros();
    }


    public void Limpiar(object sender, EventArgs e)
    {
        txtNumCredito.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        ddlLineasCred.SelectedIndex = 0;
        ddlOficina.SelectedIndex = 0;
        txtCodigoNomina.Text = string.Empty;
    }


    #endregion


    #region Eventos Grilla


    protected void gvSinGarantias_SelectIndexChanged(object sender, EventArgs e)
    {
        string Nradicacion = gvSinGarantia.SelectedRow.Cells[1].Text;

        string codLinea = gvSinGarantia.SelectedRow.Cells[2].Text;

        string codPersona = gvSinGarantia.SelectedRow.Cells[4].Text;

        if (NuevaPagina != null)
        {
            NuevaPagina(this, new ListarCreditosPorFiltroArgs(codPersona, Nradicacion, codLinea));
        }
    }


    protected void gvSinGarantia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSinGarantia.PageIndex = e.NewPageIndex;
        LlenarCreditosPorFiltros();
    }


    #endregion


    #region Metodos de Llenado


    // Lleno el DDL de TIPO DE LINEA
    protected void LlenarDDLLineaCredito()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        string ListaSolicitada = "LineasCredito";

        try
        {
            List<Persona1> lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
            ddlLineasCred.DataSource = lstDatosSolicitud;
            ddlLineasCred.DataTextField = "ListaDescripcion";
            ddlLineasCred.DataValueField = "ListaIdStr";
            ddlLineasCred.DataBind();
        }
        catch (Exception ex)
        {
            if (Error != null)
            {
                Error(this, new ListarCreditosPorFiltroArgs("", "", "", "Error en Control ListarCredito, LlenarDDLLineaCredito:  " + ex.Message));
            }
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
            int cod = Convert.ToInt32(_usuario.codusuario);
            int consulta = 0;
            consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, _usuario);
            ddlOficina.Visible = true;
            lbloficina.Visible = true;

            if (consulta >= 1)
            {
                ddlOficina.DataSource = oficinaService.ListarOficinas(new Xpinn.FabricaCreditos.Entities.Oficina(), _usuario);
                ddlOficina.DataTextField = "nombre";
                ddlOficina.DataValueField = "codigo";
                ddlOficina.DataBind();
                ddlOficina.Items.Insert(0, new ListItem("Todas las oficinas", "0"));
            }
            else
            {
                ddlOficina.Items.Insert(0, new ListItem(Convert.ToString(_usuario.nombre_oficina), Convert.ToString(_usuario.cod_oficina)));
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
    void LlenarCreditosPorFiltros()
    {
        if (_exigirFiltro)
        {
            bool tengoFiltro = ValidarSiTengoFiltros();

            if (!tengoFiltro)
            {
                if (Error != null)
                {
                    Error(this, new ListarCreditosPorFiltroArgs("", "", "", "Debe consultar obligatoriamente usando filtros"));
                }

                return;
            }
        }

        string filtroGrilla = ObtenerFiltroToQuery();

        List<Credito> lstConsulta = null;

        try
        {
            lstConsulta = _creditoServicio.ListarCreditosPorFiltro(_filtroDefinido, filtroGrilla, _usuario);
        }
        catch (Exception ex)
        {
            if (Error != null)
            {
                Error(this, new ListarCreditosPorFiltroArgs("", "", "", "Error en Control ListarCredito, LlenarCreditosPorFiltros:  " + ex.Message));
            }
            return;
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

    private bool ValidarSiTengoFiltros()
    {
        if (string.IsNullOrWhiteSpace(txtNumCredito.Text) &&
            string.IsNullOrWhiteSpace(txtIdentificacion.Text) &&
            string.IsNullOrWhiteSpace(txtPrimerNombre.Text) &&
            string.IsNullOrWhiteSpace(txtPrimerApellido.Text) &&
            string.IsNullOrWhiteSpace(txtCodigoNomina.Text) &&
            ddlLineasCred.SelectedValue == "0" &&
            ddlOficina.SelectedValue == "0")
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    #endregion


    #region Metodo para obtener filtro de acuerdo a la información suministrada


    // Dependiendo de lo escrito en los campos armo el filtro para filtrar el query a realizar
    string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;
        string radicacion = txtNumCredito.Text.Trim();
        string lineaCredito = ddlLineasCred.SelectedValue;
        string identificacion = txtIdentificacion.Text.Trim();
        string oficina = ddlOficina.SelectedValue;
        string primerNombre = txtPrimerNombre.Text;
        string primerApellido = txtPrimerApellido.Text;
        string CodigoNomina = txtCodigoNomina.Text;

        // Filtro radicacion
        if (!string.IsNullOrWhiteSpace(radicacion))
        {
            filtro += " and c.NUMERO_RADICACION like '%" + radicacion + "%'";
        }

        //Filtro linea credito
        if (lineaCredito != "0")
        {
            filtro += " and c.COD_LINEA_CREDITO ='" + lineaCredito + "'";
        }

        //Filtro linea credito
        if (!string.IsNullOrWhiteSpace(identificacion))
        {
            filtro += " and p.IDENTIFICACION like '%" + identificacion + "%'";
        }

        //Filtro linea credito
        if (oficina != "0")
        {
            filtro += " and  p.COD_OFICINA ='" + oficina + "'";
        }

        //Filtro linea credito
        if (!string.IsNullOrWhiteSpace(primerNombre))
        {
            filtro += " and p.PRIMER_NOMBRE like '%" + primerNombre.ToUpperInvariant() + "%'";
        }

        //Filtro linea credito
        if (!string.IsNullOrWhiteSpace(primerApellido))
        {
            filtro += " and p.PRIMER_APELLIDO like '%" + primerApellido.ToUpperInvariant() + "%'";
        }

        //Filtro código de nómina
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + CodigoNomina.ToUpperInvariant() + "%'";

        return filtro;
    }


    #endregion


}
