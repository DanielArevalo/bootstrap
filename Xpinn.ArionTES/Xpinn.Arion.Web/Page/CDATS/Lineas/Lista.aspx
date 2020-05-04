<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="0" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" 
                    AutoGenerateColumns="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                   OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                   OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                   OnRowDataBound="gvLista_RowDataBound"  DataKeyNames="cod_lineacdat" 
                   style="font-size: x-small">
                   <Columns>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco" Visible="False">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                   ToolTip="Detalle" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                   ToolTip="Editar" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           <ItemTemplate>
                               <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                   ToolTip="Eliminar" Width="16px" />
                           </ItemTemplate>
                           <HeaderStyle CssClass="gridIco"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:BoundField DataField="cod_lineacdat" HeaderText="Cod.Línea" >
                           <ItemStyle HorizontalAlign="Left" Width="50px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="descripcion" HeaderText="Fescripcion" >
                           <ItemStyle HorizontalAlign="Left" Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_calculo_tasa" HeaderText="Forma Tasa" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_tipo_tasa" HeaderText="Tipo de Tasa" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="tasa" HeaderText="tasa" DataFormatString="{0:N2}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="tipo_historico" HeaderText="Tipo Histórico" DataFormatString="{0:N0}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="desviacion" HeaderText="Desviacion" DataFormatString="{0:N2}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="nom_moneda" HeaderText="Moneda" DataFormatString="{0:N0}" >
                           <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
