using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Net;
using Subgurim.Controles;
using System.Drawing;
using System.Globalization;

using Subgurim.Controles; //CONTROLES GOOGLE-MAPS

public partial class Lista : GlobalWeb
{
    ClienteService clienteServicio = new ClienteService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(clienteServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                inicializargMap();

                lblDropdown.Visible = false;
                ddlAsesor.Visible = false;
                ddlCliente.Visible = false;
                LlenarComboAsesores(0, 0);
                LlenarComboOficinas();
                LlenarComboClientes(0,0);
                LlenarComboZonas();

                ddlZona_SelectedIndexChanged(ddlZona1, null);

                //CargarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);
        inicializargMap();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Validate("vgGuardar");
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, clienteServicio.CodigoPrograma);

            if (ddlZona1.SelectedValue == "2")//CONSULTA DE CLIENTES
            {
                List<Cliente> lstClientes = new List<Cliente>();
                lstClientes = clienteServicio.ListarClientesPersonageo(ObtenerValoresCliente(), (Usuario)Session["usuario"]);

                if (lstClientes.Count != 0)
                {
                    foreach (Cliente iCliente in lstClientes)
                    {
                        using (var client = new WebClient())
                        {
                            if (iCliente.latitud != null && iCliente.longitud != null) //filtrar por LATITUD Y LONGITUD 
                            {
                                GMap1.enableDragging = true;
                                GMap1.Language = "es";
                                GMap1.BackColor = Color.White;
                                GMap1.Height = 600;
                                GMap1.Width = 1000;
                                GMap1.enableHookMouseWheelToZoom = true;
                                GMap1.enableRotation = true;
                                GMap1.setCenter(new GLatLng(Convert.ToDouble(iCliente.latitud), Convert.ToDouble(iCliente.longitud)), 9);
                                GLatLng latlng = new GLatLng(Convert.ToDouble(iCliente.latitud), Convert.ToDouble(iCliente.longitud));
                                GMarkerOptions mkrOpts = new GMarkerOptions();
                                mkrOpts.draggable = false;

                                GMarker marker = new GMarker(latlng, mkrOpts);

                                GMap1.Add(marker);
                                GMap1.setCenter(latlng);

                                GInfoWindow window = new GInfoWindow(marker, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iCliente.NombreCompleto + "<br/>Ubicación:<br/>" + iCliente.nomciudad + "<br/>Dirección:<br/>" + iCliente.Direccion + "<br/>Teléfono:<br/>" + iCliente.Telefono, false, GListener.Event.mouseover);
                                GMap1.addInfoWindow(window);
                            }
                            else //filtrar por direccion
                            {
                                string sMapKey = System.Configuration.ConfigurationManager.AppSettings.Get("googlemaps.Subgurim.net");
                                GeoCode geocode = new GeoCode();
                                string NEWDireccion;

                                if (iCliente.nomciudad != "" && iCliente.Direccion != "" && iCliente.nomciudad != "DESCONOCIDA")
                                {
                                    NEWDireccion = iCliente.Direccion + " " + iCliente.nomciudad + " COLOMBIA";
                                    geocode = GMap.geoCodeRequest(NEWDireccion, sMapKey);
                                }
                                else
                                {
                                    geocode = GMap.geoCodeRequest(iCliente.Direccion + " COLOMBIA", sMapKey);
                                }
                                if (geocode.valid == true)
                                {
                                    //LATITUD
                                    string Lati  , Long;
                                    Lati = geocode.Placemark.coordinates.lat.ToString();
                                    //LONGITUD
                                    Long = geocode.Placemark.coordinates.lng.ToString();

                                    GMap1.setCenter(new GLatLng(Convert.ToDouble(Lati), Convert.ToDouble(Long)), 9);
                                    GLatLng latlng = new GLatLng(Convert.ToDouble(Lati), Convert.ToDouble(Long));
                                    GMarkerOptions mkrOpts = new GMarkerOptions();
                                    mkrOpts.draggable = false;

                                    GMarker marker = new GMarker(latlng, mkrOpts);

                                    GMap1.Add(marker);
                                    GMap1.setCenter(latlng);

                                    GInfoWindow window = new GInfoWindow(marker, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iCliente.NombreCompleto + "<br/>Ubicación:<br/>" + iCliente.nomciudad + "<br/>Dirección:<br/>" + iCliente.Direccion + "<br/>Teléfono:<br/>" + iCliente.Telefono, false, GListener.Event.mouseover);
                                    GMap1.addInfoWindow(window);
                                }
                                else
                                {
                                    //cont++;
                                    //VerError("No se puedo encontrar la ubicación de " + cont + " Clientes.");
                                }

                            }
                        }
                    }
                }
            }
            else if (ddlZona1.SelectedValue == "1")//CONSULTA DE ASESOREAS
            {
                int cont = 0;
                inicializargMap();

                    EjecutivoService ejecutivoServicio = new EjecutivoService();
                    List<Ejecutivo> lstEjecutivos = new List<Ejecutivo>();
                    Ejecutivo ejec = new Ejecutivo();

                    if (ddlAsesor.SelectedIndex > 1)
                        ejec.icodigo = Convert.ToInt64(ddlAsesor.SelectedValue);
                
                   //(List<Ejecutivo>)Session["DATOSEJECUTOVOS"];
                    lstEjecutivos = ejecutivoServicio.ListarAsesoresgeoreferencia(ObtenerValoresEjecutivo(), (Usuario)Session["usuario"],filtro);

                    if (lstEjecutivos.Count != 0)
                    {
                       
                        foreach (Ejecutivo iEjecutivo in lstEjecutivos)
                        {
                            using (var client = new WebClient())
                            {
                                if (iEjecutivo.latitud != null && iEjecutivo.longitud != null) //filtrar por LATITUD Y LONGITUD 
                                {
                                    GMap1.enableDragging = true;
                                    GMap1.Language = "es";
                                    GMap1.BackColor = Color.White;
                                    GMap1.Height = 600;
                                    GMap1.Width = 1000;
                                    GMap1.enableHookMouseWheelToZoom = true;
                                    GMap1.enableRotation = true;
                                    GMap1.setCenter(new GLatLng(Convert.ToDouble(iEjecutivo.latitud), Convert.ToDouble(iEjecutivo.longitud)), 9);
                                    GLatLng latlng = new GLatLng(Convert.ToDouble(iEjecutivo.latitud), Convert.ToDouble(iEjecutivo.longitud));
                                    GMarkerOptions mkrOpts = new GMarkerOptions();
                                    mkrOpts.draggable = false;

                                    GMarker marker = new GMarker(latlng, mkrOpts);

                                    GMap1.Add(marker);
                                    GMap1.setCenter(latlng);

                                    GInfoWindow window = new GInfoWindow(marker, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iEjecutivo.NombreCompleto + "<br/>Ubicación:<br/>" + iEjecutivo.nomciudad + "<br/>Dirección:<br/>" + iEjecutivo.Direccion + "<br/>Teléfono:<br/>" + iEjecutivo.Telefono, false, GListener.Event.mouseover);
                                    GMap1.addInfoWindow(window);
                                }
                                else //filtrar por direccion
                                {
                                    string sMapKey = System.Configuration.ConfigurationManager.AppSettings.Get("googlemaps.Subgurim.net");
                                    GeoCode geocode = new GeoCode();
                                    string NEWDireccion;

                                    if (iEjecutivo.nomciudad != "" && iEjecutivo.Direccion != "" && iEjecutivo.nomciudad != "DESCONOCIDA")
                                    {
                                        NEWDireccion = iEjecutivo.Direccion + " " + iEjecutivo.nomciudad + " COLOMBIA";
                                        geocode = GMap.geoCodeRequest(NEWDireccion, sMapKey);
                                    }
                                    else
                                    {
                                        geocode = GMap.geoCodeRequest(iEjecutivo.Direccion + " COLOMBIA", sMapKey);
                                    }

                                    if (geocode.valid == true)
                                    {
                                        //LATITUD
                                        string Lati, Long;
                                        Lati = geocode.Placemark.coordinates.lat.ToString();
                                        //LONGITUD
                                        Long = geocode.Placemark.coordinates.lng.ToString();

                                        GMap1.setCenter(new GLatLng(Convert.ToDouble(Lati), Convert.ToDouble(Long)), 9);
                                        GLatLng latlng = new GLatLng(Convert.ToDouble(Lati), Convert.ToDouble(Long));
                                        GMarkerOptions mkrOpts = new GMarkerOptions();
                                        mkrOpts.draggable = false;

                                        GMarker marker = new GMarker(latlng, mkrOpts);

                                        GMap1.Add(marker);
                                        GMap1.setCenter(latlng);

                                        GInfoWindow window = new GInfoWindow(marker, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iEjecutivo.NombreCompleto + "<br/>Ubicación:<br/>" + iEjecutivo.nomciudad + "<br/>Dirección:<br/>" + iEjecutivo.Direccion + "<br/>Teléfono:<br/>" + iEjecutivo.Telefono, false, GListener.Event.mouseover);
                                        GMap1.addInfoWindow(window);
                                    }
                                    else
                                    {
                                        cont++;
                                        VerError("No se puedo encontrar la ubicación de "+cont+" Asesores.");
                                    }
                                }
                            }
                        }
                    }
                
            }

        }
    }
    
                


    protected string mostarpuntomapa(double a, double b)
    {
        // Mostramos las coordenadas
        // Response.Write("Sus Coordenadas son: \r\n Latitud: " + e.point.lat + "\r\n" + "Logitud: " + e.point.lng);
        // creamos las coordenadas con el clic que hizo el usuario
        GLatLng latlng = new GLatLng(a, b);
        //txtLatitud.Text = Convert.ToString(e.point.lat);
        //txtLongitud.Text = Convert.ToString(e.point.lng);
        // limpiamos todos los marcadores del mapa
        //gMap.resetMarkers();
        // creamos un marcador
        GMarkerOptions mkrOpts = new GMarkerOptions();
        // seteamos que no se pueda arrastrar, asi estara obligado a dar clic de nuevo si quiere cambiar
        mkrOpts.draggable = false;
        // creamos un marcador nuevo, con las coordenadas del usuario
        GMarker marker = new GMarker(latlng, mkrOpts);
        // agregamos el marcador al mapa
        GMap1.Add(marker);
        // centramos el mapa con las coordenadas del usuario
        GMap1.setCenter(latlng);
        // agregamos un tool tip para facilitar el entendimiento al usuario
        marker.options.title = "Aqui se encuentra";
        // retornamos vacio
        return string.Empty;
    }


    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOficina.SelectedIndex != 0)
        {
            LlenarComboAsesores(1, Convert.ToInt64(ddlOficina.SelectedValue));
            LlenarComboClientes(1, Convert.ToInt64(ddlOficina.SelectedValue));
        }
        else
        {
            LlenarComboAsesores(0, 0);
            LlenarComboClientes(0, 0);
        }
    }

    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCliente.SelectedIndex = 0;
        ddlAsesor.SelectedIndex = 0;
        
        if (ddlZona1.SelectedValue == "2")
        {
            lblDropdown.Visible = true;
            lblDropdown.Text = "Cliente ";
            ddlCliente.Visible = true;
            ddlAsesor.Visible = false;
        }
        else if (ddlZona1.SelectedValue == "1")
        {
            lblDropdown.Visible = true;
            lblDropdown.Text = "Asesor ";
            ddlCliente.Visible = false;
            ddlAsesor.Visible = true;
        }
        else
        {
            ddlCliente.Visible = false;
            ddlAsesor.Visible = false;
            lblDropdown.Visible = false;
        }
    }

    #region LLENADO DE COMBOS

    protected void 
        LlenarComboAsesores(Int64 iOpcion, Int64 iDato)
    {
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();
        switch (iOpcion)
        {
            case 1: ejec.IOficina = iDato;
                break;
            case 2: ejec.icodzona = iDato;
                break;
        }
        List<Ejecutivo> lstejecutivo = new List<Ejecutivo>();
        lstejecutivo = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);       
        Session["DATOSEJECUTOVOS"] = lstejecutivo;
        ddlAsesor.DataSource = lstejecutivo;
        ddlAsesor.DataTextField = "NombreCompleto";
        ddlAsesor.DataValueField = "IdEjecutivo";
        ddlAsesor.DataBind();
        ddlAsesor.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlAsesor.SelectedIndex = 0;
        if (ddlAsesor.Items.Count != 1)
        {
            ddlAsesor.Items.Insert(1, new ListItem("Todos", "-1"));
        }
    }


    protected void LlenarComboOficinas()
    {
        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlOficina.SelectedIndex = 0;
        ddlOficina.DataBind();
    }

    protected void LlenarComboZonas()
    {
        ddlZona1.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlZona1.Items.Insert(1, new ListItem("Consulta Asesores", "1"));
        ddlZona1.Items.Insert(2, new ListItem("Consulta Clientes", "2"));
        ddlZona1.SelectedIndex = 0;
        ddlZona1.DataBind();
    }

    protected void LlenarComboClientes(Int64 iOpcion, Int64 iDato)
    {
        Cliente cliente = new Cliente();
        switch (iOpcion)
        {
            case 1: cliente.cod_oficina = iDato;
                break;
            case 2: cliente.cod_zona = iDato;
                break;
            case 3: cliente.cod_asesor = iDato;
                break;
        }

        ddlCliente.DataSource = clienteServicio.ListarClientesPersona(cliente, (Usuario)Session["usuario"]);
        ddlCliente.DataTextField = "NombreCompleto";
        ddlCliente.DataValueField = "IdCliente";
        ddlCliente.DataBind();
        ddlCliente.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        if (ddlCliente.Items.Count != 1)
        {
            ddlCliente.Items.Insert(1, new ListItem("Todos", "-1"));
        }
    }

    #endregion

  

    protected void ddlAsesor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAsesor.SelectedIndex >= 1 && ddlZona1.SelectedIndex == 1)
        {
            LlenarComboClientes(3, Convert.ToInt64(ddlAsesor.SelectedValue));
        }
        else
        {
           
        }
    }

    private Cliente ObtenerValoresCliente()
    {
        Cliente cliente = new Cliente();

        if (ddlOficina.SelectedValue != "0")
            cliente.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
        //if (ddlZona.SelectedIndex != 0)
        //    cliente.cod_zona = Convert.ToInt64(ddlZona.SelectedValue);

        if (ddlAsesor.SelectedIndex != 0 && ddlAsesor.SelectedIndex != 1)
            cliente.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
        if (ddlCliente.SelectedIndex != 1)
            cliente.cod_persona = Convert.ToInt64(ddlCliente.SelectedValue);

        return cliente;
    }

    string filtro = "";
    private Ejecutivo ObtenerValoresEjecutivo()
    {
        Ejecutivo Ejecutivo = new Ejecutivo();


        if (ddlOficina.SelectedValue != "0")
            Ejecutivo.IOficina = Convert.ToInt64(ddlOficina.SelectedValue);
        //if (ddlZona.SelectedIndex != 0)
        //    cliente.cod_zona = Convert.ToInt64(ddlZona.SelectedValue);

        //if (ddlAsesor.SelectedIndex != 0 && ddlAsesor.SelectedIndex != 1)
        //    Ejecutivo.IdEjecutivo = Convert.ToInt64(ddlAsesor.SelectedValue);
        if (ddlAsesor.SelectedIndex >= 2)        
            filtro = "ICODIGO = "+ ddlAsesor.SelectedValue;           
        
        return Ejecutivo;
    }

    private void inicializargMap()
    {
        GMap1.reset();
        GMap1.enableDragging = true;
        GMap1.Language = "es";
        GMap1.BackColor = Color.White;
        GMap1.Height = 600;
        GMap1.Width = 1000;
        GMap1.enableHookMouseWheelToZoom = true;
        GMap1.enableRotation = true;
    }

    private void ubicarClientesgMap(Cliente iCliente, string latitud, string longitud)
    {
        Double lat = Convert.ToDouble(latitud, CultureInfo.CreateSpecificCulture("en-US"));
        Double lon = Convert.ToDouble(longitud, CultureInfo.CreateSpecificCulture("en-US"));

        GLatLng latlon = new GLatLng(lat, lon);
        //GMap1.setCenter(latlon, 14);

        GMarker icono = new GMarker(latlon);
        GInfoWindow window = new GInfoWindow(icono, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iCliente.NombreCompleto + "<br/>Ubicación:<br/>" + iCliente.nomciudad + "<br/>Dirección:<br/>" + iCliente.Direccion + "<br/>Teléfono:<br/>" + iCliente.Telefono + "<br/>LatLng:<br/>" + latlon, false, GListener.Event.mouseover);
    }

    private void ubicarEjecutivosgMap(Ejecutivo iEjecutivo, string latitud, string longitud)
    {
        Double lat = Convert.ToDouble(latitud, CultureInfo.CreateSpecificCulture("en-US"));
        Double lon = Convert.ToDouble(longitud, CultureInfo.CreateSpecificCulture("en-US"));

        GLatLng latlon = new GLatLng(lat, lon);
        //GMap1.setCenter(latlon, 14);

        GMarker icono = new GMarker(latlon);
        GInfoWindow window = new GInfoWindow(icono, "INFORMACIÓN<br/><br/>Cliente:<br/>" + iEjecutivo.NombreCompleto + "<br/>Ubicación:<br/>" + iEjecutivo.nomciudad + "<br/>Dirección:<br/>" + iEjecutivo.Direccion + "<br/>Teléfono:<br/>" + iEjecutivo.Telefono + "<br/>LatLng:<br/>" + latlon, false, GListener.Event.mouseover);
    }


   


}