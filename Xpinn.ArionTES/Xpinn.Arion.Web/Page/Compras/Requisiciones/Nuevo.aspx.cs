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
using Xpinn.ActivosFijos.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.Articuloservices Articuloservices = new Xpinn.ActivosFijos.Services.Articuloservices();
    private Xpinn.ActivosFijos.Services.RequisicionServices RequisicionServices = new Xpinn.ActivosFijos.Services.RequisicionServices();

    private Xpinn.ActivosFijos.Services.Areasservices Areasservices = new Xpinn.ActivosFijos.Services.Areasservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Articuloservices.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(RequisicionServices.CodigoPrograma, "E");
            else
                VisualizarOpciones(RequisicionServices.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        try
        {

         
        }
        catch
        { }
    }

    protected void gvListaArticulo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvListaArticulo.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            //ReporteServicio.EliminarParametro(Convert.ToInt64(txtCodigo.Text), ID, _usuario);
           // TablaCodeudores();
        }
    }

    protected void InicialDetalleRequisicion()
    {
        List<Xpinn.ActivosFijos.Entities.Articulo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.Articulo>();
        Xpinn.ActivosFijos.Entities.Articulo eArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
        lstConsulta.Add(eArticulo);
        Session["Articulo"] = lstConsulta;
        gvListaArticulo.DataSource = lstConsulta;
        gvListaArticulo.DataBind();
        gvListaArticulo.Visible = true;
    }



    protected void gvListaArticulo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtIdArticulo = (TextBox)gvListaArticulo.FooterRow.FindControl("txtArticulo");
            TextBox txtCantidad = (TextBox)gvListaArticulo.FooterRow.FindControl("txtCantidad");

            if (txtIdArticulo.Text.Trim() == "")
            {
                VerError("Ingrese Codigo del Articulo.");
                return;
            }

            if (txtCantidad.Text.Trim() == "")
            {
                VerError("Ingrese Cantidad del Articulo.");
                return;
            }

            List<Articulo> lstArticulo = new List<Articulo>();
            lstArticulo = (List<Articulo>)Session["Articulo"];

            try
            {
                
                if (lstArticulo.Count == 1)
                {
                    Articulo gItem = new Articulo();
                    gItem = lstArticulo[0];
                    if (gItem.idarticulo  == 0)
                        lstArticulo.Remove(gItem);
                }
            }

            catch { }

                Xpinn.ActivosFijos .Services.Articuloservices Articuloservices = new Xpinn.ActivosFijos.Services.Articuloservices();
                Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
                vArticulo = Articuloservices.ConsultarArticulo(long.Parse(txtIdArticulo.Text), null);

                Articulo gItemNew = new Articulo();

                gItemNew.descripcion  = vArticulo.descripcion;
                gItemNew.idarticulo  = vArticulo.idarticulo;
                gItemNew.idtipo_articulo  = vArticulo.idtipo_articulo;
                gItemNew.marca  = vArticulo.marca;
                gItemNew.referencia  = vArticulo.referencia;
                gItemNew.serial  = vArticulo.serial;
                gItemNew.Cantidad = long.Parse(txtCantidad.Text);
                lstArticulo .Add(gItemNew);
                gvListaArticulo.DataSource = lstArticulo;
                gvListaArticulo.DataBind();
                Session["Articulo"] = lstArticulo ;
            

        }
        if (e.CommandName.Equals("Delete"))
        {
           
          
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Xpinn.ActivosFijos.Entities.Areas vAreas = new Xpinn.ActivosFijos.Entities.Areas();
                List<Xpinn.ActivosFijos.Entities.Areas> lstAreas = new List<Xpinn.ActivosFijos.Entities.Areas>();
                lstAreas = Areasservices.ListarAreas (vAreas, (Usuario)Session["usuario"]);
                InicialDetalleRequisicion();





                if (lstAreas.Count > 0)
                {
                    ListItem i;
                    foreach (Areas item in lstAreas)

                    {
                        i = new ListItem(item.DescripcionArea  , item.IdArea.ToString());
                        ddlArea.Items.Add(i);
                    }
                }


                if (Session[Articuloservices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[Articuloservices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(Articuloservices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(Articuloservices.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
            Xpinn.ActivosFijos.Entities.Requisicion vRequisicion = new Xpinn.ActivosFijos.Entities.Requisicion();
            Xpinn.ActivosFijos.Entities.Detalle_Requisicion vRequisicionDet = new Xpinn.ActivosFijos.Entities.Detalle_Requisicion();
            List<Xpinn.ActivosFijos.Entities.Detalle_Requisicion> lstConsultaDetalle = new List<Xpinn.ActivosFijos.Entities.Detalle_Requisicion>();

            if (idObjeto != "")
                vRequisicion = RequisicionServices.ConsultarRequisicion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);


            vRequisicion.idrequisicion = Convert.ToInt32(txtCodigo.Text.Trim());
            vRequisicion.idarea = Convert.ToInt32 (ddlArea.SelectedValue);
            vRequisicion.fecha_crea = Convert.ToDateTime (DateTime.Now);
            vRequisicion.fecha_est_entrega = Convert.ToDateTime(txtFechaEntrega.Text );
            vRequisicion.fecha_requsicion = Convert.ToDateTime(txtFechaRequisicion.Text );
            vRequisicion.cod_solicita = int.Parse(txtSolicitud.Text );
            vRequisicion.cod_usuario_crea  = txtUsuario.Text.Trim();
            vRequisicion.observacion  = Convert.ToString(TxtObservaciones .Text.Trim());
            vRequisicion.destino  = txtDestino.Text.Trim();
            int iddetrequisicion = 0;


            foreach (Articulo item in (List<Articulo>)Session["Articulo"])

            {
                Detalle_Requisicion gItemDet = new Detalle_Requisicion();
                gItemDet.iddetrequisicion = iddetrequisicion;
                gItemDet.idrequisicion = Convert.ToInt32(txtCodigo.Text.Trim());
                gItemDet.idarticulo = item.idarticulo;
                gItemDet.cantidad = item.Cantidad;
                gItemDet.detalle = item.descripcion;
                lstConsultaDetalle.Add(gItemDet);
                iddetrequisicion++;
            }

          



            if (idObjeto != "")
            {
                vArticulo.idarticulo = Convert.ToInt32(idObjeto);
                RequisicionServices.ModificarRequisicion  (vRequisicion, lstConsultaDetalle, (Usuario)Session["usuario"]);
            }
            else
            {
                vRequisicion = RequisicionServices.CrearRequisicion  (vRequisicion, lstConsultaDetalle, (Usuario)Session["usuario"]);
                idObjeto = vRequisicion.idrequisicion.ToString();
            }

            Session[RequisicionServices.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void txtArticulo_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvListaArticulo.FooterRow.FindControl("txtArticulo");
        if (ctrl != null)
        {
            TextBox txtArticulo = (TextBox)ctrl;
            Xpinn.ActivosFijos.Services.Articuloservices Articuloservices = new Xpinn.ActivosFijos.Services.Articuloservices();
            Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
            vArticulo = Articuloservices.ConsultarArticulo(long.Parse(txtArticulo.Text), null);
            gvListaArticulo.FooterRow.Cells[3].Text = vArticulo.serial;
            gvListaArticulo.FooterRow.Cells[4].Text = vArticulo.descripcion ;         
            gvListaArticulo.FooterRow.Cells[6].Text = vArticulo.marca ;
            gvListaArticulo.FooterRow.Cells[7].Text = vArticulo.referencia ;
           




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

             Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
            
            vArticulo = Articuloservices.ConsultarArticulo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

        //    if (!string.IsNullOrEmpty(vArticulo.idarticulo   .ToString()))
        //        txtCodigo.Text = HttpUtility.HtmlDecode(vArticulo.idarticulo.ToString().Trim());
        //    if (!string.IsNullOrEmpty(vArticulo.descripcion  ))
        //        txtDescripcion.Text = HttpUtility.HtmlDecode(vArticulo.descripcion .ToString().Trim());
        //    if (!string.IsNullOrEmpty(vArticulo.serial  .ToString()))
        //        txtSerial  .Text = HttpUtility.HtmlDecode(vArticulo.serial.ToString().Trim());
        //    if (!string.IsNullOrEmpty(vArticulo.referencia .ToString()))
        //        txtReferencia .Text = HttpUtility.HtmlDecode(vArticulo.referencia.ToString().Trim());
        //    if (!string.IsNullOrEmpty(vArticulo.marca ))
        //        txtMarca.Text = HttpUtility.HtmlDecode(vArticulo.marca.ToString().Trim());
        //    if (!string.IsNullOrEmpty(vArticulo.idtipo_articulo .ToString()))
        //        ddlTipoArticulo.SelectedValue  = HttpUtility.HtmlDecode(vArticulo.idtipo_articulo .ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RequisicionServices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    



}