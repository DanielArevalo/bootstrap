using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void ToolBarDelegate2(object sender, ImageClickEventArgs e);
public delegate void ActionsDelegate2(object sender, EventArgs e);

public partial class Site2 : System.Web.UI.MasterPage
{
    public event ToolBarDelegate2 eventoGuardar;
    ToolBarDelegate2 guardarToolbar;

    public event ToolBarDelegate2 eventoConsultar;
    ToolBarDelegate2 consultarToolbar;

    public event ToolBarDelegate2 eventoNuevo;
    ToolBarDelegate2 nuevoToolbar;

    public event ToolBarDelegate2 eventoEliminar;
    ToolBarDelegate2 eliminarToolbar;

    public event ToolBarDelegate2 eventoEditar;
    ToolBarDelegate2 editarToolbar;

    public event ToolBarDelegate2 eventoLimpiar;
    ToolBarDelegate2 limpiarToolbar;

    public event ToolBarDelegate2 eventoCancelar;
    ToolBarDelegate2 cancelarToolbar;    

    public event ToolBarDelegate2 eventoAdelante;
    ToolBarDelegate2 adelanteToolbar;

    public event ToolBarDelegate2 eventoAtras;
    ToolBarDelegate2 atrasToolbar;

    public event ActionsDelegate2 eventoAcciones;
    ActionsDelegate2 accionesToolbar;

    protected void Page_Load(object sender, EventArgs e)
    {
        guardarToolbar = eventoGuardar;
        consultarToolbar = eventoConsultar;
        nuevoToolbar = eventoNuevo;
        eliminarToolbar = eventoEliminar;
        editarToolbar = eventoEditar;
        limpiarToolbar = eventoLimpiar;
        cancelarToolbar = eventoCancelar;        
        adelanteToolbar = eventoAdelante;
        atrasToolbar = eventoAtras;
        accionesToolbar = eventoAcciones;

        if (guardarToolbar == null)
            btnGuardar.Visible = false;
        if (consultarToolbar == null)
            btnConsultar.Visible = false;
        if (nuevoToolbar == null)
            btnNuevo.Visible = false;
        if (eliminarToolbar == null)
            btnEliminar.Visible = false;
        if (editarToolbar == null)
            btnEditar.Visible = false;
        if (limpiarToolbar == null)
            btnLimpiar.Visible = false;
        if (cancelarToolbar == null)
            btnCancelar.Visible = false;
        
        if (accionesToolbar == null)
            ddlAcciones.Visible = false;

        

        if (Session["nombreModulo"] != null)
            lblModulo.Text = Session["nombreModulo"].ToString();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (guardarToolbar != null)
            guardarToolbar(sender, e);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (nuevoToolbar != null)
            nuevoToolbar(sender, e);
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (editarToolbar != null)
            editarToolbar(sender, e);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        if (eliminarToolbar != null)
            eliminarToolbar(sender, e);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoConsultar != null)
            eventoConsultar(sender, e);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoLimpiar != null)
            eventoLimpiar(sender, e);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoCancelar != null)
            eventoCancelar(sender, e);
    }

    protected void ddlAcciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoAcciones != null)
            eventoAcciones(sender, e);
    }
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAtras != null)
            eventoAtras(sender, e);
    }
    
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAdelante != null)
            eventoAdelante(sender, e);
    }
}
