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
                <label>Código Inactividad</label>
                <br />
                <asp:TextBox ID="txtCodigoInactividad" CssClass="textbox" onkeypress="return isNumber(event)" runat="server" Width="160px">
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
                   
                </asp:DropDownList>
            </td>
            <td style="width: 150px; height: 51px;" class="logo">&nbsp;
                <label>Remunerada?</label>
                <br />
                <asp:CheckBoxList ID="checkRemunerado" runat="server" Width="100%" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Si" Value="1" onclick="ExclusiveCheckBoxList(this);" />
                    <asp:ListItem Text="No" Value="0" onclick="ExclusiveCheckBoxList(this);" />
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Tipo Inactividad</label>
                <asp:DropDownList runat="server" ID="ddlTipo" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                    <asp:ListItem Text="Paga Todo la empresa" Value="1" />
                    <asp:ListItem Text="Paga dos dias la emp. y resto porcentaje" Value="2" />
                    <asp:ListItem Text="Utiliza porcentaje todos los dias" Value="3" />
                    <asp:ListItem Text="Porcentaje 50%" Value="4" />
                    <asp:ListItem Text="Prorroga" Value="5" />
                    <asp:ListItem Text="> Mas 180 días" Value="6" />
                </asp:DropDownList>
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Concepto Inactividad</label>
                <asp:DropDownList runat="server" ID="ddlClase" CssClass="dropdown" Width="90%">
                    
                </asp:DropDownList>
            </td>
            <td align="left" class="logo" style="width: 197px; height: 51px; text-align: center">
                <label>Contrato</label>
                <asp:DropDownList runat="server" ID="ddlContrato" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                </asp:DropDownList>
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
        </tr>
    </table>
    <br />
    <br />
    <div style="overflow-x: scroll; max-width: 1205px;">
        <table border="0" style="width: 1200px">
            <tr>
                <td>
                    <asp:GridView ID="gvInactividades" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvInactividades_PageIndexChanging" AllowPaging="True" OnSelectedIndexChanged="gvInactividades_SelectedIndexChanged"
                        OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvInactividades_RowDeleting"
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
                            <asp:BoundField DataField="consecutivo" HeaderText="Cod. Inactividad">
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
                            <asp:BoundField DataField="fechainicio" DataFormatString="{0:d}" HeaderText="Fecha Inicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaterminacion" DataFormatString="{0:d}" HeaderText="Fecha Terminación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_inactividad_remunerada" HeaderText="Inactividad Remunerada">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias" HeaderText="Dias">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_concepto" HeaderText="Concepto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_tipo" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="desc_contrato" HeaderText="Contrato">
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
