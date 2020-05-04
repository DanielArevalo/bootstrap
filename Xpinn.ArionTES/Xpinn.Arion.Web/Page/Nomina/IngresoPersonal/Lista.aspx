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
                <label>Código Ingreso</label>
                <br />
                <asp:TextBox ID="txtCodigoIngreso" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <label>Identificación</label>
                <br />
                <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                                <label>Nómina</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlNomina" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td style="text-align: center; width: 200px; padding: 10px">
                <label>Centro de Costo</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlCentroCosto" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
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
<%--    <div style="overflow-x: scroll; max-width: 1205px;">--%>
        <table border="0" style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvIngresos" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvIngresos_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvIngresos_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvIngresos_RowDeleting"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="true">
                                <ItemStyle Width="16px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>

<ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Ingreso">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaingreso" DataFormatString="{0:d}" HeaderText="Fecha Ingreso">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechainicioperiodoprueba" DataFormatString="{0:d}" HeaderText="Fecha Inicio Prueba">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaterminacionperiodoprueba" DataFormatString="{0:d}" HeaderText="Fecha Final Prueba">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_nomina" HeaderText="Nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_centro_costo" HeaderText="Centro de Costo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_estaactivocontrato" HeaderText="Estado">
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
    <%--</div>--%>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
