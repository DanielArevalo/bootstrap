using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void ToolBarDelegate1(object sender, ImageClickEventArgs e);
public delegate void ActionsDelegate1(object sender, EventArgs e);

public partial class Site1 : System.Web.UI.MasterPage
{
    public event ToolBarDelegate1 eventoGuardar;
    ToolBarDelegate1 guardarToolbar;

    public event ToolBarDelegate1 eventoConsultar;
    ToolBarDelegate1 consultarToolbar;

    public event ToolBarDelegate1 eventoNuevo;
    ToolBarDelegate1 nuevoToolbar;

    public event ToolBarDelegate1 eventoEliminar;
    ToolBarDelegate1 eliminarToolbar;

    public event ToolBarDelegate1 eventoEditar;
    ToolBarDelegate1 editarToolbar;

    public event ToolBarDelegate1 eventoLimpiar;
    ToolBarDelegate1 limpiarToolbar;

    public event ToolBarDelegate1 eventoCancelar;
    ToolBarDelegate1 cancelarToolbar;    

    public event ToolBarDelegate1 eventoAdelante;
    ToolBarDelegate1 adelanteToolbar;

    public event ToolBarDelegate1 eventoAdelante2;
    ToolBarDelegate1 adelante2Toolbar;

    public event ToolBarDelegate1 eventoAtras;
    ToolBarDelegate1 atrasToolbar;

    public event ActionsDelegate1 eventoAcciones;
    ActionsDelegate1 accionesToolbar;

    public event ToolBarDelegate1 eventoRegresar;
    ToolBarDelegate1 regresarToolbar;

    public event ToolBarDelegate1 eventoImprimir;
    ToolBarDelegate1 imprimirToolbar;

    public event ToolBarDelegate1 eventoExportar;
    ToolBarDelegate1 exportarToolbar;
   
    public event ToolBarDelegate1 eventoCopiar;
    ToolBarDelegate1 copiarToolbar;

    public event ToolBarDelegate1 eventoCargar;
    ToolBarDelegate1 cargarToolbar;

    public event ToolBarDelegate1 eventoImportar;
    ToolBarDelegate1 importarToolbar;

    protected void Page_Load(object sender, EventArgs e)
    {
        guardarToolbar = eventoGuardar;
        consultarToolbar = eventoConsultar;
        nuevoToolbar = eventoNuevo;
        eliminarToolbar = eventoEliminar;
        editarToolbar = eventoEditar;
        limpiarToolbar = eventoLimpiar;
        regresarToolbar = eventoRegresar;
        cancelarToolbar = eventoCancelar;
        imprimirToolbar = eventoImprimir;
        exportarToolbar = eventoExportar;
        accionesToolbar = eventoAcciones;
        copiarToolbar = eventoCopiar;
        cargarToolbar = eventoCargar;
        importarToolbar = eventoImportar;

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
        if (regresarToolbar == null)
            btnRegresar.Visible = false;
        if (limpiarToolbar == null)
            btnLimpiar.Visible = false;
        if (cancelarToolbar == null)
            btnCancelar.Visible = false;
        if (imprimirToolbar == null)
            btnImprimir.Visible = false;
        if (exportarToolbar == null)
            btnExportar.Visible = false;
        if (accionesToolbar == null)
            ddlAcciones.Visible = false;
        if (copiarToolbar == null)
            btnCopiar.Visible = false;
        if (cargarToolbar == null)
            btnCargar.Visible = false;
        if (importarToolbar == null)
            btnImportar.Visible = false;
        if (atrasToolbar == null)
            btnAtras.Visible = false;
        if (adelanteToolbar == null)
            btnAdelante.Visible = false;
        if (adelante2Toolbar == null)
            btnAdelante2.Visible = false;

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

    protected void btnAdelante2_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAdelante2 != null)
            eventoAdelante2(sender, e);
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (guardarToolbar != null)
            guardarToolbar(sender, new ImageClickEventArgs(0, 0));
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        if (nuevoToolbar != null)
            nuevoToolbar(sender, new ImageClickEventArgs(0, 0));
    }


    protected void btnEditar_Click(object sender, EventArgs e)
    {
        if (editarToolbar != null)
            editarToolbar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (eliminarToolbar != null)
            eliminarToolbar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (eventoConsultar != null)
            eventoConsultar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (eventoRegresar != null)
            eventoRegresar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        if (eventoLimpiar != null)
            eventoLimpiar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (eventoCancelar != null)
            eventoCancelar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        if (eventoImprimir != null)
            eventoImprimir(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (eventoExportar != null)
            eventoExportar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void ddlAcciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoAcciones != null)
            eventoAcciones(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnCopiar_Click(object sender, EventArgs e)
    {
        if (eventoCopiar != null)
            eventoCopiar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        if (eventoCargar != null)
            eventoCargar(sender, new ImageClickEventArgs(0, 0));
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        if (eventoImportar != null)
            eventoImportar(sender, new ImageClickEventArgs(0, 0));
    }

    public void MostrarGuardar(Boolean pestado)
    {
        btnGuardar.Visible = pestado;
    }

    public void MostrarConsultar(Boolean pestado)
    {
        btnConsultar.Visible = pestado;
    }

    public void MostrarEliminar(Boolean pestado)
    {
        btnEliminar.Visible = pestado;
    }

    public void MostrarRegresar(Boolean pestado)
    {
        btnRegresar.Visible = pestado;
    }

    public void MostrarCancelar(Boolean pestado)
    {
        btnCancelar.Visible = pestado;
    }

    public void MostrarLimpiar(Boolean pestado)
    {
        btnLimpiar.Visible = pestado;
    }

    public void BloquearConsultar(Boolean pestado)
    {
        if (pestado == true)
            btnConsultar.OnClientClick = "this.disabled=true;this.value='Enviando...'";
        else
            btnConsultar.OnClientClick = "";
    }

    public void MostrarImprimir(Boolean pestado)
    {
        btnImprimir.Visible = pestado;
    }

    public void MostrarExportar(Boolean pestado)
    {
        btnExportar.Visible = pestado;
    }

    public void MostrarNuevo(Boolean pestado)
    {
        btnNuevo.Visible = pestado;
    }

    public void MostrarCopiar(Boolean pestado)
    {
        btnCopiar.Visible = pestado;
    }

    public void MostrarCargar(Boolean pestado)
    {
        btnCargar.Visible = pestado;
    }

    public void MostrarImportar(Boolean pestado)
    {
        btnImportar.Visible = pestado;
    }

    public void MostrarImagenLoading(Boolean pestado)
    {

    }

}
