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

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.VehiculosService VehiculosServicio = new Xpinn.FabricaCreditos.Services.VehiculosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(VehiculosServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, VehiculosServicio.CodigoPrograma);
                //CargarListas();

                if (Session[VehiculosServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.GetType().Name, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, VehiculosServicio.CodigoPrograma);
    //    //Navegar(Pagina.Nuevo);
    //    Borrar();
    //}

    private void Borrar()
    {
        //CargarListas();

        //txtMarca.Text = "";
        txtPlaca.Text = "";
        //txtModelo.Text = "";
        txtValorcomercial.Text = "";
        txtValorprenda.Text = "";

        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, VehiculosServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, VehiculosServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[4].Text)
            {
                case "1":
                    e.Row.Cells[4].Text = "m1";
                    break;
                case "2":
                    e.Row.Cells[4].Text = "m2";
                    break;
                case "3":
                    e.Row.Cells[4].Text = "m3";
                    break;
            }

            switch (e.Row.Cells[6].Text)
            {
                case "1":
                    e.Row.Cells[6].Text = "md1";
                    break;
                case "2":
                    e.Row.Cells[6].Text = "md2";
                    break;
                case "3":
                    e.Row.Cells[6].Text = "md3";
                    break;
            }
        }

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[VehiculosServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[VehiculosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            VehiculosServicio.EliminarVehiculos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Vehiculos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Vehiculos>();
            lstConsulta = VehiculosServicio.ListarVehiculos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(VehiculosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Vehiculos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();

        vVehiculos.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vVehiculos.marca = Convert.ToString(ddlMarca.SelectedValue);
        if(txtPlaca.Text.Trim() != "")
            vVehiculos.placa = Convert.ToString(txtPlaca.Text.Trim());  
        vVehiculos.modelo = Convert.ToInt64(ddlModelo.SelectedValue);
        if(txtValorcomercial.Text.Trim() != "")
            vVehiculos.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim().Replace(".", ""));
        if(txtValorprenda.Text.Trim() != "")
            vVehiculos.valorprenda = Convert.ToInt64(txtValorprenda.Text.Trim().Replace(".", ""));

        return vVehiculos;
    }
    
    private void Edicion()
    {
        try
        {
            if (Session[VehiculosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(VehiculosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(VehiculosServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[VehiculosServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[VehiculosServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(VehiculosServicio.CodigoPrograma + ".id");
                }
            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();
            vVehiculos = VehiculosServicio.ConsultarVehiculos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vVehiculos.cod_vehiculo != Int64.MinValue)
                txtCod_vehiculo.Text = HttpUtility.HtmlDecode(vVehiculos.cod_vehiculo.ToString().Trim());
            
            if (vVehiculos.marca != null)
                ddlMarca.SelectedValue = HttpUtility.HtmlDecode(vVehiculos.marca.ToString().Trim());
            if (!string.IsNullOrEmpty(vVehiculos.placa))
                txtPlaca.Text = HttpUtility.HtmlDecode(vVehiculos.placa.ToString().Trim());
            if (vVehiculos.modelo != Int64.MinValue)
                ddlModelo.SelectedValue = HttpUtility.HtmlDecode(vVehiculos.modelo.ToString().Trim());
            if (vVehiculos.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = HttpUtility.HtmlDecode(vVehiculos.valorcomercial.ToString().Trim());
            if (vVehiculos.valorprenda != Int64.MinValue)
                txtValorprenda.Text = HttpUtility.HtmlDecode(vVehiculos.valorprenda.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();

            if (idObjeto != "")
                vVehiculos = VehiculosServicio.ConsultarVehiculos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_vehiculo.Text != "") vVehiculos.cod_vehiculo = Convert.ToInt64(txtCod_vehiculo.Text.Trim());
            vVehiculos.cod_persona = Convert.ToInt64(Session["CodCodeudor"].ToString());
            vVehiculos.marca = Convert.ToString(ddlMarca.SelectedValue);
            //vVehiculos.placa = Convert.ToString(txtPlaca.Text.Trim());
            vVehiculos.placa = (txtPlaca.Text != "") ? Convert.ToString(txtPlaca.Text.Trim()) : String.Empty;
            vVehiculos.modelo = Convert.ToInt64(ddlModelo.SelectedValue);
            if (txtValorcomercial.Text != "") vVehiculos.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim().Replace(".", ""));
            if (txtValorprenda.Text != "") vVehiculos.valorprenda = Convert.ToInt64(txtValorprenda.Text.Trim().Replace(".", ""));

            if (idObjeto != "")
            {
                vVehiculos.cod_vehiculo = Convert.ToInt64(idObjeto);
                VehiculosServicio.ModificarVehiculos(vVehiculos, (Usuario)Session["usuario"]);
            }
            else
            {
                vVehiculos = VehiculosServicio.CrearVehiculos(vVehiculos, (Usuario)Session["usuario"]);
                idObjeto = vVehiculos.cod_vehiculo.ToString();
            }

            Session[VehiculosServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
        Borrar();
    }

    protected void ddlMarca_DataBound(object sender, EventArgs e)
    {

    }
}