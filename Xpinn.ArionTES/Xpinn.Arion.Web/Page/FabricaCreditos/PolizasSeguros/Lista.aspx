<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 
        <table style="width:83%; margin-right: 0px;">
            <tr>
                <td style="height: 93px; width: 654px;">
              <asp:Panel ID="pConsulta" runat="server" style="margin-bottom: 0px" Width="759px">
                    <table cellpadding="5" cellspacing="0" style="width: 100%; height: 64px;">
                        <tr>
                            <td class="tdI" style="height: 39px; text-align: center;" colspan="3">
                                <strong>
                                <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                                </strong>
                            </td>
                            
                        </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 764px">
                                    <asp:Label ID="Label1" runat="server" 
                                        style="text-align: center; color: #FFFFFF; background-color: #359AF2" 
                                        Text=" Seleccione una Opción" Width="109%"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 764px">
                                    &nbsp;<asp:RadioButton ID="RadioNuevo" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="RadioNuevo_CheckedChanged" Text="Nuevo" 
                                        TextAlign="Left" />
                                    <asp:RadioButton ID="RadioModificar" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="RadioModificar_CheckedChanged" Text="Modificar" 
                                        TextAlign="Left" />
                                </td>
                            </tr>
                        </table>
                          <table style="width:100%;">
                        <tr>
                            <td class="tdI" style="height: 39px; width: 223px;">
                                <asp:Label ID="LabelNúmero_Radicación" runat="server" Text="Número Radicación"></asp:Label>
                                <br />
                                <asp:TextBox ID="TxtNumeroRadicación" runat="server" CssClass="textbox" 
                                    Width="189px"></asp:TextBox>
                                <strong>
                                <asp:CompareValidator ID="cvVALOR3" runat="server" 
                                    ControlToValidate="TxtNumeroRadicación" Display="Dynamic" 
                                    ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                                &nbsp;</strong></td>
                            <td class="tdI" style="height: 39px; width: 175px;">
                                <strong><asp:Label ID="LabelCódigo_poli" runat="server" Text="Código Póliza"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCodigoPoliza" runat="server" CssClass="textbox" 
                                    Width="146px"></asp:TextBox>
                                <asp:CompareValidator ID="cvVALOR" runat="server" 
                                    ControlToValidate="txtCodigoPoliza" Display="Dynamic" 
                                    ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                                </strong>
                            </td>
                        </tr>
                                </table>
                                  <table style="width:92%;">
                                 
                                          <tr>
                                          <td style="width: 108px">
                                              </td>
                                              <td style="height: 39px; width: 350px;" align="left">
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label 
                                                      ID="LabelNombres" runat="server" Text="Nombres"></asp:Label>
                                                  <br />
                                                  <asp:TextBox 
                                                      ID="TxtPrimerNombre" runat="server" CssClass="textbox" 
                                                      ontextchanged="TxtPrimerNombre_TextChanged" Width="550px"></asp:TextBox>
                                              </td>
                                          </tr>
                                    
                    </table>
                </asp:Panel>
                </td>

            </tr>
            <tr>
                <td style="height: 25px; width: 654px;">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                        
                    
                <asp:GridView ID="gvPolizasSeguros" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical"                          
                        onrowediting="gvPolizasSeguros_RowEditing" 
                        onselectedindexchanged="gvPolizasSeguros_SelectedIndexChanged" 
                        onpageindexchanging="gvPolizasSeguros_PageIndexChanging" 
                        style="margin-right: 0px">
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
                        <asp:BoundField DataField="cod_poliza" HeaderText="Código" />
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Núm. Radic" />
                        <asp:BoundField DataField="nombre_deudor" HeaderText="Nombre Deudor" />
                        <asp:BoundField DataField="monto_desembolsado" 
                            HeaderText="Vr. Credito" />
                        <asp:BoundField DataField="valor_prima_mensual" HeaderText="Vr. Prima" />
                        <asp:BoundField DataField="valor_prima_total" HeaderText="Vr. Total " />
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
                </asp:View>
               <asp:View ID="View2" runat="server">
                        
                    
                <asp:GridView ID="gvPolizassinSeguros" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                        style="margin-right: 0px" 
                        onpageindexchanging="gvPolizassinSeguros_PageIndexChanging" 
                       onselectedindexchanged="gvPolizassinSeguros_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="numero_radicacion" 
                            DataNavigateUrlFormatString="Nuevo.aspx?idRadica={0}" 
                            DataTextField="numero_radicacion" HeaderText="Número Crédito" />
                        <asp:BoundField DataField="cod_asegurado" HeaderText="Código Cliente" />
                        <asp:BoundField DataField="identificacion" 
                            HeaderText="Identificación Cliente" />
                        <asp:BoundField DataField="nombre_deudor" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="nombre_asesor" HeaderText="Asesor" />
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
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
                </asp:View>
                </asp:MultiView>
                 </td>
            </tr>
            
            <tr>
                <td style="width: 654px">
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
             document.getElementById('cphMain_txtCodigoPoliza').focus();
         }
         window.onload = SetFocus;
     </script>
</asp:Content>
