<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 1100px" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                <label>Código Nomina</label>
                <br />
                <asp:TextBox ID="txtCodigoNomina" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <label>Nombre Nómina</label>
                <br />
                <asp:TextBox ID="txtNombreNomina" CssClass="textbox" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                                <label>Oficina</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlOficina" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td style="text-align: center; width: 200px; padding: 10px">
                &nbsp;<br />
                <asp:DropDownList runat="server" ID="ddlTipoContrato" Visible="False" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td style="text-align: center; width: 150px; padding: 10px">
                <%--                <label>Activo</label>
                <br />
                <asp:CheckBoxList ID="checkActivo" runat="server" Width="100%" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Si" Value="1" onclick="ExclusiveCheckBoxList(this);" />
                    <asp:ListItem Text="No" Value="0" onclick="ExclusiveCheckBoxList(this);" />
                </asp:CheckBoxList>--%>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="overflow-x: scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>
                    <asp:GridView ID="gvNominas" runat="server" Width="82%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvNominas_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvNominas_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvNominas_RowDeleting"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                <ItemStyle Width="16px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>

<ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Nombre Nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigooficina" HeaderText="Cod.Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_tipo_nomina" HeaderText="Tipo Nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigotipocontrato" HeaderText="Cod.Tipo Contrato" Visible="False">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_tipo_contrato" HeaderText="Tipo Contrato" Visible="False">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                               <asp:BoundField DataField="permite_anticipos_mostrar" HeaderText="Permite Anticipos">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                               <asp:BoundField DataField="permite_anticipos_sub_mostrar" HeaderText="Permite Ant. Subs. Transporte">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                        </Columns>

<HeaderStyle CssClass="gridHeader"></HeaderStyle>

<PagerStyle CssClass="gridPager"></PagerStyle>

<RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>

                    <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
