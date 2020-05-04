using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using System.Drawing.Imaging;

public partial class Nuevo : GlobalWeb
{
    AtributosTasasServices AtributosTasaServices = new AtributosTasasServices();
    LineaServiciosServices LineaServicios = new LineaServiciosServices();
    private Xpinn.FabricaCreditos.Services.DestinacionService destinServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Operacion"] = null;
                Session["DatosPlanes"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;                

                if (Session[LineaServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[LineaServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(LineaServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                   
                    lblmsj.Text = "modificado";
                    txtCodigo.ReadOnly = true;
                    btnnuevodeduccion.Visible = true;
                    lblTasa.Visible = false;
                }
                else
                {
                    lblmsj.Text = "grabada";                    
                    InicializargvPlanes();
                    btnnuevodeduccion.Visible = false;
                    lblTasa.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        PoblarLista("PERIODICIDAD", ddlPerRenovacion);
        PoblarLista("PERIODICIDAD", ddlPerPago);

        ddlTipoServicio.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoServicio.Items.Insert(1, new ListItem("Medicina Prepagada", "1"));
        ddlTipoServicio.Items.Insert(2, new ListItem("Planes Exequiales", "2"));
        ddlTipoServicio.Items.Insert(3, new ListItem("Seguros", "3"));
        ddlTipoServicio.Items.Insert(4, new ListItem("Orden de Servicio", "5"));
        ddlTipoServicio.Items.Insert(5, new ListItem("Otros", "4"));
        ddlTipoServicio.SelectedIndex = 0;
        ddlTipoServicio.DataBind();

        //TRAER DATOS DE LA DESTINACION

        List<Xpinn.FabricaCreditos.Entities.Destinacion> lstConsultaD = new List<Xpinn.FabricaCreditos.Entities.Destinacion>();
        lstConsultaD = destinServicio.ListarDestinacion(ObtenerValores(), (Usuario)Session["usuario"]);

        gvRecoger.PageSize = pageSize;
        gvRecoger.EmptyDataText = emptyQuery;
        gvRecoger.DataSource = lstConsultaD;

        if (lstConsultaD.Count > 0)
        {
            gvRecoger.Visible = true;
            gvRecoger.DataBind();
            //ValidarPermisosGrilla(gvRecoger);
        }
        else
        {
            gvRecoger.Visible = false;
        }


    }

    //Lo de la linea y sus destinaciones

    private Xpinn.FabricaCreditos.Entities.Destinacion ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Destinacion vDestinacion = new Xpinn.FabricaCreditos.Entities.Destinacion();
        return vDestinacion;
    }


    protected void InicializargvPlanes()
    {
        List<planservicios> lstDeta = new List<planservicios>();
        for (int i = gvPlanes.Rows.Count; i < 5; i++)
        {
            planservicios ePlan = new planservicios();
            ePlan.cod_plan_servicio = "-1";
            //eCuenta.cod_empresa = -1;
            ePlan.nombre = "";
            ePlan.numero_usuarios = null;
            ePlan.edad_minima = null;
            ePlan.edad_maxima = null;
            ePlan.codgrupo_familiar = null;
            ePlan.valor = null;
            lstDeta.Add(ePlan);
        }
        gvPlanes.DataSource = lstDeta;
        gvPlanes.DataBind();

        Session["DatosPlanes"] = lstDeta;        
    }

    
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            LineaServicios vDetalle = LineaServicios.ConsultarLineaSERVICIO(pIdObjeto.ToString(), Usuario);

            if (vDetalle.cod_linea_servicio != "")
                txtCodigo.Text = vDetalle.cod_linea_servicio.ToString().Trim();

            if(vDetalle.tipo_servicio != 0)
                ddlTipoServicio.SelectedValue = vDetalle.tipo_servicio.ToString().Trim();

            if (vDetalle.nombre != "")
                txtDescripcion.Text = vDetalle.nombre.ToString().Trim();
            
            if (vDetalle.identificacion_proveedor != "")
                txtIdentificacion.Text = vDetalle.identificacion_proveedor;
            
            if (vDetalle.nombre != "")
                txtNombre.Text = vDetalle.nombre_proveedor;

            if (vDetalle.codperiodo_renovacion != 0)
                ddlPerRenovacion.SelectedValue = vDetalle.codperiodo_renovacion.ToString();

            if (vDetalle.codperiodo_pago != 0)
                ddlPerPago.SelectedValue = vDetalle.codperiodo_pago.ToString();

            if (vDetalle.fecha_pago_proveedor != DateTime.MinValue)
                txtFechaPago.Text = vDetalle.fecha_pago_proveedor.ToShortDateString();

            if (vDetalle.numero_beneficiarios != 0)
                txtBeneficiario.Text = vDetalle.numero_beneficiarios.ToString();

            if (vDetalle.numero_servicios != 0 || vDetalle.numero_servicios != null)
                txtServicioAño.Text = vDetalle.numero_servicios.ToString();

            if (vDetalle.tipo_cuota != 0)
                ddlTipoCuota.SelectedValue = vDetalle.tipo_cuota.ToString();

            txtOrden.Text = vDetalle.orden.ToString();

            txtMaximoValor.Text = vDetalle.maximo_valor;
            txtMaximoPlazo.Text = vDetalle.maximo_plazo;

            check1.Checked = vDetalle.requierebeneficiarios == 1 ? true : false;
            chkOrdenServicio.Checked = vDetalle.no_requiere_aprobacion == 1 ? true: false;

            chkOcultaData.Checked = vDetalle.ocultar_informacion == 1 ? true : false;

            chkCausacion.Checked = vDetalle.maneja_causacion == 1 ? true : false;

            chkMaRetirados.Checked = vDetalle.maneja_retirados == 1 ? true : false;

            chkNoGenerarVacaciones.Checked = vDetalle.no_generar_vacaciones == 1 ? true : false;

            chkServicioTelefonia.Checked = vDetalle.servicio_telefonia == 1 ? true : false;

            chkOficinaVirtual.Checked = vDetalle.oficinaVirtual == 1 ? true : false;
            txtEnlace.Text = vDetalle.enlace;
            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<planservicios> LstConsulta = new List<planservicios>();

            LstConsulta = LineaServicios.ConsultarDETALLELineaSERVICIO(pIdObjeto.ToString(), (Usuario)Session["usuario"]);
            if (LstConsulta.Count > 0)
            {
                if ((LstConsulta != null) || (LstConsulta.Count != 0))
                {
                    gvPlanes.DataSource = LstConsulta;
                    gvPlanes.DataBind();
                }
                Session["DatosPlanes"] = LstConsulta;
            }
            else
            {
                InicializargvPlanes();
            }
            ActualizarTasas();

            //RECUPERAR DATOS DE DESTINACIÓN
            List<LineaServicios> lstDestinos = new List<LineaServicios>();
            lstDestinos = LineaServicios.ConsultarDestinacion_Linea(idObjeto.ToString(), (Usuario)Session["usuario"]);
            if (lstDestinos.Count > 0)
            {
                foreach (var item in lstDestinos)
                {
                    foreach (GridViewRow rFila in gvRecoger.Rows)
                    {
                        CheckBoxGrid chkSeleccione = rFila.FindControl("cbListado") as CheckBoxGrid;
                        Label lblcodDest = (Label)rFila.FindControl("lbl_destino");
                        if (item.cod_destino == Convert.ToInt32(lblcodDest.Text))
                        {
                            chkSeleccione.Checked = true;
                        }
                    }

                }
            }
            //CARGAR FOTO 
            if(vDetalle.foto != null)
            {
                imgFoto.ImageUrl = Bytes_A_Archivo(vDetalle.cod_linea_servicio, vDetalle.foto, 1);
                imgFoto.ImageUrl = "..\\..\\..\\Images\\"+ vDetalle.cod_linea_servicio + "foto.jpg";
                //imgFoto.ImageUrl = string.Format("./Handler.ashx?id={0}", vDetalle.cod_linea_servicio + "&Us=" + Usuario.identificacion + "&Pw=" + Usuario.clave);
            }
            //CARGAR BANNER 
            if (vDetalle.banner != null)
            {
                imgBanner.ImageUrl = Bytes_A_Archivo(vDetalle.cod_linea_servicio, vDetalle.banner, 2);
                imgBanner.ImageUrl = "..\\..\\..\\Images\\" + vDetalle.cod_linea_servicio + "banner.jpg";
                //imgBanner.ImageUrl = string.Format("./Handler.ashx?id={0}", vDetalle.cod_linea_servicio + "&Us=" + Usuario.identificacion + "&Pw=" + Usuario.clave);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    public string Bytes_A_Archivo(string id, Byte[] ImgBytes, int tipo = 0)
    {
        Stream stream = null;
        string fileName;
        if (tipo == 1)
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(id +"foto"+ ".jpg");
        else if(tipo == 2)
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(id + "banner" + ".jpg");
        else
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(id + ".jpg");
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
                //this.hdFileName.Value = Path.GetFileName(id + ".jpg");
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

    public string Bytes_A_Archivo(string idLineaServicio, Byte[] ImgBytes, bool img)
    {
        string nameF = "";
        if (img)
            nameF = "Banner";
        else
            nameF = "Foto";
        Stream stream = null;
        string fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(idLineaServicio+nameF+ ".jpg");
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
                if(img)
                    this.hdFileNameB.Value = Path.GetFileName(idLineaServicio + "Banner" + ".jpg");
                else
                    this.hdFileName.Value = Path.GetFileName(idLineaServicio + "Foto" + ".jpg");
                mostrarImagen(img);
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
    private void ActualizarTasas()
    {
        try
        {
            List<RangoTasas> lstTasas = new List<RangoTasas>();
            RangoTasas pEntidad = new RangoTasas();
            string pFiltro = " cod_linea_servicio = '" + txtCodigo.Text + "'";
            lstTasas = AtributosTasaServices.ListarRangoTasas(pEntidad, pFiltro, (Usuario)Session["usuario"]);
            gvTasas.DataSource = lstTasas;
            if (lstTasas.Count > 0)
            {
                gvTasas.Visible = true;
                gvTasas.DataBind();
                lblInfo.Visible = false;
                lblTotalRegTasa.Text = "<br /> Registros encontrados " + lstTasas.Count();
                lblTotalRegTasa.Visible = true;
            }
            else
            {
                gvTasas.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegTasa.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "ActualizarTasas", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtCodigo.Text == "")
        {
            VerError("Ingrese un Codigo");
            return false;
        }
        if (ddlTipoServicio.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de Servicio");
            return false;
        }
        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la Descripción");
            return false;
        }

        if (check1.Checked)
        {
            if (txtBeneficiario.Text == "")
            {
                VerError("Ingrese un beneficiario");
                return false;
            }
        }
        if (txtIdentificacion.Text == "")
        {
            VerError("Ingrese la identificación del Proveedor.");
            txtIdentificacion.Focus();
            return false;
        }
        if (txtNombre.Text == "")
        {
            VerError("Ingrese el nombre del proveedor.");
            txtNombre.Focus();
            return false;
        }
        if (txtServicioAño.Text == "")
        {
            VerError("Ingrese el numero del servicios");
            txtServicioAño.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtMaximoPlazo.Text) || txtMaximoPlazo.Text == "0")
        {
            VerError("Ingrese el numero maximo de plazo");
            txtMaximoPlazo.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtMaximoValor.Text) || txtMaximoValor.Text == "0")
        {
            VerError("Ingrese el valor maximo del servicio");
            txtMaximoValor.Focus();
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (check1.Checked)
        {
            if (txtBeneficiario.Text == "")
            {
                VerError("Ingrese un beneficiario");
                return;
            }
        }
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los Datos Ingresados?");
        }
    }
    protected void btnCargarImagen_Click(object sender, EventArgs e)
    {
        try
        {/*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia(false);
                //mostrarImagen(false);
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    protected void linkBt_Click(object sender, EventArgs e)
    {
        try
        {
            /*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia(false);
                //mostrarImagen(false);
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    protected void btnCargarBanner_Click(object sender, EventArgs e)
    {
        try
        {/*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuBanner.HasFile == true)
            {
                cargarFotografia(true);
                mostrarImagen(true);
            }
        }
        catch (Exception ex)
        {
            lblerrorB.Text = (ex.Message);
        }
    }
    protected void linkBtB_Click(object sender, EventArgs e)
    {
        try
        {
            /*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuBanner.HasFile == true)
            {
                cargarFotografia(true);
                mostrarImagen(true);
            }
        }
        catch (Exception ex)
        {
            lblerrorB.Text = (ex.Message);
        }
    }
    private void mostrarImagen(bool img)
    {
        /*Muestra la imagen como un thumbnail*/
        System.Drawing.Image objImage = null, objThumbnail = null;
        Int32 width, height;
        String fileName = "";
        if (img)
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(this.hdFileNameB.Value);
        else
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(this.hdFileName.Value);
        Stream stream = null;
        try
        {
            /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
            stream = File.OpenRead(fileName);
            if (stream.Length > 2000000)
            {
                if (img)
                    lblerrorB.Text = ("La imagen tiene un valor muy grande");
                else
                    lblerror.Text = ("La imagen tiene un valor muy grande");
            }

            objImage = System.Drawing.Image.FromStream(stream);
            width = 100;
            height = objImage.Height / (objImage.Width / width);
            this.Response.Clear();
            /*Se crea el thumbnail y se muestra en la imagen*/
            objThumbnail = objImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            if (img)
                objThumbnail.Save(Server.MapPath("..\\..\\..\\Images\\") + "thumb_" + this.hdFileNameB.Value, ImageFormat.Jpeg);
            else
                objThumbnail.Save(Server.MapPath("..\\..\\..\\Images\\") + "thumb_" + this.hdFileName.Value, ImageFormat.Jpeg);
            if (img)
            {
                imgBanner.Visible = true;
                String nombreImgThumb = this.hdFileNameB.Value;
                this.hdFileNameThumbB.Value = nombreImgThumb;
                imgBanner.ImageUrl = "..\\..\\..\\Images\\" + nombreImgThumb;
            }else
            {
                imgFoto.Visible = true;
                String nombreImgThumb = this.hdFileName.Value;
                this.hdFileNameThumb.Value = nombreImgThumb;
                imgFoto.ImageUrl = "..\\..\\..\\Images\\" + nombreImgThumb;
            }
        }
        catch (Exception ex)
        {
            if (img)
                lblerrorB.Text = ("No pudo abrir archivo con imagen de la persona " + ex.Message);
            else
                lblerror.Text = ("No pudo abrir archivo con imagen de la persona " + ex.Message);
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
    private void cargarFotografia(bool img)
    {
        /*Obtenemos el nombre y la extension del archivo*/
        String fileName = "";
        String extension = "";
        if (img)
        {
            fileName = Path.GetFileName(this.fuBanner.PostedFile.FileName);
            extension = Path.GetExtension(this.fuBanner.PostedFile.FileName).ToLower();
        }else
        {
            fileName = Path.GetFileName(this.fuFoto.PostedFile.FileName);
            extension = Path.GetExtension(this.fuFoto.PostedFile.FileName).ToLower();
        }

        try
        {
            if (extension != ".png" && extension != ".jpg" && extension != ".bmp")
            {
                lblerror.Text = ("El archivo ingresado no es una imagen");
            }
            else
            {
                /*Se guarda la imagen en el servidor*/
                if (img)
                    fuBanner.PostedFile.SaveAs(Server.MapPath("..\\..\\..\\Images\\") + fileName);
                else
                    fuFoto.PostedFile.SaveAs(Server.MapPath("..\\..\\..\\Images\\") + fileName);
                /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                String nombreImgServer = getNombreImagenServidor(extension);
                if(img)
                    hdFileNameB.Value = nombreImgServer;
                else
                    hdFileName.Value = nombreImgServer;
                /*Cambiamos el nombre de la imagen por el nuevo*/
                File.Move(Server.MapPath("..\\..\\..\\Images\\") + fileName, Server.MapPath("..\\..\\..\\Images\\" + nombreImgServer));

                if (img)
                    imgBanner.ImageUrl = "..\\..\\..\\Images\\" + nombreImgServer;
                else
                    imgFoto.ImageUrl = "..\\..\\..\\Images\\" + nombreImgServer;
            }
        }
        catch (Exception ex)
        {
            if (img)
                lblerrorB.Text = (ex.Message);
            else
                lblerror.Text = (ex.Message);
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
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            LineaServicios pVar = new LineaServicios();            

            if (txtCodigo.Text != "")
                pVar.cod_linea_servicio = txtCodigo.Text;
            else
                pVar.cod_linea_servicio = "";
            pVar.tipo_servicio  = Convert.ToInt32(ddlTipoServicio.SelectedValue);
            pVar.nombre = txtDescripcion.Text.ToUpper();

            if (txtIdentificacion.Text != "")
                pVar.identificacion_proveedor = txtIdentificacion.Text;
            else
                pVar.identificacion_proveedor = null;

            if (txtNombre.Text != "")
                pVar.nombre_proveedor = txtNombre.Text;
            else
                pVar.nombre_proveedor = null;

            if (ddlPerRenovacion.SelectedIndex != 0)
                pVar.codperiodo_renovacion = Convert.ToInt32(ddlPerRenovacion.SelectedValue);
            else
                pVar.codperiodo_renovacion = 0;

            if (ddlPerPago.SelectedIndex != 0)
                pVar.codperiodo_pago = Convert.ToInt32(ddlPerPago.SelectedValue);
            else
                pVar.codperiodo_pago = 0;

            if (txtFechaPago.Text != "")
                pVar.fecha_pago_proveedor = Convert.ToDateTime(txtFechaPago.Text);
            else
                pVar.fecha_pago_proveedor = DateTime.MinValue;

            if (txtBeneficiario.Text != "")
                pVar.numero_beneficiarios = Convert.ToInt32(txtBeneficiario.Text);
            else
                pVar.numero_beneficiarios = 0;

            if (!string.IsNullOrWhiteSpace(txtServicioAño.Text))
                pVar.numero_servicios = Convert.ToInt32(txtServicioAño.Text);

            if (!string.IsNullOrWhiteSpace(txtOrden.Text))
                pVar.orden = Convert.ToInt32(txtOrden.Text);

            if (check1.Checked)
            {
                pVar.requierebeneficiarios = 1;
            }
            else
            {
                pVar.requierebeneficiarios = 0;
            }

            pVar.maneja_causacion = Convert.ToInt32(chkCausacion.Checked);
            pVar.maneja_retirados = Convert.ToInt32(chkMaRetirados.Checked);
            pVar.no_generar_vacaciones = Convert.ToInt32(chkCausacion.Checked);
            pVar.servicio_telefonia = Convert.ToInt32(chkServicioTelefonia.Checked);

            //POR DEFECTO NULOS
            pVar.cobra_interes = 0;
            pVar.tasa_interes = 0;
            pVar.tipo_tasa = 0;
            
            pVar.no_requiere_aprobacion = chkOrdenServicio.Checked ? 1 : 0; 

            pVar.lstPlan = new List<planservicios>();
            pVar.lstPlan = ObtenerListaplan();

            pVar.maximo_plazo = txtMaximoPlazo.Text;
            pVar.maximo_valor = txtMaximoValor.Text;
            pVar.tipo_cuota = Convert.ToInt32(ddlTipoCuota.SelectedValue);
            pVar.ocultar_informacion = chkOcultaData.Checked ? 1 : 0;

            //Cargar datos destinacion por linea de credito

            pVar.lstdestinacion = new List<LineaServicios>();
            pVar.lstdestinacion = ObtenerListaDestinación();

            //GUARDAR IMAGEN DE LA LINEA DEL SERVICIO
            byte[] foto = new Byte[0];
            if (hdFileName.Value != null)
            {
                try
                {
                    Stream stream = null;
                    /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                    stream = File.OpenRead(Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(this.hdFileName.Value));
                    this.Response.Clear();
                    if (stream.Length > 1000000)
                    {
                        lblerror.Text = ("La imagen excede el tamaño máximo que es de " + 100000);
                        return;
                    }
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        foto = br.ReadBytes(Convert.ToInt32(stream.Length));
                    }
                }
                catch
                {
                    foto = null;
                }
            }
            pVar.oficinaVirtual = chkOficinaVirtual.Checked ? 1 : 0;
            pVar.enlace = txtEnlace.Text;
            //GUARDAR BANNER DE LA LINEA DEL SERVICIO
            byte[] banner = new Byte[0];
            if (hdFileNameB.Value != null)
            {
                try
                {
                    Stream stream = null;
                    /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                    stream = File.OpenRead(Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(this.hdFileNameB.Value));
                    this.Response.Clear();
                    if (stream.Length > 1000000)
                    {
                        lblerror.Text = ("La imagen excede el tamaño máximo que es de " + 100000);
                        return;
                    }
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        banner = br.ReadBytes(Convert.ToInt32(stream.Length));
                    }
                }
                catch
                {
                    banner = null;
                }
            }

            if (idObjeto != "")
            {
                //MODIFICAR
                LineaServicios.ModificarLineaServicio(pVar, foto, banner, Usuario);
            }
            else
            {
                LineaServicios validar = LineaServicios.ConsultarLineaSERVICIO(txtCodigo.Text, Usuario);
                if (validar.cod_linea_servicio != null)
                {
                    VerError("Ya Existe una Linea con el mismo Código");
                    return;
                }

                //CREAR
                LineaServicios.CrearLineaServicio(pVar, foto, banner, Usuario);
            }

            lblNroMsj.Text = pVar.cod_linea_servicio.ToString();
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "btnContinuar_Click", ex);
        }        
    }

    protected List<LineaServicios> ObtenerListaDestinación()
    {
        try
        {
            List<LineaServicios> lstdestinacion = new List<LineaServicios>();

            foreach (GridViewRow rFila in gvRecoger.Rows)
            {
                LineaServicios destinacion_linea = new LineaServicios();

                CheckBoxGrid chkSeleccione = rFila.FindControl("cbListado") as CheckBoxGrid;

                if (chkSeleccione != null)
                    if (chkSeleccione.Checked)
                    {
                        Label lblcodDest = (Label)rFila.FindControl("lbl_destino");
                        if (lblcodDest.Text != "")
                            destinacion_linea.cod_destino = Convert.ToInt32(lblcodDest.Text);
                    }

                if (chkSeleccione.Checked)
                {
                    lstdestinacion.Add(destinacion_linea);
                }
            }
            return lstdestinacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "ObtenerListaDestinación", ex);
            return null;
        }
    }

    protected List<planservicios> ObtenerListaplan()
    {
        try
        {
            List<planservicios> lstDetalle = new List<planservicios>();
            List<planservicios> lista = new List<planservicios>();

            foreach (GridViewRow rfila in gvPlanes.Rows)
            {
                planservicios ePogra = new planservicios();

                TextBoxGrid txtCodigo = (TextBoxGrid)rfila.FindControl("txtCodigo");
                if (txtCodigo.Text != "")
                    ePogra.cod_plan_servicio = txtCodigo.Text;
                else
                    ePogra.cod_plan_servicio = null;

                TextBoxGrid txtDescripcion = (TextBoxGrid)rfila.FindControl("txtDescripcion");
                if (txtDescripcion.Text != "")
                    ePogra.nombre = txtDescripcion.Text;

                TextBoxGrid txtNumUsuarios = (TextBoxGrid)rfila.FindControl("txtNumUsuarios");
                if (txtNumUsuarios.Text != "")
                    ePogra.numero_usuarios = Convert.ToInt32(txtNumUsuarios.Text);

                TextBoxGrid txtEdadInicial = (TextBoxGrid)rfila.FindControl("txtEdadInicial");
                if (txtEdadInicial.Text != "")
                    ePogra.edad_minima = Convert.ToInt32(txtEdadInicial.Text);

                TextBoxGrid txtEdadFinal = (TextBoxGrid)rfila.FindControl("txtEdadFinal");
                if (txtEdadFinal.Text != "")
                    ePogra.edad_maxima = Convert.ToInt32(txtEdadFinal.Text);

                DropDownListGrid ddlGrupo = (DropDownListGrid)rfila.FindControl("ddlGrupo");
                if (ddlGrupo.SelectedIndex != 0)
                    ePogra.codgrupo_familiar = Convert.ToInt32(ddlGrupo.SelectedValue);

                decimales txtValor = (decimales)rfila.FindControl("txtValor");
                if (txtValor.Text != "0" && txtValor.Text != "")
                    ePogra.valor = Convert.ToDecimal(txtValor.Text);

                lista.Add(ePogra);
                Session["DatosPlanes"] = lista;

                if (ePogra.nombre != "" && ePogra.numero_usuarios != 0 && ePogra.valor != 0 && ePogra.nombre != null && ePogra.numero_usuarios != null && ePogra.valor != null)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "ObtenerListaplan", ex);          
            return null;
        }
    }

    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaplan();
        List<planservicios> LstPrograma = new List<planservicios>();
        if (Session["DatosPlanes"] != null)
        {
            LstPrograma = (List<planservicios>)Session["DatosPlanes"];

            for (int i = 1; i <= 1; i++)
            {
                planservicios ePlan = new planservicios();
                ePlan.cod_plan_servicio = "-1";
                //eCuenta.cod_empresa = -1;
                ePlan.nombre = "";
                ePlan.numero_usuarios = null;
                ePlan.edad_minima = null;
                ePlan.edad_maxima = null;
                ePlan.codgrupo_familiar = null;
                ePlan.valor = null;
                LstPrograma.Add(ePlan);
            }
            gvPlanes.PageIndex = gvPlanes.PageCount;
            gvPlanes.DataSource = LstPrograma;
            gvPlanes.DataBind();

            Session["DatosPlanes"] = LstPrograma;
        }
    }

    protected void gvPlanes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvPlanes.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaplan();

        List<planservicios> LstDetalle = new List<planservicios>();
        LstDetalle = (List<planservicios>)Session["DatosPlanes"];
        if (conseID > 0)
        {
            try
            {
                foreach (planservicios acti in LstDetalle)
                {
                    if (acti.cod_plan_servicio == conseID.ToString())
                    {
                        LineaServicios.EliminarDETALLELineaSERVICIO(conseID.ToString(), (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["DatosPlanes"] = LstDetalle;

                gvPlanes.DataSourceID = null;
                gvPlanes.DataBind();
                gvPlanes.DataSource = LstDetalle;
                gvPlanes.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(LineaServicios.CodigoPrograma, "gvPlanes_RowDeleting", ex);
            }
        }
        else
        {
            foreach (planservicios acti in LstDetalle)
            {
                //if (acti.cod_plan_servicio == null) acti.cod_plan_servicio = "0";
                if (acti.cod_plan_servicio == conseID.ToString())
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["DatosPlanes"] = LstDetalle;

            gvPlanes.DataSourceID = null;
            gvPlanes.DataBind();
            gvPlanes.DataSource = LstDetalle;
            gvPlanes.DataBind();
        }
    }

    protected void gvPlanes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlGrupo = (DropDownListGrid)e.Row.FindControl("ddlGrupo");
            if (ddlGrupo != null)
                PoblarLista("grupo_familiar", ddlGrupo);

            Label lblGrupo = (Label)e.Row.FindControl("lblGrupo");
            if (lblGrupo != null)
                ddlGrupo.SelectedValue = lblGrupo.Text;

        }
    }
   
    protected void gvTasas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvTasas.DataKeys[e.NewEditIndex].Value.ToString();
        Session["Operacion"] = "E";
        Session[LineaServicios.CodigoPrograma + ".CodRango"] = id;
        Session[LineaServicios.CodigoPrograma + ".LineaServicio"] = txtCodigo.Text;
        Session[LineaServicios.CodigoPrograma + ".id"] = txtCodigo.Text;
        Response.Redirect("~/Page/Servicios/LineaServicios/Tasas.aspx");
    }

    protected void gvTasas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int conseID = Convert.ToInt32(gvTasas.DataKeys[e.RowIndex].Values[0].ToString());
            AtributosTasaServices.EliminarRangoTopes(conseID, (Usuario)Session["usuario"]);
            ActualizarTasas();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnnuevodeduccion_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtCodigo.Text != "")
        {
            Session["Operacion"] = "N";
            Session[LineaServicios.CodigoPrograma + ".LineaServicio"] = txtCodigo.Text;
            Session[LineaServicios.CodigoPrograma + ".CodRango"] = null;
            Session[LineaServicios.CodigoPrograma + ".id"] = txtCodigo.Text;
            Response.Redirect("~/Page/Servicios/LineaServicios/Tasas.aspx");
        }
        else
        { VerError("Ingrese el Código de la Linea"); }
    }
}
