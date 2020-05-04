<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 90%">
                <tr style="text-align: left">
                    <td>
                        <strong>Registro de aumento de Sueldos</strong>
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">Código Aumento
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">
                        <asp:TextBox ID="txtCodigoAumento" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px">Identificación
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtIdentificacionn" Enabled="false" Width="90%" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Tipo Identificación
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" Enabled="false" runat="server" CssClass="textbox"
                                        Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Código Empleado
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">
                                    <asp:TextBox ID="txtCodigoEmpleado" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:HiddenField ID="hiddenCodigoPersona" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px">Nombres y Apellidos
                                </td>
                                <td style="text-align: center; width: 75%; padding: 10px" colspan="5">
                                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox"
                                        Enabled="false" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px">Tipo Nómina
                                </td>
                                <td style="text-align: center; padding: 10px" colspan="2">
                                    <asp:DropDownList ID="ddlTipoNomina" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoNomina_SelectedIndexChanged" CssClass="textbox" Width="90%">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px">Contrato
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px;" colspan="2">
                                    <asp:DropDownList ID="ddlContrato" Enabled="false" runat="server" CssClass="textbox"
                                        Width="97%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: center; padding: 10px">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr />
                        <table cellpadding="5" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="background-color: #359af2; color: white; font-weight: bold; height: 48px;">
                                    <label>Sueldo Anterior</label>
                                </td>
                                <td style="background-color: #359af2; color: white; font-weight: bold; height: 48px;">
                                    <label>Nuevo Sueldo</label>
                                </td>
                                <td style="background-color: #359af2; color: white; font-weight: bold; height: 48px;">
                                    <label>Valor a Aumentar</label>
                                </td>
                                <td style="background-color: #359af2; color: white; font-weight: bold; height: 48px;">
                                    <label>Porcentaje a Aumentar(%)</label>
                                </td>
                                <td style="background-color: #359af2; color: white; font-weight: bold; height: 48px;">
                                    <label>Fecha</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSueldoAnterior" ReadOnly="true" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNuevoSueldo" ReadOnly="true" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtValorAumentar" AutoPostBack="true" onkeypress="return isNumber(event)" OnTextChanged="txtValorAumentar_TextChanged" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPorcentajeAumentar" AutoPostBack="true" onkeypress="return isNumber(event)" OnTextChanged="txtPorcentajeAumentar_TextChanged" Height="22px" />
                                </td>
                                <td style="width: 20%">
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
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos guardados Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

         
    
    <asp:MultiView runat="server" ID="mvCargar">
        <asp:View ID="vwCargaDatos" runat="server">
            <table width="100%">
                <tr>
                    <td class="gridHeader" colspan="2" style="text-align: center"><strong>Seleccione el Archivo</strong> </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; font-size: xx-small; width: 100%"><strong>Tipo de Archivo de Carga : &nbsp;&nbsp;&nbsp; .txt
                        <br />
                        Separador de Campos de Carga : &nbsp;&nbsp;&nbsp; |
                        <br />
                        Estructura de archivo a cargar: &nbsp;&nbsp;&nbsp; Identificación,valor Aumentar,Fecha Aumento</strong>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 40%">Archivo </td>
                    <td style="text-align: left; width: 60%">
                        <asp:FileUpload ID="flpArchivo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblmsjCarga" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnAceptarCarga" runat="server" CssClass="btn8" Height="30px" OnClick="btnAceptarCarga_Click" Text="Aceptar" Width="100px" />
                        &nbsp;&nbsp;&nbsp; &nbsp;<br />
                        <asp:GridView ID="gvaumentos" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                            DataKeyNames="consecutivo" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" 
                     PagerStyle-CssClass      ="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: small" Width="100%">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" Visible="False">
                                <ItemStyle Width="16px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Select" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                    <ItemStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigoempleado" HeaderText="Cod.Empleado">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>

                                 <asp:BoundField DataField="codigopersona" HeaderText="Cod.Persona">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_empleado" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigonomina" HeaderText="Nómina">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="codigotipocontrato" HeaderText="Tipo Contrato">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="sueldoanterior" HeaderText="Sueldo Anterior">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="nuevosueldo" HeaderText="Nuevo Sueldo">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="valorparaaumentar" HeaderText="Valor a Aumentar">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="porcentajeaumentar" HeaderText="Porcentaje Aumento">
                                <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>

                               
                             
                               
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />

                               
                             
                               
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    
</asp:Content>
