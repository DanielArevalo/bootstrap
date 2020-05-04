using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;

public partial class AseMoviGralEstadoCuenta : GlobalWeb
{
   MovGralCreditoService movGrlService = new MovGralCreditoService();

    private Xpinn.Comun.Services.GDocumentalService GDocumentalService = new Xpinn.Comun.Services.GDocumentalService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GDocumentalService.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //txtFechaFinal.Text = DateTime.Now.ToShortDateString();
                //AsignarEventoConfirmar();
                if (Session[GDocumentalService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GDocumentalService.CodigoPrograma + ".id"].ToString();
                    //Session.Remove(movGrlService.CodigoPrograma + ".id");
                    //ObtenerDatos(idObjeto);
                }
            }
            String codigocliente = "";
            codigocliente = Request["idCliente"];
            if (codigocliente != null)
            {
                txtCodigo.Text = codigocliente;
                this.Actualizar2(codigocliente);

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GDocumentalService.GetType().Name);
        Actualizar();
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        Producto producto = new Producto();

        if (evt.CommandName == "EstadoCuenta")
        {
            producto.Persona.IdPersona = Convert.ToInt64(evt.CommandArgument.ToString());
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            Navegar("~/Page/GestionDocumental/Gestion/Nuevo.aspx");
        }

   }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLstMoviGralCredito.SelectedRow.Cells[0].Text;
        Session[GDocumentalService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo );
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLstMoviGralCredito.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.GetType().Name + "L", "gvLstMoviGralCredito_PageIndexChanging", ex);
        }
    }

 
    private void Actualizar()
    {
        try
        {
            Producto producto = new Producto();
            List<Persona> lstPersona = movGrlService.ListarMovGral(ObtenerValores(), (Usuario)Session["usuario"]);

            if (lstPersona.Count == 1)
            {
                gvLstMoviGralCredito.Visible = true;
                producto.Persona.IdPersona = ((Persona)(lstPersona[0])).IdPersona;
                Session[MOV_GRAL_CRED_PRODUC] = producto;
                Navegar("~/Page/GestionDocumental/Gestion/Nuevo.aspx");
            }
            else if (lstPersona.Count == 0)
            {
                gvLstMoviGralCredito.Visible = false;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> No se encontraròn registros con los datos dados";
            }
            else if (lstPersona.Count > 1)
            {
                var lstMovGral = (from p in lstPersona
                                  select new
                                  {
                                     pIdentificacion=p.NumeroDocumento,
                                     pNombre = p.PrimerNombre,
                                      sNombre = p.SegundoNombre,
                                      pApellido = p.PrimerApellido,
                                      sApellido = p.SegundoApellido,
                                      idPersona = p.IdPersona,
                                      codNomina = p.CodigoNomina
                                  }).ToList();

                gvLstMoviGralCredito.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvLstMoviGralCredito.DataSource = lstMovGral;

                if (lstMovGral.Count() > 0)
                {
                    gvLstMoviGralCredito.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstMovGral.Count().ToString();
                    gvLstMoviGralCredito.DataBind();
                    ValidarPermisosGrilla(gvLstMoviGralCredito);
                }
                else
                {
                    gvLstMoviGralCredito.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = false;
                }

            }

            Session.Add(movGrlService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movGrlService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void Actualizar2(String pIdObjeto)
    {
        try
        {
            Producto producto = new Producto();
            List<Persona> lstPersona = movGrlService.ListarMovGral(ObtenerValores(), (Usuario)Session["usuario"]);

            if (lstPersona.Count == 1)
            {
                producto.Persona.IdPersona = ((Persona)(lstPersona[0])).IdPersona;
                Session[MOV_GRAL_CRED_PRODUC] = producto;
                Navegar("~/Page/GestionDocumental/Gestion/Nuevo.aspx");
            }
            else if (lstPersona.Count == 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> No se encontraròn registros con los datos dados";
            }
            else if (lstPersona.Count > 1)
            {
                var lstMovGral = (from p in lstPersona
                                  select new
                                  {
                                      pNombre = p.PrimerNombre,
                                      sNombre = p.SegundoNombre,
                                      pApellido = p.PrimerApellido,
                                      sApellido = p.SegundoApellido,
                                      idPersona = p.IdPersona,
                                      codNomina = p.CodigoNomina
                                  }).ToList();

                gvLstMoviGralCredito.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvLstMoviGralCredito.DataSource = lstMovGral;

                if (lstMovGral.Count() > 0)
                {
                    gvLstMoviGralCredito.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstMovGral.Count().ToString();
                    gvLstMoviGralCredito.DataBind();
                    ValidarPermisosGrilla(gvLstMoviGralCredito);
                }
                else
                {
                    gvLstMoviGralCredito.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = false;
                }

            }

            Session.Add(movGrlService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movGrlService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Persona ObtenerValores()
    {
        Persona entityPersona = new Persona();
        if (!string.IsNullOrEmpty(this.txtCodigo.Text.Trim())) entityPersona.IdPersona = Convert.ToInt64(txtCodigo.Text.Trim());      
        if (!string.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) entityPersona.PrimerNombre = txtPrimerNombre.Text.Trim().ToUpper();
        if (!string.IsNullOrEmpty(txtSegundoNombre.Text.Trim())) entityPersona.SegundoNombre = txtSegundoNombre.Text.Trim().ToUpper();      
        if (!string.IsNullOrEmpty(txtPrimerApellido.Text.Trim())) entityPersona.PrimerApellido = txtPrimerApellido.Text.Trim().ToUpper();
        if (!string.IsNullOrEmpty(txtNumeIdentificacion.Text.Trim())) entityPersona.NumeroDocumento = Convert.ToString(Convert.ToInt64(txtNumeIdentificacion.Text.Trim()));
        if (!string.IsNullOrEmpty(this.txtSegundoApellido.Text.Trim())) entityPersona.SegundoApellido = txtSegundoApellido.Text.Trim().ToUpper();
        if (!string.IsNullOrEmpty(this.txtCodNomina.Text.Trim())) entityPersona.CodigoNomina = txtCodNomina.Text.Trim().ToUpper();


        return entityPersona;
    }
}