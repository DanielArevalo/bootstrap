using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
    JurisdiccionDepaServices _JurisdiccionDepa = new JurisdiccionDepaServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_JurisdiccionDepa.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_JurisdiccionDepa.CodigoPrograma, "E");
            else
                VisualizarOpciones(_JurisdiccionDepa.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_JurisdiccionDepa.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlTipo.SelectedValue = "D";
                CargarListas();
                if (Session[_JurisdiccionDepa.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_JurisdiccionDepa.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_JurisdiccionDepa.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_JurisdiccionDepa.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        if (ddlTipo.SelectedValue == "C")
        {
            poblar.PoblarListaDesplegable("CIUDADES", "CODCIUDAD, NOMCIUDAD", "TIPO = 3", "", ddlDep, (Usuario)Session["usuario"]);
        }
        else if(ddlTipo.SelectedValue == "P")
        {
            poblar.PoblarListaDesplegable("CIUDADES", "CODCIUDAD, NOMCIUDAD", "TIPO = 1", "", ddlDep, (Usuario)Session["usuario"]);
        }
        else{
            poblar.PoblarListaDesplegable("CIUDADES", "CODCIUDAD, NOMCIUDAD", "TIPO = 2", "", ddlDep, (Usuario)Session["usuario"]);
        }     
    }

    private bool ValidarDatos()
    {

        if (ddlDep.SelectedValue == null)
        {
            VerError("Seleccione un departamento de riesgo");
            return false;
        }
        if (ddlValor.SelectedValue == "0" || ddlValor.SelectedValue == null)
        {
            VerError("Ingrese una valoracion para  la actividad");
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
                JurisdiccionDepa pJurisdiccionDepa = new JurisdiccionDepa();
                pJurisdiccionDepa.Cod_Depa = Convert.ToInt64(ddlDep.SelectedValue.Trim());
                pJurisdiccionDepa.Nombre = ddlDep.SelectedItem.Text;
                pJurisdiccionDepa.valoracion = Convert.ToString(ddlValor.SelectedValue.Trim());

                if (hdIdjurisdic.Value == "")
                    pJurisdiccionDepa = _JurisdiccionDepa.CrearJurisdiccionDepa(pJurisdiccionDepa, (Usuario)Session["usuario"]);
                else
                {
                    pJurisdiccionDepa.Cod_Depa = Convert.ToInt64(ddlDep.SelectedValue.Trim());
                    pJurisdiccionDepa = _JurisdiccionDepa.ModificarJurisdiccionDepa(pJurisdiccionDepa, (Usuario)Session["usuario"]);
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
        Session.Remove(_JurisdiccionDepa.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        JurisdiccionDepa pJurisdiccionDepa = new JurisdiccionDepa();
        pJurisdiccionDepa.Cod_Depa = Convert.ToInt64(idObjeto);

        pJurisdiccionDepa = _JurisdiccionDepa.ConsultarFactorRiesgo(pJurisdiccionDepa, (Usuario)Session["usuario"]);

        if (pJurisdiccionDepa != null)
        {
            if (!string.IsNullOrWhiteSpace(pJurisdiccionDepa.Tipo.ToString()))
                if (pJurisdiccionDepa.Tipo == 3)
                { 
                    ddlTipo.SelectedValue = "C";
                    CargarListas();
                }
            if (!string.IsNullOrWhiteSpace(pJurisdiccionDepa.Id_Jurid.ToString()))
                hdIdjurisdic.Value = pJurisdiccionDepa.Id_Jurid.ToString();
            if (!string.IsNullOrWhiteSpace(pJurisdiccionDepa.Cod_Depa.ToString()))
                ddlDep.SelectedValue = pJurisdiccionDepa.Cod_Depa.ToString();
            if (!string.IsNullOrWhiteSpace(pJurisdiccionDepa.valoracion.ToString()))
                ddlValor.SelectedValue = pJurisdiccionDepa.valoracion.ToString();
        }
    }

    protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CargarListas();
    }


}