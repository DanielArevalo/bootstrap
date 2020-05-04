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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.ConsecutivoOficinasService ConsecutivoOficinaServicio = new Xpinn.Seguridad.Services.ConsecutivoOficinasService();
    PoblarListas poblar = new PoblarListas();
    Int64 rangoinicial = 0;
    Int64 rangofinal = 0;
    Int64 codigooficina = 0;
    String tabla;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ConsecutivoOficinaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ConsecutivoOficinaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDllOficinas();
                if (Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConsecutivoOficinaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void guardar()
    {
        try
        {
            //Usuario usuap = new Usuario();
            Usuario usuap = (Usuario)Session["usuario"];
            Xpinn.Seguridad.Entities.ConsecutivoOficinas vConsecutivo = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();
            vConsecutivo = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();

            if (idObjeto != "")
                vConsecutivo = ConsecutivoOficinaServicio.ConsultarConsecutivoOficinas(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vConsecutivo.tabla = Convert.ToString(this.ddltabla.Text.Trim());
            if (vConsecutivo.tabla == "CREDITO")
            {
                vConsecutivo.columna = "numero_radicacion";
            }
            if (vConsecutivo.tabla == "COMPROBANTES")
            {
                vConsecutivo.columna = "num_comp";
            }
            if (vConsecutivo.tabla == "PERSONA")
            {
                vConsecutivo.columna = "cod_persona";
            }
            if (vConsecutivo.tabla == "APORTE")
            {
                vConsecutivo.columna = "numero_aporte";
            }            
            vConsecutivo.cod_oficina = Convert.ToInt32(this.ddlOficina.Text.Trim());
            vConsecutivo.rango_inicial = Convert.ToInt64(this.txtRangoInicial.Text.Trim());

            vConsecutivo.rango_final = Convert.ToInt64(this.txtRangoFinal.Text.Trim());
            vConsecutivo.fechacreacion = Convert.ToDateTime(this.txtFechacreacion.Text.Trim());
            

            vConsecutivo.tipo_consecutivo = 1;

            if (idObjeto != "")
            {
                vConsecutivo.idconsecutivo = Convert.ToInt32(idObjeto);
                DateTime fecha = DateTime.Now;
                if (vConsecutivo.fecultmod != DateTime.MinValue)
                    vConsecutivo.fecultmod = fecha;

                // if (!string.IsNullOrEmpty(vConsecutivo.usuarioultmod))
                vConsecutivo.usuarioultmod = Convert.ToString(usuap.nombre);
                ConsecutivoOficinaServicio.ModificarConsecutivoOficinas(vConsecutivo, (Usuario)Session["usuario"]);
            }
            else
            {
                if (vConsecutivo.fechacreacion != DateTime.MinValue)
                    this.txtFechacreacion.Text = HttpUtility.HtmlDecode(vConsecutivo.fechacreacion.ToString().Trim());


                // if (!string.IsNullOrEmpty(vConsecutivo.usuariocreacion))
                vConsecutivo.usuariocreacion = Convert.ToString(usuap.nombre);
                validarreglasGrabar();
                vConsecutivo = ConsecutivoOficinaServicio.CrearConsecutivoOficinas(vConsecutivo, (Usuario)Session["usuario"]);
                idObjeto = vConsecutivo.idconsecutivo.ToString();
            }

            Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        if (this.validarreglasGrabar())
        {
            this.guardar();
        }

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Seguridad.Entities.ConsecutivoOficinas vConsecutivo = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();
            vConsecutivo = ConsecutivoOficinaServicio.ConsultarConsecutivoOficinas(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            // Usuario usuap = new Usuario();
            Usuario usuap = (Usuario)Session["usuario"];
            //vConsecutivo.tabla = Convert.ToString(this.ddltabla.Text.Trim());
            if (vConsecutivo.cod_oficina != Int32.MinValue)
                this.ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vConsecutivo.cod_oficina.ToString().Trim());

            if (!string.IsNullOrEmpty(vConsecutivo.tabla))
                this.ddltabla.Text = HttpUtility.HtmlDecode(vConsecutivo.tabla.ToString().Trim());

            if (vConsecutivo.rango_inicial != Int32.MinValue)
                txtRangoInicial.Text = HttpUtility.HtmlDecode(vConsecutivo.rango_inicial.ToString().Trim());

            if (vConsecutivo.rango_final != Int32.MinValue)
                txtRangoFinal.Text = HttpUtility.HtmlDecode(vConsecutivo.rango_final.ToString().Trim());

            if (vConsecutivo.fechacreacion != DateTime.MinValue)
                this.txtFechacreacion.Text = HttpUtility.HtmlDecode(vConsecutivo.fechacreacion.ToString().Trim());

            if (!string.IsNullOrEmpty(vConsecutivo.usuariocreacion))
                vConsecutivo.usuariocreacion = Convert.ToString(usuap.codusuario);

            if (!string.IsNullOrEmpty(vConsecutivo.usuarioultmod))
                vConsecutivo.usuarioultmod = Convert.ToString(usuap.codusuario);

            DateTime fecha = DateTime.Now;
            if (vConsecutivo.fecultmod != DateTime.MinValue)
                vConsecutivo.fecultmod = fecha;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    // carga ddl
    void CargarDllOficinas()
    {
        poblar.PoblarListaDesplegable("consecutivo_oficinas", ddlOficina, (Usuario)Session["usuario"]);

        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }


    }
    private Boolean validarreglasGrabar()
    {
        Boolean result = true;
        Int64 rangoin = Convert.ToInt64(txtRangoInicial.Text);
        Int64 rangofin = Convert.ToInt64(txtRangoFinal.Text);
        Int32 oficina = 0;
        Int64 rango_inicial = 0;
        Int64 rango_final = 0;
        String tabla_of = "";
        if (rangoin == 0)
        {
            String Error = "El rango inicial deber ser mayor a 0";
            this.Lblerror.Text = Error;
            result = false;
        }

        if (rangofin == 0)
        {
            String Error = "El rango final deber ser mayor a 0";
            this.Lblerror.Text = Error;
            result = false;
        }
        if (rangoin >= rangofin)
        {
            String Error = "El rango inicial no puede ser mayor al rango final";
            this.Lblerror.Text = Error;
            result = false;
        }
        oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        tabla_of = Convert.ToString(ddltabla.SelectedItem.Text);
        rango_inicial = Convert.ToInt64(txtRangoInicial.Text);
        rango_final = Convert.ToInt64(txtRangoFinal.Text);
        ObtenerDatosConsecutivo(tabla_of, oficina, rango_inicial, rango_final);
        if (rangoinicial > 0 || rangofinal > 0)
        {
            if (oficina == codigooficina && tabla == tabla_of)
            {

                String Error = "El rango  ya esta asignado en esta oficina y  a esta tabla";
                this.Lblerror.Text = Error;
                result = false;


                //if (rangofin <= rangofinal)
                //{
                //    String Error = "El rango final ya esta asignado en esta oficina y  a esta tabla";
                //    this.Lblerror.Text = Error;
                //    result = false;
                //}
            }
        }

        //if (rangoin >= rangoinicial)
        //{
        //    String Error = "El rango inicial ya esta asignado";
        //    this.Lblerror.Text = Error;
        //    result = false;
        //}
        //if (rangofin <= rangofinal)
        //{
        //    String Error = "El rango final ya esta asignado";
        //    this.Lblerror.Text = Error;
        //    result = false;
        //}

        return result;

    }
    protected void ObtenerDatosConsecutivo(String pIdTabla, Int64 pIdOficina, Int64 rango_inicial, Int64 rango_final)
    {
        try
        {
            Xpinn.Seguridad.Entities.ConsecutivoOficinas vConsecutivo = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();
            vConsecutivo = ConsecutivoOficinaServicio.ConsultarConsOfiXOfyTabla(Convert.ToString(pIdTabla), Convert.ToInt32(pIdOficina), rango_inicial, rango_final, (Usuario)Session["usuario"]);
            if (vConsecutivo.cod_oficina != null)
            {

                if (vConsecutivo.rango_inicial != Int32.MinValue && vConsecutivo.rango_inicial != 0)
                    rangoinicial = Convert.ToInt64(vConsecutivo.rango_inicial.ToString().Trim());
                if (vConsecutivo.rango_final != Int32.MinValue && vConsecutivo.rango_final != 0)
                    rangofinal = Convert.ToInt64(vConsecutivo.rango_final.ToString().Trim());

                if (vConsecutivo.cod_oficina != Int32.MinValue)
                    codigooficina = Convert.ToInt32(vConsecutivo.cod_oficina.ToString().Trim());

                if (!string.IsNullOrEmpty(vConsecutivo.tabla))
                    tabla = HttpUtility.HtmlDecode(vConsecutivo.tabla.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "ObtenerDatosConsecutivo", ex);
        }
    }
}