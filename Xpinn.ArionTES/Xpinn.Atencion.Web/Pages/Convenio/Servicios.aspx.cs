using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using xpinnWSAppFinancial;
using System.IO;
using System.Data;
using System.Globalization;

public partial class Servicios : GlobalWeb
{
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppServices = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSAppFinancial.LineaServicios linea = new xpinnWSAppFinancial.LineaServicios();
    xpinnWSLogin.Persona1 _persona;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.solicitarServicio, "Sol");
            Site toolBar = (Site)Master;
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EstadoDeCuenta", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        _persona = (xpinnWSLogin.Persona1)Session["persona"];

        string filtro = "";
        if (!string.IsNullOrEmpty(txtFiltro.Text))
        {
            filtro += " and l.nombre like '%" + txtFiltro.Text.ToUpper() + "%'";
        }
        if(ddlCategoria.SelectedValue != "0")
        {
            filtro += " and l.tipo_servicio = " + ddlCategoria.SelectedValue;
        }
        filtro += " and l.oficina_virtual = 1 ";
        List<xpinnWSAppFinancial.LineaServicios> lstServicios = new List<xpinnWSAppFinancial.LineaServicios>();
        lstServicios = AppServices.ListarServicios(linea, filtro, Session["sec"].ToString());
        if (lstServicios != null)
        {
            cargarServicios(lstServicios);
        }
        DateTime fecha = DateTime.Now;
        txtFechaServicio.Text = fecha.ToString();
        txtFechaPago.Text = fecha.ToString();
    }


    private void cargarServicios(List<LineaServicios> lstServicios)
    {
        //Ordena los servicion por tipo
        lstServicios = lstServicios.OrderBy(x => x.nombre).ToList();
        string seccion = "";
        foreach (var item in lstServicios){
            if(item.nomtiposervicio != seccion.Trim())
            {                
                if (!string.IsNullOrEmpty(seccion))                                     
                    pnlCards.Controls.Add(new LiteralControl("</div></div></br>"));
                pnlCards.Controls.Add(new LiteralControl(@"<div onclick='ocultarMostrarPanel("+ "\"" + item.nomtiposervicio.Trim().Replace(" ","_")+ "\"" + @")' style='cursor: pointer;'>
                                                            <div class='col-md-12'>
                                                                     <a href = '#!' class='accordion-titulo'>" + item.nomtiposervicio + @"<span class='toggle-icon'></span></a>
                                                                 </div>
                                                             </div>"));
                pnlCards.Controls.Add(new LiteralControl(@"<div class='col-md-12' id='" + item.nomtiposervicio.Trim().Replace(" ", "_") + @"'><br/><div class='row justify-content-md-center'>"));
                seccion = item.nomtiposervicio;
            }

            string url = "../../Imagenes/default.jpg";
            if (item.foto != null)
            {
                try
                {
                    Bytes_A_Archivo(item.cod_linea_servicio, item.foto);
                    url = "../../Imagenes/TempImages/" + item.cod_linea_servicio + ".jpg";
                }
                catch
                {
                    url = "../../Imagenes/default.jpg";
                }
            }

            pnlCards.Controls.Add(new LiteralControl(@"<div class='col-md-3 col-sm-12'>
                                                           <div class='card' style='width: 20rem;width: 20rem;margin: 10px auto;padding: 10px;border: 1px solid #c1b1b18f;border-radius: 5%;'>
                                                               <img class='card-img-top' style='object-fit: cover; width: 100%; height: 100px;' src='" + url+ @"' alt='Card image cap'>
                                                               <div class='card-block'>
                                                                   <h4 class='card-title' style='height: 38px;'>" + item.nombre + @"</h4>"));

            //Agrega boton
            Button myButton = new Button();
            pnlCards.Controls.Add(myButton);
            myButton.ToolTip = item.nombre;
            myButton.Text = "Solicitar";
            myButton.ID = item.cod_linea_servicio+"|"+item.maximo_plazo+"|"+item.maximo_valor;
            myButton.CssClass = "btn btn-primary";
            myButton.Click += btnSolicitar_Click;

            pnlCards.Controls.Add(new LiteralControl(@"</div></div></div>"));
            
        }
        pnlCards.Controls.Add(new LiteralControl("</div></div>"));
    }    

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        try
        {             
            string cadena = ((Button)sender).ID;
            string[] datos;
            datos = cadena.Split('|');
            string linea = datos[0].ToString();
            if (!string.IsNullOrEmpty(datos[1].ToString()))
                txtPlazoMax.Text = datos[1].ToString();
            if (!string.IsNullOrEmpty(datos[2].ToString()))
                txtValorMax.Text = Convert.ToDecimal(datos[2].ToString()).ToString("C0");
            string titulo = ((Button)sender).ToolTip;
            txtLinea.Text = linea;
            txtServicio.Text = titulo;
            pnlCards.Visible = false;
            pnlDatos.Visible = true;
            if (!string.IsNullOrEmpty(txtLinea.Text))
            {
                //carga enlace e imagen promocional
                _persona = (xpinnWSLogin.Persona1)Session["persona"];
                string filtro = "";
                filtro += " and l.cod_linea_servicio = "+Convert.ToInt32(txtLinea.Text);
                List<xpinnWSAppFinancial.LineaServicios> lstServicios = new List<xpinnWSAppFinancial.LineaServicios>();
                xpinnWSAppFinancial.LineaServicios lin = new LineaServicios();
                lstServicios = AppServices.ListarServicios(lin, filtro, Session["sec"].ToString());
                if (lstServicios != null)
                {
                    lin = lstServicios.ElementAt(0);
                    if (!string.IsNullOrWhiteSpace(lin.enlace))
                    {
                        pnlinfo.Controls.Add(new LiteralControl("<br><br><a href='"+lin.enlace+ "' target='_blank' class='btn btn-success'>Ver página</a><br><br>"));
                    }
                    if(lin.banner != null)
                    {
                        string url = "../../Imagenes/default.jpg";
                        try
                        {
                            Bytes_A_Archivo("banner"+lin.cod_linea_servicio, lin.banner);
                            url = "../../Imagenes/TempImages/banner" + lin.cod_linea_servicio+".jpg";
                            pnlinfo.Controls.Add(new LiteralControl("<div><img class='card-img-top' style='object-fit: cover; margin:0 auto; margin-bottom: 15px; width: 700px; height: auto;' src='" + url + @"' alt='Card image cap'></div>"));
                        }
                        catch
                        {
                            url = "../../Imagenes/default.jpg";
                        }                        
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            lblError.Text = "Se presento un problema " + ex.Message;
        }
    }

    private bool Validar()
    {
        if (string.IsNullOrWhiteSpace(txtFechaPago.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtNumeroCuotas.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtValor.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtFechaServicio.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (!string.IsNullOrWhiteSpace(txtPlazoMax.Text) && txtPlazoMax.Text != "0")
        {
            if(Convert.ToInt32(txtNumeroCuotas.Text) > Convert.ToInt32(txtPlazoMax.Text))
            {
                lblError.Text = "El número de cuotas no puede superar el plazo máximo";
                return false;
            }
        }
        if (!string.IsNullOrWhiteSpace(txtValorMax.Text.Replace("$","")) && txtValorMax.Text != "0")
        {
            if (Convert.ToDecimal(txtValor.Text.Replace(",","").Replace(".","").Replace("$","")) > Convert.ToDecimal(txtValorMax.Text.Replace("$", "").Replace(",", "").Replace(".", "")))
            {
                lblError.Text = "No puede superar el valor máximo para este servicio";
                return false;
            }
        }
        return true;
    }

    protected void btnCerrarCambioCuota_Click(object sender, EventArgs e)
    {
        txtFechaPago.Text = "";
        txtLinea.Text = "";
        txtNumeroCuotas.Text = "";
        txtServicio.Text = "";
        txtValor.Text = "";
        pnlCards.Visible = true;
        pnlDatos.Visible = false;
    }
    

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (Validar())
        {
            _persona = (xpinnWSLogin.Persona1)Session["persona"];
            //Guarda la solicitud
            xpinnWSAppFinancial.Servicio serv = new Servicio();
            serv.fecha_solicitud = Convert.ToDateTime(txtFechaServicio.Text);
            serv.cod_persona = _persona.cod_persona;
            serv.cod_linea_servicio = txtLinea.Text;
            serv.valor_total = Convert.ToDecimal(txtValor.Text.Replace(",","").Replace(".","").Replace("$",""));
            serv.fecha_primera_cuota = Convert.ToDateTime(txtFechaPago.Text);
            serv.numero_cuotas = Convert.ToInt32(txtNumeroCuotas.Text);
            serv.descripcion = txtDescripcion.Text;
            serv.observaciones = txtDescripcion.Text;
            serv = AppServices.GrabarSolicitudServicio(serv, Session["sec"].ToString());
            if(serv != null)
            {
                btnCerrarCambioCuota_Click(null, null);
                pnlCards.Visible = false;
                pnlFinal.Visible = true;
                lblCodigoGenerado.Text = serv.numero_servicio.ToString();
            }
        }
        else
        {            
            lblError.Visible = true;
        }
    }


    public string Bytes_A_Archivo(string id, Byte[] ImgBytes)
    {
        Stream stream = null;
        try
        {
            File.Delete(Server.MapPath("..\\..\\Imagenes\\TempImages\\") + Path.GetFileName(id + ".jpg"));
        }catch(Exception e)
        {

        }
        string fileName = Server.MapPath("..\\..\\Imagenes\\TempImages\\") + Path.GetFileName(id + ".jpg");
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {        
    }

}                  