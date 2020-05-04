<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Bloqueo&Desbloqueo:." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 180px; text-align: left">Num.Tarjeta<br />
                <asp:TextBox ID="txtNumTarjeta" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 180px">Identificacion<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 180px">Num.Cuenta<br />
                <asp:TextBox ID="txtNumeroCuenta" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 180px">Estado Actual<br />
                <asp:DropDownList ID="ddlEstadoActual" runat="server" CssClass="textbox" Width="180px" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                    <asp:ListItem Text="Activa" Value="1" />
                    <asp:ListItem Text="Bloqueada" Value="3" />
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 180px">Estado Futuro<br />
                <asp:DropDownList ID="ddlEstadoFuturo" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="180px">
                    <asp:ListItem Text="Seleccione un Item" Value=" " />
                    <asp:ListItem Text="Desbloquear" Value="1" />
                    <asp:ListItem Text="Bloquear" Value="2" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server" Visible="True" ForeColor="#009999" /><br />
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="idtarjeta">
                        <Columns>
                            <asp:BoundField DataField="nombres" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numtarjeta" HeaderText="Num.Tarjeta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_cuenta" HeaderText="Tipo De Cuenta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Dias de Mora" />
                            <asp:BoundField DataField="desc_estado" HeaderText="Estado Actual" />
                            <asp:BoundField DataField="desc_estado_pendiente" HeaderText="Estado Futuro" />
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <label>Seleccionar</label>
                                    <br />
                                    <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="true"
                                        OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged" AutoPostBack="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionEmpleado" runat="server" Checked="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblpendienteParaBloquear" Visible="false" Text='<%# Bind("pendienteParaBloquear") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblpendienteParaDesbloquear" Visible="false" Text='<%# Bind("pendienteParaDesbloquear") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEstado" Visible="false" Text='<%# Bind("estado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblidtarjeta" Visible="True" Text='<%# Bind("idtarjeta") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
