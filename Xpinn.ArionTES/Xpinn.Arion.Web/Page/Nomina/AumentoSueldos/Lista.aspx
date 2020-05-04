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
                <label>Código Aumento</label>
                <br />
                <asp:TextBox ID="txtCodigoAumento" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
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
            <td style="text-align: center; width: 150px; padding: 10px">
                <label>Fecha Cambio</label>
                <br />
                <asp:TextBox ID="txtFechaCambio" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                    PopupButtonID="imagenCalendario"
                    TargetControlID="txtFechaCambio"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imagenCalendario" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </td>
        </tr>
        <tr>
            <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                <asp:Button ID="btnCargarDatos" runat="server" CssClass="btn8" OnClick="btnCargarDatos_Click" OnClientClick="btnCargarDatos_Click" Text="Cargar Datos" Width="150px" />
            </td>
            <td style="width: 209px; height: 51px;" class="logo">
                &nbsp;</td>
            <td style="width: 209px; height: 51px;" class="logo">&nbsp;</td>
            <td style="text-align: center; width: 150px; padding: 10px">
                &nbsp;</td>
        </tr>
    </table>
    <div style="overflow-x: scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>
                    <asp:GridView ID="gvAumentos" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvAumentos_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvAumentos_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvAumentos_RowDeleting"
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
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Aumento">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="consecutivo_empleado" HeaderText="Cod.Empleado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorparaaumentar" HeaderText="Aumento Valor" DataFormatString="${0:#,##0.00}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="porcentajeaumentar" HeaderText="Aumento (%)">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nuevosueldo" HeaderText="Sueldo con Aumento" DataFormatString="${0:#,##0.00}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha Cambio">
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
