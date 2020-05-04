<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="5" cellspacing="0" style="width: 80%; margin-right: 0px;">
            <tr>               
                <td style="text-align: left; width: 120px;">
                    Mes<br />
                    <asp:DropDownList ID="ddlmes" runat="server" Width="150px" CssClass="textbox"
                        AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Seleccione Un Item</asp:ListItem>
                        <asp:ListItem Value="1">Enero</asp:ListItem>
                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Abril</asp:ListItem>
                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                        <asp:ListItem Value="6">Junio</asp:ListItem>
                        <asp:ListItem Value="7">Julio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 99px;">
                    Año<br />
                    <asp:TextBox ID="txtAño" runat="server" Width="100px " CssClass="textbox">
                    </asp:TextBox>
                </td>
                <td style="text-align: left; width: 99px;">
                    % Fondo<br />
                    <asp:TextBox ID="txtFondo" runat="server" Width="100px " CssClass="textbox">
                    </asp:TextBox>
                </td>
                <td style="text-align: left; " colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 80%">
        <tr>
            <td class="logo" colspan ="4">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" OnClick="btnExportar_Click"
                    Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>
            <td class="logo" colspan ="4">
                <asp:GridView ID="gvLista" runat="server" Width="75%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                    DataKeyNames="numero_cuenta" Style="font-size: xx-small">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo a la fecha" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />                    
                </asp:GridView>                                                                                
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" Width="75%" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelTotales" runat="server">
        <table style="width: 80%">
            <tr>
                <td style="text-align: left; height: 15px;">
                    <asp:Label ID="lbldias" runat="server" Text="Numero Dias" /><br />
                    <asp:TextBox ID="txtDias" runat="server" Width="80px" CssClass="textbox" Enabled="false">
                    </asp:TextBox>
                </td>
                <td style="text-align: left; height: 15px;">
                    <asp:Label ID="lblPromedio" runat="server" Text="Promedio Diario Mes" /><br />
                    <asp:TextBox ID="txtPromedio" runat="server" Width="140px" CssClass="textbox" Enabled="false">
                    </asp:TextBox>
                </td>
                <td style="text-align: left; height: 15px;">
                    <asp:Label ID="lblValorFondo" runat="server" Text="Valor Fondo Liquidez" /><br />
                    <asp:TextBox ID="txtFondos" runat="server" Width="140px" CssClass="textbox" Enabled="false">
                    </asp:TextBox>
                </td>            
                <td style="text-align: left; height: 15px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
