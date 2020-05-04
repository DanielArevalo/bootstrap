<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <table border="0" style="height: 84px; width: 1050px" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                <label>Código Pago</label>
                <br />
                <asp:TextBox ID="txtCodigoPago" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
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
            <td style="width: 209px; height: 51px;" class="logo">&nbsp;
                <label>Valor</label>
                <br />
                <asp:TextBox ID="txtValorCuota" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="80%">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Centro Costo</label>
                <asp:DropDownList runat="server" ID="ddlCentroCosto" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Concepto</label>
                <asp:DropDownList runat="server" ID="ddlConcepto" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Descuento en periocidad</label>
                <asp:DropDownList runat="server" ID="ddlDescuentoPeriocidad" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                    <asp:ListItem Text="1er Periodo" Value="1" />
                    <asp:ListItem Text="2do Periodo" Value="2" />
                    <asp:ListItem Text="3er Periodo" Value="3" />
                    <asp:ListItem Text="4to Periodo" Value="4" />
                    <asp:ListItem Text="Todos los Periodos" Value="5" />
                </asp:DropDownList>
            </td>
            <td style="text-align: center; width: 20%; padding: 10px">
                <label>Fecha</label>
                <br />
                <asp:TextBox ID="txtFecha" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                    PopupButtonID="imagenCalendario"
                    TargetControlID="txtFecha"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imagenCalendario" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="overflow-x:scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>
                    <asp:GridView ID="gvPagos" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvPagos_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvPagos_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvPagos_RowDeleting"
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

                    <asp:Label ID="lblNumeroRegistros" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
