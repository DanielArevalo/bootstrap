using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;


public partial class ctlGiro : System.Web.UI.UserControl
{
    PoblarListas Poblar = new PoblarListas();
    Int64 COD_PERSONA = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    public void Inicializar()
    {
        Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

        DropDownFormaDesembolso.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
        DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
        DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));        
        DropDownFormaDesembolso.Items.Insert(4, new ListItem("TranferenciaAhorroVistaInterna", "4")); 
        DropDownFormaDesembolso.DataBind();
        DropDownFormaDesembolso.SelectedIndex = 1;
        DropDownFormaDesembolso.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();

        Poblar.PoblarListaDesplegable("V_BANCOS_ENTIDAD", ddlEntidadOrigen, pUsuario);
        CargarCuentas();

        Poblar.PoblarListaDesplegable("BANCOS", "COD_BANCO,NOMBREBANCO", "", "1", DropDownEntidad, pUsuario);

        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.DataBind();

        ActivarDesembolso();
       
    }
    public void cargarCuentasAhorro(Int64  pCodPersona)
    {      
        ActivarDesembolso();
        CargarCuentasAhorro(pCodPersona);
    }
    protected void ActivarDesembolso()
    {
        TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();

        if (formaDesembolso == TipoFormaDesembolso.Transferencia)
        {
            panelCheque.Visible = true;
            panelTrans.Visible = true;
            pnlCuentaAhorroVista.Visible = false;
        }
        else if (formaDesembolso == TipoFormaDesembolso.Efectivo || DropDownFormaDesembolso.SelectedIndex == 0)
        {
            panelCheque.Visible = false;
            panelTrans.Visible = false;
            pnlCuentaAhorroVista.Visible = false;
        }
        else if (formaDesembolso == TipoFormaDesembolso.Cheque)
        {
            panelCheque.Visible = true;
            panelTrans.Visible = false;
            pnlCuentaAhorroVista.Visible = false;
        }
        else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
        {
            panelTrans.Visible = false;
            panelCheque.Visible = false;
            pnlCuentaAhorroVista.Visible = true;
         


        }
    }

    protected void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }

    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }

    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEntidadOrigen.SelectedIndex != 0)
            CargarCuentas();
        else
            ddlCuentaOrigen.Items.Clear();
    }


    /// <summary>
    /// VALORES DE LOS OBJETOS
    /// </summary>
    public string ValueFormaDesem
    {
        set {
            try
            {
                DropDownFormaDesembolso.SelectedValue = value;
            }
            catch
            {
                return;
            }
            ViewState["DropDownFormaDesembolso"] = value;
            DropDownFormaDesembolso_SelectedIndexChanged(DropDownFormaDesembolso, null);
        }
        get { return DropDownFormaDesembolso.SelectedValue; }
    }

    public string ValueEntidadOrigen
    {
        set {
            try
            {
                ddlEntidadOrigen.SelectedValue = value;
            }
            catch
            {
                return;
            }
            ViewState["ddlEntidadOrigen"] = value;
            ddlEntidadOrigen_SelectedIndexChanged(ddlEntidadOrigen, null);        
        }
        get { return ddlEntidadOrigen.SelectedValue; }
    }

    public string ValueCuentaOrigen
    {
        set { ddlCuentaOrigen.SelectedValue = value; }       
        get { return ddlCuentaOrigen.SelectedValue; }
    }

    public string TextCuentaOrigen
    {
        set { ddlCuentaOrigen.SelectedItem.Text = value; }
        get { return ddlCuentaOrigen.SelectedItem.Text; }
    }

    public string ValueEntidadDest
    {
        set { DropDownEntidad.SelectedValue = value; }
        get { return DropDownEntidad.SelectedValue; }
    }

    public string ValueTipoCta
    {
        set { ddlTipo_cuenta.SelectedValue = value; }
        get { return ddlTipo_cuenta.SelectedValue; }
    }


    public string ValueCuentaAhorro
    {
        set { ddlCuentaAhorroVista.SelectedValue = value; }
        get { return ddlCuentaAhorroVista.SelectedValue; }
    }

    /// <summary>
    /// INDICES DE LOS OBJETOS
    /// </summary>
    public int IndiceFormaDesem
    {
        set { DropDownFormaDesembolso.SelectedIndex = value; }
        get { return DropDownFormaDesembolso.SelectedIndex; }
    }

    public int IndiceEntidadOrigen
    {
        set { ddlEntidadOrigen.SelectedIndex = value; }
        get { return ddlEntidadOrigen.SelectedIndex; }
    }

    public int IndiceEntidadDest
    {
        set { DropDownEntidad.SelectedIndex = value; }
        get { return DropDownEntidad.SelectedIndex; }
    }

    public int IndiceCuentaAhorro
    {
        set { ddlCuentaAhorroVista.SelectedIndex = value; }
        get { return ddlCuentaAhorroVista.SelectedIndex; }
    }

    /// <summary>
    /// Texto del Numero de Cuenta
    /// </summary>
    public string TextNumCuenta
    {
        set { txtnumcuenta.Text = value; }
        get { return txtnumcuenta.Text; }
    }

    public int IndiceCuentaAhorroVista
    {
        set { ddlCuentaAhorroVista.SelectedIndex = value; }
        get { return ddlCuentaAhorroVista.SelectedIndex; }
    }

    public Xpinn.FabricaCreditos.Entities.Giro ObtenerEntidadGiro(TextBox txtValor)
   {
        Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
        if (this.Visible)
        {
            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
            if (this.ValueEntidadOrigen != "")
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(this.ValueEntidadOrigen), this.TextCuentaOrigen, (Usuario)Session["usuario"]);
            Int64 idCta = CuentaBanc.idctabancaria;

            if (this.IndiceFormaDesem == 3)     // Transferencia 
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(this.ValueEntidadDest);
                pGiro.num_cuenta = this.TextNumCuenta;
                pGiro.tipo_cuenta = Convert.ToInt32(this.ValueTipoCta);
            }
            else if (this.IndiceFormaDesem == 2) // Cheque
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            else
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(txtValor.Text.Replace(".", ""));
        }
        try
        {
            pGiro.valor = Convert.ToInt64(txtValor.Text.Replace(".", ""));
        }
        catch
        {
            pGiro.valor = 0;
        }

        return pGiro;   
   }

   public Xpinn.FabricaCreditos.Entities.Giro ObtenerEntidadGiro(Int64 pCodPersona, DateTime pFecha, Decimal pValor, Usuario pUsuario)
   {
       return ObtenerEntidadGiro(2, pCodPersona, pFecha, pValor, pUsuario);
   }


   public Xpinn.FabricaCreditos.Entities.Giro ObtenerEntidadGiro(Int32 pTipoActo, Int64 pCodPersona, DateTime pFecha, Decimal pValor, Usuario pUsuario)
   {
       Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
       pGiro.idgiro = 0;
       pGiro.cod_persona = pCodPersona; 
       pGiro.forma_pago = Convert.ToInt32(this.ValueFormaDesem);
       pGiro.tipo_acto = 2;
       pGiro.fec_reg = pFecha;
       pGiro.fec_giro = DateTime.Now;
       pGiro.numero_radicacion = 0;
       pGiro.usu_gen = pUsuario.nombre;
       pGiro.usu_apli = null;
       pGiro.estadogi = 0;
       pGiro.usu_apro = null;

       //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
       Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
       Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
       if (this.ValueEntidadOrigen != "")
           CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(this.ValueEntidadOrigen), this.TextCuentaOrigen, pUsuario);
       Int64 idCta = CuentaBanc.idctabancaria;

       //DATOS DE FORMA DE PAGO
       if (this.Visible)
       {
           if (this.IndiceFormaDesem == 3)      // Transferencia
           {
               pGiro.idctabancaria = idCta;
               pGiro.cod_banco = Convert.ToInt32(this.ValueEntidadDest);
               pGiro.num_cuenta = this.TextNumCuenta;
               pGiro.tipo_cuenta = Convert.ToInt32(this.ValueTipoCta);
           }
           else if (this.IndiceFormaDesem == 2) // Cheque
           {
               pGiro.idctabancaria = idCta;
               pGiro.cod_banco = 0; 
               pGiro.num_cuenta = null;
               pGiro.tipo_cuenta = -1;
           }
           else if (this.IndiceFormaDesem == 5) // Transferencia cuenta Interna
           {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
           }
           else
           {
               pGiro.idctabancaria = 0;
               pGiro.cod_banco = 0;
               pGiro.num_cuenta = null;
               pGiro.tipo_cuenta = -1;
           }
           pGiro.fec_apro = DateTime.MinValue;
           pGiro.cob_comision = 0;
           pGiro.valor = Convert.ToInt64(pValor);
       }
       try
       {
           pGiro.valor = Convert.ToInt64(pValor); 
       }
       catch
       {
           pGiro.valor = 0;
       }
       return pGiro;
   }



   public void CargarCuentasAhorro(Int64 pCodPersona)
   {
       
        if (pCodPersona != 0)
        {
            AhorroVistaServices ahorroServices = new AhorroVistaServices();
            Usuario usuario = (Usuario)Session["usuario"];
          
            ddlCuentaAhorroVista.DataSource = ahorroServices.ListarCuentaAhorroVistaGiros(pCodPersona, usuario);
            ddlCuentaAhorroVista.DataTextField = "numero_cuenta";
            ddlCuentaAhorroVista.DataValueField = "numero_cuenta";
            ddlCuentaAhorroVista.DataBind();
        }
   }

    public Xpinn.Tesoreria.Entities.Giro ConvertirEntidad(Xpinn.FabricaCreditos.Entities.Giro pGiro)
    {
        Xpinn.Tesoreria.Entities.Giro eGiro = new Xpinn.Tesoreria.Entities.Giro();
        eGiro.idgiro = pGiro.idgiro;
        eGiro.cod_persona = pGiro.cod_persona;
        eGiro.identificacion = pGiro.identificacion;
        eGiro.nombre = pGiro.nombre;
        eGiro.forma_pago = pGiro.forma_pago;
        eGiro.tipo_acto = pGiro.tipo_acto;
        eGiro.fec_reg = pGiro.fec_reg;
        eGiro.fec_giro = pGiro.fec_giro;
        eGiro.numero_radicacion = pGiro.numero_radicacion;
        eGiro.cod_ope = pGiro.cod_ope;
        eGiro.num_comp = pGiro.num_comp;
        eGiro.tipo_comp = pGiro.tipo_comp;
        eGiro.usu_gen = pGiro.usu_gen;
        eGiro.usu_apli = pGiro.usu_apli;
        eGiro.estadogi = pGiro.estadogi;
        eGiro.usu_apro = pGiro.usu_apro;
        eGiro.idctabancaria = pGiro.idctabancaria;
        eGiro.cod_banco = pGiro.cod_banco;
        eGiro.num_cuenta = pGiro.num_cuenta;
        eGiro.tipo_cuenta = pGiro.tipo_cuenta;
        eGiro.fec_apro = pGiro.fec_apro;
        eGiro.cob_comision = pGiro.cob_comision;
        eGiro.valor = pGiro.valor;
        return eGiro;
    }



}