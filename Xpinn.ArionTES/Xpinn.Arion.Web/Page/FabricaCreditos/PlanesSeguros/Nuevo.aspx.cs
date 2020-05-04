using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;



public partial class Nuevo : GlobalWeb
{
    PlanesSegurosService planSegurosServicio = new PlanesSegurosService();

    PlanesSegurosAmparosService planSegurosAmparosServicio = new PlanesSegurosAmparosService();
    String operacion = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[planSegurosServicio.CodigoPlan + ".id"] != null)
            {
                idObjeto = Session[planSegurosServicio.CodigoPlan.ToString() + ".id"].ToString();
                Session.Remove(planSegurosServicio.CodigoPlan.ToString() + ".id");
                ObtenerDatos(idObjeto);
            }
            operacion = (String)Session["operacion"];
            if (operacion == null)
            {
                operacion = "";
            }
            if (!IsPostBack)
            {
                MostrarDatosAmparos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "A", "Page_Load", ex);
        }


    }
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[planSegurosServicio.CodigoPlan + ".id"] != null)
                VisualizarOpciones(planSegurosServicio.CodigoPrograma.ToString(), "E");
            else
                VisualizarOpciones(planSegurosServicio.CodigoPrograma.ToString(), "A");

            Site toolBar = (Site)this.Master;

            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[planSegurosServicio.CodigoPlan + ".id"] = idObjeto;
            Session["PlanesSegurosAmparos"] = null;
            Session["PlanesSegurosAmparosVida"] = null;
            Session["PlanesSegurosAmparosAcc"] = null;
            Navegar(Pagina.Detalle);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Session["PlanesSegurosAmparos"] = null;
        Session["PlanesSegurosAmparosVida"] = null;
        Session["PlanesSegurosAmparosAcc"] = null;
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ConsultarAccidentes();
        ConsultarVidaGrupo();
        Navegar(Pagina.Lista);

    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            planSegurosServicio.EliminarPlanesSeguros(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.CodigoPlan + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[planSegurosServicio.CodigoPlan + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (VerificarDatosVacios())
        {
            this.grabar();
        }
        Navegar(Pagina.Lista);

    }

    private Boolean VerificarDatosVacios()
    {
        Boolean continuar = true;
        if (this.txtnombreplan.Text == "")
        {
            String Error = "Se debe ingresar datos para guardar el plan";
            this.Lblerror.Text = Error;
            continuar = false;
            
            this.quitarfilainicialaccidentes();
            this.quitarfilainicialvidagrupo();

        }
        return continuar;
    }

    private void grabar()
    {

        try
        {
            PlanesSeguros planSeguros = new PlanesSeguros();

            planSeguros.tipo_plan = 0;
            planSeguros.descripcion = txtnombreplan.Text;
            planSeguros.prima_individual = (Int64.Parse("0" + txtPrima_Ind.Text));
            planSeguros.prima_conyuge = (Int64.Parse("0" + txtPrima_Cony.Text));
            planSeguros.prima_accidentes_individual = (Int64.Parse("0" + txtPrima_Acci_Ind.Text));
            planSeguros.prima_accidentes_familiar = (Int64.Parse("0" + txtPrima_Acci_Fam.Text));

            if (idObjeto != "")
            {
                planSeguros.tipo_plan = Convert.ToInt64(idObjeto);
                planSegurosServicio.ModificarPlanesSeguros(planSeguros, (Usuario)Session["usuario"]);
            }
            else
            {
                planSeguros = planSegurosServicio.CrearPlanesSeguros(planSeguros, (Usuario)Session["usuario"]);
                if (planSeguros != null)
                {
                    if (planSeguros.tipo_plan > 0)
                    {
                        GuardarPlanessegurosamparos(planSeguros.tipo_plan);
                    }
                }
                Session["PlanesSegurosAmparos"] = null;
                Session["PlanesSegurosAmparosVida"] = null;
                Session["PlanesSegurosAmparosAcc"] = null;
                Navegar(Pagina.Lista);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name, "btnGuardar_Click", ex);
        }
    }



    private void GuardarPlanessegurosamparos(long conseid)
    {

        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosvida = new List<PlanesSegurosAmparos>();

        LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];

        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosvida)
        {
            plan.tipo_plan = conseid;
            if (plan.descripcion != null)
            {
                if (plan.descripcion.Trim().Length > 0)
                {
                    planSegurosAmparosServicio.InsertarPlanesSegurosAmparos(plan, (Usuario)Session["usuario"]);
                }

            }
        }

        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosAcc = new List<PlanesSegurosAmparos>();

        LstPlanesSegurosAmparosAcc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];

        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosAcc)
        {
            plan.tipo_plan = conseid;
            if (plan.descripcion != null)
            {
                if (plan.descripcion.Trim().Length > 0)
                {
                    planSegurosAmparosServicio.InsertarPlanesSegurosAmparos(plan, (Usuario)Session["usuario"]);
                }
            }
        }

    }

    protected void gvVidaGrupo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvVidaGrupo.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvVidaGrupo.EditIndex = e.NewEditIndex;
           this.ConsultarVidaGrupo();

        }
        else
        {
            e.Cancel = true;
        }

    }

    private void quitarfilainicialvidagrupo()
    {
        try
        {
            int conseID = Convert.ToInt32(gvVidaGrupo.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvVidaGrupo.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvVidaGrupo.Rows[0].Cells[1].Text = "";
                this.gvVidaGrupo.Rows[0].Cells[2].Text = "";
                this.gvVidaGrupo.Rows[0].Cells[3].Visible = false;
                this.gvVidaGrupo.Rows[0].Cells[4].Visible = false;
            }
        }
        catch 
        {
        }
    }

    private void quitarfilainicialaccidentes()
    {
        try
        {
            int conseID = Convert.ToInt32(gvAccPers.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvAccPers.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;
                this.gvAccPers.Rows[0].Cells[5].Text = "";
                this.gvAccPers.Rows[0].Cells[1].Text = "";
                this.gvAccPers.Rows[0].Cells[2].Visible = false;
                this.gvAccPers.Rows[0].Cells[3].Visible = false;
                this.gvAccPers.Rows[0].Cells[4].Visible = false;

            }
        }
        catch 
        {
        }

    }

    protected void gvVidaGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtdescripcion = (TextBox)gvVidaGrupo.FooterRow.FindControl("txtnewdescripcion");
            TextBox txtvalor_cubierto = (TextBox)gvVidaGrupo.FooterRow.FindControl("txtnewvalor_cubierto");


            List<PlanesSegurosAmparos> LstPlanesSegurosAmparosvida = new List<PlanesSegurosAmparos>();

            LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];

            PlanesSegurosAmparos planSegurosAmparos = new PlanesSegurosAmparos();

            planSegurosAmparos.consecutivo = this.buscarultimoconsecutivo(LstPlanesSegurosAmparosvida) + 1;
            planSegurosAmparos.tipo_plan = -99;
            planSegurosAmparos.tipo = "Vida Grupo";
            planSegurosAmparos.descripcion = txtdescripcion.Text;

            if (txtvalor_cubierto.Text.Trim().Length == 0 || txtdescripcion.Text == "")
            {
                String Error = "Debe diligenciar los datos del Amparo vida Grupo y el valor.";
                this.Lblerror.Text = Error;

            }
            else
            {
                planSegurosAmparos.valor_cubierto = Convert.ToInt64(txtvalor_cubierto.Text.Trim());


                planSegurosAmparos.valor_cubierto_conyuge = 0;
                planSegurosAmparos.valor_cubierto_hijos = 0;



                gvVidaGrupo.EditIndex = -1;
                if (!this.operacion.Equals("N"))
                {
                    planSegurosAmparos.tipo_plan = Convert.ToInt64("0" + txtcodigoplan.Text);
                    planSegurosAmparosServicio.InsertarPlanesSegurosAmparos(planSegurosAmparos, (Usuario)Session["usuario"]);
                }


                LstPlanesSegurosAmparosvida.Add(planSegurosAmparos);
                this.MostrarDatosAmparos();
              
            }
            quitarfilainicialaccidentes();
            quitarfilainicialvidagrupo();

        }
    }


    protected void gvVidaGrupo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvVidaGrupo.EditIndex = -1;
        this.ConsultarVidaGrupo();

    }

    protected void gvVidaGrupo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvVidaGrupo.DataKeys[e.RowIndex].Values[0].ToString());
        if (conseID != 0)
        {
            try
            {          
                if (this.operacion.Equals("N"))
                {
                    eliminarGrupo_AmparosVidaNuevo(conseID);
                }
                else
                {
                    this.planSegurosAmparosServicio.EliminarPlanesSegurosAmparos(conseID, (Usuario)Session["usuario"]);     
                    this.ConsultarVidaGrupo();
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(planSegurosAmparosServicio.consecutivo + "L", "gvVidaGrupo_RowDeleting", ex);
            }

        }
        else
        {
            e.Cancel = true;
        }
    }

    private void eliminarGrupo_AmparosVidaNuevo(int conseID)
    {
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosvida;
        LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosvida)
        {
            if (plan.consecutivo == conseID)
            {
                LstPlanesSegurosAmparosvida.Remove(plan);
                break;
            }
        }
        Session["PlanesSegurosAmparosVida"] = LstPlanesSegurosAmparosvida;
        MostrarDatosAmparos();

    }

    protected void gvVidaGrupo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtdescripcion = (TextBox)gvVidaGrupo.Rows[e.RowIndex].FindControl("txtdescripcion");
        TextBox txtvalor_cubierto = (TextBox)gvVidaGrupo.Rows[e.RowIndex].FindControl("txtvalor_cubierto");

        long conseID = Convert.ToInt32(gvVidaGrupo.DataKeys[e.RowIndex].Values[0].ToString());

        PlanesSegurosAmparos planSegurosAmparos = new PlanesSegurosAmparos();

        planSegurosAmparos.consecutivo = conseID;
        planSegurosAmparos.descripcion = txtdescripcion.Text;
        planSegurosAmparos.valor_cubierto = Convert.ToInt64(txtvalor_cubierto.Text);
        planSegurosAmparos.valor_cubierto_conyuge = 0;
        planSegurosAmparos.valor_cubierto_hijos = 0;

        gvVidaGrupo.EditIndex = -1;

        if (operacion.Equals("N"))
        {
            ActualizarGrupo_AmparosVidaNuevo(planSegurosAmparos);
        }
        else
        {
            planSegurosAmparosServicio.ModificarPlanesSegurosAmparos(planSegurosAmparos, (Usuario)Session["usuario"]);
            this.ConsultarVidaGrupo();

        }
        quitarfilainicialvidagrupo();
    }

    private void ActualizarGrupo_AmparosVidaNuevo(PlanesSegurosAmparos planSegurosAmparos)
    {
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosvida;
        LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosvida)
        {
            if (plan.consecutivo == planSegurosAmparos.consecutivo)
            {
                plan.consecutivo = planSegurosAmparos.consecutivo;
                plan.descripcion = planSegurosAmparos.descripcion;
                plan.tipo = planSegurosAmparos.tipo;
                plan.tipo_plan = planSegurosAmparos.tipo_plan;
                plan.valor_cubierto = planSegurosAmparos.valor_cubierto;
                plan.valor_cubierto_conyuge = planSegurosAmparos.valor_cubierto_conyuge;
                plan.valor_cubierto_hijos = planSegurosAmparos.valor_cubierto_hijos;
                break;
            }
        }
        Session["PlanesSegurosAmparosVida"] = LstPlanesSegurosAmparosvida;
        MostrarDatosAmparos();

    }

    protected void gvVidaGrupo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }

    protected void gvVidaGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvVidaGrupo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvAccPers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtdescripcionacc = (TextBox)gvAccPers.FooterRow.FindControl("txtnewdescripcionacc");
            TextBox txtvalor_cubiertoacc = (TextBox)gvAccPers.FooterRow.FindControl("txtnewvalor_cubiertoacc");
            TextBox txtvalor_cubierto_cony = (TextBox)gvAccPers.FooterRow.FindControl("txtnewvalor_cubierto_cony");
            TextBox txtvalor_cubierto_hijos = (TextBox)gvAccPers.FooterRow.FindControl("txtnewvalor_cubierto_hijos");

            List<PlanesSegurosAmparos> LstPlanesSegurosAmparosacc = new List<PlanesSegurosAmparos>();

            LstPlanesSegurosAmparosacc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];


            PlanesSegurosAmparos planSegurosAmparos = new PlanesSegurosAmparos();

            planSegurosAmparos.consecutivo = this.buscarultimoconsecutivo(LstPlanesSegurosAmparosacc) + 1;
            planSegurosAmparos.tipo_plan = -99;
            planSegurosAmparos.tipo = "Accidentes Personales";
            planSegurosAmparos.descripcion = txtdescripcionacc.Text;

            if (txtvalor_cubiertoacc.Text.Trim().Length == 0 || txtvalor_cubierto_hijos.Text.Trim().Length == 0 || txtvalor_cubierto_cony.Text.Trim().Length == 0)
            {
                String Error = "Debe diligenciar los datos del Grupo Accidentes y sus valores";
                this.Lblerror.Text = Error;

            }
            else
            {
                planSegurosAmparos.valor_cubierto = Convert.ToInt64(txtvalor_cubiertoacc.Text);

                planSegurosAmparos.valor_cubierto_conyuge = Convert.ToInt64(txtvalor_cubierto_cony.Text);

                planSegurosAmparos.valor_cubierto_hijos = Convert.ToInt64(txtvalor_cubierto_hijos.Text);

                gvAccPers.EditIndex = -1;

                if (!this.operacion.Equals("N"))
                {
                    planSegurosAmparos.tipo_plan = Convert.ToInt64("0" + txtcodigoplan.Text);
                    planSegurosAmparosServicio.InsertarPlanesSegurosAmparos(planSegurosAmparos, (Usuario)Session["usuario"]);
                }
                LstPlanesSegurosAmparosacc.Add(planSegurosAmparos);
                this.MostrarDatosAmparos();

            }

            quitarfilainicialaccidentes();
            quitarfilainicialvidagrupo();

        }
    }
    protected void gvAccPers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAccPers.EditIndex = -1;
        this.ConsultarAccidentes();
    }

    protected void gvAccPers_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void gvAccPers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvAccPers.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                if (this.operacion.Equals("N"))
                {
                    this.eliminarGrupo_AccidentesNuevo(conseID);
                }
                else
                {
                    this.planSegurosAmparosServicio.EliminarPlanesSegurosAmparos(conseID, (Usuario)Session["usuario"]);
                    this.ConsultarAccidentes();
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(planSegurosAmparosServicio.consecutivo + "L", "gvAccPers_RowDeleting", ex);
            }
        }

        else
        {
            e.Cancel = true;
        }
    }

    private void eliminarGrupo_AccidentesNuevo(int conseID)
    {
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosacc;
        LstPlanesSegurosAmparosacc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosacc)
        {
            if (plan.consecutivo == conseID)
            {
                LstPlanesSegurosAmparosacc.Remove(plan);
                break;
            }
        }
        Session["PlanesSegurosAmparosAcc"] = LstPlanesSegurosAmparosacc;
        MostrarDatosAmparos();

    }

    protected void gvAccPers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvAccPers.DataKeys[e.NewEditIndex].Values[0].ToString());
        if (conseID != 0)
        {
            gvAccPers.EditIndex = e.NewEditIndex;
            this.ConsultarAccidentes();
        }
        else
        {
            e.Cancel = true;
        }

    }

    protected void gvAccPers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }

    protected void gvAccPers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtdescripcionacc = (TextBox)gvAccPers.Rows[e.RowIndex].FindControl("txtdescripcionacc");
        TextBox txtvalor_cubiertoacc = (TextBox)gvAccPers.Rows[e.RowIndex].FindControl("txtvalor_cubiertoacc");
        TextBox txtvalor_cubierto_cony = (TextBox)gvAccPers.Rows[e.RowIndex].FindControl("txtvalor_cubierto_cony");
        TextBox txtvalor_cubierto_hijos = (TextBox)gvAccPers.Rows[e.RowIndex].FindControl("txtvalor_cubierto_hijos");


        long conseID = Convert.ToInt32(gvAccPers.DataKeys[e.RowIndex].Values[0].ToString());


        PlanesSegurosAmparos planSegurosAmparos = new PlanesSegurosAmparos();

        planSegurosAmparos.consecutivo = conseID;
        planSegurosAmparos.descripcion = txtdescripcionacc.Text;
        planSegurosAmparos.valor_cubierto = Convert.ToInt64(txtvalor_cubiertoacc.Text);
        planSegurosAmparos.valor_cubierto_hijos = Convert.ToInt64(txtvalor_cubierto_hijos.Text);
        planSegurosAmparos.valor_cubierto_conyuge = Convert.ToInt64(txtvalor_cubierto_cony.Text);

        gvAccPers.EditIndex = -1;

        if (operacion.Equals("N"))
        {
            ActualizarGrupo_AccidentesNuevo(planSegurosAmparos);
        }
        else
        {

            planSegurosAmparosServicio.ModificarPlanesSegurosAmparos(planSegurosAmparos, (Usuario)Session["usuario"]);
            this.ConsultarAccidentes();
        }

        this.quitarfilainicialaccidentes();

    }

    private void ActualizarGrupo_AccidentesNuevo(PlanesSegurosAmparos planSegurosAmparos)
    {
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosacc;
        LstPlanesSegurosAmparosacc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparosacc)
        {
            if (plan.consecutivo == planSegurosAmparos.consecutivo)
            {
                plan.consecutivo = planSegurosAmparos.consecutivo;
                plan.descripcion = planSegurosAmparos.descripcion;
                plan.tipo = planSegurosAmparos.tipo;
                plan.tipo_plan = planSegurosAmparos.tipo_plan;
                plan.valor_cubierto = planSegurosAmparos.valor_cubierto;
                plan.valor_cubierto_conyuge = planSegurosAmparos.valor_cubierto_conyuge;
                plan.valor_cubierto_hijos = planSegurosAmparos.valor_cubierto_hijos;
                break;
            }
        }
        Session["PlanesSegurosAmparosAcc"] = LstPlanesSegurosAmparosacc;
        MostrarDatosAmparos();

    }

    protected void gvAccPers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    private void MostrarDatosAmparos()
    {

        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosvida = new List<PlanesSegurosAmparos>();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparosacc = new List<PlanesSegurosAmparos>();

        LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
        if ((LstPlanesSegurosAmparosvida == null) || (LstPlanesSegurosAmparosvida.Count==0))
        {
            crearPlanseguroamparosinicial("Vida Grupo", 0, "PlanesSegurosAmparosVida");
            LstPlanesSegurosAmparosvida = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
        }

        gvVidaGrupo.DataSource = LstPlanesSegurosAmparosvida;
        gvVidaGrupo.DataBind();

        LstPlanesSegurosAmparosacc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];

        if ((LstPlanesSegurosAmparosacc == null) || (LstPlanesSegurosAmparosacc.Count == 0))
        {
            crearPlanseguroamparosinicial("Accidentes Personales", 0, "PlanesSegurosAmparosAcc");
            LstPlanesSegurosAmparosacc = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        }

        gvAccPers.DataSource = LstPlanesSegurosAmparosacc;
        gvAccPers.DataBind();
        quitarfilainicialvidagrupo();
        quitarfilainicialaccidentes();

    }

    private void crearPlanseguroamparosinicial(String tipo, int consecutivo, String nombresession)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();

        pPlanesSegurosAmparos.consecutivo = consecutivo;
        pPlanesSegurosAmparos.tipo_plan = 99;
        pPlanesSegurosAmparos.tipo = tipo;
        pPlanesSegurosAmparos.descripcion = "";
        pPlanesSegurosAmparos.valor_cubierto = 0;
        pPlanesSegurosAmparos.valor_cubierto_conyuge = 0;
        pPlanesSegurosAmparos.valor_cubierto_hijos = 0;

        LstPlanesSegurosAmparos.Add(pPlanesSegurosAmparos);

        Session[nombresession] = LstPlanesSegurosAmparos;


    }
    private long buscarultimoconsecutivo(List<PlanesSegurosAmparos> LstPlanesSegurosAmparos)
    {
        long conseid = 0;

        if (LstPlanesSegurosAmparos != null)
        {
            foreach (PlanesSegurosAmparos plan in LstPlanesSegurosAmparos)
            {
                if (plan.consecutivo > conseid)
                {
                    conseid = plan.consecutivo;
                }

            }
        }
        conseid++;
        return conseid;

    }
    protected void ObtenerDatos(String pIdObjeto)
    {

        try
        {
            PlanesSeguros planesSeguros = new PlanesSeguros();
            if (pIdObjeto != null)
            {
                planesSeguros.tipo_plan = Int32.Parse(pIdObjeto);
                planesSeguros = planSegurosServicio.ConsultarPlanesSeguros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                PlanesSegurosAmparos planesSegurosAmparos = new PlanesSegurosAmparos();



                if (!string.IsNullOrEmpty(planesSeguros.tipo_plan.ToString()))
                {
                    txtcodigoplan.Text = planesSeguros.tipo_plan.ToString();
                    txtnombreplan.Text = planesSeguros.descripcion.ToString();
                    txtPrima_Ind.Text = planesSeguros.prima_individual.ToString();
                    txtPrima_Cony.Text = planesSeguros.prima_conyuge.ToString();
                    txtPrima_Acci_Ind.Text = planesSeguros.prima_accidentes_individual.ToString();
                    txtPrima_Acci_Fam.Text = planesSeguros.prima_accidentes_familiar.ToString();

                }

                this.ConsultarVidaGrupo();
                this.ConsultarAccidentes();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    private void ConsultarVidaGrupo()
    {
        String tipo_plan = Convert.ToInt64("0" + txtcodigoplan.Text).ToString();
        if (tipo_plan != "0")
        {
            MostrarDatosAmparos("Vida Grupo", tipo_plan, gvVidaGrupo, "PlanesSegurosAmparosVida");

        }
        else
        {
            MostrarDatosAmparosNuevo("Vida Grupo", tipo_plan, gvVidaGrupo, "PlanesSegurosAmparosVida");
        }
            quitarfilainicialaccidentes();
            quitarfilainicialvidagrupo();
        
       
    }

    private void ConsultarAccidentes()
    {
        String tipo_plan = Convert.ToInt64("0" + txtcodigoplan.Text).ToString();
        if (tipo_plan != "0")
        {
            MostrarDatosAmparos("Accidentes Personales", tipo_plan, gvAccPers, "PlanesSegurosAmparosAcc");
        }
        else
        {
            MostrarDatosAmparosNuevo("Accidentes Personales", tipo_plan, gvAccPers, "PlanesSegurosAmparosAcc");
        }
        quitarfilainicialaccidentes();
        quitarfilainicialvidagrupo();
    }


    private void MostrarDatosAmparos(String Tipo, String tipo_plan, GridView gvAmparos, String Var)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();
      
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + tipo_plan);
        pPlanesSegurosAmparos.tipo = Tipo;

        LstPlanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);

       

        if (LstPlanesSegurosAmparos.Count == 0 && Tipo == "Vida Grupo")
        {

            crearPlanseguroamparosinicial("Vida Grupo", 0, "PlanesSegurosAmparosVida");
            LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
            //crearPlanseguroamparosinicial("Accidentes Personales", 0, "PlanesSegurosAmparosAcc");
            //LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        }

        if (LstPlanesSegurosAmparos.Count == 0 && Tipo == "Accidentes Personales")
        {

            //crearPlanseguroamparosinicial("Vida Grupo", 0, "PlanesSegurosAmparosVida");
            //LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
            crearPlanseguroamparosinicial("Accidentes Personales", 0, "PlanesSegurosAmparosAcc");
            LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        }

         gvAmparos.DataSource = LstPlanesSegurosAmparos;
        gvAmparos.DataBind();
        Session[Var] = LstPlanesSegurosAmparos;
    }

    private void MostrarDatosAmparosNuevo(String Tipo, String tipo_plan, GridView gvAmparos, String Var)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();

        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + tipo_plan);
        pPlanesSegurosAmparos.tipo = Tipo;

        if (Convert.ToInt64(pPlanesSegurosAmparos.tipo_plan)>0)
        {

            LstPlanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        }
        else
        {
            if (Tipo == "Vida Grupo")
            {
                LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
            }
            if (Tipo == "Accidentes Personales")
            {
                LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
            }

        }

        gvAmparos.DataSource = LstPlanesSegurosAmparos;
        gvAmparos.DataBind();
        Session[Var] = LstPlanesSegurosAmparos;

        if (LstPlanesSegurosAmparos.Count == 0 && Tipo == "Vida Grupo")
        {

            crearPlanseguroamparosinicial("Vida Grupo", 0, "PlanesSegurosAmparosVida");
            LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];
        }

        if (LstPlanesSegurosAmparos.Count == 0 && Tipo == "Accidentes Personales")
        {

            crearPlanseguroamparosinicial("Accidentes Personales", 0, "PlanesSegurosAmparosAcc");
            LstPlanesSegurosAmparos = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];
        }
    }

    private void ListarDatosPlanesAmparos(Int64 tipo_plan)
    {
        PlanesSegurosAmparos planesegurosamparos = new PlanesSegurosAmparos();
        planesegurosamparos.tipo_plan = planesegurosamparos.tipo_plan = tipo_plan;


        List<PlanesSegurosAmparos> lstPlanesSegurosAmparosData = new List<PlanesSegurosAmparos>();

        lstPlanesSegurosAmparosData = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(planesegurosamparos, (Usuario)Session["usuario"]);

        this.gvVidaGrupo.DataSource = lstPlanesSegurosAmparosData;
        gvVidaGrupo.DataBind();
        Session["PlanesSegurosAmparos"] = lstPlanesSegurosAmparosData;
        //quitarfilainicialvidagrupo();
        // quitarfilainicialaccidentes();

    }
    private Boolean VerificarDatos()
    {
        Boolean continuar = true;
        Boolean segvid = false;
        Boolean segacc = false;

        List<PlanesSegurosAmparos> lstPlanesSegurosAmparosData = new List<PlanesSegurosAmparos>();
        lstPlanesSegurosAmparosData = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosVida"];

        if (lstPlanesSegurosAmparosData != null)
        {
            foreach (PlanesSegurosAmparos plan in lstPlanesSegurosAmparosData)
            {
                if (plan.descripcion != null)
                {
                    if (plan.descripcion.Trim().Length > 0)
                    {
                        segvid = true;
                        break;

                    }

                }
            }
        }
        lstPlanesSegurosAmparosData = (List<PlanesSegurosAmparos>)Session["PlanesSegurosAmparosAcc"];

        if (lstPlanesSegurosAmparosData != null)
        {
            foreach (PlanesSegurosAmparos plan in lstPlanesSegurosAmparosData)
            {
                if (plan.descripcion != null)
                {
                    if (plan.descripcion.Trim().Length > 0)
                    {
                        segacc = true;
                        break;
                    }
                }
            }
        }
        if ((segacc == false) || (segvid == false))
        {
            continuar = false;
        }

        return continuar;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.gvVidaGrupo.EditIndex = 0;


    }
    protected void txtdescripcion_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtPrima_Cony_TextChanged(object sender, EventArgs e)
    {

    }
}
