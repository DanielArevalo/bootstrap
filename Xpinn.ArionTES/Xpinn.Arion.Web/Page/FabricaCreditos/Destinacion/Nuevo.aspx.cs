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
using System.IO;
using System.Drawing.Imaging;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.DestinacionService destinServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[destinServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(destinServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(destinServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[destinServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[destinServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Enabled = false;
                    txtCodigo.Text = "0";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Destinacion vdestinacion = new Xpinn.FabricaCreditos.Entities.Destinacion();

            if (idObjeto != "")
                vdestinacion = destinServicio.ConsultarDestinacion(Convert.ToInt32(idObjeto), (Usuario)Session["usuario"]);

            vdestinacion.cod_destino = Convert.ToInt32(txtCodigo.Text.Trim());
            vdestinacion.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            //Datos de oficina virtual
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
            vdestinacion.oficinaVirtual = chkOficinaVirtual.Checked ? 1 : 0;
            vdestinacion.enlace = txtEnlace.Text;
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
                vdestinacion.cod_destino = Convert.ToInt32(idObjeto);
                destinServicio.ModificarDestinacion(vdestinacion, (Usuario)Session["usuario"], foto, banner);
            }
            else
            {
                vdestinacion = destinServicio.CrearDestinacion(vdestinacion, (Usuario)Session["usuario"], foto, banner);
                idObjeto = vdestinacion.cod_destino.ToString();
            }

            Session[destinServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Destinacion vdestinacion = new Xpinn.FabricaCreditos.Entities.Destinacion();
            vdestinacion = destinServicio.ConsultarDestinacion(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vdestinacion.cod_destino.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vdestinacion.cod_destino.ToString().Trim());
            if (!string.IsNullOrEmpty(vdestinacion.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vdestinacion.descripcion.ToString().Trim());

            chkOficinaVirtual.Checked = vdestinacion.oficinaVirtual == 1 ? true : false;
            txtEnlace.Text = vdestinacion.enlace;

            //CARGAR FOTO 
            if (vdestinacion.foto != null)
            {
                imgFoto.ImageUrl = Bytes_A_Archivo(vdestinacion.cod_destino.ToString(), vdestinacion.foto, 1);
                imgFoto.ImageUrl = "..\\..\\..\\Images\\" + vdestinacion.cod_destino + "foto.jpg";                
            }
            //CARGAR BANNER 
            if (vdestinacion.banner != null)
            {
                imgBanner.ImageUrl = Bytes_A_Archivo(vdestinacion.cod_destino.ToString(), vdestinacion.banner, 2);
                imgBanner.ImageUrl = "..\\..\\..\\Images\\" + vdestinacion.cod_destino + "banner.jpg";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(destinServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    public string Bytes_A_Archivo(string id, Byte[] ImgBytes, int tipo = 0)
    {
        Stream stream = null;
        string fileName;
        if (tipo == 1)
            fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(id + "foto" + ".jpg");
        else if (tipo == 2)
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
            }
            else
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
        }
        else
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
                if (img)
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

    public string Bytes_A_Archivo(string idLineaServicio, Byte[] ImgBytes, bool img)
    {
        string nameF = "";
        if (img)
            nameF = "Banner";
        else
            nameF = "Foto";
        Stream stream = null;
        string fileName = Server.MapPath("..\\..\\..\\Images\\") + Path.GetFileName(idLineaServicio + nameF + ".jpg");
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
                if (img)
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
}