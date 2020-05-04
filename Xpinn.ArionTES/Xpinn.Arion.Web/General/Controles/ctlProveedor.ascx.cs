using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlProveedor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            if (cargar == "1")
                ActualizarGridPersonas();
        }
    }
    
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtcodProveedor", "txtIdentificacion", "txtNombreProveedor");
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        Persona1.seleccionar = "Identificacion";
        Persona1.noTraerHuella = 1;
        Persona1.identificacion = txtIdentificacion.Text;
        Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);
        if (Persona1.cod_persona != 0)
        {
            txtcodProveedor.Text = Persona1.cod_persona.ToString();
            if (Persona1.tipo_persona == "N")
                txtNombreProveedor.Text = Persona1.nombres.Trim() + " " + Persona1.apellidos.Trim();
            else
            {
                if (Persona1.nombres != null)
                    txtNombreProveedor.Text = Persona1.nombres;                
            }
        }
        else
        {
            txtNombreProveedor.Text = "";            
        }
    }


    public bool ValidarPersona()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

            Persona1.seleccionar = "Identificacion";
            Persona1.noTraerHuella = 1;
            Persona1.identificacion = txtIdentificacion.Text;
            Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);
            if (Persona1 == null)
                return false;
            txtcodProveedor.Text = Persona1.cod_persona.ToString();
            if (Persona1.tipo_persona == "N")
                txtNombreProveedor.Text = Persona1.nombres.Trim() + " " + Persona1.apellidos.Trim();
            else
            {
                if (Persona1.nombres != null)
                    txtNombreProveedor.Text = Persona1.nombres;
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    
    public void ActualizarGridPersonas()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        try
        {            
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
            Session["LSTPERSONAS"] = lstConsulta;            
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public string cargar
    {
        set { hfCargar.Value = value; ViewState["cargar"] = value; }
        get { hfCargar.Value = ViewState["cargar"] == null ? "1" : ViewState["cargar"].ToString();  return hfCargar.Value; }
    }

    public bool VisibleCtl
    {
        set { panelCtl.Visible = value; }
        get { return panelCtl.Visible; }
    }

    public bool CheckedOrd
    {
        set
        {
            chkManeja_Orden.Checked = value;
            chkManeja_Orden_CheckedChanged(chkManeja_Orden, null);
        }
        get { return chkManeja_Orden.Checked; }
    }

    public bool VisibleOrd
    {
        set
        {
            chkManeja_Orden.Visible = value;
            chkManeja_Orden_CheckedChanged(chkManeja_Orden, null);
        }
        get { return chkManeja_Orden.Visible; }
    }

    public string TextOrdCred
    {
        set { txtOrdenCred.Text = value; }
        get { return txtOrdenCred.Text; }
    }

    public string TextOrdAux
    {
        set { txtOrdenAux.Text = value; }
        get { return txtOrdenAux.Text; }
    }

    public Int64 TextCodigo
    {
        set { txtcodProveedor.Text = value.ToString(); }
        get { try { return Convert.ToInt64(txtcodProveedor.Text); } catch { return 0; } }
    }

    public string TextIdentif
    {
        set { txtIdentificacion.Text = value; }
        get { return txtIdentificacion.Text; }
    }

    public string TextNomProv
    {
        set { txtNombreProveedor.Text = value; }
        get { return txtNombreProveedor.Text; }
    }
    
    public void Editable(bool pEditable)
    {
        txtIdentificacion.Enabled = pEditable;
        btnConsultaPersonas.Visible = pEditable;
    }

    public void AsignarDatos(string pIdentificacion, string pNombre)
    {
        txtIdentificacion.Text = pIdentificacion;
        txtNombreProveedor.Text = pNombre;        
    }

    public bool ObtenerDatosOrdenCred(string pNumero_radicacion)
    {
        try
        {
            if (pNumero_radicacion != "" && pNumero_radicacion != null)
            {
                //CONSULTAR EN CREDITO_ORDEN_SERVICIO
                Xpinn.FabricaCreditos.Services.CreditoService vCredito = new Xpinn.FabricaCreditos.Services.CreditoService();
                Xpinn.FabricaCreditos.Entities.CreditoOrdenServicio pEntidad = new Xpinn.FabricaCreditos.Entities.CreditoOrdenServicio();
                String pFiltro = " WHERE NUMERO_RADICACION = " + pNumero_radicacion;
                pEntidad = vCredito.ConsultarCREDITO_OrdenServ(pFiltro, (Usuario)Session["usuario"]);
                if (pEntidad.idordenservicio != 0)
                {
                    txtOrdenCred.Text = pEntidad.idordenservicio.ToString();
                    if (pEntidad.cod_persona != null)
                        txtcodProveedor.Text = pEntidad.cod_persona.ToString();
                    if (pEntidad.idproveedor != null)
                        txtIdentificacion.Text = pEntidad.idproveedor;
                    if (pEntidad.nomproveedor != null)
                        txtNombreProveedor.Text = pEntidad.nomproveedor;
                }
                else
                    return false;
            }
        }
        catch(Exception ex)
        {
            ex.ToString();
            return false;
        }
        return true;
    }

    public bool ObtenerDatosOrdenAuXradicacion(string pNumero_radicacion)
    {
        try
        {
            if (pNumero_radicacion != "" && pNumero_radicacion != null)
            {
                Xpinn.Auxilios.Services.SolicitudAuxilioServices AuxilioService = new Xpinn.Auxilios.Services.SolicitudAuxilioServices();
                Xpinn.Auxilios.Entities.SolicitudAuxilio pAuxilio = new Xpinn.Auxilios.Entities.SolicitudAuxilio();
                //CONSULTANDO DATOS DEL AUXILIO
                string pFilt = " WHERE NUMERO_RADICACION = " + pNumero_radicacion;
                pAuxilio = AuxilioService.Consultar_Auxilio_Variado(pFilt, (Usuario)Session["usuario"]);
                if (pAuxilio.numero_auxilio != 0 && pAuxilio.numero_auxilio != null)
                {
                    Xpinn.Auxilios.Entities.Auxilio_Orden_Servicio pAuxOrden = new Xpinn.Auxilios.Entities.Auxilio_Orden_Servicio();
                    string pFiltro = " WHERE NUMERO_AUXILIO = " + pAuxilio.numero_auxilio
                        + " AND (SELECT NUMERO_RADICACION FROM AUXILIOS WHERE NUMERO_AUXILIO = " + pAuxilio.numero_auxilio + ") = " + pNumero_radicacion;
                    pAuxOrden = AuxilioService.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);
                    if (pAuxOrden.idproveedor != null)
                    {
                        if (pAuxOrden.idordenservicio != 0)
                        {
                            txtOrdenAux.Text = pAuxOrden.idordenservicio.ToString();
                            if (pAuxOrden.cod_persona != null && txtcodProveedor.Text == "")
                                txtcodProveedor.Text = pAuxOrden.cod_persona.ToString();
                            if (pAuxOrden.idproveedor != null && txtIdentificacion.Text == "")
                                txtIdentificacion.Text = pAuxOrden.idproveedor;
                            if (pAuxOrden.nomproveedor != null && txtNombreProveedor.Text == "")
                                txtNombreProveedor.Text = pAuxOrden.nomproveedor;
                        }
                        else
                            return false;
                    }
                }
                else
                    return false;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            return false;
        }
        return true;
    }


    protected void chkManeja_Orden_CheckedChanged(object sender, EventArgs e)
    {
        if (chkManeja_Orden.Checked)
            panelProveedor.Visible = true;
        else
            panelProveedor.Visible = false;
    }
}