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
                <td>
                    <asp:CheckBox Text="ordenar la fecha de corte" ID="chkOrden" Checked="false" AutoPostBack="true" OnCheckedChanged="chkOrden_CheckedChanged" runat="server" />
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
                    <asp:GridView ID="gvReportecierreAporte" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="numero_aporte" HeaderStyle-CssClass="gridHeader"
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
                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos Cliente">
                                <ItemStyle HorizontalAlign="left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_aporte" HeaderText="Número de Aporte">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_linea_aporte" HeaderText="Cód Línea Aporte">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea_aporte" HeaderText="Línea de aporte">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                             <asp:BoundField DataField="cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Saldo" DataFormatString="{0:C}" HeaderText="Saldo al cierre">
                                <ItemStyle ForeColor="#009999" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fecha de proximo pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="Fecha Ultimo Pago" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad">
                                <ItemStyle HorizontalAlign="center" />
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
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

</asp:Content>

