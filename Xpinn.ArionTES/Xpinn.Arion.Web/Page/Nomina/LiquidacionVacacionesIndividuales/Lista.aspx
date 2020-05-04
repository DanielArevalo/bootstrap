<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" style="height: 84px; width: 1100px" cellspan="1">
        <tr>
            <td style="width: 209px; height: 51px; text-align: center;" class="logo">
                <label>Código Liquidación Individual</label>
                <br />
                <asp:TextBox ID="txtCodigoVacacionIndividual" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
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
                <label>Nómina</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlNomina" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Código Empleado</label>
                <asp:TextBox ID="txtCodigoEmpleado" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
                </asp:TextBox>
            </td>
            <td style="text-align: center; width: 150px; padding: 10px">
                <label>Fecha Inicio</label>
                <br />
                <asp:TextBox ID="txtFechaInicio" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                    PopupButtonID="imagenCalendario"
                    TargetControlID="txtFechaInicio"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imagenCalendario" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </td>
            <td style="text-align: center; width: 150px; padding: 10px">
                <label>Fecha Terminación</label>
                <br />
                <asp:TextBox ID="txtFechaTerminacion" MaxLength="10" CssClass="textbox"
                    runat="server" Width="80%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                    PopupButtonID="imagenCalendarioTerminacion"
                    TargetControlID="txtFechaTerminacion"
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <img id="imagenCalendarioTerminacion" alt="Calendario"
                    src="../../../Images/iconCalendario.png" />
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Centro de Costo</label>
                <br />
                <asp:DropDownList runat="server" ID="ddlCentroCosto" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
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
                    <asp:GridView ID="gvVacaciones" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvVacaciones_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvVacaciones_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvVacaciones_RowDeleting"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="consecutivo" Style="font-size: small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="False">
                                <ItemStyle Width="16px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>

<ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Liquidacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_nomina" HeaderText="Nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_centro_costo" HeaderText="Centro Costos">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechainicio" DataFormatString="{0:d}" HeaderText="Fecha Inicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaterminacion" DataFormatString="{0:d}" HeaderText="Fecha Terminación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="diasliquidados" HeaderText="Dias Liquidados">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorTotalPagar" HeaderText="Valor Pagar" DataFormatString="${0:#,##0.00}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>

                            
                              <asp:BoundField DataField="num_comp" HeaderText="Num. Comp.">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                             <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Com.p">
                                <ItemStyle HorizontalAlign="Left" />
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
