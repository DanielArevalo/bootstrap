<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="CuadreSaldos" Title=".: Xpinn - Cuadre Saldos :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="ddlCierreFecha" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 90%;">
            <tr>
                <td align="center">Consultar<br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">CARTERA-CAPITAL</asp:ListItem>
                        <asp:ListItem Value="2">CAUSACION DE CARTERA</asp:ListItem>
                        <asp:ListItem Value="3">CONTINGENTES-CARTERA</asp:ListItem>
                        <asp:ListItem Value="4">PROVISION-CARTERA</asp:ListItem>
                        <asp:ListItem Value="5">APORTES SOCIALES</asp:ListItem>
                        <asp:ListItem Value="6">AHORRO PERMANENTE</asp:ListItem>
                        <asp:ListItem Value="7">AHORRO A LA VISTA</asp:ListItem>
                        <asp:ListItem Value="8">AHORRO CONTRACTUAL</asp:ListItem>
                        <asp:ListItem Value="9">CDATS</asp:ListItem>
                        <asp:ListItem Value="10">PROVISION GENERAL</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align:center">Fecha Cierre:<br />
                    <ctl:ddlCierreFecha ID="ddlCierreFecha" runat="server" Requerido="True" />
                </td>
                <td style="text-align:center">
                    <asp:Label ID="lblNum" runat="server" Text="Número Comprobante" Visible="false"></asp:Label><br />
                    <asp:TextBox ID="txtNumComp" runat="server" Visible="false" CssClass="textbox" Enabled="false"/>
                </td>
                <td style="text-align:center">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo Comprobante" Visible="false"></asp:Label><br />
                    <asp:DropDownList ID="ddlTipoComp" runat="server" Visible="false" CssClass="dropdown" Enabled="false"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteCierre" runat="server">
            <div style="overflow: scroll; height: 500px; width: 100%;">
                <div style="width: 95%;">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />
                    <br />
                    <asp:GridView ID="gvReportecierre" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal"
                        OnRowDataBound="gvReportecierre_RowDataBound">
                        <Columns>                            
                            <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta">
                                <ItemStyle HorizontalAlign="left" Width="40" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Cuenta" >
                                <ItemStyle HorizontalAlign="left" Width="180" />
                            </asp:BoundField>
                            <asp:BoundField DataField="centro_costo" HeaderText="C/C" >                            
                                <ItemStyle HorizontalAlign="left" Width="20" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Tercero" >                            
                                <ItemStyle HorizontalAlign="left" Width="20" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_operativo" DataFormatString="{0:C}" HeaderText="Saldo Operativo">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_contable" DataFormatString="{0:C}" HeaderText="Saldo Contable">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diferencia_actual" DataFormatString="{0:C}" HeaderText="Diferencia Contable">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>                    
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>

