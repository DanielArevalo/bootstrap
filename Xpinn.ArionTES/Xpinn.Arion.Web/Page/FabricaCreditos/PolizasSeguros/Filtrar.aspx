<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Filtrar.aspx.cs" Inherits="Filtrar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
 
        <table style="width:100%;">
            <tr>
                <td>
              <asp:Panel ID="pConsulta" runat="server" style="margin-bottom: 0px">
                    <table cellpadding="5" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="tdI" style="height: 51px; text-align: center;" colspan="2">
                                <strong>
                                <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                                </strong>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="tdI" style="height: 51px; width: 339px;">
                                Identificación&nbsp; Cliente<br />
                                <asp:TextBox ID="txtCedulaCliente" runat="server" CssClass="textbox" 
                                    Width="214px"></asp:TextBox>
                                <strong>
                                <asp:CompareValidator ID="cvVALOR" runat="server" 
                                    ControlToValidate="txtCedulaCliente" Display="Dynamic" 
                                    ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                                </strong>
                            </td>
                            <td class="tdI" style="height: 51px; font-weight: 700;">
                                <span style="font-weight: normal">Número Radicación</span><br />
                                <asp:TextBox ID="TxtNumeroRadicacion" runat="server" CssClass="textbox" 
                                    Width="214px"></asp:TextBox>
                                <asp:CompareValidator ID="cvVALOR2" runat="server" 
                                    ControlToValidate="TxtNumeroRadicacion" Display="Dynamic" 
                                    ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="height: 59px; width: 339px;">
                                Primer<span style="font-weight: normal"> Nombre</span><br />
                                <asp:TextBox ID="TxtPrimerNombre" runat="server" CssClass="textbox" 
                                    Width="214px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="height: 59px; font-weight: 700;">
                                <span style="font-weight: normal">Segundo Nombre</span><br />&nbsp;<asp:TextBox 
                                    ID="TxtSegundoNombre" runat="server" CssClass="textbox" Width="214px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="height: 55px; width: 339px;">
                                Primer Apellido<br />&nbsp;<asp:TextBox ID="TxtPrimerApellido" runat="server" 
                                    CssClass="textbox" Width="214px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="height: 55px; font-weight: 700;">
                                <span style="font-weight: normal">Segundo Apellido</span><br />
                                <asp:TextBox ID="TxtSegundoApellido" runat="server" CssClass="textbox" 
                                    Width="214px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>

            </tr>
            <tr>
                <td style="height: 25px">
                <asp:GridView ID="gvPolizasSeguros" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                        style="margin-right: 0px" 
                        onpageindexchanging="gvPolizasSeguros_PageIndexChanging">
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
             document.getElementById('cphMain_txtCodigoCliente').focus();
         }
         window.onload = SetFocus;
     </script>
</asp:Content>
