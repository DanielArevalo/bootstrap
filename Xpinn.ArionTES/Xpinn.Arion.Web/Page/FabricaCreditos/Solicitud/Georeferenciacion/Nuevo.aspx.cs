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
using Subgurim.Controles;
//using System.ComponentModel.DataAnnotations.Resources;


partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService GeoreferenciaServicio = new Xpinn.FabricaCreditos.Services.GeoreferenciaService(); 
    private Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService(); // Servicio necesario para georeferenciar en el mapa
    public string CsVariable = "";
    public string posiionlon = "";
    public string posiionlat = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GeoreferenciaServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoAdelante -= btnAdelante_Click;
            toolBar.eventoAtras += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoAtras -= btnAtras_Click;

            if (Session["Nombres"] != null) ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            if (Session["Identificacion"] != null) ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            ImageButton btnAtras = Master.FindControl("btnAtras") as ImageButton; 
            btnAdelante.ValidationGroup = "";

            if (Session["TipoCredito"] != null)
            {
                if (Session["TipoCredito"].ToString() == "C")
                    btnAdelante.ImageUrl = "~/Images/btnCapturaDocumentos.jpg";
                else
                    btnAdelante.ImageUrl = "~/Images/btnSolicitudCreditosRecogidos.jpg";
            }
            else
            {
                btnAdelante.Visible = false;
                btnAtras.Visible = false;
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "obtener_localizacion();", true); 
        
       
        try
        {
           

            if (!IsPostBack)
            {
                InicializarGoogleMapsServer(4.60971, -74.08175);
                CargarValoresConsulta(pConsulta, GeoreferenciaServicio.CodigoPrograma);

               
               

                


                //UpdatePanel1.Visible = false;
                //UpdatePanel2.Visible = false;
            }
        }

        
              
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GeoreferenciaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }
 

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       

            GuardarValoresConsulta(pConsulta, GeoreferenciaServicio.CodigoPrograma);
        
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, GeoreferenciaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();
            vGeoreferencia = GeoreferenciaServicio.ConsultarGeoreferencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (vGeoreferencia.codgeoreferencia != Int64.MinValue)
            //    txtcodgeoreferencia.Text = vGeoreferencia.codgeoreferencia.ToString().Trim();
            ////if (vGeoreferencia.cod_persona != Int64.MinValue)
            ////    txtCod_persona.Text = vGeoreferencia.cod_persona.ToString().Trim();
            //if (!string.IsNullOrEmpty(vGeoreferencia.latitud))
            //    txtLatitud.Text = vGeoreferencia.latitud.ToString().Trim();
            //if (!string.IsNullOrEmpty(vGeoreferencia.longitud))
            //    txtLongitud.Text = vGeoreferencia.longitud.ToString().Trim();
            //if (!string.IsNullOrEmpty(vGeoreferencia.observaciones))
            //    txtObservaciones.Text = vGeoreferencia.observaciones.ToString().Trim();
            //if (vGeoreferencia.fechacreacion != DateTime.MinValue)
            //    txtFechacreacion.Text = vGeoreferencia.fechacreacion.ToShortDateString();
            //if (!string.IsNullOrEmpty(vGeoreferencia.usuariocreacion))
            //    txtUsuariocreacion.Text = vGeoreferencia.usuariocreacion.ToString().Trim();
            //if (vGeoreferencia.fecultmod != DateTime.MinValue)
            //    txtFecultmod.Text = vGeoreferencia.fecultmod.ToShortDateString();
            //if (!string.IsNullOrEmpty(vGeoreferencia.usuultmod))
            //    txtUsuultmod.Text = vGeoreferencia.usuultmod.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
          
           
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    





    private Xpinn.FabricaCreditos.Entities.Georeferencia ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();

        if (Session["Cod_persona"].ToString() != null)
            vGeoreferencia.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        //if(txtLatitud.Text.Trim() != "")
        //    vGeoreferencia.latitud = Convert.ToString(txtLatitud.Text.Trim());
        //if(txtLongitud.Text.Trim() != "")
        //    vGeoreferencia.longitud = Convert.ToString(txtLongitud.Text.Trim());
        //if(txtObservaciones.Text.Trim() != "")
        //    vGeoreferencia.observaciones = Convert.ToString(txtObservaciones.Text.Trim());
       
            //vGeoreferencia.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
       
            //vGeoreferencia.usuariocreacion = "";

            //vGeoreferencia.fecultmod = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
      
            //vGeoreferencia.usuultmod = "";

            return vGeoreferencia;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            if (idObjeto != "")
                vGeoreferencia = GeoreferenciaServicio.ConsultarGeoreferencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //if (txtcodgeoreferencia.Text != "") vGeoreferencia.codgeoreferencia = Convert.ToInt64(txtcodgeoreferencia.Text.Trim());
            vGeoreferencia.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            //if (txtLatitud.Text != "") vGeoreferencia.latitud = Convert.ToString(txtLatitud.Text.Trim());
            vGeoreferencia.latitud = (TextBox3.Text != "") ? Convert.ToString(TextBox3.Text.Trim()) : String.Empty;
            //if (txtLongitud.Text != "") vGeoreferencia.longitud = Convert.ToString(txtLongitud.Text.Trim());
            vGeoreferencia.longitud = (TextBox4.Text != "") ? Convert.ToString(TextBox4.Text.Trim()) : String.Empty;
            //if (txtObservaciones.Text != "") vGeoreferencia.observaciones = Convert.ToString(txtObservaciones.Text.Trim());
            vGeoreferencia.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
            vGeoreferencia.fechacreacion = DateTime.Now;
            //if (txtUsuariocreacion.Text != "") vGeoreferencia.usuariocreacion = Convert.ToString(txtUsuariocreacion.Text.Trim());
            vGeoreferencia.usuariocreacion = lblUsuario.Text; ;
            vGeoreferencia.fecultmod = DateTime.Now;
            //if (txtUsuultmod.Text != "") vGeoreferencia.usuultmod = Convert.ToString(txtUsuultmod.Text.Trim());
            




            vGeoreferencia.usuultmod = lblUsuario.Text;

           
            

                vGeoreferencia.numero_radicacion = Convert.ToInt64(Session["Cod_radicacion"].ToString());

                vGeoreferencia.NOMBRE_REFERENCIAS = txtreferencia1.Text;
                
                if(Radioreferencia11.Checked==true)
                vGeoreferencia.TIEMPO_NEGOCIO="Mas de 1 a�o";
                else
                vGeoreferencia.TIEMPO_NEGOCIO="Menos de 1 a�o";


                if (Radioreferencia13.Checked == true)
                    vGeoreferencia.PROPIETARIO_SI_NO = "Propietario";
                else
                    vGeoreferencia.PROPIETARIO_SI_NO = "Empleado";

                if (Radioreferencia15.Checked == true)
                    vGeoreferencia.CONCEPTO = "Bueno";

                  if (Radioreferencia16.Checked == true)
                    vGeoreferencia.CONCEPTO = "Regular";

                if (Radioreferencia17.Checked == true)
                    vGeoreferencia.CONCEPTO = "Malo";

                if (Radioreferencia18.Checked == true)
                    vGeoreferencia.CONCEPTO = "Ninguno";
                vGeoreferencia = GeoreferenciaServicio.CrearGeoreferencia(vGeoreferencia, (Usuario)Session["usuario"]);

                vGeoreferencia.NOMBRE_REFERENCIAS = txtreferencia2.Text;


                if (Radioreferencia21.Checked == true)
                    vGeoreferencia.TIEMPO_NEGOCIO = "Mas de 1 a�o";
                else
                    vGeoreferencia.TIEMPO_NEGOCIO = "Menos de 1 a�o";

                if (Radioreferencia23.Checked == true)
                    vGeoreferencia.TIEMPO_NEGOCIO = "Propietario";
                else
                    vGeoreferencia.TIEMPO_NEGOCIO = "Empleado";

                if (Radioreferencia25.Checked == true)
                    vGeoreferencia.CONCEPTO = "Bueno";

                if (Radioreferencia26.Checked == true)
                    vGeoreferencia.CONCEPTO = "Regular";

                if (Radioreferencia27.Checked == true)
                    vGeoreferencia.CONCEPTO = "Malo";

                if (Radioreferencia28.Checked == true)
                    vGeoreferencia.CONCEPTO = "Ninguno";

                vGeoreferencia = GeoreferenciaServicio.CrearGeoreferencia(vGeoreferencia, (Usuario)Session["usuario"]);

                vGeoreferencia.NOMBRE_REFERENCIAS = txtreferencia3.Text;
                if (Radioreferencia31.Checked == true)
                    vGeoreferencia.TIEMPO_NEGOCIO = "Mas de 1 a�o";
                else
                    vGeoreferencia.TIEMPO_NEGOCIO = "Menos de 1 a�o";

                if (Radioreferencia33.Checked == true)
                    vGeoreferencia.TIEMPO_NEGOCIO = "Propietario";
                else
                    vGeoreferencia.TIEMPO_NEGOCIO = "Empleado";
                if (Radioreferencia35.Checked == true)
                    vGeoreferencia.CONCEPTO = "Bueno";

                if (Radioreferencia36.Checked == true)
                    vGeoreferencia.CONCEPTO = "Regular";

                if (Radioreferencia37.Checked == true)
                    vGeoreferencia.CONCEPTO = "Malo";

                if (Radioreferencia38.Checked == true)
                    vGeoreferencia.CONCEPTO = "Ninguno";

                
                vGeoreferencia = GeoreferenciaServicio.CrearGeoreferencia(vGeoreferencia, (Usuario)Session["usuario"]);
                idObjeto = vGeoreferencia.codgeoreferencia.ToString();
            

            Session[GeoreferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");
            //Navegar(Pagina.Detalle);
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/SolicitudCreditosRecogidos/Lista.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/CapturaDocumentos/DocumentosAnexos/Lista.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/SolicitudCreditosRecogidos/Lista.aspx");
    }

  

    //private void fnSetValue()
    //{
    //    string strtest = hidtest.Value;
    //}


    private void InicializarGoogleMapsServer(double a, double b)
    {

                
        // centramos en Paraguay el mapa al iniciar
        GLatLng latlng = new GLatLng(4.60971, -74.08175);

        // centramos el mapa en las coordenadas
        gMap.setCenter(latlng,16);

        // agregamos los controles de navegacion y zoom a los 3
        gMap.addControl(new GControl(GControl.preBuilt.LargeMapControl));

        // agregamos los listeners
        gMap.addListener(new GListener(gMap.GMap_Id, GListener.Event.zoomend,
        string.Format(@"
            function(oldLevel, newLevel)
            {{
              if ((newLevel > 7) || (newLevel < 4))
              {{
                  var ev = new serverEvent('AdvancedZoom', {0});
                  ev.addArg(newLevel);
                  ev.send();
              }}
            }}
            ", gMap.GMap_Id)));

    }

    protected internal virtual void Render(HtmlTextWriter writer) 
    {
        
    
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      
       
        //  UpdatePanel1.Visible = true;
       // UpdatePanel2.Visible = false;
       
       
        
       
        //InicializarGoogleMapsServer();

    }

    protected string gMap_Click(object s, GAjaxServerEventArgs e)
    {
        
        // Mostramos las coordenadas
        Response.Write("Sus Coordenadas son: \r\n Latitud: " + e.point.lat + "\r\n" + "Logitud: " + e.point.lng);
        // creamos las coordenadas con el clic que hizo el usuario
        GLatLng latlng = new GLatLng(e.point.lat, e.point.lng);
        posiionlat = Convert.ToString(e.point.lat);
        posiionlon = Convert.ToString(e.point.lng);
        
        // limpiamos todos los marcadores del mapa
        gMap.resetMarkers();
        // creamos un marcador
        GMarkerOptions mkrOpts = new GMarkerOptions();
        // seteamos que no se pueda arrastrar, asi estara obligado a dar clic de nuevo si quiere cambiar
        mkrOpts.draggable = false;
        // creamos un marcador nuevo, con las coordenadas del usuario
        GMarker marker = new GMarker(latlng, mkrOpts);
        // agregamos el marcador al mapa
        gMap.Add(marker);
        // centramos el mapa con las coordenadas del usuario
        gMap.setCenter(latlng);
        // agregamos un tool tip para facilitar el entendimiento al usuario
        marker.options.title = "Aqui se encuentra";
        // retornamos vacio
        TextBox3.Text = posiionlat;
        TextBox4.Text = posiionlon;
        return string.Empty;
    }
    protected void Radioreferencia11_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia11.Checked = true;
        if (Radioreferencia11.Checked == true)
            Radioreferencia12.Checked = false;
       

    }
    protected void Radioreferencia12_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia12.Checked = true;
        if (Radioreferencia12.Checked == true)
            Radioreferencia11.Checked = false;

    }
    protected void Radioreferencia13_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia13.Checked = true;
        if (Radioreferencia13.Checked == true)
            Radioreferencia14.Checked = false;

    }
    protected void Radioreferencia14_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia14.Checked = true;
        if (Radioreferencia14.Checked == true)
            Radioreferencia13.Checked = false;

    }
    protected void Radioreferencia15_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia15.Checked = true;
        if (Radioreferencia15.Checked == true)
        {
            Radioreferencia16.Checked = false;
            Radioreferencia17.Checked = false;
            Radioreferencia18.Checked = false;
        }
    }
    protected void Radioreferencia16_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia16.Checked = true;
        if (Radioreferencia16.Checked == true)
        {
            Radioreferencia15.Checked = false;
            Radioreferencia17.Checked = false;
            Radioreferencia18.Checked = false;
        }
    }
    protected void Radioreferencia17_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia17.Checked = true;
        if (Radioreferencia17.Checked == true)
        {
            Radioreferencia16.Checked = false;
            Radioreferencia15.Checked = false;
            Radioreferencia18.Checked = false;
        }

    }
    protected void Radioreferencia18_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia18.Checked = true;
        if (Radioreferencia18.Checked == true)
        {
            Radioreferencia16.Checked = false;
            Radioreferencia17.Checked = false;
            Radioreferencia15.Checked = false;
        }
    }
    protected void Radioreferencia21_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia21.Checked = true;
        if (Radioreferencia21.Checked == true)
            Radioreferencia22.Checked = false;


    }
    protected void Radioreferencia22_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia22.Checked = true;
        if (Radioreferencia22.Checked == true)
            Radioreferencia21.Checked = false;

    }
    protected void Radioreferencia23_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia23.Checked = true;
        if (Radioreferencia23.Checked == true)
            Radioreferencia24.Checked = false;

    }
    protected void Radioreferencia24_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia24.Checked = true;
        if (Radioreferencia24.Checked == true)
            Radioreferencia23.Checked = false;

    }
    protected void Radioreferencia25_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia25.Checked = true;
        if (Radioreferencia25.Checked == true)
        {
            Radioreferencia26.Checked = false;
            Radioreferencia27.Checked = false;
            Radioreferencia28.Checked = false;
        }
    }
    protected void Radioreferencia26_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia26.Checked = true;
        if (Radioreferencia26.Checked == true)
        {
            Radioreferencia25.Checked = false;
            Radioreferencia27.Checked = false;
            Radioreferencia28.Checked = false;
        }
    }
    protected void Radioreferencia27_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia27.Checked = true;
        if (Radioreferencia27.Checked == true)
        {
            Radioreferencia26.Checked = false;
            Radioreferencia25.Checked = false;
            Radioreferencia28.Checked = false;
        }

    }
    protected void Radioreferencia28_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia28.Checked = true;
        if (Radioreferencia28.Checked == true)
        {
            Radioreferencia26.Checked = false;
            Radioreferencia27.Checked = false;
            Radioreferencia25.Checked = false;
        }
    }
    protected void Radioreferencia31_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia31.Checked = true;
        if (Radioreferencia31.Checked == true)
            Radioreferencia32.Checked = false;


    }
    protected void Radioreferencia32_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia32.Checked = true;
        if (Radioreferencia32.Checked == true)
            Radioreferencia31.Checked = false;

    }
    protected void Radioreferencia33_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia33.Checked = true;
        if (Radioreferencia33.Checked == true)
            Radioreferencia34.Checked = false;

    }
    protected void Radioreferencia34_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia34.Checked = true;
        if (Radioreferencia34.Checked == true)
            Radioreferencia33.Checked = false;

    }
    protected void Radioreferencia35_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia35.Checked = true;
        if (Radioreferencia35.Checked == true)
        {
            Radioreferencia36.Checked = false;
            Radioreferencia37.Checked = false;
            Radioreferencia38.Checked = false;
        }
    }
    protected void Radioreferencia36_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia36.Checked = true;
        if (Radioreferencia36.Checked == true)
        {
            Radioreferencia35.Checked = false;
            Radioreferencia37.Checked = false;
            Radioreferencia38.Checked = false;
        }
    }
    protected void Radioreferencia37_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia37.Checked = true;
        if (Radioreferencia37.Checked == true)
        {
            Radioreferencia36.Checked = false;
            Radioreferencia35.Checked = false;
            Radioreferencia38.Checked = false;
        }

    }
    protected void Radioreferencia38_CheckedChanged(object sender, EventArgs e)
    {
        Radioreferencia38.Checked = true;
        if (Radioreferencia38.Checked == true)
        {
            Radioreferencia36.Checked = false;
            Radioreferencia37.Checked = false;
            Radioreferencia35.Checked = false;
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {

    }

   

    protected void z_btnAceptar_Click(object sender, EventArgs e)
    {
       double a, b;
        TextBox1.Visible = true;
        TextBox2.Visible = true;
        if (posiionlat == "") 
        {
            try
            {
                a = Convert.ToDouble(TextBox1.Text.Replace(".", ","));
                b = Convert.ToDouble(TextBox2.Text.Replace(".", ","));
                TextBox3.Text = Convert.ToString(a);
                TextBox4.Text = Convert.ToString(b);
                InicializarGoogleMapsServer(a, b);
                mostarpuntomapa(a, b);
            }
            catch
            {
                
            }


        }
        else
        {
           
                
            
        }

    }

    protected string mostarpuntomapa(double a, double b)
    {
        // Mostramos las coordenadas
       // Response.Write("Sus Coordenadas son: \r\n Latitud: " + e.point.lat + "\r\n" + "Logitud: " + e.point.lng);
        // creamos las coordenadas con el clic que hizo el usuario
        GLatLng latlng = new GLatLng(a, b);
       // txtLatitud.Text = Convert.ToString(e.point.lat);
        //txtLongitud.Text = Convert.ToString(e.point.lng);
        // limpiamos todos los marcadores del mapa
        gMap.resetMarkers();
        // creamos un marcador
        GMarkerOptions mkrOpts = new GMarkerOptions();
        // seteamos que no se pueda arrastrar, asi estara obligado a dar clic de nuevo si quiere cambiar
        mkrOpts.draggable = false;
        // creamos un marcador nuevo, con las coordenadas del usuario
        GMarker marker = new GMarker(latlng, mkrOpts);
        // agregamos el marcador al mapa
        gMap.Add(marker);
        // centramos el mapa con las coordenadas del usuario
        gMap.setCenter(latlng);
        // agregamos un tool tip para facilitar el entendimiento al usuario
        marker.options.title = "Aqui se encuentra";
        // retornamos vacio
        return string.Empty;
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        z_btnAceptar_Click(null,null);
       
    }
    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {

    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");
    }
}

