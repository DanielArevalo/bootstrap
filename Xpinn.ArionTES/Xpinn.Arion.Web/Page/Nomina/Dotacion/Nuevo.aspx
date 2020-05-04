<%@ Page Title=".: Empleados :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagPrefix="uc1" TagName="ctlFecha" %>
<%@ Register Src="~/General/Controles/ctlCentroCosto.ascx" TagPrefix="uc2" TagName="ctlCentroCosto" %>
<%@ Register Src="~/General/Controles/ctlResponsable.ascx" TagPrefix="uc1" TagName="ctlResponsable" %>
<%@ Register Src="~/General/Controles/ctlPersonaEd.ascx" TagPrefix="uc1" TagName="ctlPersonaEd" %>
<%@ Register Src="~/General/Controles/ctlListarEmpleados.ascx" TagName="ctlListarEmpleados" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewEmpleados">
            <uc1:ctlListarEmpleados ID="ctlListarEmpleados" OnEmpleadoSeleccionado="ctlListarEmpleados_OnEmpleadoSeleccionado" OnErrorControl="ctlListarEmpleados_OnErrorControl" runat="server" />
        </asp:View>
        <asp:View runat="server" ID="viewDatos">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">

                <tr>
                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos De Dotación</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="panelpersona">
                            <table>
                                <tr>
                                    <td style="" class="space">
                                        <label>Identificación</label>
                                    </td>
                                    <td style="text-align: left" class="space">
                                        <asp:TextBox runat="server" ID="txtIdentificacion" Enabled="false" CssClass="textbox" />
                                    </td>
                                    <td style="" class="space">
                                        <label>Tipo Identificación</label>
                                    </td>
                                    <td style="text-align: left" class="space">
                                        <asp:DropDownList runat="server" ID="ddlTipoIdentificacion" Enabled="false" CssClass="dropdown" Width="80%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="" class="space">
                                        <label>Nombre Empleado</label>
                                    </td>
                                    <td style="text-align: left" class="space" colspan="2">
                                        <asp:TextBox runat="server" ID="txtNombreEmpleado" Width="80%" CssClass="textbox" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server">
                            <table>
                                <tr>
                                    <td style="width: 200px; text-align: left">Consecutivo:
                                   <br />
                                        <asp:TextBox ID="txtconsecutivo" runat="server" CssClass="textbox" Enabled="false" Width="155px"></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; text-align: left">Código Del Empleado:
                                   <br />
                                        <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" Enabled="false" Width="155px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">Centro De Costos:
                                   <br />
                                        <uc2:ctlCentroCosto runat="server" ID="ctlCentroCosto" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Cantidad:
                                   <br />
                                        <asp:TextBox ID="txtcantidad" runat="server" CssClass="textbox" ValidationGroup="return isString(event)"   onkeypress="return isNumber(event)"  Width="143px"></asp:TextBox>
                                    </td>

                                    <td style="text-align: left">Ubicación
                                   <br />
                                        <asp:TextBox ID="txtubicacion" runat="server" CssClass="textbox" ValidationGroup="return isString(event)"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>

                    <td>
                        <br />
                        <asp:Panel runat="server">
                            <table runat="server" width="100%">
                                <tr>
                                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                                        <strong>Detalle De Dotación</strong>
                                    </td>
                                </tr>

                                <tr>

                                    <td>
                                        <br />
                                        <asp:GridView
                                            ID="gvdatos"
                                            runat="server"
                                            Width="100%"
                                            TabIndex="50"
                                            AutoGenerateColumns="False"
                                            CellPadding="4"
                                            DataKeyNames="id_detalle_dotacion"
                                            ShowFooter="True"
                                            Style="font-size: xx-small"
                                            OnRowCommand="gvdatos_RowCommand" 
                                            OnRowDeleting="gvdatos_RowDeleting"
                                            ShowHeaderWhenEmpty="True">
                                            <Columns>
                                                <asp:TemplateField  HeaderStyle-CssClass="gridIco">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("id_detalle_dotacion") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnadd" runat="server" CommandName="AddNew" Text="Agregar" CssClass="btn8" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Delete"
                                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Detalle" Width="16px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Descripción" SortExpression="descripcion" HeaderStyle-CssClass="gridIco">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("descripcion") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Característica" SortExpression="caracteristica" HeaderStyle-CssClass="gridIco">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("caracteristica") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtcaracteristica" runat="server" CssClass="textbox"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("caracteristica") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valor" SortExpression="valor" InsertVisible="False" HeaderStyle-CssClass="gridIco">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("valor") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtvalor" runat="server" onkeypress="return isNumber(event)" CssClass="textbox"></asp:TextBox>
                                                    </FooterTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("valor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <FooterStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="viewGuardado">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Información  Guardada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
