using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.Linq;
using System.Configuration;

public partial class Lista : GlobalWeb
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("90116", "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            //toolBar.eventoNuevo += (s, evt) =>
            //{
            //    Session.Remove("90116" + ".id");
            //    Navegar(Pagina.Nuevo);
            //};
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("90116", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        txtFechaCreacion.Attributes.Add("readonly", "readonly");
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string path = ConfigurationManager.AppSettings["RutaDirectorioVirtualDefault"] as string;
        DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(path));

        List<FileInfo> listaArchivos = directoryInfo.GetFiles("*", SearchOption.AllDirectories).ToList();

        IQueryable<FileInfo> queryable = listaArchivos.AsQueryable();
        queryable = queryable.Where(x => x.Extension.Trim().ToUpper().Contains("RAR") || x.Extension.Trim().ToUpper().Contains("ZIP"));

        if (!string.IsNullOrWhiteSpace(txtNombreArchivo.Text))
        {
            queryable = queryable.Where(x => x.Name.Trim().ToUpper().Contains(txtNombreArchivo.Text.Trim().ToUpper()));
        }

        if (!string.IsNullOrWhiteSpace(txtNombreExtension.Text))
        {
            queryable = queryable.Where(x => x.Extension.Trim().ToUpper().Contains(txtNombreExtension.Text.Trim().ToUpper()));
        }

        if (!string.IsNullOrWhiteSpace(txtDirectorio.Text))
        {
            queryable = queryable.Where(x => x.DirectoryName.Trim().ToUpper().Contains(txtDirectorio.Text.Trim().ToUpper()));
        }

        if (!string.IsNullOrWhiteSpace(txtFechaCreacion.Text))
        {
            DateTime fechaCreacion = Convert.ToDateTime(txtFechaCreacion.Text);
            queryable = queryable.Where(x => Convert.ToDateTime(x.CreationTime.ToShortDateString()) == fechaCreacion);
        }

        List<FileInfo> listaFiltrada = queryable.ToList();

        ViewState["BackUpFiles"] = listaFiltrada;

        gvLista.DataSource = listaFiltrada;
        gvLista.DataBind();
    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtDirectorio.Text = string.Empty;
        txtFechaCreacion.Text = string.Empty;
        txtNombreArchivo.Text = string.Empty;
        txtNombreExtension.Text = string.Empty;
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = GetRowIndexOfControlInsideGridViewOneLevel(e.CommandSource as Control);

            if (e.CommandName == "Delete")
            {
                ViewState.Add("indexParaBorrar", index);

                ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar este archivo?");
            }
            else if (e.CommandName == "Guardar")
            {
                List<FileInfo> listaArchivos = ViewState["BackUpFiles"] as List<FileInfo>;
                FileInfo archivoParaDescargar = listaArchivos.ElementAtOrDefault(index);

                using (FileStream stream = archivoParaDescargar.OpenRead())
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", archivoParaDescargar.Name));
                    stream.CopyTo(Response.OutputStream);
                    Response.End();
                }
            }
        }
        catch { }
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        try
        {
            int? indexParaBorrar = ViewState["indexParaBorrar"] as int?;
            List<FileInfo> listaArchivos = ViewState["BackUpFiles"] as List<FileInfo>;
            FileInfo archivoParaBorrar = listaArchivos.ElementAtOrDefault(indexParaBorrar.Value);

            archivoParaBorrar.Delete();
            listaArchivos.Remove(archivoParaBorrar);

            gvLista.DataSource = listaArchivos;
            gvLista.DataBind();

            ViewState["indexParaBorrar"] = null;
            ViewState["BackUpFiles"] = listaArchivos;
        }
        catch (Exception ex)
        {
            VerError("No se pudo borrar el archivo, " + ex.Message);
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            btnConsultar_Click(null, null);
        }
        catch { }
    }


}