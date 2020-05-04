<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
     <tr>
        <td width="35%">
            Fecha de Recepción <br/>
            <asp:TextBox ID="txtFechaRecepcion" runat="server" enabled="false" CssClass="textbox" 
                MaxLength="10"  Width="70"></asp:TextBox>
        </td>
         <td>
            Oficina <br/>
            <asp:TextBox ID="txtOficina" runat="server" Enabled="False" CssClass="textbox" Width="260px"></asp:TextBox>
        </td>
    </tr>
     <tr>   
          <td class="tdI">
            Caja<br/>
            <asp:TextBox ID="txtCaja" runat="server" Enabled="False" CssClass="textbox" Width="260px"></asp:TextBox>
        </td>
         <td class="tdI">
            Cajero <br/>
            <asp:TextBox ID="txtCajero" runat="server" Enabled="False" CssClass="textbox" Width="260px"></asp:TextBox>
        </td>
    </tr>
    <tr>    
        <td colspan="2">
            <asp:GridView ID="gvTraslados" runat="server" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                ForeColor="Black" AutoGenerateColumns="false" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <columns>
                  <asp:BoundField DataField="cod_traslado" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                  <asp:BoundField DataField="cod_moneda" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                  <asp:BoundField DataField="fecha_traslado" HeaderText="Fecha" DataFormatString="{0:d}" />
                  <asp:BoundField DataField="nomoficina_ori" HeaderText="Oficina" />
                  <asp:BoundField DataField="nomcaja_ori" HeaderText="Caja" />
                  <asp:BoundField DataField="nomcajero_ori" HeaderText="Cajero" /> 
                  <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" />
                  <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}" >
                    <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                    <asp:TemplateField HeaderText="Recibe">
                         <ItemTemplate>
                            <asp:CheckBox ID="chkRecibe" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
</asp:Content>
