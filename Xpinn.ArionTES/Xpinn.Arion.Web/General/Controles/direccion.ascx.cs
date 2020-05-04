using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Direccion : System.Web.UI.UserControl
{
    String[] direccion = new String[17];

    protected void Page_Load(object sender, EventArgs e)
    {
        direccion[0] = ddlVia.SelectedValue.ToString();
        direccion[1] = txtNombreVia.Text.ToString();
        direccion[2] = ddlLetra.SelectedValue.ToString();
        direccion[3] = ddlBis.SelectedValue.ToString();
        direccion[4] = ddlLetra2.SelectedValue.ToString();
        direccion[5] = ddlSentido.SelectedValue.ToString();
        direccion[6] = txtNumero.Text;
        direccion[7] = ddlLetra3.SelectedValue.ToString();
        direccion[8] = ddlBis2.SelectedValue.ToString();
        direccion[9] = ddlLetra4.SelectedValue.ToString();
        direccion[10] = txtPlaca.Text;
        direccion[11] = ddlSentido2.SelectedValue.ToString();
        direccion[12] = ddlManzana.SelectedValue.ToString();
        direccion[13] = txtNombreManzana.Text;
        direccion[14] = ddlUnidad.SelectedValue.ToString();
        direccion[15] = txtNombreUnidad.Text;
        direccion[16] = txtComplemento.Text;
        

        if (!IsPostBack)
        {
            ViewState["direccion"] = direccion;

            if (!TipoZona.Equals("R") && !TipoZona.Equals("U"))
            {
                ddlVia.Visible = false;
                ddlManzana.Visible = false;
                pVia.Visible = false;
                pManzana.Visible = false;
                //txtDireccion.Visible = false;
                txtComplemento.Visible = false;
                btnLimpiarDireccion.Visible = false;
                ddlLetra2.Enabled = false;
                ddlLetra4.Enabled = false;
            }
        }

    }

    protected void rbtnDetalleZonaGeo_SelectedIndexChanged(object sender, EventArgs e)
    { 
        //   try
        //  {
        if (rbtnDetalleZonaGeo.SelectedValue == "R")
        {
            LimpiarDirecciones();
            ddlVia.Visible = false;
            ddlManzana.Visible = false;
            ActivarZonaRural();
        }
        else if (rbtnDetalleZonaGeo.SelectedValue == "U")
        {
            ddlVia.Visible = false;
            ddlManzana.Visible = false;
            txtComplemento.Visible = false;
            LimpiarDirecciones();
            ActivarZonaUrbana();
        }
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }
    }

    //protected void DesactivarLetra2(object sender, EventArgs e)
    protected void DesactivarLetra2()
    {
        if (ddlBis.SelectedValue != "BIS")
        {
            ddlLetra2.ClearSelection();
            ddlLetra2.Enabled = false;
            direccion[4] = "";
            ddlLetra2.Visible = false;
            actualizarComplementoVia();
        }
        else
        {
            ddlLetra2.Enabled = true;
            ddlLetra2.Visible = true;
            actualizarComplementoVia();
        }
    }

    //protected void DesactivarLetra4(object sender, EventArgs e)
    protected void DesactivarLetra4()
{
        if (ddlBis2.SelectedValue != "BIS")
        {
            ddlLetra4.ClearSelection();
            ddlLetra4.Enabled = false;
            ddlLetra4.Visible = false;
            direccion[9] = "";
            actualizarComplementoVia();
        }
        else
        {
            ddlLetra4.Enabled = true;
            ddlLetra4.Visible = true;
            actualizarComplementoVia();
        }
    }

    protected void ActivarZonaRural()
    {
        //  try
        //  {
        txtDireccion.Visible = true;
        txtDireccion.Enabled = true;
        btnLimpiarDireccion.Visible = true;
        txtComplemento.Visible = false;
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }
    }

    protected void ActivarZonaUrbana()
    {
        //  try
        //  {
        //txtDireccion.Visible = false;
        ddlVia.Visible = true;
        ddlManzana.Visible = true;
        txtDireccion.Enabled = false;
        btnLimpiarDireccion.Visible = false;
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }

    }

    protected void ddlVia_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  try
        //  {
        txtDireccion.Visible = true;
        btnLimpiarDireccion.Visible = true;
        DesactivarManzana();
        ActivarVia();
        actualizarComplementoVia();

        if (ddlVia.SelectedValue == "CL" || ddlVia.SelectedValue == "DG")
        {
            CargarSentidoCllDg();
        }
        else if (ddlVia.SelectedValue == "KR" || ddlVia.SelectedValue == "TV")
        {
            CargarSentidoKrTv();
        }
        else
            CargarSentido();

        ddlVia.Focus();
       
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }
    }

    protected void ActivarVia()
    {
        //  try
        //  {
        ddlVia.Visible = true;
        ddlSentido.Items.Clear();
        ddlSentido2.Items.Clear();
        pVia.Visible = true;
        txtComplemento.Visible = true;
        rfvTxtNombreVia.Enabled = true;
        rfvTxtNumero.Enabled = true;
        rfvTxtPlaca.Enabled = true;
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }

    }

    protected void DesactivarVia()
    {
        //  try
        //  {
        ddlVia.Visible = false;
        pVia.Visible = false;
        rfvTxtNombreVia.Enabled = false;
        rfvTxtNumero.Enabled = false;
        rfvTxtPlaca.Enabled = false;
        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }

    }

    protected void ddlManzana_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        // {
        txtDireccion.Visible = true;
        btnLimpiarDireccion.Visible = true;
        actualizarComplementoVia();
        DesactivarVia();
        ActivarManzana();
        //}
        // catch (Exception ex)
        // {
        //    sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void ActivarManzana()
    {
        //  try
        //  {
        ddlManzana.Visible = true;
        pManzana.Visible = true;
        txtComplemento.Visible = true;
        //rfvDdlUnidad.Enabled = true;
        rfvTxtNombreManzana.Enabled = true;
        rfvTxtNombreUnidad.Enabled = true;

        //  }
        //  catch (Exception ex)
        //  {
        //      sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }

    }

    protected void DesactivarManzana()
    {
        // try
        // {
        ddlManzana.Visible = false;
        pManzana.Visible = false;
        rfvDdlUnidad.Enabled = false;
        rfvTxtNombreManzana.Enabled = false;
        rfvTxtNombreUnidad.Enabled = false;
        //  }
        //  catch (Exception ex)
        // {
        //    sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        //  }

    }

    protected void LimpiarDirecciones()
    {
        // try
        //  {
        pManzana.Visible = false;
        pVia.Visible = false;
        ddlSentido.Items.Clear();
        ddlSentido2.Items.Clear();
        ddlVia.ClearSelection();
        ddlManzana.ClearSelection();
        ddlLetra.ClearSelection();
        ddlLetra2.ClearSelection();
        ddlLetra3.ClearSelection();
        ddlLetra4.ClearSelection();
        ddlBis.ClearSelection();
        ddlBis2.ClearSelection();
        ddlUnidad.ClearSelection();
        if (txtDireccion.Text.Split(' ').Length >= 4)
            txtDireccion.Text = "";
        txtNombreManzana.Text = "";
        txtNombreUnidad.Text = "";
        txtNombreVia.Text = "";
        txtNumero.Text = "";
        txtPlaca.Text = "";
        txtComplemento.Text = "";


        // }
        // catch (Exception ex)
        // {
        //     sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void CargarSentidoCllDg()
    {
        //try
        // {
        ddlSentido.Items.Add("");
        ddlSentido.Items.Add("NORTE");
        ddlSentido.Items.Add("SUR");

        ddlSentido2.Items.Add("");
        ddlSentido2.Items.Add("ESTE");
        ddlSentido2.Items.Add("OESTE");
        //}
        // catch (Exception ex)
        // {
        //    sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void CargarSentidoKrTv()
    {
        //try
        // {
        ddlSentido.Items.Add("");
        ddlSentido.Items.Add("ESTE");
        ddlSentido.Items.Add("OESTE");

        ddlSentido2.Items.Add("");
        ddlSentido2.Items.Add("NORTE");
        ddlSentido2.Items.Add("SUR");
        //}
        // catch (Exception ex)
        // {
        //    sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void CargarSentido()
    {
        //try
        // {
        ddlSentido.Items.Add("");
        ddlSentido.Items.Add("NORTE");
        ddlSentido.Items.Add("SUR");
        ddlSentido.Items.Add("ESTE");
        ddlSentido.Items.Add("OESTE");

        ddlSentido2.Items.Add("");
        ddlSentido2.Items.Add("NORTE");
        ddlSentido2.Items.Add("SUR");
        ddlSentido2.Items.Add("ESTE");
        ddlSentido2.Items.Add("OESTE");


        //}
        // catch (Exception ex)
        // {
        //    sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void actualizarComplementoVia()
    {
        // try
        //  {
        txtDireccion.Text = "";
        for (int i = 0; i < direccion.Length; i++)
        {
            if ((direccion[i].Trim()).Length > 0)
                txtDireccion.Text = txtDireccion.Text + direccion[i].ToUpper() + " ";
        }

        // }
        // catch (Exception ex)
        // {
        //     sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    protected void Obj_LostFocus(object sender, System.EventArgs e)
    {
        // try
        //  {
        actualizarComplementoVia();
        // }
        // catch (Exception ex)
        // {
        //     sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }

    }

    protected void btnLimpiarDireccion_Click(object sender, EventArgs e)
    {
        // try
        //  {
        ddlSentido.Items.Clear();
        ddlSentido2.Items.Clear();
        ddlVia.ClearSelection();
        ddlManzana.ClearSelection();
        ddlLetra.ClearSelection();
        ddlLetra2.ClearSelection();
        ddlLetra3.ClearSelection();
        ddlLetra4.ClearSelection();
        ddlBis.ClearSelection();
        ddlBis2.ClearSelection();
        ddlUnidad.ClearSelection();
        txtDireccion.Text = "";
        txtNombreManzana.Text = "";
        txtNombreUnidad.Text = "";
        txtNombreVia.Text = "";
        txtNumero.Text = "";
        txtPlaca.Text = "";
        txtComplemento.Text = "";
        rfvDdlUnidad.Enabled = false;
        rfvTxtNombreManzana.Enabled = false;
        rfvTxtNombreUnidad.Enabled = false;
        rfvTxtNombreVia.Enabled = false;
        rfvTxtNumero.Enabled = false;
        rfvTxtPlaca.Enabled = false;
        if (rbtnDetalleZonaGeo.SelectedValue == "R")
        {
            LimpiarDirecciones();
            ddlVia.Visible = false;
            ddlManzana.Visible = false;
            ActivarZonaRural();
        }
        else
        {
            ddlVia.Visible = false;
            ddlManzana.Visible = false;
            txtComplemento.Visible = false;
            LimpiarDirecciones();
            ActivarZonaUrbana();
        }

        // }
        // catch (Exception ex)
        // {
        //     sevenExceptionBO.Throw(this.Programa + "A", "rbtnDetalleZona_SelectedIndexChanged", ex);
        // }
    }

    public string Text
    {
        get
        {
            return txtDireccion.Text.Trim().ToUpper();
        }

        set
        {
            try
            {
                LimpiarDirecciones();
                txtDireccion.Text = value;
                String[] eDireccion = value.Trim().Split(' ');
                int total = eDireccion.Length;
                ddlLetra2.Visible = false;
                ddlLetra4.Visible = false;
                if (total >= 4)
                {
                    if (ddlVia.Items.FindByValue(eDireccion[0]) != null)
                    {
                        rbtnDetalleZonaGeo.SelectedValue = "U";
                        ActivarVia();
                        DesactivarManzana();
                        if (ddlVia.Items.FindByValue(eDireccion[0]) != null)
                            ddlVia.SelectedValue = eDireccion[0];
                        if (ddlVia.SelectedValue == "CL" || ddlVia.SelectedValue == "DG")
                        {
                            CargarSentidoCllDg();
                        }
                        else if (ddlVia.SelectedValue == "KR" || ddlVia.SelectedValue == "TV")
                        {
                            CargarSentidoKrTv();
                        }
                        else
                            CargarSentido();
                        txtDireccion.Text = value;
                        txtDireccion.Visible = true;
                        btnLimpiarDireccion.Visible = true;
                        txtNombreVia.Text = eDireccion[1];
                        int controlvia = 1;
                        int control=2;
                        int validador = 0;
                        while(controlvia != 0)
                        {
                            if ((eDireccion[control].Length >= 3 && eDireccion[control].ToUpper() != "BIS" && (!Int32.TryParse(eDireccion[control], out validador))) || (eDireccion[control].ToUpper() == "LA" || eDireccion[control].ToUpper() == "DE" || eDireccion[control].ToUpper() == "LOS" || eDireccion[control].ToUpper() == "LAS" || eDireccion[control].ToUpper() == "DEL"))
                            {
                                txtNombreVia.Text = txtNombreVia.Text + " " + eDireccion[control];
                                control++;
                            }
                            else
                                controlvia = 0;
                        }
                        validador = 0;
                        while (txtNumero.Text.Trim().Length < 1 && control < total)
                        {
                            if (txtNumero.Text.Trim().Length < 1 && control<total)
                            {
                                if (!Int32.TryParse(eDireccion[control], out validador))
                                {
                                    if ((eDireccion[control].Trim()).Length < 3 && eDireccion[control] != "" && ddlLetra.SelectedIndex == 0 && ddlBis.SelectedValue!="BIS")
                                    {
                                        if (ddlLetra.Items.FindByValue(eDireccion[control]) != null)
                                            ddlLetra.SelectedValue = eDireccion[control];
                                        eDireccion[control] = "";
                                    }
                                    else if (eDireccion[control].Trim() == "BIS" && ddlBis.SelectedIndex == 0)
                                    {
                                        if (ddlBis.Items.FindByValue(eDireccion[control]) != null)
                                            ddlBis.SelectedValue = eDireccion[control];
                                        eDireccion[control] = "";
                                    }
                                    else if ((eDireccion[control].Trim()).Length < 3 && eDireccion[control] != "" && ddlLetra2.SelectedIndex == 0)
                                    {
                                        if (ddlLetra2.Items.FindByValue(eDireccion[control]) != null)
                                        {
                                            ddlLetra2.SelectedValue = eDireccion[control];
                                            ddlLetra2.Visible = true;
                                        }
                                        eDireccion[control] = "";
                                    }
                                    else if ((eDireccion[control] == "NORTE" || eDireccion[control] == "SUR" || eDireccion[control] == "ESTE" || eDireccion[control] == "OESTE") && ddlSentido.SelectedIndex == 0)
                                    {
                                        if (ddlSentido.Items.FindByValue(eDireccion[control]) != null)
                                            ddlSentido.SelectedValue = eDireccion[control];
                                        eDireccion[control] = "";
                                    }
                                }
                                else
                                {
                                    txtNumero.Text = eDireccion[control];
                                    eDireccion[control] = "";
                                }
                                control++;
                            }
                        }

                        while (txtPlaca.Text.Trim().Length < 1 && control < total)
                        {
                            if (txtPlaca.Text.Trim().Length < 1 && control<total)
                            {
                                if (!Int32.TryParse(eDireccion[control], out validador))
                                {
                                    if ((eDireccion[control].Trim()).Length < 3 && eDireccion[control] != "" && ddlLetra3.SelectedIndex == 0 && ddlBis2.SelectedValue!="BIS")
                                    {
                                        if (ddlLetra3.Items.FindByValue(eDireccion[control]) != null)
                                            ddlLetra3.SelectedValue = eDireccion[control];
                                        eDireccion[control] = "";
                                    }
                                    else if (eDireccion[control].Trim() == "BIS" && ddlBis2.SelectedIndex == 0)
                                    {
                                        if (ddlBis2.Items.FindByValue(eDireccion[control]) != null)
                                            ddlBis2.SelectedValue = eDireccion[control];
                                        eDireccion[control] = "";
                                    }
                                    else if ((eDireccion[control].Trim()).Length < 3 && eDireccion[control] != "" && ddlLetra4.SelectedIndex == 0)
                                    {
                                        if (ddlLetra4.Items.FindByValue(eDireccion[control]) != null)
                                        {
                                            ddlLetra4.SelectedValue = eDireccion[control];
                                            ddlLetra4.Visible = true;
                                        }
                                else
                                        eDireccion[control] = "";
                                    }
                                }
                                {
                                    txtPlaca.Text = eDireccion[control];
                                    eDireccion[control] = "";
                                }
                            }
                            control++;
                        }

                        while (control < total)
                        {
                            if ((eDireccion[control] == "NORTE" || eDireccion[control] == "SUR" || eDireccion[control] == "ESTE" || eDireccion[control] == "OESTE") && ddlSentido2.SelectedIndex == 0)
                            {
                                if (ddlSentido2.Items.FindByValue(eDireccion[control]) != null)
                                    ddlSentido2.SelectedValue = eDireccion[control];
                                eDireccion[control] = "";
                            }
                            else
                            {
                                txtComplemento.Text = txtComplemento.Text + " " + eDireccion[control];
                                eDireccion[control] = "";
                            }
                            control++;
                        }
                    }


                    if (ddlManzana.Items.FindByValue(eDireccion[0]) != null)
                    {
                        rbtnDetalleZonaGeo.SelectedValue = "U";
                        ActivarManzana();
                        DesactivarVia();
                        if (ddlManzana.Items.FindByValue(eDireccion[0]) != null)
                            ddlManzana.SelectedValue=eDireccion[0];
                        txtDireccion.Text = value;
                        txtDireccion.Visible = true;
                        btnLimpiarDireccion.Visible = true;
                        txtNombreManzana.Text = eDireccion[1];
                        int control = 2;
                        int controlvia = 1;
                        while (controlvia != 0)
                        {
                            if ((eDireccion[control].ToUpper() != "AL" && eDireccion[control].ToUpper() != "AP" && eDireccion[control].ToUpper() != "BG" && eDireccion[control].ToUpper() != "CS" && eDireccion[control].ToUpper() != "CN" && eDireccion[control].ToUpper() != "DP" && eDireccion[control].ToUpper() != "DS" && eDireccion[control].ToUpper() != "GA" && eDireccion[control].ToUpper() != "GS" && eDireccion[control].ToUpper() != "LC" && eDireccion[control].ToUpper() != "LM" && eDireccion[control].ToUpper() != "LT" && eDireccion[control].ToUpper() != "MN" && eDireccion[control].ToUpper() != "OF" && eDireccion[control].ToUpper() != "PA" && eDireccion[control].ToUpper() != "PN" && eDireccion[control].ToUpper() != "PL" && eDireccion[control].ToUpper() != "PD" && eDireccion[control].ToUpper() != "SS" && eDireccion[control].ToUpper() != "SO" && eDireccion[control].ToUpper() != "ST" && eDireccion[control].ToUpper() != "TZ" && eDireccion[control].ToUpper() != "UN" && eDireccion[control].ToUpper() != "UL"))
                            {
                                txtNombreManzana.Text = txtNombreManzana.Text + " " + eDireccion[control];
                                control++;
                            }
                            else
                                controlvia = 0;
                        }
                        ddlUnidad.SelectedValue=eDireccion[control];
                        control++;
                        txtNombreUnidad.Text = eDireccion[control];
                        control++;
                        if (total > 4)
                        {
                            while (control < total)
                            {
                                txtComplemento.Text = txtComplemento.Text + " " + eDireccion[control];
                                control++;
                            }
                        }
                    }
                }
                else
                    txtDireccion.Text = value;
                    txtDireccion.Visible = true;
                    btnLimpiarDireccion.Visible = true;
            }
            catch (Exception e)
            {
                throw new Exception("Set Direccion:" + e.Message);
            }
        }
    }

    public Boolean Requerido 
    {
        set 
        {
            rfvDdlUnidad.Enabled = value;
            rfvTxtDireccion.Enabled = value;
            rfvTxtNombreManzana.Enabled = value;
            rfvTxtNombreUnidad.Enabled = value;
            rfvTxtNombreVia.Enabled = value;
            rfvTxtNumero.Enabled = value;
            rfvTxtPlaca.Enabled = value;
        } 
    
    }

    public Boolean Zona
    {
        set
        {
            rbtnDetalleZonaGeo.Enabled = value;
            //rfvZonaGeo.Enabled = value;
            rbtnDetalleZonaGeo.Visible = value;
        }

    }

    public String TipoZona
    {
        get 
        {
            return rbtnDetalleZonaGeo.SelectedValue;
        }
        
        set 
        {
            rbtnDetalleZonaGeo.SelectedValue = value;
            if (rbtnDetalleZonaGeo.SelectedValue == "R")
            {
                LimpiarDirecciones();
                ddlVia.Visible = false;
                ddlManzana.Visible = false;
                ActivarZonaRural();
                
            }
            else if (rbtnDetalleZonaGeo.SelectedValue == "U")
            {
                ddlVia.Visible = false;
                ddlManzana.Visible = false;
                txtComplemento.Visible = false;
                LimpiarDirecciones();
                ActivarZonaUrbana();
            }
        }
        
    
    }

    public Boolean ReadOnly
    {
        set 
        {
                rbtnDetalleZonaGeo.Enabled = !value;
                ddlVia.Enabled = !value;
                ddlManzana.Enabled = !value;
                txtNombreVia.ReadOnly = value;
                ddlLetra.Enabled = !value;
                ddlBis.Enabled = !value;
                ddlLetra2.Enabled = !value;
                if (ddlBis.SelectedValue=="BIS")
                    ddlLetra2.Visible = true;
                else
                    ddlLetra2.Visible = false;
                ddlSentido.Enabled = !value;
                txtNumero.ReadOnly = value;
                ddlLetra3.Enabled = !value;
                ddlBis2.Enabled = !value;
                ddlSentido2.Enabled = !value;
                ddlLetra4.Enabled = !value;
                if(ddlBis2.SelectedValue=="BIS")
                    ddlLetra4.Visible = true;
                else
                    ddlLetra4.Visible = false;
                txtPlaca.ReadOnly = value;
                ddlSentido2.Enabled = !value;
                txtNombreManzana.ReadOnly = value;
                ddlUnidad.Enabled = !value;
                txtNombreUnidad.ReadOnly = value;
                txtComplemento.ReadOnly = value;
                txtDireccion.ReadOnly = value;
                btnLimpiarDireccion.Visible = !value;
         }
    }

    public Boolean Enabled
    {
        set
        {
            rbtnDetalleZonaGeo.Enabled = value;
            ddlVia.Enabled = value;
            ddlManzana.Enabled = value;
            txtNombreVia.ReadOnly = !value;
            ddlLetra.Enabled = value;
            ddlBis.Enabled = value;
            ddlLetra2.Enabled = value;
            if (ddlBis.SelectedValue == "BIS")
                ddlLetra2.Visible = true;
            else
                ddlLetra2.Visible = false;
            ddlSentido.Enabled = value;
            txtNumero.ReadOnly = !value;
            ddlLetra3.Enabled = value;
            ddlBis2.Enabled = value;
            ddlSentido2.Enabled = value;
            ddlLetra4.Enabled = value;
            if (ddlBis2.SelectedValue == "BIS")
                ddlLetra4.Visible = true;
            else
                ddlLetra4.Visible = false;
            txtPlaca.ReadOnly = !value;
            ddlSentido2.Enabled = value;
            txtNombreManzana.ReadOnly = !value;
            ddlUnidad.Enabled = value;
            txtNombreUnidad.ReadOnly = !value;
            txtComplemento.ReadOnly = !value;
            txtDireccion.ReadOnly = !value;
            btnLimpiarDireccion.Visible = value;
        }
    }
    protected void txtNombreVia_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra.Focus();

    }
    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra.Focus();
    }
    protected void ddlBis_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DesactivarLetra2();
        ddlBis.Focus();
    }
    protected void ddlLetra2_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra2.Focus();
    }
    protected void ddlSentido_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlSentido.Focus();
    }
    protected void txtNumero_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra3.Focus();
    }
    protected void ddlLetra3_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra3.Focus();
    }
    protected void ddlBis2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DesactivarLetra4();
        ddlBis2.Focus();
    }
    protected void ddlLetra4_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlLetra4.Focus();
    }
    protected void txtPlaca_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlSentido2.Focus();
    }
    protected void ddlUnidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlUnidad.Focus();
    }
    protected void txtNombreUnidad_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        txtComplemento.Focus();
    }
    protected void txtComplemento_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        btnLimpiarDireccion.Focus();
    }
    protected void ddlSentido2_SelectedIndexChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        ddlSentido2.Focus();
    }
    protected void txtNombreManzana_TextChanged(object sender, EventArgs e)
    {
        actualizarComplementoVia();
        txtNombreManzana.Focus();
    }
}