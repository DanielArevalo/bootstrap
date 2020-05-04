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
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{
    Xpinn.Contabilidad.Services.TerceroService _terceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[_terceroServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_terceroServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(_terceroServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_terceroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                txtCodigo.Enabled = false;
                CargarListas();
                rbJuridica.Checked = true;
                rbNatural.Checked = false;
                if (Session[_terceroServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_terceroServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_terceroServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    mvDatos.ActiveViewIndex = 1;
                }
                else
                {
                    txtCodigo.Text = _terceroServicio.ObtenerSiguienteCodigo(_usuario).ToString();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_terceroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");
        if (idObjeto == "")
        {
            if (txtIdentificacion.Text == "")
            {
                VerError("Ingrese el Nit");
                return false;
            }

            Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, _usuario);
            if (vPersona1.cod_persona != 0)
            {
                VerError("Ya existe una persona con el NIT asignado");
                return false;
            }
        }

        if (txtRazonSocial.Text == "")
        {
            VerError("Ingrese la Razón social");
            return false;
        }
        if (txtSigla.Text == "")
        {
            VerError("Ingrese la Sigla");
            return false;
        }
        if (txtDireccion.Text == "")
        {
            VerError("Ingrese la dirección");
            return false;
        }
        if (ddlCiudad.SelectedIndex == 0)
        {
            VerError("Seleccione la ciudad");
            return false;
        }
        if (txtTelefono.Text == "")
        {
            VerError("Ingrese el teléfono");
            return false;
        }
        if (txtEmail.Text == "")
        {
            VerError("Ingrese el Email");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha de Creación");
            return false;
        }
        if (ddlActividad.SelectedIndex == 0)
        {
            VerError("Seleccione la Actividad");
            return false;
        }
        if (ddlRegimen.SelectedIndex == 0)
        {
            VerError("Seleccione el Régimen");
            return false;
        }

        return true;
    }




    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            try
            {
                Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();

                if (idObjeto != "")
                    vTercero = _terceroServicio.ConsultarTercero(Convert.ToInt64(idObjeto), null, _usuario);

                vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
                vTercero.tipo_persona = "J";
                vTercero.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
                if (txtDigitoVerificacion.Text != "")
                    vTercero.digito_verificacion = Convert.ToInt32(txtDigitoVerificacion.Text);
                vTercero.tipo_identificacion = 2;

                vTercero.fechaexpedicion = txtFecha.ToDateTime;
                vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue);

                vTercero.primer_apellido = txtSigla.Text.ToUpper();
                vTercero.razon_social = txtRazonSocial.Text.ToUpper();

                vTercero.ActividadEconomicaEmpresaStr = ddlActividad.SelectedValue;

                vTercero.direccion = txtDireccion.Text.ToUpper();
                vTercero.telefono = txtTelefono.Text;
                vTercero.email = txtEmail.Text;
                vTercero.cod_oficina = _usuario.cod_oficina;
                vTercero.estado = "A";
                vTercero.regimen = ddlRegimen.SelectedValue;
                if (ddlRegimen.SelectedValue != "")
                    vTercero.regimen = ddlRegimen.SelectedValue;
                else
                    vTercero.regimen = "";
                vTercero.fecultmod = DateTime.Now;
                vTercero.usuultmod = _usuario.identificacion;
                if (vTercero.fechacreacion == null)
                    vTercero.fechacreacion = System.DateTime.Now;
                if (vTercero.usuariocreacion == null)
                    vTercero.usuariocreacion = _usuario.identificacion;
                vTercero.valor_afiliacion = 0;
                vTercero.cod_zona = 0; 
                // ACTIVIDADES CIIU
                if (gvActividadesCIIU.Rows.Count > 0)
                {
                    byte NumActiSeleccionadas = 0;
                    bool ActPrincipalSeleccionada = false;
                    Label lblCodigo;
                    vTercero.lstActividadCIIU = new List<Actividades>();
                    Actividades objActividad;
                    foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                    {
                        CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;
                        CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                        if (chkSeleccionado.Checked)
                        {
                            if (!chkPrincipal.Checked)
                            {
                                objActividad = new Actividades();
                                lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                                objActividad.codactividad = lblCodigo.Text;
                                vTercero.lstActividadCIIU.Add(objActividad);
                            }
                            else
                            {
                                Label lblDescripcion = rFila.FindControl("lbl_descripcion") as Label;
                                VerError("La actividad económica " + lblDescripcion.Text + " fue seleccionada tanto como principal como secundaria");
                                return;
                            }
                            NumActiSeleccionadas++;
                        }

                        if (chkPrincipal.Checked)
                        {
                            if (!ActPrincipalSeleccionada)
                            {
                                ActPrincipalSeleccionada = true;
                                lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                                vTercero.codactividadStr = lblCodigo.Text;
                            }
                            else
                            {
                                VerError("Ha seleccionado más de una actividad economica principal");
                                return;
                            }
                        }

                        if (NumActiSeleccionadas > 3)
                        {
                            VerError("Se han seleccionado mas de 3 actividades econocmicas secundarias");
                            return;
                        }
                    }
                }

                //AGREGADO
                vTercero.tipo_acto_creacion = 0;
                vTercero.num_acto_creacion = null;
                vTercero.celular = null;
                vTercero.fechanacimiento = txtFecConstitucion.Text.Trim() != "" ? Convert.ToDateTime(txtFecConstitucion.Text) : DateTime.MinValue;

                if (hdFileName.Value != null)
                {
                    try
                    {
                        Stream stream = null;
                        /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                        stream = File.OpenRead(Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value));
                        this.Response.Clear();
                        if (stream.Length > 100000)
                        {
                            VerError("La imagen excede el tamaño máximo que es de " + 100000);
                            return;
                        }
                        using (BinaryReader br = new BinaryReader(stream))
                        {
                            vTercero.foto = br.ReadBytes(Convert.ToInt32(stream.Length));
                        }
                    }
                    catch
                    {
                        vTercero.foto = null;
                    }
                }

                if (idObjeto != "")
                {
                    vTercero.cod_persona = Convert.ToInt64(idObjeto);
                    _terceroServicio.ModificarTercero(vTercero, _usuario);
                }
                else
                {
                    vTercero = _terceroServicio.CrearTercero(vTercero, _usuario);
                    idObjeto = Convert.ToString(vTercero.cod_persona);
                }

                // ELIMINANDO ARCHIVOS GENERADOS
                try
                {
                    string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Images\\"));
                    if (File.Exists(Server.MapPath("Images\\" + hdFileName.Value)) ||
                        File.Exists(Server.MapPath("Images\\" + hdFileNameThumb.Value)))
                    {
                        foreach (string ficheroActual in ficherosCarpeta)
                        {
                            if (ficheroActual == Server.MapPath("Images\\" + hdFileName.Value))
                                File.Delete(ficheroActual);
                            if (ficheroActual == Server.MapPath("Images\\" + hdFileNameThumb.Value))
                                File.Delete(ficheroActual);
                        }
                    }
                }
                catch
                { }

                Navegar(Pagina.Lista);
            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(_terceroServicio.CodigoPrograma, "btnGuardar_Click", ex);
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Services.ActividadesServices _servicesActividad = new Xpinn.FabricaCreditos.Services.ActividadesServices();
            Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
            vTercero = _terceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, _usuario);

            txtCodigo.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
            if (vTercero.digito_verificacion != 0)
                txtDigitoVerificacion.Text = HttpUtility.HtmlDecode(vTercero.digito_verificacion.ToString().Trim());
            else
                txtDigitoVerificacion.Text = CalcularDigitoVerificacion(txtIdentificacion.Text);
            if (!string.IsNullOrEmpty(vTercero.primer_apellido))
                txtSigla.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.razon_social))
                txtRazonSocial.Text = HttpUtility.HtmlDecode(vTercero.razon_social.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
            if (vTercero.codciudadexpedicion.ToString() != "")
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vTercero.email.ToString().Trim());
            if (vTercero.fechaexpedicion != null)
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vTercero.fechaexpedicion.ToString()));
            try
            {
                if (vTercero.actividadempresa != null)
                    ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vTercero.actividadempresa.ToString().Trim());
            }
            catch { ddlActividad.SelectedValue = ""; }
            if (!string.IsNullOrEmpty(vTercero.regimen))
                ddlRegimen.SelectedValue = HttpUtility.HtmlDecode(vTercero.regimen.ToString().Trim());
            if (vTercero.fechanacimiento != null)
                txtFecConstitucion.Text = HttpUtility.HtmlDecode(vTercero.fechanacimiento.ToString());

            List<Actividades> lstActividad = _servicesActividad.ConsultarActividadesEconomicasSecundarias(vTercero.cod_persona, Usuario);
            // ACTIVIDADES CIIU

            Label lblCodigo;
            if (lstActividad != null)
            {
                if (gvActividadesCIIU.Rows.Count > 0)
                {
                    foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                    {
                        lblCodigo = (Label)rFila.FindControl("lbl_codigo");

                        //Identificar la actividad principal
                        if (lblCodigo.Text == vTercero.codactividadStr)
                        {
                            CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                            chkPrincipal.Checked = true;
                            Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
                            txtActividadCIIU.Text = lblDescripcion.Text;
                        }

                        foreach (Xpinn.FabricaCreditos.Entities.Actividades objActividad in lstActividad)
                        {
                            CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;

                            if (objActividad.codactividad == lblCodigo.Text)
                            {
                                chkSeleccionado.Checked = true;
                                break;
                            }
                        }
                    }
                }
            }

            //if (vTercero.tipo_acto_creacion != 0)
            //    ddlTipoActoCrea.SelectedValue = vTercero.tipo_acto_creacion.ToString();

            //if (vTercero.num_acto_creacion != "")
            //    txtNumActoCrea.Text = vTercero.num_acto_creacion;

            //if (vTercero.celular != "")
            //    txtcelular.Text = vTercero.celular;

            ////RECUPERAR INFORMACION ADICIONAL
            //InformacionAdicionalServices infoService = new InformacionAdicionalServices();
            //List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();

            //LstInformacion = infoService.ListarPersonaInformacion(Convert.ToInt64(pIdObjeto), _usuario);
            //if (LstInformacion.Count > 0)
            //{
            //    gvInfoAdicional.DataSource = LstInformacion;
            //    gvInfoAdicional.DataBind();
            //}


            //#region RECUPERAR DATOS DE AFILIACIóN

            //Afiliacion pAfili = new Afiliacion();
            //pAfili = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(pIdObjeto), _usuario);
            //if (pAfili.idafiliacion != 0)
            //    txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
            //if (pAfili.fecha_afiliacion != DateTime.MinValue)
            //    txtFechaAfili.Text = Convert.ToString(pAfili.fecha_afiliacion.ToString(gFormatoFecha));
            //if (pAfili.estado != "")
            //    ddlEstadoAfi.SelectedValue = pAfili.estado;

            //ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
            //if (pAfili.fecha_retiro != DateTime.MinValue)
            //    txtFechaRetiro.Text = Convert.ToString(pAfili.fecha_retiro.ToString(gFormatoFecha));
            //if (pAfili.valor != 0)
            //    txtValorAfili.Text = Convert.ToString(pAfili.valor);
            //if (pAfili.fecha_primer_pago != DateTime.MinValue)
            //    txtFecha1Pago.Text = Convert.ToString(pAfili.fecha_primer_pago.ToString(gFormatoFecha));
            //if (pAfili.cuotas != 0)
            //    txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
            //if (pAfili.cod_periodicidad != 0)
            //    ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);

            //#endregion

            // Mostrar imagenes de la persona
            if (vTercero.foto != null)
            {
                try
                {
                    imgFotoJaLo.ImageUrl = Bytes_A_Archivo(pIdObjeto, vTercero.foto);
                    imgFotoJaLo.ImageUrl = string.Format("Handler.ashx?id={0}", vTercero.idimagen + "&Us=" + _usuario.identificacion + "&Pw=" + _usuario.clave);
                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }

            txtDigitoVerificacion.Enabled = false;
            txtIdentificacion.Enabled = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_terceroServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    //protected void ObtenerDatos(String pIdObjeto)
    //{
    //    try
    //    {
    //        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
    //        vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, _usuario);

    //        txtCodigo.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.identificacion))
    //            txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
    //        if (vTercero.digito_verificacion.ToString() != "")
    //            txtDigitoVerificacion.Text = HttpUtility.HtmlDecode(vTercero.digito_verificacion.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.primer_apellido))
    //            txtSigla.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.razon_social))
    //            txtRazonSocial.Text = HttpUtility.HtmlDecode(vTercero.razon_social.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.direccion))
    //            txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
    //        if (vTercero.codciudadexpedicion.ToString() != "")
    //            ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codciudadexpedicion.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.telefono))
    //            txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
    //        if (!string.IsNullOrEmpty(vTercero.email))
    //            txtEmail.Text = HttpUtility.HtmlDecode(vTercero.email.ToString().Trim());
    //        if (vTercero.fechaexpedicion != null)
    //            txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vTercero.fechaexpedicion.ToString()));
    //        try
    //        {
    //            if (vTercero.codactividadStr.ToString() != "")
    //                ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codactividadStr.ToString().Trim());
    //        }
    //        catch { ddlActividad.SelectedValue = ""; }
    //        if (!string.IsNullOrEmpty(vTercero.regimen))
    //            ddlRegimen.SelectedValue = HttpUtility.HtmlDecode(vTercero.regimen.ToString().Trim());

    //        txtDigitoVerificacion.Enabled = false;
    //        txtIdentificacion.Enabled = false;
    //        Site toolBar = (Site)this.Master;
    //        toolBar.MostrarGuardar(true);
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "ObtenerDatos", ex);
    //    }
    //}

    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaId";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

            List<Persona1> lstActividades = TraerResultadosLista("Actividad2");
            ViewState["DTACTIVIDAD" + Usuario.codusuario] = lstActividades;
            gvActividadesCIIU.DataSource = lstActividades;
            gvActividadesCIIU.DataBind();
            
            // Llenar la actividad
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaIdStr";
            ddlActividad.DataSource = TraerResultadosLista("Actividad_Negocio");
            ddlActividad.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_terceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
        return lstDatosSolicitud;
    }

    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        if (rbJuridica.Checked == true)
        {
            mvDatos.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            //toolBar.MostrarGuardar(true);
        }
        else
            Navegar("../Personas/Nuevo.aspx");

    }

    protected void rbJuridica_CheckedChanged(object sender, EventArgs e)
    {
        if (rbJuridica.Checked)
            rbNatural.Checked = false;
        else
            rbNatural.Checked = true;
    }

    protected void rbNatural_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNatural.Checked)
            rbJuridica.Checked = false;
        else
            rbJuridica.Checked = true;
    }


    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1.identificacion = txtIdentificacion.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, _usuario);
            if (idObjeto != "")
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
            txtDigitoVerificacion.Text = CalcularDigitoVerificacion(txtIdentificacion.Text);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    public string CalcularDigitoVerificacion(string Nit)
    {
        string Temp;
        int Contador;
        int Residuo;
        int Acumulador;
        int[] Vector = new int[15];

        Vector[0] = 3;
        Vector[1] = 7;
        Vector[2] = 13;
        Vector[3] = 17;
        Vector[4] = 19;
        Vector[5] = 23;
        Vector[6] = 29;
        Vector[7] = 37;
        Vector[8] = 41;
        Vector[9] = 43;
        Vector[10] = 47;
        Vector[11] = 53;
        Vector[12] = 59;
        Vector[13] = 67;
        Vector[14] = 71;

        Acumulador = 0;

        Residuo = 0;

        for (Contador = 0; Contador < Nit.Length; Contador++)
        {
            Temp = Nit.Substring((Nit.Length - 1) - Contador, 1);
            Acumulador = Acumulador + (Convert.ToInt32(Temp) * Vector[Contador]);
        }

        Residuo = Acumulador % 11;

        return Residuo > 1 ? Convert.ToString(11 - Residuo) : Residuo.ToString();
    }


    protected void btnCargarImagen_Click(object sender, EventArgs e)
    {
        try
        {/*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFotoJaLo.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



    private void cargarFotografia()
    {
        /*Obtenemos el nombre y la extension del archivo*/
        String fileName = Path.GetFileName(this.fuFotoJaLo.PostedFile.FileName);
        String extension = Path.GetExtension(this.fuFotoJaLo.PostedFile.FileName).ToLower();
        try
        {
            if (extension != ".png" && extension != ".jpg" && extension != ".bmp")
            {
                VerError("El archivo ingresado no es una imagen");
            }
            else
            {
                /*Se guarda la imagen en el servidor*/
                fuFotoJaLo.PostedFile.SaveAs(Server.MapPath("Images\\") + fileName);
                /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                String nombreImgServer = getNombreImagenServidor(extension);
                hdFileName.Value = nombreImgServer;
                /*Cambiamos el nombre de la imagen por el nuevo*/
                File.Move(Server.MapPath("Images\\") + fileName, Server.MapPath("Images\\" + nombreImgServer));
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    public String getNombreImagenServidor(String extension)
    {
        /*Devuelve el nombre temporal de la imagen*/
        Random nRandom = new Random();
        String nr = Convert.ToString(nRandom.Next(0, 32000));
        String nombre = nr + "_" + DateTime.Today.ToString("ddMMyyyy") + extension;
        nRandom = null;
        return nombre;
    }

    private void mostrarImagen()
    {
        /*Muestra la imagen como un thumbnail*/
        System.Drawing.Image objImage = null, objThumbnail = null;
        Int32 width, height;
        String fileName = Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value);
        Stream stream = null;
        try
        {
            /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
            stream = File.OpenRead(fileName);
            objImage = System.Drawing.Image.FromStream(stream);
            width = 100;
            height = objImage.Height / (objImage.Width / width);
            this.Response.Clear();
            /*Se crea el thumbnail y se muestra en la imagen*/
            objThumbnail = objImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            objThumbnail.Save(Server.MapPath("Images\\") + "thumb_" + this.hdFileName.Value, ImageFormat.Jpeg);
            imgFotoJaLo.Visible = true;
            String nombreImgThumb = "thumb_" + this.hdFileName.Value;
            this.hdFileNameThumb.Value = nombreImgThumb;
            imgFotoJaLo.ImageUrl = "Images\\" + nombreImgThumb;

        }
        catch (Exception ex)
        {
            VerError("No pudro abrir archivo con imagen de la persona " + ex.Message);
        }
        finally
        {
            /*Limpiamos los objetos*/
            objImage.Dispose();
            objThumbnail.Dispose();
            stream.Dispose();
            objImage = null;
            objThumbnail = null;
            stream = null;
        }
    }


    public string Bytes_A_Archivo(string idPersona, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("Images\\") + Path.GetFileName(idPersona + ".jpg");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                this.hdFileName.Value = Path.GetFileName(idPersona + ".jpg");
                mostrarImagen();
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        return fileName;
    }



    protected void btnCambiarTipoPersona_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            long cod_persona = Convert.ToInt64(txtCodigo.Text);

            Tuple<bool,string> cambioSatisfactorio = _terceroServicio.CambiarTipoDePersona(cod_persona, "N", _usuario);

            if (cambioSatisfactorio.Item1)
            {
                Session[_terceroServicio.CodigoPrograma + ".id"] = cod_persona;
                Session[_terceroServicio.CodigoPrograma + ".modificar"] = 1;

                Navegar("../Personas/Nuevo.aspx");
            }
            else
            {
                VerError(cambioSatisfactorio.Item2);
            }
        }
        catch (Exception ex)
        {
            VerError("Problemas al cambiar el tipo de persona, " + ex.Message);
        }
    }

    #region METODO DE ACTIVIDADES CIIU

    protected void gvActividadesCIIU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPrincipal = (CheckBox)e.Row.FindControl("chkPrincipal");
            Label lblDescripcion = (Label)e.Row.FindControl("lbl_descripcion");
            chkPrincipal.Attributes.Add("onclick", "MostrarCIIUPrincipal('" + lblDescripcion.Text + "')");
        }
    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["DTACTIVIDAD" + Usuario.codusuario] != null)
        {
            List<Persona1> lstActividad = (List<Persona1>)ViewState["DTACTIVIDAD" + Usuario.codusuario];
            if (lstActividad != null)
            {
                if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()) && !string.IsNullOrEmpty(txtBuscarDescripcion.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text) || x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                gvActividadesCIIU.DataSource = lstActividad;
                gvActividadesCIIU.DataBind();
            }
        }
        MostrarModal();
    }

    private void MostrarModal()
    {
        var ahh = txtRecoger_PopupControlExtender.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }


    #endregion
    

}