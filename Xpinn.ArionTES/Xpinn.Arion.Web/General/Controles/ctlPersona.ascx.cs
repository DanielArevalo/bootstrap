using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlPersona : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ActualizarGridPersonas();
            if (Session["LSTPERSONAS"] != null)
            {
                List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
                lstConsulta = (List<Xpinn.FabricaCreditos.Entities.Persona1>)Session["LSTPERSONAS"];
                txtIdentificacion.DataTextField = "identificacion";
                txtIdentificacion.DataValueField = "cod_persona";
                txtIdentificacion.DataSource = lstConsulta;
                txtIdentificacion.DataBind();

                Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
                Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
                ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, (Usuario)Session["usuario"]);
                ddlTipoIdentificacion.DataTextField = "descripcion";
                ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
                ddlTipoIdentificacion.DataBind();
            }
        }
    }

    protected void txtIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {       
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);
        txtNombres.Text = Persona1.nombre;        
    }

    protected void ActualizarGridPersonas()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        try
        {            
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
            Session["LSTPERSONAS"] = lstConsulta;            
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public string Value
    {
        set { txtIdentificacion.SelectedItem.Value = value; }
        get { return txtIdentificacion.SelectedItem.Value; }

    }

    public string Text
    {
        set { txtIdentificacion.SelectedItem.Text = value; }
        get { return txtIdentificacion.SelectedItem.Text; }

    }

    public bool Requerido
    {
        set { if (rfvIdentificacion != null) rfvIdentificacion.Enabled = value; }
        get { return rfvIdentificacion.Enabled; }

    }

    public bool Habilitar
    {
        set { txtIdentificacion.Enabled = value; ddlTipoIdentificacion.Enabled = value; } 
        get { return txtIdentificacion.Enabled; }
    }

    public bool DatosPersona()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Persona1 = Persona1Servicio.ConsultaDatosPersona(Convert.ToInt64(txtIdentificacion.Text), (Usuario)Session["usuario"]);
            if (Persona1 == null)
                return false;
            txtNombres.Text = Persona1.nombre;
        }
        catch
        {
            return false;
        }
        return true;
    }

    public void AdicionarPersona(string pIdentificacion, Int64 pCodPersona, string pNombre)
    {
        txtIdentificacion.Items.Clear();
        txtIdentificacion.Items.Insert(0, new ListItem(pIdentificacion, pCodPersona.ToString()));
        txtIdentificacion.SelectedItem.Text = pIdentificacion;
        txtNombres.Text = pNombre;
    }

}