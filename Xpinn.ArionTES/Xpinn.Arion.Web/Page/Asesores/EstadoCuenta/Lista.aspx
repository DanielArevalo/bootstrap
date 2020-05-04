<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="AseMoviGralEstadoCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">

                    <table cellpadding="5" cellspacing="0" style="width: 100%" border="0">

                        <tr>
                            <td style="height: 15px; text-align: center;" >Código Cliente<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td style="height: 15px; text-align: center;">Número Identificación Cliente<br />
                                <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align: center">Código de nómina<br />
                                <asp:TextBox ID="txtCodNomina" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI" style="text-align: center; width: 205px;">Primer Nombre Cliente
                                <br />
                                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align: center">Segundo NombreCliente<br />
                                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align: center">Primer Apellido Cliente<br />
                                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align: center">Segundo Apellido Cliente<br />
                                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox"
                                    Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLstMoviGralCredito" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnRowCommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdPersona" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Estado Cuenta" CommandName="EstadoCuenta" CommandArgument='<%#Eval("IdPersona")%>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="pIdentificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="pNombre" HeaderText="Primer Nombre" />
                        <asp:BoundField DataField="sNombre" HeaderText="Segundo Nombre" />
                        <asp:BoundField DataField="pApellido" HeaderText="Primer Apellido" />
                        <asp:BoundField DataField="sApellido" HeaderText="Segundo Apellido" />
                        <asp:BoundField DataField="codNomina" HeaderText="Código de nómina" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>

    </table>
</asp:Content>