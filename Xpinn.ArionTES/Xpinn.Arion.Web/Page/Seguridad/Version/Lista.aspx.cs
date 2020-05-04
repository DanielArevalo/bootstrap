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
            VisualizarOpciones("90117", "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("90117", "Page_PreInit", ex);
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
        Usuario usuario = (Usuario)Session["Usuario"];
        txtVersion.Text = usuario.version_pl;

        string path = "../../../bin";
        DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(path));

        List<FileInfo> listaArchivos = directoryInfo.GetFiles().ToList();

        IQueryable<FileInfo> queryable = listaArchivos.AsQueryable();
        queryable = queryable.Where(x => x.Name.Trim().ToUpper().Contains("XPINN"));
        queryable = queryable.Where(x => x.Name.Trim().ToUpper().Contains(".DLL"));
        if (!string.IsNullOrWhiteSpace(txtNombreArchivo.Text))
        {
            queryable = queryable.Where(x => x.Name.Trim().ToUpper().Contains(txtNombreArchivo.Text.Trim().ToUpper()));
        }
        if (!string.IsNullOrWhiteSpace(txtFechaCreacion.Text))
        {
            DateTime fechaCreacion = Convert.ToDateTime(txtFechaCreacion.Text);
            queryable = queryable.Where(x => Convert.ToDateTime(x.LastWriteTime.ToShortDateString()) == fechaCreacion);
        }
        List<FileInfo> listaFiltrada = queryable.ToList();
        ViewState["VersionFiles"] = listaFiltrada;
        gvLista.DataSource = listaFiltrada;
        gvLista.DataBind();
    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaCreacion.Text = string.Empty;
        txtNombreArchivo.Text = string.Empty;
    }

    
}