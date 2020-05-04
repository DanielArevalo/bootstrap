<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="85%">
            <tr>
                <td class="tdI" style="text-align:left;">
                    Código<br/>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                </td>
                <td class="tdD" style="text-align:left">
                    Identificación<br/>
                    <asp:TextBox ID="txtIdentific" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="120px" />
                </td>
                <td class="tdD" style="text-align:left">
                    Num.Radic<br/>
                    <asp:TextBox ID="txtNumRadic" runat="server" CssClass="textbox" 
                        MaxLength="128" Width="120px" />
                    <asp:FilteredTextBoxExtender ID="txtNumRadic_FilteredTextBoxExtender" runat="server" Enabled="True" 
                        FilterType="Numbers, Custom" TargetControlID="txtNumRadic" ValidChars="." />
                </td>
                <td class="tdD" style="text-align:left">
                    Fecha Registro<br />
                    <uc1:fecha ID="txtFechaRegistro" runat="server"></uc1:fecha>                
                </td>
                <td class="tdD" style="text-align:left">
                    Fecha Giro<br />
                    <uc1:fecha ID="txtFechaGiro" runat="server"></uc1:fecha>                
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
            onclick="btnExportar_Click" Text="Exportar a Excel" />
        <br />
        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="idgiro" 
            style="font-size: x-small">
            <Columns>
                <asp:BoundField DataField="idgiro" HeaderText="No." >
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_persona" HeaderText="Cod.Per" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="identificacion" HeaderText="Identific." >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fec_giro" HeaderText="Fecha Giro" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="num_comp" HeaderText="No.Comp" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="detalle" HeaderText="Detalle" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo" HeaderText="T.Mov" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="valor_comprobante" HeaderText="Valor Contable" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_ope_realiza" HeaderText="Cod.Ope Realiza" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="fec_realiza" HeaderText="Fecha Realiza" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="usu_realiza" HeaderText="Usu.Realiza" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cod_cuenta_realiza" HeaderText="Cod.Cuenta Realiza" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="valor_realiza" HeaderText="Valor Realiza" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="gridHeader" />
            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
            <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
            Visible="False" />
    <br />
</asp:Content>
