using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.NIIF.Entities;
using Xpinn.Util;


public partial class General_Controles_ctlDocumentosAnexo : System.Web.UI.UserControl
{
    #region Variables Globales

    List<Xpinn.FabricaCreditos.Entities.Imagenes> lstAnexos = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
    CreditoSolicitadoService _creditosolServicio = new CreditoSolicitadoService();
    DocumentosAnexosService documentosAnexosService = new DocumentosAnexosService();
    private string numeroCuenta;


    #endregion

    #region Metodos Inicio

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Archivos"] = null;
            Session["totalArchivos"] = null;
        }
    }

    #endregion

    #region Propiedades
    public class ArchivosClase
    {
        public string NombreArchivo { get; set; }
        public string Nombre { get; set; }
        public string Formato { get; set; }
        public byte[] File { get; set; }
    }

    #endregion

    #region Metodos tabla

    protected void gvAnexos_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Anexo")
        {
            string rootCompleto = Server.MapPath("~/Page/FabricaCreditos/PlanPagos/Archivos");
            string nombreArchivo = @"\DocumentoAnexo_" + ((Usuario)Session["usuario"]).codperfil;
            string pathDocumento;

            //Consulta el numero de documento y tre el documento en byte
            numeroCuenta = evt.CommandArgument.ToString();
            byte[] documento = _creditosolServicio.ConsultarDocAnexo(Convert.ToInt64(numeroCuenta), (Usuario)Session["usuario"]);

            //Trae de la tabla que formato es 
            GridViewRow gvr = (GridViewRow)(((ImageButton)evt.CommandSource).NamingContainer);
            string formatoDocumento = gvr.Cells[4].Text;

            // If directory does not exist, create it. 
            if (!Directory.Exists(rootCompleto))
                Directory.CreateDirectory(rootCompleto);

            if (!string.IsNullOrEmpty(formatoDocumento))
            {
                if (formatoDocumento.Replace(".", "") == "pdf")
                {
                    imgDocAnexo.Visible = false;
                    LiteralDcl.Visible = true;

                    string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"90%\">";
                    adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    adjuntar +=
                        " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    adjuntar += "</object>";

                    pathDocumento = CrearArchivo(rootCompleto, formatoDocumento, nombreArchivo, documento);

                    if (!string.IsNullOrEmpty(pathDocumento))
                        LiteralDcl.Text = string.Format(adjuntar,
                            ResolveUrl("~/Page/FabricaCreditos/PlanPagos/Archivos" + nombreArchivo + formatoDocumento));
                }
                else
                {
                    imgDocAnexo.Visible = true;
                    LiteralDcl.Visible = false;
                    pathDocumento = CrearArchivo(rootCompleto, formatoDocumento, nombreArchivo, documento);

                    if (!string.IsNullOrEmpty(pathDocumento))
                        imgDocAnexo.ImageUrl =
                            string.Format(ResolveUrl("~/Page/FabricaCreditos/PlanPagos/Archivos" + nombreArchivo +
                                                     formatoDocumento));
                }
                mpeDocAnexo.Show();
            }
        }
    }
    protected void btnCloseDocAnexo_Click(object sender, EventArgs e)
    {
        mpeDocAnexo.Hide();
    }
    public void gvCuoExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<ArchivosClase> lstArchivosClases = new List<ArchivosClase>();
        lstArchivosClases = (List<ArchivosClase>)Session["Archivos"];
        if (lstArchivosClases.Count >= 1)
        {
            ArchivosClase archivosClase = new ArchivosClase();
            int index = Convert.ToInt32(e.RowIndex);
            archivosClase = lstArchivosClases[index];

            lstArchivosClases.Remove(archivosClase);
        }
        gvArchivosPlus.DataSource = lstArchivosClases;
        gvArchivosPlus.DataBind();
    }
    protected void gvAnexos_OnDataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvArchivosPlus.Rows)
        {
            FileUpload file = (FileUpload)row.FindControl("fileUpload");
            Label lblfile = (Label)row.FindControl("lblFile");
            Label nombreAr = (Label)row.FindControl("lblNombreArhivo");

            if (!string.IsNullOrEmpty(nombreAr.Text))
            {
                lblfile.Visible = false;
                file.Visible = false;
                nombreAr.Visible = true;
            }
        }
        return;
    }
    public void gvArchivosPlus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvArchivosPlus.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            //Ignore
        }
    }
    public void BtnAgregarOnClick(object sender, EventArgs e)
    {
        List<ArchivosClase> lstArcvhios = new List<ArchivosClase>();
        List<ArchivosClase> lstArcvhio = new List<ArchivosClase>();
        if (Session["Archivos"] != null)
            lstArcvhios = (List<ArchivosClase>)Session["Archivos"];

        if (gvArchivosPlus.Rows.Count >= 1)
        {
            foreach (GridViewRow row in gvArchivosPlus.Rows)
            {
                ArchivosClase existenteArchivosClase = new ArchivosClase();
                FileUpload file = (FileUpload)row.FindControl("fileUpload");
                TextBox nombreAr = (TextBox)row.FindControl("txtNombreArchivo");
                TextBox formato = (TextBox)row.FindControl("txtFormato");
                byte[] bytes = null;
                if (file.PostedFile == null)
                    continue;
                Stream fs = file.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);

                existenteArchivosClase.NombreArchivo = file.FileName;
                existenteArchivosClase.Nombre = nombreAr.Text;
                existenteArchivosClase.Formato = formato.Text;
                existenteArchivosClase.File = bytes;


                lstArcvhios.Add(existenteArchivosClase);
                Session["totalArchivos"] = lstArcvhios;
            }
        }
        else
        {
            ArchivosClase additem = new ArchivosClase();
            lstArcvhio.Add(additem);
        }
        foreach (ArchivosClase item in lstArcvhios)
        {
            ArchivosClase sl = new ArchivosClase();
            if (!lstArcvhio.Exists(x => x.Nombre == item.Nombre))
            {
                sl.Nombre = item.Nombre;
                sl.File = item.File;
                sl.Formato = item.Formato;
                sl.NombreArchivo = item.NombreArchivo;
                lstArcvhio.Add(sl);
            }
        }
       
        Documentos.Visible = false;
        gvArchivosPlus.Visible = true;
        Session["Archivos"] = lstArcvhio;
        gvArchivosPlus.DataSource = lstArcvhio;
        gvArchivosPlus.DataBind();

    }

    #endregion

    #region Metodo Externos

    /// <summary>
    /// El campo EsSolicitud = Si se basa en la solicitud pasar:1, numero radicación: 0
    /// </summary>
    public void TablaDocumentosAnexo(string numeroRadicacion, int tipoProducto, int esSolicitud = 0)
    {
        lstAnexos = _creditosolServicio.ListaDocumentosAnexos(esSolicitud, Int64.Parse(numeroRadicacion), tipoProducto, (Usuario)Session["usuario"]);
        gvAnexos.DataSource = lstAnexos;
        lblAnexos.Text = "Se encontraron " + lstAnexos.Count() + " anexos para el producto " + numeroRadicacion;
        gvAnexos.DataBind();
    }

    private string CrearArchivo(string path, string extencion, string nombreDocumneto, byte[] arrayBytes)
    {
        // si existe el archivo con esa extecion lo eliminara y creara 
        string nombreCompleto = path + nombreDocumneto + extencion;
        if (File.Exists(nombreCompleto))
            File.Delete(nombreCompleto);
        //Crea el archivo
        using (var fileStream = new FileStream(nombreCompleto, FileMode.OpenOrCreate))
        {
            // read from file or write to file
            fileStream.Write(arrayBytes, 0, arrayBytes.Length);
            fileStream.Flush();
            fileStream.Close();
        }
        //valida de que el archivo exista.
        if ((File.Exists(nombreCompleto)))
            return nombreCompleto;

        return "";
    }

    public void EliminarDocumentos(string root, string nombre)
    {
        Task.Factory.StartNew(() =>
        {
            string[] files = Directory.GetFiles(root);
            foreach (string file in files)
            {
                //Encuantra la posición de la última aparición del punto
                var coincidencia = file.LastIndexOf('.');
                //Elimina de la cadena a partir de la posición especificada
                string nombreArchivo = file.Remove(coincidencia);
                if (nombreArchivo.Equals(root + nombre))
                    File.Delete(file);
            }
        });
    }

    public void IniciarTable()
    {
        List<ArchivosClase> archivos = new List<ArchivosClase>();
        gvArchivosPlus.DataSource = archivos;
        gvArchivosPlus.DataBind();

    }

    public void GuardarArchivos(string numeroRadicacion)
    {
        if (gvArchivosPlus.Visible)
        {
            BtnAgregarOnClick(null, null);

            List<ArchivosClase> lsArchivosClases = (List<ArchivosClase>)Session["Archivos"];
            foreach (ArchivosClase item in lsArchivosClases)
            {
                if (!string.IsNullOrEmpty(item.Nombre))
                {
                    DocumentosAnexos documentos = new DocumentosAnexos();
                    documentos.numero_radicacion = Convert.ToInt64(numeroRadicacion);
                    documentos.tipo_documento = 0;
                    documentos.fechaanexo = DateTime.Now;
                    documentos.imagen = item.File;
                    documentos.descripcion = item.Nombre;
                    documentos.estado = 1;
                    documentos.tipo_producto = 999;
                    documentos.extension = "." + item.Formato;
                    documentosAnexosService.CrearDocumentosAnexos(documentos, (Usuario)Session["usuario"]);
                }
            }
        }

    }
    public bool ContarTablaDatos() { return gvArchivosPlus.Rows.Count > 0 ? true : false; }
    #endregion


}
