using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
	SeguimientoServices seguimientoServicio = new SeguimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[seguimientoServicio.CodigoProgramaC + ".id"] != null)
                VisualizarOpciones(seguimientoServicio.CodigoProgramaC, "E");
            else
                VisualizarOpciones(seguimientoServicio.CodigoProgramaC, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[seguimientoServicio.CodigoProgramaC + ".id"] != null)
                {
                    idObjeto = Session[seguimientoServicio.CodigoProgramaC + ".id"].ToString();
                    Session.Remove(seguimientoServicio.CodigoProgramaC + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlAreaEjecucion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlResponsable, (Usuario)Session["usuario"]);

        ddlClase.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlClase.Items.Insert(1, new ListItem("Preventivo", "1"));
        ddlClase.Items.Insert(2, new ListItem("Detectivo", "2"));
        ddlClase.Items.Insert(3, new ListItem("Correctivo", "3"));
        ddlClase.DataBind();

        ddlGradoAceptacion.Items.Insert(0, new ListItem("Seleccione un item",""));
        ddlGradoAceptacion.Items.Insert(1, new ListItem("10%", "10"));
        ddlGradoAceptacion.Items.Insert(2, new ListItem("20%", "20"));
        ddlGradoAceptacion.Items.Insert(3, new ListItem("30%", "30"));
        ddlGradoAceptacion.Items.Insert(4, new ListItem("40%", "40"));
        ddlGradoAceptacion.Items.Insert(5, new ListItem("50%", "50"));
        ddlGradoAceptacion.Items.Insert(6, new ListItem("60%", "60"));
        ddlGradoAceptacion.Items.Insert(7, new ListItem("70%", "70"));
        ddlGradoAceptacion.Items.Insert(8, new ListItem("80%", "70"));
        ddlGradoAceptacion.Items.Insert(9, new ListItem("90%", "90"));
        ddlGradoAceptacion.Items.Insert(10, new ListItem("100%", "100"));
        ddlGradoAceptacion.DataBind();
    }

    private bool ValidarDatos()
    {
        if(string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            VerError("Ingrese la descripción");
            return false;
        }
        if(ddlClase.SelectedValue == "")
        {
            VerError("Ingrese la clase o tipo de control");
            return false;
        }
        if(ddlAreaEjecucion.SelectedValue == "")
        {
            VerError("Ingrese el área donde se debe realizar el control");
            return false;
        }
        if(ddlResponsable.SelectedValue == "")
        {
            VerError("Ingrese el responsable de realizar el control");
            return false;
        }
        if(ddlGradoAceptacion.SelectedValue == "")
        {
            VerError("Ingrese el grado o nivel de aceptación del control");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Seguimiento pControl = new Seguimiento();
                pControl.descripcion = txtDescripcion.Text;
                pControl.clase = Convert.ToInt64(ddlClase.SelectedValue);
                pControl.cod_area = Convert.ToInt64(ddlAreaEjecucion.SelectedValue);
                pControl.cod_cargo = Convert.ToInt64(ddlResponsable.SelectedValue);
                pControl.grado_aceptacion = Convert.ToInt32(ddlGradoAceptacion.SelectedValue);

                if (idObjeto == "")
                    pControl = seguimientoServicio.CrearFormaControl(pControl, (Usuario)Session["usuario"]);
                else
                {
                    pControl.cod_control = Convert.ToInt64(idObjeto);
                    pControl = seguimientoServicio.ModificarFormaControl(pControl, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(seguimientoServicio.CodigoProgramaC + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Seguimiento pControl = new Seguimiento();
        pControl.cod_control = Convert.ToInt64(idObjeto);

        pControl = seguimientoServicio.ConsultarFormaControl(pControl, (Usuario)Session["usuario"]);

        if (pControl != null)
        {
            if (!string.IsNullOrEmpty(pControl.cod_control.ToString()))
                txtCodigoControl.Text = pControl.cod_control.ToString();
            if (!string.IsNullOrEmpty(pControl.descripcion.ToString()))
                txtDescripcion.Text = pControl.descripcion.ToString();
            if (!string.IsNullOrEmpty(pControl.clase.ToString()))
                ddlClase.SelectedValue = pControl.clase.ToString();
            if (!string.IsNullOrEmpty(pControl.cod_area.ToString()))
                ddlAreaEjecucion.SelectedValue = pControl.cod_area.ToString();
            if (!string.IsNullOrEmpty(pControl.cod_cargo.ToString()))
                ddlResponsable.SelectedValue = pControl.cod_cargo.ToString();
            if (!string.IsNullOrEmpty(pControl.grado_aceptacion.ToString()))
                ddlGradoAceptacion.SelectedValue = pControl.grado_aceptacion.ToString();
        }
    }
}