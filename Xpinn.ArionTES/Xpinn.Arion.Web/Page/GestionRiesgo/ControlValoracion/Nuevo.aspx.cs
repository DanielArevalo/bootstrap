using System;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb

{
   valoracion_controlService valoracioncontrol = new valoracion_controlService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[valoracioncontrol.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(valoracioncontrol.CodigoPrograma, "E");
            else
                VisualizarOpciones(valoracioncontrol.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdIdActividad.Value = "";
                CargarListas();
                if (Session[valoracioncontrol.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[valoracioncontrol.CodigoPrograma + ".id"].ToString();
                    Session.Remove(valoracioncontrol.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();

        ddlFrecuencia.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFrecuencia.Items.Insert(1, new ListItem("Minimo una vez al año", "1"));
        ddlFrecuencia.Items.Insert(2, new ListItem("Esporádico, dos veces al año", "2"));
        ddlFrecuencia.Items.Insert(3, new ListItem("Tres veces al año", "3"));
        ddlFrecuencia.Items.Insert(4, new ListItem("Trimestral", "4"));
        ddlFrecuencia.Items.Insert(5, new ListItem("Bimensual", "5"));
        ddlFrecuencia.DataBind();


    }




    bool ValidarDatos()
    {
        int nivel = Convert.ToInt32(txtRangoMaximo.Text); ;
        int ranmaximo = nivel;
        int nivel2 = Convert.ToInt32(txtRangoMinimo.Text); ;
        int ranmini = nivel2;
        

        if (ddlValor.SelectedValue == "0" || ddlValor.SelectedValue == null)
        {
            VerError("Seleccione la Calificacion que necesita ");
            return false;
        }

        if (txtvalor.Text.Trim() == "")
        {
            VerError("Debe ingresar el Valor");
            return false;
        }

        if (txtDescripcion.Text.Trim() == "")
        {
            VerError("Ingrese La descripcion");
            return false;
        }

        if (ddlFrecuencia.SelectedValue == "0" || ddlFrecuencia.SelectedValue == null)
        {
            VerError("Seleccione la Frecuencia que necesita ");
            return false;
        }

        if (txtRangoMinimo.Text.Trim() == "")
        {
            VerError("Ingrese un valor minimo");
            return false;
        }

        if (txtRangoMaximo.Text.Trim() == "")
        {
            VerError("Ingrese un valor maximo");
            return false;
        }



        if (ranmini >= ranmaximo)
        {
            VerError("El Rango maximo es menor que el Rango minimo");
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
                valoracion_control pcontrol = new valoracion_control();
                pcontrol.cod_control = ConvertirStringToInt(txtCodControl.Text.Trim());
                pcontrol.descripcion = txtDescripcion.Text;
                pcontrol.valor = ConvertirStringToInt(txtvalor.Text.Trim());
                pcontrol.calificacion = ddlValor.SelectedItem.Text.Trim();
                pcontrol.frecuencia = ConvertirStringToInt32N(ddlFrecuencia.SelectedValue.Trim());
                pcontrol.desc_frecuencia = ddlFrecuencia.SelectedItem.Text.Trim();
                pcontrol.rango_minimo = ConvertirStringToInt32N(txtRangoMinimo.Text);
                pcontrol.rango_maximo = ConvertirStringToInt32N(txtRangoMaximo.Text);
                pcontrol.rango = 1;

                

                if (hdIdActividad.Value == "")
                    pcontrol = valoracioncontrol.Crearvaloracion_control(pcontrol, (Usuario)Session["usuario"]);
                else
                {
                    pcontrol = valoracioncontrol.Modificarvaloracion_control(pcontrol, (Usuario)Session["usuario"]);
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
        Session.Remove(valoracioncontrol.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        valoracion_control pcontrol = new valoracion_control();
        pcontrol.cod_control = Convert.ToInt64(idObjeto);

        pcontrol = valoracioncontrol.Consultarvaloracion_control(pcontrol, (Usuario)Session["usuario"]);

        if (pcontrol != null)
        {
            if (!string.IsNullOrWhiteSpace(pcontrol.cod_control.ToString()))
                txtCodControl.Text = pcontrol.cod_control.ToString();
            if (!string.IsNullOrWhiteSpace(pcontrol.descripcion.ToString()))
                txtDescripcion.Text = pcontrol.descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pcontrol.valor.ToString()))
                ddlValor.SelectedValue = pcontrol.valor.ToString();
            
            if (!string.IsNullOrWhiteSpace(pcontrol.valor.ToString()))
                txtvalor.Text = pcontrol.valor.ToString();


            if (!string.IsNullOrWhiteSpace(pcontrol.frecuencia.ToString()))
                ddlFrecuencia.SelectedValue = pcontrol.frecuencia.ToString();
            if (!string.IsNullOrWhiteSpace(pcontrol.rango_minimo.ToString()))
                txtRangoMinimo.Text = pcontrol.rango_minimo.ToString();
            if (!string.IsNullOrWhiteSpace(pcontrol.rango_minimo.ToString()))
                txtRangoMaximo.Text = pcontrol.rango_maximo.ToString();
            hdIdActividad.Value = txtCodControl.Text;
        }
    }

}