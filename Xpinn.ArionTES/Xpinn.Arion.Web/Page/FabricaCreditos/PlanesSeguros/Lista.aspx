<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server"> 
   
     <table style="width:100%;">
            <tr>
                <td>
              <asp:Panel ID="pConsulta" runat="server" style="margin-bottom: 0px">
                    <table cellpadding="5" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="tdI" style="height: 55px">
                                &nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td class="tdI" style="height: 55px">
                                Código Plan<br />
                                <asp:TextBox ID="txtCodigoPlan" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>

            </tr>
            <tr>
                <td style="height: 25px">
                <asp:GridView ID="gvPlanesSeguros" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                        onselectedindexchanged="gvPlanesSeguros_SelectedIndexChanged" 
                        onpageindexchanging="gvPlanesSeguros_PageIndexChanging" 
                        onrowediting="gvPlanesSeguros_RowEditing">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="tipo_plan" HeaderText="Código" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="prima_individual" HeaderText="Vr. Prima Vida Ind." />
                        <asp:BoundField DataField="prima_conyuge" HeaderText="Vr. Prima Vida Cony." />
                        <asp:BoundField DataField="prima_accidentes_individual" 
                            HeaderText="Vr. Prima Accid. Ind." />
                        <asp:BoundField DataField="prima_accidentes_familiar" 
                            HeaderText="Vr. Prima Vida Accid. Fam." />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                </td>
            </tr>
        </table>
        <br />
      <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigoPlan').focus();
        }
        window.onload = SetFocus;
     </script>
</asp:Content>

