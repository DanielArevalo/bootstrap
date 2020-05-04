using System;
using System.Web.UI;
using Xpinn.Util;
using System.Configuration;

public partial class SolicitudCredito : GlobalWeb
{
    //Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    //PoblarListas poblarLista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            AdicionarTitulo("Solicitud de Crédito", "A");                        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudCredito", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            lblCod_persona.Text = "";
            panelDataPersona.Visible = true;
            panelNormal.Visible = true;
            panelFinal.Visible = false;
            if (Session["persona"] != null)
            {
                Int64 pCodPersona = 0;
                try
                {
                    /*
                    Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                    pPersona = (Xpinn.FabricaCreditos.Entities.Persona1)Session["persona"];
                    pCodPersona = pPersona.cod_persona;
                    */
                }
                catch
                {
                    xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();
                    DataPersona = (xpinnWSLogin.Persona1)Session["persona"];
                    pCodPersona = DataPersona.cod_persona;
                }
                lblCod_persona.Text = pCodPersona.ToString();
                ObtenerDatos(pCodPersona.ToString());
                CargarDropDown(pCodPersona);
                string FormaPago = ConfigurationManager.AppSettings["ddlFormaPago"].ToString();
                if (FormaPago != null && FormaPago != "")
                {
                    ddlFormaPago.SelectedValue = FormaPago;
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                ddlLineaCredito_SelectedIndexChanged(ddlLineaCredito, null);
                txtFechaSolicitud.Text = DateTime.Now.ToShortDateString();
            }            
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Usuario pUsu = new Usuario();
            /*
            Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1.seleccionar = "Cod_persona";
            vPersona1.cod_persona = Convert.ToInt64(pIdObjeto);
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, pUsu);

            txtIdentificacion.Text = vPersona1.identificacion != null ? vPersona1.identificacion : "";
            ddlTipoIdent.SelectedValue = vPersona1.tipo_identificacion != 0 ? vPersona1.tipo_identificacion.ToString() : "";
            txtNombre.Text = vPersona1.nombres != null ? vPersona1.nombres : "";
            txtApellido.Text = vPersona1.apellidos != null ? vPersona1.apellidos : "";
            txtDireccion.Text = vPersona1.direccion != null ? vPersona1.direccion : "";
            txtTelefono.Text = vPersona1.telefono != null ? vPersona1.telefono : "";
            */
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudCredito", "ObtenerDatos", ex);
        }
    }

    protected void CargarDropDown(Int64 pCod_persona)
    {
        /*
        Usuario pUsu = new Usuario();
        poblarLista.PoblarListaDesplegable("TIPOIDENTIFICACION", "", "", "1", ddlTipoIdent, pUsu);
        poblarLista.PoblarListaDesplegable("PERIODICIDAD", "", "", "1", ddlperiodicidad, pUsu);
        poblarLista.PoblarListaDesplegable("MEDIOS", "", "", "1", ddlMedio, pUsu);
        ddlMedio.SelectedValue = "2";

        if (ddlperiodicidad.SelectedItem != null)
            ddlperiodicidad.SelectedValue = "1";

        //CARGANDO LAS LINEAS DE CREDITO
        string pFiltro = " where Nvl(l.Credito_Gerencial, 0) != 1";
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstLineas = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstLineas = DatosClienteServicio.listaddlServices(pFiltro, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLineaCredito.DataSource = lstLineas; 
            ddlLineaCredito.DataTextField = "empresa";
            ddlLineaCredito.DataValueField = "cod_persona";
            ddlLineaCredito.SelectedIndex = 0;
            ddlLineaCredito.DataBind();
        }

        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();


        Int64 codigo = pCod_persona;
        ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(codigo, (Usuario)Session["usuario"]);

        ddlEmpresa.DataTextField = "nom_empresa";
        ddlEmpresa.DataValueField = "cod_empresa";
        ddlEmpresa.AppendDataBoundItems = true;
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEmpresa.SelectedIndex = 0;
        ddlEmpresa.DataBind();
        */
    }

    protected void ddlLineaCredito_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Calcular_Cupo();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "ddlLineaCredito_SelectedIndexChanged", ex);
        }
    }

    private void Calcular_Cupo()
    {
        /*
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            DatosLinea = LineaCredito.Calcular_Cupo(ddlLineaCredito.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = DatosLinea.Monto_Maximo.ToString();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
        */
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "ddlEmpresa_SelectedIndexChanged", ex);
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
            {
                ddlEmpresa.Visible = true;
                lblPagaduri.Visible = true;
            }
            else
            {
                ddlEmpresa.Visible = false;
                lblPagaduri.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "ddlFormaPago_SelectedIndexChanged", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        if (txtVrCredito.Text == "0" || txtVrCredito.Text == "")
        {
            VerError("Ingrese el valor a solicitar, verifique sus datos.");
            txtVrCredito.Focus();
            return false;
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el plazo perteneciente a la solicitud, verifique sus datos.");
            txtPlazo.Focus();
            return false;
        }
        if (Convert.ToInt32(txtPlazo.Text) > Convert.ToInt32(txtPlazoMaximo.Text))
        {
            VerError("El plazo deseado supera el plazo máximo con la que cuenta la linea "+ ddlLineaCredito.SelectedItem.Text + ", verifique sus datos.");
            txtPlazo.Focus();
            return false;
        }
        if (Convert.ToInt64(txtVrCredito.Text.Replace(".", "")) > Convert.ToInt64(txtMontoMaximo.Text.Replace(".", "")))
        {
            VerError("El valor deseado supera el valor máximo con la que cuenta la linea " + ddlLineaCredito.SelectedItem.Text + ", verifique sus datos.");
            txtVrCredito.Focus();
            return false;
        }
        if (ddlperiodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad con la que se realizaran sus pagos, verifique sus datos.");
            ddlperiodicidad.Focus();
            return false;
        }
        
        if (ddlFormaPago.SelectedValue == "2")
        {
            if (ddlEmpresa.SelectedItem == null)
            {
                VerError("Usted no cuenta con una pagaduria asignada, comuniquece con nosotros para modificar sus datos.");
                return false;
            }
            if (ddlEmpresa.SelectedIndex == 0)
            {
                VerError("Seleccione una pagaduria para realizar su solicitud.");
                ddlEmpresa.Focus();
                return false;
            }
        }
        return true;
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                string sError = "";
                /*
                Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
                Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
                Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = (Xpinn.FabricaCreditos.Entities.Persona1)Session["persona"];

                // Determinando datos de la solicitud
                datosSolicitud.numerosolicitud = 0;
                datosSolicitud.fechasolicitud = DateTime.Now;
                datosSolicitud.cod_cliente = Convert.ToString(lblCod_persona.Text);
                datosSolicitud.cod_persona = Convert.ToInt64(lblCod_persona.Text);
                datosSolicitud.montosolicitado = Convert.ToInt64(txtVrCredito.Text.Replace(".",""));
                datosSolicitud.plazosolicitado = Convert.ToInt64(txtPlazo.Text);
                datosSolicitud.cuotasolicitada = 0;
                datosSolicitud.tipocrdito = Convert.ToString(ddlLineaCredito.SelectedValue);
                datosSolicitud.periodicidad = Convert.ToInt64(ddlperiodicidad.SelectedValue);
                datosSolicitud.medio = Convert.ToInt64(ddlMedio.SelectedValue);
                datosSolicitud.otro = null;
                datosSolicitud.concepto = "Atencion al Cliente";
                datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                datosSolicitud.garantia = 0;
                datosSolicitud.garantia_comunitaria = 0;
                datosSolicitud.poliza = 0;
                datosSolicitud.tipo_liquidacion = 0;

                if (datosSolicitud.forma_pago == 2)
                    datosSolicitud.empresa_recaudo = Convert.ToInt64(ddlEmpresa.SelectedValue);
                else
                    datosSolicitud.empresa_recaudo = null;

                datosSolicitud.cod_oficina = vPersona1.cod_oficina;

                datosSolicitud.cod_usuario = 0;
                datosSolicitud.identificacionprov = "";
                datosSolicitud.nombreprov = null;

                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                // Validar los datos de la solicitud                
                datosSolicitud = DatosSolicitudServicio.ValidarSolicitud(datosSolicitud, usuario, ref sError);
                if (sError.Trim() != "")
                {
                    if (sError.Contains("ORA-20101"))
                    {
                        VerError("No se pudieron validar datos de la solicitud. " + sError);
                    }
                    else
                    {
                        VerError("No se pudieron validar datos de la solicitud. Error:" + sError);
                    }
                    return;
                }
                else if (datosSolicitud.mensaje.Trim() != "")
                {
                    VerError(datosSolicitud.mensaje);
                    return;
                }
                else
                {
                    // Grabar datos de la solicitud
                    datosSolicitud = DatosSolicitudServicio.CrearSolicitud(datosSolicitud, usuario);
                    lblmsjFinal.Text = "Su solicitud de crédito quedó radicado con el número  " + datosSolicitud.numerosolicitud;
                    panelDataPersona.Visible = false;
                    panelNormal.Visible = false;
                    panelFinal.Visible = true;
                }
                */
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "btnSolicitar_Click", ex);
        }
    }
    

}