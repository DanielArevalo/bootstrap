using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using Xpinn.Util;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            TextBox identificacion = Master.FindControl("IDENTIFICACION") as TextBox;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnContinuar.Click += btnContinuar_Click;
        if (!Page.IsPostBack)
        {
            buildSecure();
            cargarCombos();
        }
    }

    protected bool buildSecure()
    {
        string key = ConfigurationManager.AppSettings["key"].ToString();
        string usr = ConfigurationManager.AppSettings["usr"].ToString();
        string ip = "";
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress address in ipHostInfo.AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
                ip = address.ToString();
        }
        if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(usr) || !string.IsNullOrEmpty(ip))
        {
            CifradoBusiness cifrar = new CifradoBusiness();
            string sec = ip + ";" + usr + ";" + key;
            sec = cifrar.Encriptar(sec);
            Session["sec"] = sec;
        }
        return true;
    }

    protected void cargarCombos()
    {
        if (Session["sec"] == null)
            Session["sec"] = "";
        //LLenando CheckBoxList Tipo Identificacion
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoIdenti;
        /*Validacion por empresa debido a que la cooperativa Chipaque solicita que los tipos de
         documentos solo sean CEDULA Y CEDULA EXTRANJERIA*/
        string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
        if (empresa == "COOCHIPAQUE")
        {
            lstTipoIdenti = EstadoServicio.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", " CODTIPOIDENTIFICACION IN (1,4)", "2", Session["sec"].ToString());
            if (lstTipoIdenti.Count > 0)
            {
                for (int i = 0; i < lstTipoIdenti.Count(); i++)
                {
                    ddlDocumento.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
                }
            }
        }
        else
        {
            lstTipoIdenti = EstadoServicio.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", " CODTIPOIDENTIFICACION IN (1,4,5,7,8)", "2", Session["sec"].ToString());
            if (lstTipoIdenti.Count > 0)
            {
                for (int i = 0; i < lstTipoIdenti.Count(); i++)
                {
                    ddlDocumento.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
                }
            }
        }
    }

    protected void txtNumero_TextChanged(object sender, EventArgs e)
    {
        cargarDatosPrevios();
    }

    protected void ddlDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarDatosPrevios();
    }

    private void cargarDatosPrevios()
    {
        //Valida que no exista el usuario y luego carga datos de clientes potenciales
        xpinnWSEstadoCuenta.Persona1 existe = EstadoServicio.ConsultarPersonaExiste(txtNumero.Text);
        if (existe != null)
        {
            if (existe.cod_persona > 0)
            {
                switch (existe.estado)
                {
                    case "A":   
                        //if (existe.bandera==2) { lblError.Text = "La identificación ingresada pertenece a un tercero,codeudor u otros."; } else { lblError.Text = "La identificación ingresada ya cuenta con un usuario creado."; }                        
                        lblError.Text = "La identificación ingresada pertenece a un tercero,codeudor u otros.";
                        return;
                    case "R":
                        lblError.Text = "La identificación ingresada cuanta con un registro con estado RETIRADO, comuniquese con la entidad para la activar su cuenta.";
                        return;
                    default:
                        lblError.Text = "La identificación ingresada ya cuenta con un usuario creado.";
                        break;
                }
                return;
            }
        }

        string filtro = " and s.identificacion = '" + txtNumero.Text + "' ";
        pEntidad = EstadoServicio.ConsultarPersonaAfi(filtro);
        if (pEntidad.id_persona > 0)
        {
            //cliente.cod_persona 
            if (pEntidad.primer_nombre != null) txtNombre1.Text = pEntidad.primer_nombre;
            if (pEntidad.segundo_nombre != null) txtNombre2.Text = pEntidad.segundo_nombre;
            if (pEntidad.primer_apellido != null) txtApellido1.Text = pEntidad.primer_apellido;
            if (pEntidad.segundo_apellido != null) txtApellido2.Text = pEntidad.segundo_apellido;
        }
        else
        //Traer datos de clientes potenciales        
        if (!string.IsNullOrWhiteSpace(txtNumero.Text) && !string.IsNullOrWhiteSpace(ddlDocumento.SelectedValue.ToString()))
        {
            xpinnWSEstadoCuenta.Persona1 cliente = new xpinnWSEstadoCuenta.Persona1();
            cliente.identificacion = txtNumero.Text;
            cliente.tipo_identificacion = Convert.ToInt64(ddlDocumento.SelectedValue);
            cliente = EstadoServicio.ConsultarDatosCliente(cliente, Session["sec"].ToString());
            if (cliente != null)
            {
                //cliente.cod_persona 
                if (cliente.primer_nombre != null) txtNombre1.Text = cliente.primer_nombre;
                if (cliente.segundo_nombre != null) txtNombre2.Text = cliente.segundo_nombre;
                if (cliente.primer_apellido != null) txtApellido1.Text = cliente.primer_apellido;
                if (cliente.segundo_apellido != null) txtApellido2.Text = cliente.segundo_apellido;
            }
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        pEntidad.id_persona = 0;
        pEntidad.identificacion = txtNumero.Text;
        pEntidad.tipo_identificacion = Convert.ToInt64(ddlDocumento.SelectedValue);
        pEntidad.primer_nombre = txtNombre1.Text.ToUpper().Trim();
        pEntidad.segundo_nombre = txtNombre2.Text.Trim() != "" ? txtNombre2.Text.ToUpper().Trim() : null;
        pEntidad.primer_apellido = txtApellido1.Text.ToUpper().Trim();
        pEntidad.segundo_apellido = txtApellido2.Text.Trim() != "" ? txtApellido2.Text.ToUpper().Trim() : null;
        pEntidad.tipo_persona = "N";
        //ALMACENAR INFORMACION
        pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 1, Session["sec"].ToString());
        if (pEntidad.id_persona > 0)
        {
            string filtro = " and s.id_persona = " + pEntidad.id_persona + " ";
            pEntidad = EstadoServicio.ConsultarPersonaAfi(filtro);
        }
        //ALMACENAR INFORMACION
        Session["afiliacion"] = pEntidad;
        Response.Redirect("R02_Asociado.aspx");
    }

}
