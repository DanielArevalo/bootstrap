<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register src="../../../General/Controles/imprimir.ascx" tagname="imprimir" tagprefix="ucImprimir" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
<br/><br/>
<div id="gvDiv">
       

     <table cellpadding="5" cellspacing="0" style="width: 100%">
     <tr>
       
           
           
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align:left; width: 7%;">
                Fecha de Arqueo <br/>
                <asp:TextBox ID="txtFechaArqueo" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td style="text-align:left; width: 7%;">
                Oficina <br/>
                <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox"></asp:TextBox>
            </td>
             <td style="text-align:left; width: 7%;">
                Caja<br/>
                <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="10%" style="text-align:left; width: 0%;">
                Cajero <br/>
                <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox"></asp:TextBox>
            </td>
            <td width="10%" style="text-align:left; width: 5%;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left; width: 7%;">
                &nbsp;</td>
            <td style="text-align:left; width: 7%;">
            <div id="DivButtons" >
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Arqueo" 
                        onclick="btnImprimir_Click"/>
                        </div>
            </td>
             <td style="text-align:left; width: 7%;">
                 &nbsp;</td>
            <td width="10%" style="text-align:left; width: 0%;">
                &nbsp;</td>
            <td width="10%" style="text-align:left; width: 5%;">
                &nbsp;</td>
        </tr>
    </table> 
        <table cellpadding="5" cellspacing="0" style="width: 100%; text-align:center">
        <tr> 
            <td colspan="2"  style="text-align: center; color: #FFFFFF; background-color: #0066FF">
                Saldos
            </td>
        </tr>
        <tr> 
            <td colspan="2">
                 <asp:GridView ID="gvSaldos" runat="server" AutoGenerateColumns="false" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="538px">
                <AlternatingRowStyle BackColor="White" />
                <columns>
                    <asp:BoundField DataField="cod_cajero" HeaderStyle-CssClass="gridColNo" 
                        ItemStyle-CssClass="gridColNo" />
                    <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                    <asp:BoundField DataField="concepto" HeaderText="Concepto"  >
                     <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="efectivo" DataFormatString="{0:N0}" 
                        HeaderText="Efectivo">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cheque" DataFormatString="{0:N0}" 
                        HeaderText="Cheque">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="total" DataFormatString="{0:N0}" HeaderText="Total">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            </td>
        </tr>
    </table>                                      
    </div>
</asp:Content>