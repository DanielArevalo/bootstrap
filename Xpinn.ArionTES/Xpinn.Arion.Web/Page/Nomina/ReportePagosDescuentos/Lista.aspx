<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 549px;" cellspan="1">
        <tr>
            <td style="width: 390px; height: 51px;" class="logo">
                <label>Identificación</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlProveedor" AppendDataBoundItems="true" CssClass="dropdown" Width="400px">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td style="width: 68%; height: 51px;" class="logo">
                <label>Concepto</label>
                <asp:DropDownList runat="server" ID="ddlConcepto" AppendDataBoundItems="true" CssClass="dropdown" Width="400px">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="logo" style="width: 390px; height: 51px; text-align: center">
                <label>
                Fecha</label>
                <br />
                <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" MaxLength="10" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" PopupButtonID="imagenCalendario" TargetControlID="txtFechaIni">
                </asp:CalendarExtender>
                <img id="imagenCalendario0" alt="Calendario" src="../../../Images/iconCalendario.png" />
            </td>
            <td style="text-align: center; width: 65%; padding: 10px">
                <label>Fecha</label>
                <br />
                <asp:TextBox ID="txtFechaFin" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                    PopupButtonID="imagenCalendario"
                    TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imagenCalendario" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </td>
        </tr>
    </table>
                    <asp:GridView ID="gvPagos" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvPagos_PageIndexChanging" AllowPaging="True"
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
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Pago">
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
                            <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorcuota" HeaderText="Valor Cuota" DataFormatString="${0:#,##0.00}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valortotal" HeaderText="Valor Total" DataFormatString="${0:#,##0.00}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_centro_costo" HeaderText="Centro de Costo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_descuento_periocidad" HeaderText="Descuento en periocidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_concepto_nomina" HeaderText="Concepto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

    <br />
    <br />
    <div style="overflow-x:scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>

                    <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </asp:Content>
