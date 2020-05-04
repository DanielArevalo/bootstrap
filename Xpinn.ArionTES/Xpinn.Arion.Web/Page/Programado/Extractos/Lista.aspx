<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Extracto Programado :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlModalidad.ascx" TagName="ddlModalidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="ddlFormaPago" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
        $('#bntvalidar').on('click', function (e) {

            console.log('Hola');
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
     <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
        <asp:Panel ID="pConsulta" runat="server">
        <table>
            <tr>
                <td style="text-align: left" colspan="6">
                    <strong>Datos de la Cuenta</strong>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 4px; text-align: left; width: 150px;">
                    Fecha Corte<br />
                    <ucFecha:Fecha ID="txtFecha_corte" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="height: 4px; text-align: left; width: 150px;">
                    Periodo<br />
                    <ucFecha:Fecha ID="txtFecha_periodo" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
                <td style="height: 4px; text-align: left; width: 150px;">
                    <br />
                    <strong>a</strong>
                    <ucFecha:Fecha ID="Txtfechaperiodo_final" runat="server" AutoPostBack="True" CssClass="textbox"
                        MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 69px;" colspan="2">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="120px" />
                   <strong>a</strong>&nbsp;
                    <asp:TextBox ID="txtcodigo_final" runat="server" CssClass="textbox" Width="152px" />
                </td>
                <td style="text-align: left; width: 373px; height: 69px;" colspan="2" enabled="True">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Identificación<br />
                    <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="92px" />
                    <strong>a&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtidentificacion_final" runat="server" CssClass="textbox" Enabled="true"
                            Width="92px" />
                    </strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="2">
                   Nombres<br />
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="320px" />
                </td>
                <td style="text-align: left" colspan="2">
                      Apellidos<br />
                   <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="443px" />
                   
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;" colspan="2">
                    Empresa<br />
                    <asp:DropDownList ID="ddlEmpresa" runat="server" Width="321px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 15px; text-align: left;" colspan="2">
                   Ciudad De Residencia<br />
                    <asp:DropDownList ID="ddlCiudad" runat="server" Width="450px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 354px;">
                   
                </td>
            </tr>
            <tr>
                <td  style="text-align: left" colspan="2">
                    Numero De Cuenta<br />
                    <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="textbox"
                        Width="103px" />
                    a<asp:TextBox ID="txtNumCuenta_final" runat="server" CssClass="textbox" 
                        Width="103px" />
                </td>
                <td style="height: 15px; text-align: left;" colspan="2">
                    Linea De Ahorro<br />
                    <asp:DropDownList ID="ddllineaahorro" runat="server" Width="450px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="6">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Observaciones Para El Extracto<br />
                    <asp:TextBox ID="txtObservacionesExtracto" runat="server" CssClass="textbox" Width="786px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
      </asp:View>
      <asp:View ID="vwListado" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
            <td>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
            </td>
            <td>
                <br />
            </td>
        </tr>
        
    </table>

     
    <table>
    
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" 
                    AutoGenerateColumns="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="numero_cuenta" 
                    Style="font-size: xx-small">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="check" Checked="true" runat="server" Style="text-align: right" TipoLetra="XX-Small"
                                    Habilitado="True" AutoPostBack_="True" Enabled="True" Width_="80" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_cuenta" HeaderText="No.Cuenta">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea" HeaderText="Línea">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_apertura" HeaderText="F.Apertura" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="direccion" HeaderText="Dirección">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo Actual" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                           <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F.Prox Pago"  DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                           <asp:BoundField DataField="valor_total" HeaderText="Valor Total A Pagar" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>

    </table>
        </asp:View>
    <asp:View ID="vReporteExtracto" runat="server">
        <table>
            <tr>
                <td style="width: 100%">
                    <br />
                    <br />
                    <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="100%" Font-Names="Verdana"
                        Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14pt" Height="500px">
                        <LocalReport ReportPath="Page\Programado\Extractos\rptExtracto.rdlc" EnableExternalImages="True">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td>
                   
                </td>
            </tr>
        </table>
    </asp:View>
    </asp:MultiView>
</asp:Content>
