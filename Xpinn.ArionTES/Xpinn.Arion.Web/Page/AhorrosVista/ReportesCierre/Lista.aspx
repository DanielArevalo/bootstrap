<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="ddlCierreFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 90%;">
            <tr>
                <td align="Left">Tipo de Producto<br />
                    <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="H">Cierre Ahorros a la Vista</asp:ListItem>
                        <asp:ListItem Value="P">Cierre Ahorros Programados</asp:ListItem>
                        <asp:ListItem Value="M">Cierre Cdat</asp:ListItem>
                    </asp:DropDownList>
                   </td>
                <td>
                    <asp:CheckBox Text="ordenar la fecha de corte" ID="chkOrden" Checked="false" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkOrden_CheckedChanged" runat="server" />
                </td>
                <td style="text-align: left">Fecha de Corte:
                    <ctl:ddlCierreFecha ID="ddlCierreFecha" runat="server" Requerido="True" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteCierre" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1100px;">
                <div style="width: 1200px;">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />
                    <br />
                    <asp:GridView ID="gvReportecierreAhorroVista" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="numero_cuenta" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField HeaderText="Oficina" DataField="cod_oficina" />
                            <asp:BoundField DataField="cod_persona" HeaderText="Código Persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación de la persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos Cliente">
                                <ItemStyle HorizontalAlign="left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuenta" HeaderText="Número de Cuenta">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_ahorro" HeaderText="Cód Línea ahorro">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Línea de ahorro">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha de Apertura">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_total" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha de proximo pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="Fecha Ultimo Pago" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa" HeaderText="Tasa de interés" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           <asp:BoundField DataField="fecha_interes" HeaderText="Fecha de Interés" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="interes" HeaderText="Intereses causados" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:GridView ID="gvReportecierreAhorroProgramado" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="numero_programado" HeaderStyle-CssClass="gridHeader" Visible="false"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField HeaderText="Oficina" DataField="cod_oficina" />
                            <asp:BoundField DataField="cod_persona" HeaderText="Código Persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación de la persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos Cliente">
                                <ItemStyle HorizontalAlign="left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_programado" HeaderText="Número Programado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_programado" HeaderText="Cód Línea Programado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Línea Programado">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha de Apertura">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_total" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha de proximo pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="Fecha Ultimo Pago" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa de interés" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           <asp:BoundField DataField="fecha_interes" HeaderText="Fecha de Interés" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="saldo_intereses" HeaderText="Intereses causados" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:GridView ID="gvReportecierreCdat" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="numero_cdat" HeaderStyle-CssClass="gridHeader" Visible="false"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Oficina"/>
                            <asp:BoundField DataField="cod_persona" HeaderText="Código Persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación de la persona">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="apellidos" HeaderText="Apellidos cliente -- PRINCIPAL">
                                <ItemStyle HorizontalAlign="left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Nombres cliente -- PRINCIPAL">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cdat" HeaderText="Número de la cuenta">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Cód Cdat">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_fisico" HeaderText="Número. cdat">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea_cdat" HeaderText="Línea de cdat">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fecha de Apertura">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="fecha_inicio" DataFormatString="{0:d}" HeaderText="Fecha de Inicio">
                                <ItemStyle HorizontalAlign="center" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="valor" DataFormatString="{0:C}" HeaderText="Valor">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_periodicidad" HeaderText="periodicidad de interes">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa de interés" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           <asp:BoundField DataField="fecha_intereses" HeaderText="Fecha de Interés" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="intereses" HeaderText="Intereses causados" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="left" Width="90" />
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

</asp:Content>

