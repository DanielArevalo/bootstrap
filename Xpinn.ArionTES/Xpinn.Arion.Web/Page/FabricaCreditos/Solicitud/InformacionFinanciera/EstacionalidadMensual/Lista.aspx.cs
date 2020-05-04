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
    private Xpinn.FabricaCreditos.Services.EstacionalidadMensualService EstacionalidadMensualServicio = new Xpinn.FabricaCreditos.Services.EstacionalidadMensualService();
    int contFilas = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstacionalidadMensualServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstacionalidadMensualServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, EstacionalidadMensualServicio.CodigoPrograma);
                if (Session[EstacionalidadMensualServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstacionalidadMensualServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, EstacionalidadMensualServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, EstacionalidadMensualServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, EstacionalidadMensualServicio.CodigoPrograma);
    }


    List<Xpinn.FabricaCreditos.Entities.EstacionalidadMensual> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.EstacionalidadMensual>();
    Xpinn.FabricaCreditos.Entities.EstacionalidadMensual entTotales = new Xpinn.FabricaCreditos.Entities.EstacionalidadMensual(); // Entidad donde se devuelven los calculos


    private void Actualizar()
    {
        try
        {
            lstConsulta = EstacionalidadMensualServicio.ListarEstacionalidadMensual(ObtenerValores(), (Usuario)Session["usuario"]);
            
            if (lstConsulta.Count >= 4)
            {
                //Pasa lista a bussines para hacer calculos
                entTotales = EstacionalidadMensualServicio.CalculosEstacionalidadMensual(lstConsulta.GetRange(0, 3));
                txtPromedioMes.Text = entTotales.promedioMensual.ToString();

                contFilas = 0;

                while (contFilas <= 3)
                {
                    switch (lstConsulta[contFilas].tipoventas)
                    {
                        case "1":       //Ventas Buenas

                            rblEne.Items[0].Selected = lstConsulta[contFilas].enero == 1 ? true : false;
                            rblFeb.Items[0].Selected = lstConsulta[contFilas].febrero == 1 ? true : false;
                            rblMar.Items[0].Selected = lstConsulta[contFilas].marzo == 1 ? true : false;
                            rblAbr.Items[0].Selected = lstConsulta[contFilas].abril == 1 ? true : false;
                            rblMay.Items[0].Selected = lstConsulta[contFilas].mayo == 1 ? true : false;
                            rblJun.Items[0].Selected = lstConsulta[contFilas].junio == 1 ? true : false;
                            rblJul.Items[0].Selected = lstConsulta[contFilas].julio == 1 ? true : false;
                            rblAgo.Items[0].Selected = lstConsulta[contFilas].agosto == 1 ? true : false;
                            rblSep.Items[0].Selected = lstConsulta[contFilas].septiembre == 1 ? true : false;
                            rblOct.Items[0].Selected = lstConsulta[contFilas].octubre == 1 ? true : false;
                            rblNov.Items[0].Selected = lstConsulta[contFilas].noviembre == 1 ? true : false;
                            rblDic.Items[0].Selected = lstConsulta[contFilas].diciembre == 1 ? true : false;
                            txtValorB.Text = lstConsulta[contFilas].valor.ToString();
                            contFilas++;
                            break;
                        case "2":       //Ventas Regulares


                            rblEne.Items[1].Selected = lstConsulta[contFilas].enero == 1 ? true : false;
                            rblFeb.Items[1].Selected = lstConsulta[contFilas].febrero == 1 ? true : false;
                            rblMar.Items[1].Selected = lstConsulta[contFilas].marzo == 1 ? true : false;
                            rblAbr.Items[1].Selected = lstConsulta[contFilas].abril == 1 ? true : false;
                            rblMay.Items[1].Selected = lstConsulta[contFilas].mayo == 1 ? true : false;
                            rblJun.Items[1].Selected = lstConsulta[contFilas].junio == 1 ? true : false;
                            rblJul.Items[1].Selected = lstConsulta[contFilas].julio == 1 ? true : false;
                            rblAgo.Items[1].Selected = lstConsulta[contFilas].agosto == 1 ? true : false;
                            rblSep.Items[1].Selected = lstConsulta[contFilas].septiembre == 1 ? true : false;
                            rblOct.Items[1].Selected = lstConsulta[contFilas].octubre == 1 ? true : false;
                            rblNov.Items[1].Selected = lstConsulta[contFilas].noviembre == 1 ? true : false;
                            rblDic.Items[1].Selected = lstConsulta[contFilas].diciembre == 1 ? true : false;
                            txtValorR.Text = lstConsulta[contFilas].valor.ToString();
                            contFilas++;
                            break;
                        case "3":       //Ventas Malas

                            rblEne.Items[2].Selected = lstConsulta[contFilas].enero == 1 ? true : false;
                            rblFeb.Items[2].Selected = lstConsulta[contFilas].febrero == 1 ? true : false;
                            rblMar.Items[2].Selected = lstConsulta[contFilas].marzo == 1 ? true : false;
                            rblAbr.Items[2].Selected = lstConsulta[contFilas].abril == 1 ? true : false;
                            rblMay.Items[2].Selected = lstConsulta[contFilas].mayo == 1 ? true : false;
                            rblJun.Items[2].Selected = lstConsulta[contFilas].junio == 1 ? true : false;
                            rblJul.Items[2].Selected = lstConsulta[contFilas].julio == 1 ? true : false;
                            rblAgo.Items[2].Selected = lstConsulta[contFilas].agosto == 1 ? true : false;
                            rblSep.Items[2].Selected = lstConsulta[contFilas].septiembre == 1 ? true : false;
                            rblOct.Items[2].Selected = lstConsulta[contFilas].octubre == 1 ? true : false;
                            rblNov.Items[2].Selected = lstConsulta[contFilas].noviembre == 1 ? true : false;
                            rblDic.Items[2].Selected = lstConsulta[contFilas].diciembre == 1 ? true : false;
                            txtValorM.Text = lstConsulta[contFilas].valor.ToString();
                            contFilas++;
                            break;

                        case "4":       //Checks para incluir o excluir días
                            ChkEne.Checked = lstConsulta[contFilas].enero == 1 ? true : false;
                            ChkFeb.Checked = lstConsulta[contFilas].febrero == 1 ? true : false;
                            ChkMar.Checked = lstConsulta[contFilas].marzo == 1 ? true : false;
                            ChkAbr.Checked = lstConsulta[contFilas].abril == 1 ? true : false;
                            ChkMay.Checked = lstConsulta[contFilas].mayo == 1 ? true : false;
                            ChkJun.Checked = lstConsulta[contFilas].junio == 1 ? true : false;
                            ChkJul.Checked = lstConsulta[contFilas].julio == 1 ? true : false;
                            ChkAgo.Checked = lstConsulta[contFilas].agosto == 1 ? true : false;
                            ChkSep.Checked = lstConsulta[contFilas].septiembre == 1 ? true : false;
                            ChkOct.Checked = lstConsulta[contFilas].octubre == 1 ? true : false;
                            ChkNov.Checked = lstConsulta[contFilas].noviembre == 1 ? true : false;
                            ChkDic.Checked = lstConsulta[contFilas].diciembre == 1 ? true : false;
                            contFilas++;
                            break;
                    }
                }

                contFilas = 0;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();         
            }

            Session.Add(EstacionalidadMensualServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstacionalidadMensualServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

   

    /// <summary>
    /// Mètodo para cargar los valores a la entidad para poder grabar los datos
    /// </summary>
    /// <returns></returns>
    private Xpinn.FabricaCreditos.Entities.EstacionalidadMensual ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.EstacionalidadMensual vEstacionalidadMensual = new Xpinn.FabricaCreditos.Entities.EstacionalidadMensual();

        if (txtTipoventas.Text.Trim() != "")
            vEstacionalidadMensual.tipoventas = Convert.ToString(txtTipoventas.Text.Trim());
        if (txtValor.Text.Trim() != "")
            vEstacionalidadMensual.valor = ConvertirToInt64(txtValor.Text.Trim());
        if (txtEnero.Text.Trim() != "")
            vEstacionalidadMensual.enero = ConvertirToInt64(txtEnero.Text.Trim());
        if (txtFebrero.Text.Trim() != "")
            vEstacionalidadMensual.febrero = ConvertirToInt64(txtFebrero.Text.Trim());
        if (txtMarzo.Text.Trim() != "")
            vEstacionalidadMensual.marzo = ConvertirToInt64(txtMarzo.Text.Trim());
        if (txtAbril.Text.Trim() != "")
            vEstacionalidadMensual.abril = ConvertirToInt64(txtAbril.Text.Trim());
        if (txtMayo.Text.Trim() != "")
            vEstacionalidadMensual.mayo = ConvertirToInt64(txtMayo.Text.Trim());
        if (txtJunio.Text.Trim() != "")
            vEstacionalidadMensual.junio = ConvertirToInt64(txtJunio.Text.Trim());
        if (txtJulio.Text.Trim() != "")
            vEstacionalidadMensual.julio = ConvertirToInt64(txtJulio.Text.Trim());
        if (txtAgosto.Text.Trim() != "")
            vEstacionalidadMensual.agosto = ConvertirToInt64(txtAgosto.Text.Trim());
        if (txtSeptiembre.Text.Trim() != "")
            vEstacionalidadMensual.septiembre = ConvertirToInt64(txtSeptiembre.Text.Trim());
        if (txtOctubre.Text.Trim() != "")
            vEstacionalidadMensual.octubre = ConvertirToInt64(txtOctubre.Text.Trim());
        if (txtNoviembre.Text.Trim() != "")
            vEstacionalidadMensual.noviembre = ConvertirToInt64(txtNoviembre.Text.Trim());
        if (txtDiciembre.Text.Trim() != "")
            vEstacionalidadMensual.diciembre = ConvertirToInt64(txtDiciembre.Text.Trim());
        if (txtTotal.Text.Trim() != "")
            vEstacionalidadMensual.total = ConvertirToInt64(txtTotal.Text.Trim());

        if (Session["Cod_persona"] != null)
            vEstacionalidadMensual.codpersona = Convert.ToInt64(Session["Cod_persona"].ToString());
        else
            return null;

        return vEstacionalidadMensual;
    }

    /// <summary>
    /// Mètodo para convertir el dato de los textbox a numèricos
    /// </summary>
    /// <param name="sValor"></param>
    /// <returns></returns>
    private Int64 ConvertirToInt64(String sValor)
    {
        try
        {
            return Convert.ToInt64(sValor);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Mètodo para guardar la estacionalidad mensual teniendo en cuenta si se actualiza o modifica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lstConsulta = EstacionalidadMensualServicio.ListarEstacionalidadMensual(ObtenerValores(), (Usuario)Session["usuario"]);

        try
        {
            Xpinn.FabricaCreditos.Entities.EstacionalidadMensual vEstacionalidadMensual = new Xpinn.FabricaCreditos.Entities.EstacionalidadMensual();
            vEstacionalidadMensual.codpersona = Convert.ToInt64(Session["Cod_persona"].ToString());

            vEstacionalidadMensual.tipoventas = "1";
            if (txtValorB.Text != "") vEstacionalidadMensual.valor = Convert.ToInt64(txtValorB.Text.Trim().Replace(@".", ""));
            vEstacionalidadMensual.enero = rblEne.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.febrero = rblFeb.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.marzo = rblMar.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.abril = rblAbr.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.mayo = rblMay.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.junio = rblJun.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.julio = rblJul.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.agosto = rblAgo.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.septiembre = rblSep.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.octubre = rblOct.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.noviembre = rblNov.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.diciembre = rblDic.SelectedValue == "B" ? 1 : 0;
            vEstacionalidadMensual.total = 0;
            vEstacionalidadMensual.cod_ventas = 1;
            if (lstConsulta.Count >= 4) //Verifica si ya existen registros para poder modificarlos           
                EstacionalidadMensualServicio.ModificarEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);                
            else
                vEstacionalidadMensual = EstacionalidadMensualServicio.CrearEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
                
            vEstacionalidadMensual.tipoventas = "2";
            if (txtValorR.Text != "") vEstacionalidadMensual.valor = Convert.ToInt64(txtValorR.Text.Trim().Replace(@".", ""));
            vEstacionalidadMensual.enero = rblEne.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.febrero = rblFeb.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.marzo = rblMar.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.abril = rblAbr.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.mayo = rblMay.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.junio = rblJun.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.julio = rblJul.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.agosto = rblAgo.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.septiembre = rblSep.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.octubre = rblOct.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.noviembre = rblNov.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.diciembre = rblDic.SelectedValue == "R" ? 1 : 0;
            vEstacionalidadMensual.total = 0;
            vEstacionalidadMensual.cod_ventas = 2;
            if (lstConsulta.Count >= 4) //Verifica si ya existen registros para poder modificarlos   
                EstacionalidadMensualServicio.ModificarEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
            else
                vEstacionalidadMensual = EstacionalidadMensualServicio.CrearEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
            

            vEstacionalidadMensual.tipoventas = "3";
            if (txtValorM.Text != "") vEstacionalidadMensual.valor = Convert.ToInt64(txtValorM.Text.Trim().Replace(@".", ""));
            vEstacionalidadMensual.enero = rblEne.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.febrero = rblFeb.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.marzo = rblMar.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.abril = rblAbr.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.mayo = rblMay.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.junio = rblJun.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.julio = rblJul.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.agosto = rblAgo.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.septiembre = rblSep.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.octubre = rblOct.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.noviembre = rblNov.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.diciembre = rblDic.SelectedValue == "M" ? 1 : 0;
            vEstacionalidadMensual.total = 0;
            vEstacionalidadMensual.cod_ventas = 3;
            if (lstConsulta.Count >= 4) //Verifica si ya existen registros para poder modificarlos 
                EstacionalidadMensualServicio.ModificarEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
            else
                vEstacionalidadMensual = EstacionalidadMensualServicio.CrearEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
            

            vEstacionalidadMensual.tipoventas = "4";
            if (txtValorM.Text != "") vEstacionalidadMensual.valor = Convert.ToInt64(txtValorM.Text.Trim().Replace(@".", ""));
            vEstacionalidadMensual.enero = ChkEne.Checked == true ? 1 : 0;
            vEstacionalidadMensual.febrero = ChkFeb.Checked == true ? 1 : 0;
            vEstacionalidadMensual.marzo = ChkMar.Checked == true ? 1 : 0;
            vEstacionalidadMensual.abril = ChkAbr.Checked == true ? 1 : 0;
            vEstacionalidadMensual.mayo = ChkMay.Checked == true ? 1 : 0;
            vEstacionalidadMensual.junio = ChkJun.Checked == true ? 1 : 0;
            vEstacionalidadMensual.julio = ChkJul.Checked == true ? 1 : 0;
            vEstacionalidadMensual.agosto = ChkAgo.Checked == true ? 1 : 0;
            vEstacionalidadMensual.septiembre = ChkSep.Checked == true ? 1 : 0;
            vEstacionalidadMensual.octubre = ChkOct.Checked == true ? 1 : 0;
            vEstacionalidadMensual.noviembre = ChkNov.Checked == true ? 1 : 0;
            vEstacionalidadMensual.diciembre = ChkDic.Checked == true ? 1 : 0;
            vEstacionalidadMensual.total = 0;
            vEstacionalidadMensual.cod_ventas = 4;
            if (lstConsulta.Count >= 4) //Verifica si ya existen registros para poder modificarlos 
                EstacionalidadMensualServicio.ModificarEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);
            else
                vEstacionalidadMensual = EstacionalidadMensualServicio.CrearEstacionalidadMensual(vEstacionalidadMensual, (Usuario)Session["usuario"]);

            // Se calcula el valor total
            Actualizar();

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstacionalidadMensualServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/MargenVentas/Lista.aspx");
    }

    protected void txtValorB_DataBinding(object sender, EventArgs e)
    {

    }

    protected void txtValorB_Disposed(object sender, EventArgs e)
    {

    }

    protected void txtValorB_Init(object sender, EventArgs e)
    {

    }
    protected void txtValorB_Load(object sender, EventArgs e)
    {

    }

    protected void txtValorB_PreRender(object sender, EventArgs e)
    {

    }

    protected void txtValorB_Unload(object sender, EventArgs e)
    {

    }

    protected void txtValorB_TextChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkEne_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkFeb_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkMar_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkAbr_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }
    protected void ChkMay_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkJun_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkJul_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkAgo_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkSep_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkOct_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkNov_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    protected void ChkDic_CheckedChanged(object sender, EventArgs e)
    {
        validarChecks();
    }

    private void validarChecks()
    {
        //rblLunes.Visible = ChkLunes.Checked == true ? true : false;  

        if (ChkEne.Checked == false)
        {
            rblEne.Items[0].Selected = false; 
            rblEne.Items[1].Selected = false;
            rblEne.Items[2].Selected = false;
            rblEne.Enabled = false;
        }
        else
        { rblEne.Enabled = true;
        rblEne.Items[0].Selected = true;
        }


        if (ChkFeb.Checked == false)
        {
            rblFeb.Items[0].Selected = false; 
            rblFeb.Items[1].Selected = false;
            rblFeb.Items[2].Selected = false;
            rblFeb.Enabled = false;
        }
        else
        { rblFeb.Enabled = true;
        rblFeb.Items[0].Selected = true;
        }


        if (ChkMar.Checked == false)
        {
            rblMar.Items[0].Selected = false; 
            rblMar.Items[1].Selected = false;
            rblMar.Items[2].Selected = false;
            rblMar.Enabled = false;
        }
        else
        { rblMar.Enabled = true;
        rblMar.Items[0].Selected = true;
        }


        if (ChkAbr.Checked == false)
        {
            rblAbr.Items[0].Selected = false; 
            rblAbr.Items[1].Selected = false;
            rblAbr.Items[2].Selected = false;
            rblAbr.Enabled = false;
        }
        else
        { rblAbr.Enabled = true;
        rblAbr.Items[0].Selected = true;
        }


        if (ChkMay.Checked == false)
        {
            rblMay.Items[0].Selected = false; 
            rblMay.Items[1].Selected = false;
            rblMay.Items[2].Selected = false;
            rblMay.Enabled = false;
        }
        else
        { rblMay.Enabled = true;
        rblMay.Items[0].Selected = true;
        }


        if (ChkJun.Checked == false)
        {
            rblJun.Items[0].Selected = false; 
            rblJun.Items[1].Selected = false;
            rblJun.Items[2].Selected = false;
            rblJun.Enabled = false;
        }
        else
        { rblJun.Enabled = true;
        rblJun.Items[0].Selected = true;
        }


        if (ChkJul.Checked == false)
        {
            rblJul.Items[0].Selected = false; 
            rblJul.Items[1].Selected = false;
            rblJul.Items[2].Selected = false;
            rblJul.Enabled = false;
        }
        else
        { rblJul.Enabled = true;
        rblJul.Items[0].Selected = true;
        }


        if (ChkAgo.Checked == false)
        {
            rblAgo.Items[0].Selected = false; 
            rblAgo.Items[1].Selected = false;
            rblAgo.Items[2].Selected = false;
            rblAgo.Enabled = false;
        }
        else
        { rblAgo.Enabled = true;
        rblAgo.Items[0].Selected = true;
        }


        if (ChkSep.Checked == false)
        {
            rblSep.Items[0].Selected = false; 
            rblSep.Items[1].Selected = false;
            rblSep.Items[2].Selected = false;
            rblSep.Enabled = false;
        }
        else
        { rblSep.Enabled = true;
        rblSep.Items[0].Selected = true;
        }


        if (ChkOct.Checked == false)
        {
            rblOct.Items[0].Selected = false; 
            rblOct.Items[1].Selected = false;
            rblOct.Items[2].Selected = false;
            rblOct.Enabled = false;
        }
        else
        { rblOct.Enabled = true;
        rblOct.Items[0].Selected = true;
        }


        if (ChkNov.Checked == false)
        {
            rblNov.Items[0].Selected = false; 
            rblNov.Items[1].Selected = false;
            rblNov.Items[2].Selected = false;
            rblNov.Enabled = false;
        }
        else
        { rblNov.Enabled = true;
        rblNov.Items[0].Selected = true;
        }


        if (ChkDic.Checked == false)
        {
            rblDic.Items[0].Selected = false; 
            rblDic.Items[1].Selected = false;
            rblDic.Items[2].Selected = false;
            rblDic.Enabled = false;
        }
        else
        { rblDic.Enabled = true;
        rblDic.Items[0].Selected = true;
        }
    }
}