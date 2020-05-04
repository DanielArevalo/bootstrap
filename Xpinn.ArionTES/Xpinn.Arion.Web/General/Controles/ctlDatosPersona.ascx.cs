using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlDatosPersona : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
            Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
            ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, (Usuario)Session["usuario"]);
            ddlTipoIdentificacion.DataTextField = "descripcion";
            ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
            ddlTipoIdentificacion.DataBind();
        }
    }


    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        Persona1.seleccionar = "Identificacion";
        Persona1.noTraerHuella = 1;
        Persona1.identificacion = txtIdentificacion.Text;
        Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);

        if (Persona1.cod_persona != 0)
        {
            txtCodigo.Text = Persona1.cod_persona.ToString();
            if (Persona1.tipo_persona == "N")
                txtNombres.Text = Persona1.nombres.Trim() + " " + Persona1.apellidos.Trim();
            else
            {
                if (Persona1.nombres != null)
                    txtNombres.Text = Persona1.nombres;
            }

            if (Persona1.tipo_identificacion != 0)
                ddlTipoIdentificacion.SelectedValue = Persona1.tipo_identificacion.ToString();
        }
        else
        {
            txtCodigo.Text = "";
            txtNombres.Text = "";
            ddlTipoIdentificacion.SelectedIndex = 0;
            txtNombres.Text = "";
        }
    }

    public string CodPersona
    {
        set { txtCodigo.Text = value; }
        get { return txtCodigo.Text; }
    }

    public string Identificacion
    {
        set { txtIdentificacion.Text = value; }
        get { return txtIdentificacion.Text; }
    }

    public bool DatosPersona(Int64 pCodPersona)
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Persona1 = Persona1Servicio.ConsultaDatosPersona(pCodPersona, (Usuario)Session["usuario"]);
            if (Persona1 == null)
                return false;
            txtCodigo.Text = pCodPersona.ToString();
            txtIdentificacion.Text = Persona1.identificacion;
            txtNombres.Text = Persona1.nombre;
            if (Persona1.tipo_identificacion != null)
                ddlTipoIdentificacion.SelectedItem.Value = Persona1.tipo_identificacion.ToString();            
        }
        catch
        {
            return false;
        }
        return true;
    }

    public void AdicionarPersona(string pIdentificacion, Int64 pCodPersona, string pNombre, Int32 pTipoIdentificacion)
    {
        txtIdentificacion.Text = pIdentificacion;
        txtCodigo.Text = pCodPersona.ToString();
        txtNombres.Text = pNombre;
        ddlTipoIdentificacion.SelectedValue = pTipoIdentificacion.ToString();
    }

}