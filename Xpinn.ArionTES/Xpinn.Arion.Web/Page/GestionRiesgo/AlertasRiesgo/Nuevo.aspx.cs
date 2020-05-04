using System;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Service;

public partial class Nuevo : GlobalWeb
{
    alertasService AlerRies = new alertasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AlerRies.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AlerRies.CodigoPrograma, "E");
            else
                VisualizarOpciones(AlerRies.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AlerRies.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdIdActividad.Value = "";
                CargarListas();
                if (Session[AlerRies.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AlerRies.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AlerRies.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AlerRies.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();

        ddlPeriocidad.Items.Insert(0, new ListItem("Seleccione un item"));
        ddlPeriocidad.Items.Insert(1, new ListItem("Diaria"));
        ddlPeriocidad.Items.Insert(2, new ListItem("Semanal"));
        ddlPeriocidad.Items.Insert(3, new ListItem("Quincenal"));
        ddlPeriocidad.Items.Insert(4, new ListItem("Mensual"));
        ddlPeriocidad.Items.Insert(5, new ListItem("Trimestral"));
        ddlPeriocidad.Items.Insert(6, new ListItem("Semestral"));
        ddlPeriocidad.Items.Insert(7, new ListItem("Anual"));
        ddlPeriocidad.DataBind();


    }




    bool ValidarDatos()
    {

        if (txtnomAlerta.Text.Trim() == "")
        {
            VerError("Debe ingresar el nombre");
            return false;
        }

        if (txtIndicador.Text.Trim() == "")
        {
            VerError("Debe ingresar el indicador");
            return false;
        }

        if (txtSentenciasql.Text.Trim() == "")
        {
            VerError("Ingrese La formula-sql");
            return false;
        }

        if (txtdescripcion.Text.Trim() == "")
        {
            VerError("Ingrese La descripcion la alerta");
            return false;
        }

        if (ddlPeriocidad.SelectedValue == "0" || ddlPeriocidad.SelectedValue == null)
        {
            VerError("Seleccione la Periocidad que necesita ");
            return false;
        }

        

        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                alertas_ries palertas = new alertas_ries();
                palertas.Cod_Alerta = ConvertirStringToInt(txtCodalerta.Text.Trim());
                palertas.Nom_Alerta = txtnomAlerta.Text;
                palertas.Descripcion = txtdescripcion.Text;
                palertas.Periocidad = ddlPeriocidad.SelectedValue;
                palertas.Sentencia_Sql = txtSentenciasql.Text;
                palertas.Indicador = txtIndicador.Text;




                if (hdIdActividad.Value == "")
                    palertas = AlerRies.Crearalertas(palertas, (Usuario)Session["usuario"]);
                else
                {
                    palertas = AlerRies.Modificaralertas(palertas, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(AlerRies.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {

        alertas_ries pAlerta = new alertas_ries();
        pAlerta.Cod_Alerta = Convert.ToInt64(idObjeto);

        pAlerta = AlerRies.Consultaralertas(pAlerta, (Usuario)Session["usuario"]);

        if (pAlerta != null)
        {
            if (!string.IsNullOrWhiteSpace(pAlerta.Cod_Alerta.ToString()))
                txtCodalerta.Text = pAlerta.Cod_Alerta.ToString();
            if (!string.IsNullOrWhiteSpace(pAlerta.Nom_Alerta.ToString()))
                txtnomAlerta.Text = pAlerta.Nom_Alerta.ToString();
            if (!string.IsNullOrWhiteSpace(pAlerta.Descripcion.ToString()))
                txtdescripcion.Text = pAlerta.Descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pAlerta.Periocidad.ToString()))
                ddlPeriocidad.SelectedValue = pAlerta.Periocidad.ToString();
            if (!string.IsNullOrWhiteSpace(pAlerta.Sentencia_Sql.ToString()))
                txtSentenciasql.Text = pAlerta.Sentencia_Sql.ToString();
            if (!string.IsNullOrWhiteSpace(pAlerta.Indicador.ToString()))
                txtSentenciasql.Text = pAlerta.Indicador.ToString();

        }
    }

}