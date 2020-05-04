<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 1000px" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                <label>Código Retroactivo</label>
                <br />
                <asp:TextBox ID="txtCodigoRetroactivo" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                <label>Identificación</label>
                <br />
                <asp:TextBox ID="txtIdentificacion" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                <label>Nombre</label>
                <br />
                <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server" Width="180px">
                </asp:TextBox>
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Concepto</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlConcepto" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                    <asp:ListItem Text="Sueldo" Value="39" />
                    <asp:ListItem Text="Inactividad" Value="23" />
                    <asp:ListItem Text="Cesantias" Value="7" />
                    <asp:ListItem Text="Vacaciones" Value="43" />
                    <asp:ListItem Text="Prima" Value="32" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="overflow-x: scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>
                    <asp:GridView ID="gvRetroactivos" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvRetroactivos_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvRetroactivos_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvRetroactivos_RowDeleting"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                <ItemStyle Width="16px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Retroactivo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigoempleado" HeaderText="Cod. Empleado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechainicio" DataFormatString="{0:d}" HeaderText="Fecha Inicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechafinal" DataFormatString="{0:d}" HeaderText="Fecha Terminación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechapago" DataFormatString="{0:d}" HeaderText="Fecha Pago">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numeropagos" HeaderText="N° Pagos">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_periodo" HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_concepto" HeaderText="Concepto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
