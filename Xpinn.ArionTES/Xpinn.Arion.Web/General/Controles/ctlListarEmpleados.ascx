<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlListarEmpleados.ascx.cs" Inherits="ctlListarEmpleados" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<style type="text/css">
    .auto-style1 {
        width: 15%;
        height: 51px;
    }
</style>

<table border="0" width="90%">
    <tr>
        <td>
            <asp:Label runat="server" ID="lblContrato" Font-Bold="true"/>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tbCriterios" border="0" width="100%">
                <tr>
                    <td class="auto-style1">Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                    </td>
                    <td class="auto-style1">Identificación<br />
                        <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                    </td>
                    <td class="auto-style1">Cod. Empledo<br />
                        <asp:TextBox ID="txtCodEmpleado" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                    </td>
                    <td class="auto-style1">Cod. Persona<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                    </td>
                    <td class="auto-style1">Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" AppendDataBoundItems="true" Width="70%" CssClass="dropdown">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <%--<td style="width: 25%">Tipo Contrato<br />
                        <asp:DropDownList ID="ddlTipoContrato" runat="server" AppendDataBoundItems="true" Width="70%" CssClass="dropdown">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>--%>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvLista"
                runat="server"
                AllowPaging="True"
                AutoGenerateColumns="False"
                GridLines="Horizontal"
                PageSize="20"
                HorizontalAlign="Center"
                ShowHeaderWhenEmpty="false"
                Width="100%"
                OnPageIndexChanging="gvLista_PageIndexChanging"
                OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                DataKeyNames="consecutivo"
                Visible="false"
                Style="font-size: x-small">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="consecutivo" HeaderText="Cod.Empleado" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="profesion" HeaderText="Profesion" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="celular" HeaderText="Celular" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <label>Seleccionar</label> <br />
                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false"
                                OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged" AutoPostBack="True" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSeleccionEmpleado" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <asp:Label ID="lblTotalRegs" Visible="false" runat="server" />
        </td>
    </tr>
</table>
<uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
