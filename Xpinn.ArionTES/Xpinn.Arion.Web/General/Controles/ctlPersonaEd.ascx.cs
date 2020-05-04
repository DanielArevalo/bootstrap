using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlPersonaEd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ActualizarGridPersonas();
            //if (Session["LSTPERSONAS"] != null)
            //{
                Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
                Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
                ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, (Usuario)Session["usuario"]);
                ddlTipoIdentificacion.DataTextField = "descripcion";
                ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
                ddlTipoIdentificacion.DataBind();
            //}
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodigo", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombres");
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
    

    public string TextCodigo
    {
        set { txtCodigo.Text = value; }
        get { return txtCodigo.Text; }
    }
    
    public string Text
    {
        set { txtIdentificacion.Text = value; }
        get { return txtIdentificacion.Text; }
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
            Persona1 = Persona1Servicio.ConsultaDatosPersona(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
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

    public void AdicionarPersona(string pIdentificacion, Int64 pCodPersona, string pNombre,Int32 pTipoIdentificacion)
    {
        txtIdentificacion.Text = pIdentificacion;
        txtCodigo.Text = pCodPersona.ToString();
        txtNombres.Text = pNombre;
        ddlTipoIdentificacion.SelectedValue = pTipoIdentificacion.ToString();
    }

    public void Editable(bool pEditable)
    {
        txtIdentificacion.Enabled = pEditable;
        ddlTipoIdentificacion.Enabled = pEditable;
        btnConsultaPersonas.Visible = pEditable;
    }

}