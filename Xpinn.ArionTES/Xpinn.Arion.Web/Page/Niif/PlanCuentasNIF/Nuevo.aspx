<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Plan de Cuentas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:MultiView ID="mvComprobante" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwCuenta" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panelNif" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="80%">
                            
                            <tr>
                                <td colspan="8" style="text-align: left">
                                    <span style="font-size: small; font-weight: bold">NIF:</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    Cod.Cuenta&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcodcuentaNif" runat="server"
                                        Style="font-size: x-small" ControlToValidate="txtCodCuentaNif" ErrorMessage="Campo Requerido"
                                        SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                                    <asp:TextBox ID="txtCodCuentaNif" runat="server" CssClass="textbox" MaxLength="20" />
                                </td>
                                <td colspan="5" style="text-align: left">
                                    Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNombreNif" runat="server" ControlToValidate="txtNombreNif"
                                        ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                                        Style="font-size: x-small" ForeColor="Red" Display="Dynamic" /><br />
                                    <asp:TextBox ID="txtNombreNif" runat="server" CssClass="textbox" MaxLength="120"
                                        Width="500px" />
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: left">
                                    Depende de&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDependeDeNif" runat="server"
                                        Style="font-size: x-small" ControlToValidate="ddlDependedeNif" ErrorMessage="Campo Requerido"
                                        SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                                    <br />
                                    <asp:DropDownList ID="ddlDependedeNif" runat="server" CssClass="textbox" Width="427px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: left">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="0" width="80%">
                        <tr>
                            <td style="text-align: left; width: 30%">
                                Moneda<br />
                                <asp:DropDownList ID="ddlMonedas" runat="server" Width="220px" CssClass="textbox">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 30%">
                                Tipo&nbsp;<br />
                                <asp:DropDownList ID="ddlTipo" runat="server" Width="120px" CssClass="textbox">
                                    <asp:ListItem Value="D">Dèbito</asp:ListItem>
                                    <asp:ListItem Value="C">Crédito</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td style="text-align: left; width: 20%">
                                Nivel<br />
                                <asp:DropDownList ID="ddlNivel" runat="server" CssClass="textbox" Width="120px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-Clase"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-Grupo"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3-Mayor"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4-Subcuenta"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5-Auxiliar"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6-Auxiliar"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7-Auxiliar"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8-Auxiliar"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9-Auxiliar"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 20%">
                                Estado<br />
                                <asp:CheckBox ID="chkEstado" runat="server" Text="Activa" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <br />
                                <asp:CheckBox ID="cbNoCorriente" runat="server" Text="No Corriente" OnCheckedChanged="cbNoCorriente_CheckedChanged"
                                    AutoPostBack="true" />&nbsp;&nbsp;
                                <asp:CheckBox ID="cbCorriente" runat="server" Text="Corriente" OnCheckedChanged="cbCorriente_CheckedChanged"
                                    AutoPostBack="True" />
                                &nbsp;&nbsp;
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lbl1" runat="server" Text="Tipo de Distribución"></asp:Label><br />
                                <asp:DropDownList ID="ddlTipoDistribucion" runat="server" CssClass="textbox" Width="120px"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDistribucion_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Seleccione un Item"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Por Valor"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Por Porcentaje"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left" colspan="2">
                                <asp:Label ID="lbl2" runat="server" Text="Porcentaje Distribución"></asp:Label>
                                <asp:Label ID="lbl3" runat="server" Text="Valor Distribución"></asp:Label><br />
                                <uc1:decimales ID="txtPorDistribucion" runat="server"></uc1:decimales>
                                <uc1:decimales ID="txtvalorDistribucion" runat="server"></uc1:decimales>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkTerceros" runat="server" Text="Maneja Terceros" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkCentroCosto" runat="server" Text="Maneja Centro de Costo" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkCentroGestion" runat="server" Text="Maneja Centro de Gestión"
                                    Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left; width: 207px;">
                                <asp:CheckBox ID="chkCuentaPagar" runat="server" Text="Maneja Cuenta por Pagar" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkImpuestos" runat="server" Text="Maneja Impuesto" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkSupersolidaria" runat="server" Text="Reportar Solamente Cuenta Mayor a SUPERSOLIDARIA" Font-Size="X-Small" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: left">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top" colspan="6">
                                <asp:Panel ID="panelGrilla" runat="server">
                                    <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                        OnClientClick="btnDetalle_Click" Text="+ Adicionar Homologación" />
                                    <br />
                                    <asp:GridView ID="gvHomologa" runat="server" AutoGenerateColumns="false" BorderStyle="None" 
                                        BorderWidth="0px" CellPadding="0" DataKeyNames="idhomologa" ForeColor="Black"
                                        GridLines="None" Style="font-size: x-small" OnRowDeleting="gvHomologa_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:BoundField DataField="idhomologa" HeaderText="Tipo" Visible="false"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Cod Cuenta Local">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                                                        BackColor="#F4F5FF" Width="90px" Text='<%# Bind("cod_cuenta") %>' OnTextChanged="txtCodCuenta_TextChanged"
                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>&nbsp;<cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server"
                                                        Text="..." OnClick="btnListadoPlan_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;<cc1:TextBoxGrid ID="lblNombreCuenta" runat="server" Text='<%# Bind("nombre_cuenta") %>'
                                                        Style="padding-left: 15px" Width="350px" Enabled="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />                                
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; vertical-align: top;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Cuenta Grabada Correctamente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                           &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </asp:View>
    </asp:MultiView>
</asp:Content>
