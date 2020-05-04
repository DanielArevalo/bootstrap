using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;

public partial class Lista : GlobalWeb
{
    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(movCajaServicio.CodigoPrograma3,"L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Session["listSaldos2"] = Session["listSaldos"];
        Navegar("../../CajaFin/ArqueoCaja/Nuevo.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja
                
                ImprimirGrilla();

                horario = HorarioService.VerificarHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);
                Session["conteoOfiHorario"] = horario.conteo;

                horario = HorarioService.getHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                Session["Resp1"] = 0;
                Session["Resp2"] = 0;

                //si la hora actual es mayor que de la hora inicial
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_inicial.TimeOfDay) > 0)
                    Session["Resp1"] = 1;
                //si la hora actual es menor que la hora final
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_final.TimeOfDay) < 0)
                    Session["Resp2"] = 1;

                if (long.Parse(Session["estadoOfi"].ToString()) == 2)
                    VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                else
                {
                    if (long.Parse(Session["estadoCaja"].ToString()) == 0)
                        VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                    else
                    {
                        if (long.Parse(Session["conteoOfiHorario"].ToString()) == 0)
                            VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                        else
                        {
                            if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                            {
                                if (long.Parse(Session["estadoCaj"].ToString()) == 0)
                                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                            }
                            else
                                VerError("La Oficina se encuentra por fuera del horario configurado");
                        }

                    }
                }

                txtFecha.Text = Convert.ToDateTime(Session["FechaArqueo"]).ToShortDateString();
                txtCajero.Text = Session["CajeroNom"].ToString();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void ImprimirGrilla()
    {
          if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            string printScript =
                            @"function PrintGridView()

                             {
                                div = document.getElementById('DivButtons');
                                div.style.display='none';

                                var gridInsideDiv = document.getElementById('gvDiv');

                                var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');

                                printWindow.document.write(gridInsideDiv.innerHTML);

                                printWindow.document.close();

                                printWindow.focus();

                                printWindow.print();

                                printWindow.close();}";

                            this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

                            btnImprimir.Attributes.Add("onclick", "PrintGridView();");
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
          else
              VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

    }

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
           if (long.Parse(Session["estadoOfi"].ToString()) == 1)
            {
                if (long.Parse(Session["estadoCaja"].ToString()) == 1)
                {
                    if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                    {
                        if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                        {

                            if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                            {

                                if (gvChequePendiente.Rows.Count > 0 || gvChequeAsignado.Rows.Count > 0)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    StringWriter sw = new StringWriter(sb);
                                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                                    Page pagina = new Page();
                                    dynamic form = new HtmlForm();
                                    gvChequePendiente.EnableViewState = false;
                                    gvChequeAsignado.EnableViewState = false;
                                    pagina.EnableEventValidation = false;
                                    pagina.DesignerInitialize();
                                    pagina.Controls.Add(form);
                                    form.Controls.Add(gvChequePendiente);
                                    form.Controls.Add(gvChequeAsignado);
                                    pagina.RenderControl(htw);
                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.ContentType = "application/vnd.ms-excel";
                                    Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                                    Response.Charset = "UTF-8";
                                    Response.ContentEncoding = Encoding.Default;
                                    Response.Write(sb.ToString());
                                    Response.End();
                                }
                                else
                                    VerError("Se debe consultar primero los Cheques");
                            }
                            else
                                VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                        }
                        else
                            VerError("La Oficina se encuentra por fuera del horario configurado");
                    }
                    else
                        VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                }
                else
                    VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
            }
           else
               VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    public void Actualizar()
    {
        try
        {
            List<Xpinn.Caja.Entities.MovimientoCaja> lstChequePendiente = new List<Xpinn.Caja.Entities.MovimientoCaja>();
            List<Xpinn.Caja.Entities.MovimientoCaja> lstChequeAsignado = new List<Xpinn.Caja.Entities.MovimientoCaja>();

            lstChequePendiente = movCajaServicio.ListarChequesPendientes(ObtenerValores(), (Usuario)Session["usuario"]);
            lstChequeAsignado = movCajaServicio.ListarChequesAsignados(ObtenerValores(), (Usuario)Session["usuario"]);


            gvChequePendiente.DataSource = lstChequePendiente;
            gvChequeAsignado.DataSource = lstChequeAsignado;

            if (lstChequePendiente.Count > 0)
            {
                gvChequePendiente.Visible = true;
                gvChequePendiente.DataBind();
                ValidarPermisosGrilla(gvChequePendiente);
            }
            else
            {
                gvChequePendiente.Visible = false;
            }


            if (lstChequeAsignado.Count > 0)
            {
                gvChequeAsignado.Visible = true;
                gvChequeAsignado.DataBind();
                ValidarPermisosGrilla(gvChequeAsignado);
            }
            else
            {
                gvChequeAsignado.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValores()
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["CajaId"].ToString());
        movCaja.cod_cajero = long.Parse(Session["CajeroId"].ToString());

        return movCaja;
    }

    protected void btnVerCheques_Click(object sender, EventArgs e)
    {
       Actualizar();
    }
}