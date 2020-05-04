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
using System.Web.Configuration;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
    List<Xpinn.FabricaCreditos.Entities.VentasSemanales> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.VentasSemanales>();
    Xpinn.FabricaCreditos.Entities.VentasSemanales entTotales = new Xpinn.FabricaCreditos.Entities.VentasSemanales(); // Entidad donde se devuelven los calculos
    
    int contFilas = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {            
            VisualizarOpciones(VentasSemanalesServicio.CodigoPrograma, "L");
           
            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VentasSemanalesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, VentasSemanalesServicio.CodigoPrograma);
                if (Session[VentasSemanalesServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VentasSemanalesServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, VentasSemanalesServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, VentasSemanalesServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, VentasSemanalesServicio.CodigoPrograma);
    }

    /// <summary>
    /// Método para traer la informaciòn de la base de datos y realizar los càlculos de los totales
    /// </summary>
    private void Actualizar()
    {        
        try
        {
            lstConsulta = VentasSemanalesServicio.ListarVentasSemanales(ObtenerValores(), (Usuario)Session["usuario"]);

            if (lstConsulta.Count >= 4)
            {
                //Pasa lista a bussines para hacer calculos 
                txtPorContado.Text = lstConsulta[0].porContado.ToString();

                entTotales = VentasSemanalesServicio.CalculosTotalesSemanales(lstConsulta.GetRange(0, 3)); //Trae solo ventas b r y m, no los checks
                txtTotalSemanal.Text = entTotales.totalSemanal.ToString();

                txtVentasMes.Text = string.Format("{0:0}", entTotales.ventasMes.ToString());
                txtVentasContado.Text = entTotales.venContado.ToString();
                txtVentasCredito.Text = entTotales.venCredito.ToString();
                    
                if (Session["Guardar"] != null)
                {
                    contFilas = 0;
                    while (contFilas <= 3)
                    {                       
                        switch (lstConsulta[contFilas].tipoventas)
                        {
                            case "1":       //Ventas Buenas
                                rblLunes.Items[0].Selected = lstConsulta[contFilas].lunes == 1 ? true : false;
                                rblMartes.Items[0].Selected = lstConsulta[contFilas].martes == 1 ? true : false;
                                rblMiercoles.Items[0].Selected = lstConsulta[contFilas].miercoles == 1 ? true : false;
                                rblJueves.Items[0].Selected = lstConsulta[contFilas].jueves == 1 ? true : false;
                                rblViernes.Items[0].Selected = lstConsulta[contFilas].viernes == 1 ? true : false;
                                rblSabado.Items[0].Selected = lstConsulta[contFilas].sabados == 1 ? true : false;
                                rblDomingo.Items[0].Selected = lstConsulta[contFilas].domingo == 1 ? true : false;
                                txtValorB.Text = lstConsulta[contFilas].valor.ToString();
                                contFilas++;
                                break;

                            case "2":       //Ventas Regulares    
                                rblLunes.Items[1].Selected = lstConsulta[contFilas].lunes == 1 ? true : false;
                                rblMartes.Items[1].Selected = lstConsulta[contFilas].martes == 1 ? true : false;
                                rblMiercoles.Items[1].Selected = lstConsulta[contFilas].miercoles == 1 ? true : false;
                                rblJueves.Items[1].Selected = lstConsulta[contFilas].jueves == 1 ? true : false;
                                rblViernes.Items[1].Selected = lstConsulta[contFilas].viernes == 1 ? true : false;
                                rblSabado.Items[1].Selected = lstConsulta[contFilas].sabados == 1 ? true : false;
                                rblDomingo.Items[1].Selected = lstConsulta[contFilas].domingo == 1 ? true : false;
                                txtValorR.Text = lstConsulta[contFilas].valor.ToString();
                                contFilas++;
                                break;

                            case "3":       //Ventas Malas
                                rblLunes.Items[2].Selected = lstConsulta[contFilas].lunes == 1 ? true : false;
                                rblMartes.Items[2].Selected = lstConsulta[contFilas].martes == 1 ? true : false;
                                rblMiercoles.Items[2].Selected = lstConsulta[contFilas].miercoles == 1 ? true : false;
                                rblJueves.Items[2].Selected = lstConsulta[contFilas].jueves == 1 ? true : false;
                                rblViernes.Items[2].Selected = lstConsulta[contFilas].viernes == 1 ? true : false;
                                rblSabado.Items[2].Selected = lstConsulta[contFilas].sabados == 1 ? true : false;
                                rblDomingo.Items[2].Selected = lstConsulta[contFilas].domingo == 1 ? true : false;
                                txtValorM.Text = lstConsulta[contFilas].valor.ToString();
                                contFilas++;
                                break;

                            case "4":       //Checks para incluir o excluir días
                                ChkLunes.Checked = lstConsulta[contFilas].lunes     == 1 ? true : false;
                                ChkMartes.Checked = lstConsulta[contFilas].martes    == 1 ? true : false;
                                ChkMiercoles.Checked = lstConsulta[contFilas].miercoles == 1 ? true : false;
                                ChkJueves.Checked = lstConsulta[contFilas].jueves    == 1 ? true : false;
                                ChkViernes.Checked = lstConsulta[contFilas].viernes   == 1 ? true : false;
                                ChkSabado.Checked = lstConsulta[contFilas].sabados   == 1 ? true : false;
                                ChkDomingo.Checked = lstConsulta[contFilas].domingo   == 1 ? true : false;
                                contFilas++;
                                break;
                        }
                    }
                }
                else
                {
                    rblLunes.Items[0].Selected = true ;
                    rblMartes.Items[0].Selected = true;
                    rblMiercoles.Items[0].Selected = true;
                    rblJueves.Items[0].Selected = true;
                    rblViernes.Items[0].Selected = true;
                    rblSabado.Items[0].Selected = true;
                    rblDomingo.Items[0].Selected = true;

                    ChkLunes.Checked = true;
                    ChkMartes.Checked = true;
                    ChkMiercoles.Checked =true;
                    ChkJueves.Checked = true;
                    ChkViernes.Checked = true;
                    ChkSabado.Checked = true;
                    ChkDomingo.Checked = true;    
                    
                    txtTotalSemanal.Text = "";
                    txtVentasMes.Text = "";
                    txtVentasContado.Text = "";
                    txtVentasCredito.Text = "";
                }                     
                                           
            }

            Session.Add(VentasSemanalesServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VentasSemanalesServicio.CodigoPrograma, "Actualizar", ex);
        }
        
    }                        


 
    private Xpinn.FabricaCreditos.Entities.VentasSemanales ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.VentasSemanales vVentasSemanales = new Xpinn.FabricaCreditos.Entities.VentasSemanales();

        if(txtTipoventas.Text.Trim() != "")
            vVentasSemanales.tipoventas = Convert.ToString(txtTipoventas.Text.Trim());
        if(txtValor.Text.Trim() != "")
            vVentasSemanales.valor = Convert.ToInt64(txtValor.Text.Trim());
        if(txtLunes.Text.Trim() != "")
            vVentasSemanales.lunes = Convert.ToInt64(txtLunes.Text.Trim());
        if(txtMartes.Text.Trim() != "")
            vVentasSemanales.martes = Convert.ToInt64(txtMartes.Text.Trim());
        if(txtMiercoles.Text.Trim() != "")
            vVentasSemanales.miercoles = Convert.ToInt64(txtMiercoles.Text.Trim());
        if(txtJueves.Text.Trim() != "")
            vVentasSemanales.jueves = Convert.ToInt64(txtJueves.Text.Trim());
        if(txtViernes.Text.Trim() != "")
            vVentasSemanales.viernes = Convert.ToInt64(txtViernes.Text.Trim());
        if(txtSabados.Text.Trim() != "")
            vVentasSemanales.sabados = Convert.ToInt64(txtSabados.Text.Trim());
        if(txtDomingo.Text.Trim() != "")
            vVentasSemanales.domingo = Convert.ToInt64(txtDomingo.Text.Trim());
        if(txtTotal.Text.Trim() != "")
            vVentasSemanales.total = Convert.ToInt64(txtTotal.Text.Trim());

        vVentasSemanales.codPersona = Convert.ToInt64(Session["Cod_persona"].ToString());

        return vVentasSemanales;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Session["Guardar"] = "1";
        Guardar();
        Actualizar();
    }

    /// <summary>
    /// Método para guardar la informaciòn registrada.
    /// </summary>
    private void Guardar()
    {       
        try
            {
                lstConsulta = VentasSemanalesServicio.ListarVentasSemanales(ObtenerValores(), (Usuario)Session["usuario"]);

                if (lstConsulta.Count >= 4)
                {
                    Xpinn.FabricaCreditos.Entities.VentasSemanales vVentasSemanales = new Xpinn.FabricaCreditos.Entities.VentasSemanales();

                    vVentasSemanales.tipoventas = "1";
                    if (txtValorB.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorB.Text.Trim().Replace(@".", ""));
                    vVentasSemanales.lunes = rblLunes.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.martes = rblMartes.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.miercoles = rblMiercoles.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.jueves = rblJueves.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.viernes = rblViernes.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.sabados = rblSabado.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.domingo = rblDomingo.SelectedValue == "B" ? 1 : 0;
                    vVentasSemanales.total = 0;
                    vVentasSemanales.cod_ventas = 1;
                    vVentasSemanales.porContado = 100;
                    vVentasSemanales.codPersona = Convert.ToInt64(Session["Cod_persona"].ToString());
                    VentasSemanalesServicio.ModificarVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);

                    vVentasSemanales.tipoventas = "2";
                    if (txtValorR.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorR.Text.Trim().Replace(@".", ""));
                    vVentasSemanales.lunes = rblLunes.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.martes = rblMartes.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.miercoles = rblMiercoles.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.jueves = rblJueves.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.viernes = rblViernes.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.sabados = rblSabado.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.domingo = rblDomingo.SelectedValue == "R" ? 1 : 0;
                    vVentasSemanales.total = 0;
                    vVentasSemanales.cod_ventas = 2;
                    vVentasSemanales.porContado = 100;
                    VentasSemanalesServicio.ModificarVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);

                    vVentasSemanales.tipoventas = "3";
                    if (txtValorM.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorM.Text.Trim().Replace(@".", ""));
                    vVentasSemanales.lunes = rblLunes.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.martes = rblMartes.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.miercoles = rblMiercoles.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.jueves = rblJueves.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.viernes = rblViernes.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.sabados = rblSabado.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.domingo = rblDomingo.SelectedValue == "M" ? 1 : 0;
                    vVentasSemanales.total = 0;
                    vVentasSemanales.cod_ventas = 3;
                    vVentasSemanales.porContado = 100;
                    VentasSemanalesServicio.ModificarVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);

                    vVentasSemanales.tipoventas = "4";
                    vVentasSemanales.lunes = ChkLunes.Checked == true ? 1 : 0;
                    vVentasSemanales.martes = ChkMartes.Checked == true ? 1 : 0;
                    vVentasSemanales.miercoles = ChkMiercoles.Checked == true ? 1 : 0;
                    vVentasSemanales.jueves = ChkJueves.Checked == true ? 1 : 0;
                    vVentasSemanales.viernes = ChkViernes.Checked == true ? 1 : 0;
                    vVentasSemanales.sabados = ChkSabado.Checked == true ? 1 : 0;
                    vVentasSemanales.domingo = ChkDomingo.Checked == true ? 1 : 0;
                    vVentasSemanales.total = 0;
                    vVentasSemanales.cod_ventas = 4;
                    vVentasSemanales.porContado = 100;
                    VentasSemanalesServicio.ModificarVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);

                }
                else
                {
                    Xpinn.FabricaCreditos.Entities.VentasSemanales vVentasSemanales = new Xpinn.FabricaCreditos.Entities.VentasSemanales();
                    int TipoVenta = 1;
                    string sTipo = "";
                    vVentasSemanales.codPersona = Convert.ToInt64(Session["Cod_persona"].ToString());

                    while (TipoVenta <= 3)
                    {
                        vVentasSemanales.tipoventas = TipoVenta.ToString();                        
                        if (TipoVenta == 1)
                        {
                            if (txtValorB.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorB.Text.Trim().Replace(@".", ""));
                            sTipo = "B";
                        }
                        if (TipoVenta == 2)
                        {
                            if (txtValorR.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorR.Text.Trim().Replace(@".", ""));
                            sTipo = "R";
                        }
                        if (TipoVenta == 3)
                        {
                            if (txtValorM.Text != "") vVentasSemanales.valor = Convert.ToInt64(txtValorM.Text.Trim().Replace(@".", ""));
                            sTipo = "M";
                        }
                        vVentasSemanales.lunes = rblLunes.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.martes = rblMartes.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.miercoles = rblMiercoles.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.jueves = rblJueves.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.viernes = rblViernes.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.sabados = rblSabado.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.domingo = rblDomingo.SelectedValue == sTipo ? 1 : 0;
                        vVentasSemanales.total = 0;
                        vVentasSemanales.porContado = 100;                        
                        vVentasSemanales = VentasSemanalesServicio.CrearVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);
                        TipoVenta++;
                    }

                    vVentasSemanales.tipoventas = "4";
                    vVentasSemanales.lunes = ChkLunes.Checked == true ? 1 : 0;
                    vVentasSemanales.martes = ChkMartes.Checked == true ? 1 : 0;
                    vVentasSemanales.miercoles = ChkMiercoles.Checked == true ? 1 : 0;
                    vVentasSemanales.jueves = ChkJueves.Checked == true ? 1 : 0;
                    vVentasSemanales.viernes = ChkViernes.Checked == true ? 1 : 0;
                    vVentasSemanales.sabados = ChkSabado.Checked == true ? 1 : 0;
                    vVentasSemanales.domingo = ChkDomingo.Checked == true ? 1 : 0;
                    vVentasSemanales.total = 0;
                    vVentasSemanales.cod_ventas = 4;
                    vVentasSemanales.porContado = 100;
                    VentasSemanalesServicio.CrearVentasSemanales(vVentasSemanales, (Usuario)Session["usuario"]);
                }                
                           
            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(VentasSemanalesServicio.CodigoPrograma, "btnGuardar_Click", ex);
            }
}

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/EstacionalidadMensual/Lista.aspx");
    }
   
    protected void rblMartes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Guardar();
    }
    protected void rblMiercoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Guardar();
    }
    protected void rblJueves_SelectedIndexChanged(object sender, EventArgs e)
    {
         //Guardar();
    } 
    protected void rblViernes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Guardar();
    }
    protected void rblSabado_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Guardar();
    }
    protected void rblDomingo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Guardar();
    }
    protected void ChkLunes_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();        
    }
    protected void ChkMartes_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }
    protected void ChkMiercoles_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }
    protected void ChkJueves_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }
    protected void ChkViernes_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }
    protected void ChkSabado_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }
    protected void ChkDomingo_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks(); 
    }

     private void validarChecks()
    {
        //rblLunes.Visible = ChkLunes.Checked == true ? true : false;  

        if (ChkLunes.Checked == false)
        {
            rblLunes.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblLunes.Items[1].Selected = false;
            rblLunes.Items[2].Selected = false;
            rblLunes.Enabled = false;
        }
        else
        {   rblLunes.Enabled = true;
            rblLunes.Items[0].Selected = true;
        }


        if (ChkMartes.Checked == false)
        {
            rblMartes.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblMartes.Items[1].Selected = false;
            rblMartes.Items[2].Selected = false;
            rblMartes.Enabled = false;
        }
        else
        { rblMartes.Enabled = true;
        rblMartes.Items[0].Selected = true;
        }


        if (ChkMiercoles.Checked == false)
        {
            rblMiercoles.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblMiercoles.Items[1].Selected = false;
            rblMiercoles.Items[2].Selected = false;
            rblMiercoles.Enabled = false;
        }
        else
        { rblMiercoles.Enabled = true;
        rblMiercoles.Items[0].Selected = true;
        }


        if (ChkJueves.Checked == false)
        {
            rblJueves.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblJueves.Items[1].Selected = false;
            rblJueves.Items[2].Selected = false;
            rblJueves.Enabled = false;
        }
        else
        { rblJueves.Enabled = true;
        rblJueves.Items[0].Selected = true;
        }


        if (ChkViernes.Checked == false)
        {
            rblViernes.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblViernes.Items[1].Selected = false;
            rblViernes.Items[2].Selected = false;
            rblViernes.Enabled = false;
        }
        else
        { rblViernes.Enabled = true;
        rblViernes.Items[0].Selected = true;
        }


        if (ChkSabado.Checked == false)
        {
            rblSabado.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblSabado.Items[1].Selected = false;
            rblSabado.Items[2].Selected = false;
            rblSabado.Enabled = false;
        }
        else
        { rblSabado.Enabled = true;
        rblSabado.Items[0].Selected = true;
        }
        
         
        if (ChkDomingo.Checked == false)
        {
            rblDomingo.Items[0].Selected = false;// ChkLunes.Checked == true ? true : false;  
            rblDomingo.Items[1].Selected = false;
            rblDomingo.Items[2].Selected = false;
            rblDomingo.Enabled = false;
        }
        else
        { rblDomingo.Enabled = true;
        rblDomingo.Items[0].Selected = true;
        }
     }
     protected void txtVentasContado_TextChanged(object sender, EventArgs e)
     {

     }
}