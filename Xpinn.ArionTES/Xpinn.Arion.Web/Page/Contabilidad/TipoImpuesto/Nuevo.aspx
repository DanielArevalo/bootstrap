<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipo de Impuesto :." %>

<%@ Register TagPrefix="cc1" Namespace="Xpinn.Util" Assembly="Xpinn.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">Código<br />
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                            </td>
                            <td class="tdD">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">Nombre<br />
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128"
                                    Width="574px" />
                                <br />
                            </td>
                            <td class="tdD">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">Descripción<br />
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128"
                                    Width="574px" />
                                <br />
                            </td>
                            <td class="tdD">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                <label>Impuesto Principal</label>
                                <asp:CheckBox ID="chkPrincipal" runat="server" OnChange ="principal(this);" />
                                <br />
                            </td>
                            <td class="tdD">
                                <br />
                            </td>
                        </tr>
                        <asp:GridView ID="gvActEmpresa" runat="server" Width="35%" AutoGenerateColumns="False" Style="margin-left: 20pc;"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" >
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Codigo" runat="server" Text='<%# Bind("cod_tipo_impuesto") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Impuesto">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Nombre" runat="server" Text='<%# Bind("nombre_impuesto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Secundario">
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chkSecundario" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                        <tr>
                            <td class="tdI" >

                                
                            </td>
                            <td class="tdD">
                                <br />
                            </td>
                        </tr>


                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <script>
        function principal(e) {
            if ($(e).find("#cphMain_chkPrincipal").is(":checked")) {
                $("#cphMain_gvActEmpresa").hide();
            } else {
                $("#cphMain_gvActEmpresa").show();
            }

        }
    </script>
</asp:Content>
