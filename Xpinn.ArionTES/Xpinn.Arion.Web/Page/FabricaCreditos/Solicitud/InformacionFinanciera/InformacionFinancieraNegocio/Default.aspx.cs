using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Page_FabricaCreditos_Solicitud_InformacionFinanciera_InformacionFinancieraNegocio_Default : System.Web.UI.Page
{
    private Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
    Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();

    protected void Page_PreInit(object sender, EventArgs e)
    {
       
        Site1 toolBar = (Site1)this.Master;
        toolBar.eventoAdelante += btnAdelante_Click;
        toolBar.eventoAtras += btnAtras_Click;  
        ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
        ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

        ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
        btnAdelante.ValidationGroup = "";
        
        if (Session["TipoCredito"].ToString() == "C")     //Si el credito es ordinario deja invisible la primera pestaña
        { 
            tcInfFinNeg.Tabs[0].Visible = false;
            btnAdelante.ImageUrl = "~/Images/btnPatrimonio.jpg";
        } 
        else
            btnAdelante.ImageUrl = "~/Images/btnInventarios.jpg";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["TipoCredito"].ToString() == "C")
            {
                TabPanel0.Visible = true;
                TabPanel1.Visible = false;

                Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
                InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(Convert.ToInt64(Session["Cod_persona"].ToString()), (Usuario)Session["Usuario"]);
                txtsueldo.Text = InformacionFinanciera.sueldo.ToString();
                txtsueldoconyuge.Text = InformacionFinanciera.sueldoconyuge.ToString();
                txthonorarios.Text = InformacionFinanciera.honorarios.ToString();
                txthonorariosconyuge.Text = InformacionFinanciera.honorariosconyuge.ToString();
                txtarrendamientos.Text = InformacionFinanciera.arrendamientos.ToString();
                txtarrendamientosconyuge.Text = InformacionFinanciera.arrendamientosconyuge.ToString();
                txtotrosingre.Text = InformacionFinanciera.otrosingresos.ToString();
                txtotrosingreconyuge.Text = InformacionFinanciera.otrosingresosconyuge.ToString();
                txttotalingre.Text = InformacionFinanciera.totalingreso.ToString();
                txttotalingreconyuge.Text = InformacionFinanciera.totalingresoconyuge.ToString();
                txtCuota.Text = InformacionFinanciera.hipoteca.ToString();
                txtCuotaconyuge.Text = InformacionFinanciera.hipotecaconyuge.ToString();
                txtcuotatrgcredito.Text = InformacionFinanciera.targeta_credito.ToString();
                txtcuotatrgcreditoconyuge.Text = InformacionFinanciera.targeta_creditoconyuge.ToString();
                txtotrosprestamos.Text = InformacionFinanciera.otrosprestamos.ToString();
                txtotrosprestamosconyuge.Text = InformacionFinanciera.otrosprestamosconyuge.ToString();
                txtgastosfamiliares.Text = InformacionFinanciera.gastofamiliar.ToString();
                txtgastosfamiliaresconyuge.Text = InformacionFinanciera.gastofamiliarconyuge.ToString();
                txtdescuentosnomina.Text = InformacionFinanciera.decunomina.ToString();
                txtdescuentosnominaconyuge.Text = InformacionFinanciera.decunominaconyuge.ToString();
                txttotalegresos.Text = InformacionFinanciera.totalegresos.ToString();
                txttotalegresosconyuge.Text = InformacionFinanciera.totalegresosconyuge.ToString();

            }
            else 
            {
                TabPanel0.Visible = false;
                TabPanel1.Visible = true;  
            }
        }
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["TipoCredito"].ToString() == "C")     //Si el credito es ordinario redirecciona a GrupoFamiliar
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/GrupoFamiliar/Lista.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/MargenVentas/Lista.aspx");         
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        guardar();
        if (Session["TipoCredito"].ToString() == "C")     //Si el credito es ordinario redirecciona a patrimonio
             Response.Redirect("~/Page/FabricaCreditos/Solicitud/Patrimonio/Default.aspx");
        else
             Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Inventarios/Default.aspx");
    }

    /// <summary>
    /// Método para guardar los datos de la informaciòn financiera.
    /// </summary>
    public void guardar()
    {
           Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();

           InformacionFinanciera.sueldo = ConvertirToInt64(txtsueldo.Text);
           InformacionFinanciera.sueldoconyuge = ConvertirToInt64(txtsueldoconyuge.Text);
           InformacionFinanciera.honorarios = ConvertirToInt64(txthonorarios.Text);
           InformacionFinanciera.honorariosconyuge = ConvertirToInt64(txthonorariosconyuge.Text);
           InformacionFinanciera.arrendamientos = ConvertirToInt64(txtarrendamientos.Text);
           InformacionFinanciera.arrendamientosconyuge = ConvertirToInt64(txtarrendamientosconyuge.Text);
           InformacionFinanciera.otrosingresos = ConvertirToInt64(txtotrosingre.Text);
           InformacionFinanciera.otrosingresosconyuge = ConvertirToInt64(txtotrosingreconyuge.Text);
           InformacionFinanciera.totalingreso = ConvertirToInt64(txttotalingre.Text);
           InformacionFinanciera.totalingresoconyuge = ConvertirToInt64(txttotalingreconyuge.Text);
           InformacionFinanciera.hipoteca = ConvertirToInt64(txtCuota.Text);
           InformacionFinanciera.hipotecaconyuge = ConvertirToInt64(txtCuotaconyuge.Text);
           InformacionFinanciera.targeta_credito = ConvertirToInt64(txtcuotatrgcredito.Text);
           InformacionFinanciera.targeta_creditoconyuge = ConvertirToInt64(txtcuotatrgcreditoconyuge.Text);
           InformacionFinanciera.otrosprestamos = ConvertirToInt64(txtotrosprestamos.Text);
           InformacionFinanciera.otrosprestamosconyuge = ConvertirToInt64(txtotrosprestamosconyuge.Text);
           InformacionFinanciera.gastofamiliar = ConvertirToInt64(txtgastosfamiliares.Text);
           InformacionFinanciera.gastofamiliarconyuge = ConvertirToInt64(txtgastosfamiliaresconyuge.Text);
           if (txtdescuentosnomina.Text == "")
               InformacionFinanciera.decunomina = 0;
           else
               InformacionFinanciera.decunomina = ConvertirToInt64(txtdescuentosnomina.Text);
           InformacionFinanciera.decunominaconyuge = ConvertirToInt64(txtdescuentosnominaconyuge.Text);
           InformacionFinanciera.totalegresos = ConvertirToInt64(txttotalegresos.Text);
           InformacionFinanciera.totalegresosconyuge = ConvertirToInt64(txttotalegresosconyuge.Text);

           InformacionFinanciera.cod_persona = ConvertirToInt64(Session["Cod_persona"].ToString());
           if (Session["Cod_persona_conyuge"] != null)
               InformacionFinanciera.cod_personaconyuge = ConvertirToInt64(Session["Cod_persona_conyuge"].ToString());

           EstadosFinancierosServicio.guardarIngreEgre(InformacionFinanciera, (Usuario)Session["Usuario"]);
 
    }

    /// <summary>
    /// Mètodo para convertir un valor a entero
    /// </summary>
    /// <param name="svalor"></param>
    /// <returns></returns>
    private Int64 ConvertirToInt64(string svalor)
    {
        try
        {
            return Convert.ToInt64(svalor);
        }
        catch
        {
            return 0;
        }
    }

    protected void txtsueldo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txttotalingre.Text = Convert.ToString(Convert.ToInt32(txtsueldo.Text) + Convert.ToInt32(txthonorarios.Text) + Convert.ToInt32(txtarrendamientos.Text) + Convert.ToInt32(txtotrosingre.Text));
        }
        catch { }                
    }

    protected void txthonorarios_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingre.Text = Convert.ToString(Convert.ToInt32(txtsueldo.Text) + Convert.ToInt32(txthonorarios.Text) + Convert.ToInt32(txtarrendamientos.Text) + Convert.ToInt32(txtotrosingre.Text));
        }
        catch { }   
    }

    protected void txtarrendamientos_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingre.Text = Convert.ToString(Convert.ToInt32(txtsueldo.Text) + Convert.ToInt32(txthonorarios.Text) + Convert.ToInt32(txtarrendamientos.Text) + Convert.ToInt32(txtotrosingre.Text));
        }
        catch { }   
    }

    protected void txtotrosingre_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingre.Text = Convert.ToString(Convert.ToInt32(txtsueldo.Text) + Convert.ToInt32(txthonorarios.Text) + Convert.ToInt32(txtarrendamientos.Text) + Convert.ToInt32(txtotrosingre.Text));
        }
        catch { }   
    }

    protected void txtsueldoconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingreconyuge.Text = Convert.ToString(Convert.ToInt32(txtsueldoconyuge.Text) + Convert.ToInt32(txthonorariosconyuge.Text) + Convert.ToInt32(txtarrendamientosconyuge.Text) + Convert.ToInt32(txtotrosingreconyuge.Text));
        }
        catch { }   
    }

    protected void txthonorariosconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingreconyuge.Text = Convert.ToString(Convert.ToInt32(txtsueldoconyuge.Text) + Convert.ToInt32(txthonorariosconyuge.Text) + Convert.ToInt32(txtarrendamientosconyuge.Text) + Convert.ToInt32(txtotrosingreconyuge.Text));
        }
        catch { }   
    }

    protected void txtarrendamientosconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingreconyuge.Text = Convert.ToString(Convert.ToInt32(txtsueldoconyuge.Text) + Convert.ToInt32(txthonorariosconyuge.Text) + Convert.ToInt32(txtarrendamientosconyuge.Text) + Convert.ToInt32(txtotrosingreconyuge.Text));
        }
        catch { }   
    }

    protected void txtotrosingreconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalingreconyuge.Text = Convert.ToString(Convert.ToInt32(txtsueldoconyuge.Text) + Convert.ToInt32(txthonorariosconyuge.Text) + Convert.ToInt32(txtarrendamientosconyuge.Text) + Convert.ToInt32(txtotrosingreconyuge.Text));
        }
        catch { }   
    }

    protected void txtCuota_TextChanged(object sender, EventArgs e)
    {      
        try {
        txttotalegresos.Text = Convert.ToString(Convert.ToInt32(txtCuota.Text) + Convert.ToInt32(txtcuotatrgcredito.Text) + Convert.ToInt32(txtotrosprestamos.Text) + Convert.ToInt32(txtgastosfamiliares.Text) + Convert.ToInt32(txtdescuentosnomina.Text));
        }
        catch { }   
    }

    protected void txtcuotatrgcredito_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresos.Text = Convert.ToString(Convert.ToInt32(txtCuota.Text) + Convert.ToInt32(txtcuotatrgcredito.Text) + Convert.ToInt32(txtotrosprestamos.Text) + Convert.ToInt32(txtgastosfamiliares.Text) + Convert.ToInt32(txtdescuentosnomina.Text));
        }
        catch { }   
    }

    protected void txtotrosprestamos_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresos.Text = Convert.ToString(Convert.ToInt32(txtCuota.Text) + Convert.ToInt32(txtcuotatrgcredito.Text) + Convert.ToInt32(txtotrosprestamos.Text) + Convert.ToInt32(txtgastosfamiliares.Text) + Convert.ToInt32(txtdescuentosnomina.Text));
        }
        catch { }   
    }

    protected void txtgastosfamiliares_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresos.Text = Convert.ToString(Convert.ToInt32(txtCuota.Text) + Convert.ToInt32(txtcuotatrgcredito.Text) + Convert.ToInt32(txtotrosprestamos.Text) + Convert.ToInt32(txtgastosfamiliares.Text) + Convert.ToInt32(txtdescuentosnomina.Text));
        }
        catch { }   
    }

    protected void txtdescuentosnomina_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresos.Text = Convert.ToString(Convert.ToInt32(txtCuota.Text) + Convert.ToInt32(txtcuotatrgcredito.Text) + Convert.ToInt32(txtotrosprestamos.Text) + Convert.ToInt32(txtgastosfamiliares.Text) + Convert.ToInt32(txtdescuentosnomina.Text));
        }
        catch { }   
    }

    protected void txtCuotaconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresosconyuge.Text = Convert.ToString(Convert.ToInt32(txtCuotaconyuge.Text) + Convert.ToInt32(txtcuotatrgcreditoconyuge.Text) + Convert.ToInt32(txtotrosprestamosconyuge.Text) + Convert.ToInt32(txtgastosfamiliaresconyuge.Text) + Convert.ToInt32(txtdescuentosnominaconyuge.Text));
        }
        catch { }   
    }

    protected void txtcuotatrgcreditoconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresosconyuge.Text = Convert.ToString(Convert.ToInt32(txtCuotaconyuge.Text) + Convert.ToInt32(txtcuotatrgcreditoconyuge.Text) + Convert.ToInt32(txtotrosprestamosconyuge.Text) + Convert.ToInt32(txtgastosfamiliaresconyuge.Text) + Convert.ToInt32(txtdescuentosnominaconyuge.Text));
        }
        catch { }   
    }

    protected void txtotrosprestamosconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresosconyuge.Text = Convert.ToString(Convert.ToInt32(txtCuotaconyuge.Text) + Convert.ToInt32(txtcuotatrgcreditoconyuge.Text) + Convert.ToInt32(txtotrosprestamosconyuge.Text) + Convert.ToInt32(txtgastosfamiliaresconyuge.Text) + Convert.ToInt32(txtdescuentosnominaconyuge.Text));
        }
        catch { }   
    }

    protected void txtgastosfamiliaresconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresosconyuge.Text = Convert.ToString(Convert.ToInt32(txtCuotaconyuge.Text) + Convert.ToInt32(txtcuotatrgcreditoconyuge.Text) + Convert.ToInt32(txtotrosprestamosconyuge.Text) + Convert.ToInt32(txtgastosfamiliaresconyuge.Text) + Convert.ToInt32(txtdescuentosnominaconyuge.Text));
        }
        catch { }   
    }

    protected void txtdescuentosnominaconyuge_TextChanged(object sender, EventArgs e)
    {
        try {
        txttotalegresosconyuge.Text = Convert.ToString(Convert.ToInt32(txtCuotaconyuge.Text) + Convert.ToInt32(txtcuotatrgcreditoconyuge.Text) + Convert.ToInt32(txtotrosprestamosconyuge.Text) + Convert.ToInt32(txtgastosfamiliaresconyuge.Text) + Convert.ToInt32(txtdescuentosnominaconyuge.Text));
        }
        catch { }   
    }

}