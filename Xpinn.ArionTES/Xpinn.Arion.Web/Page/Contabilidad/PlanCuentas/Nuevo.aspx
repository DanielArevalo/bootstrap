<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Plan de Cuentas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ctlPlanCuentas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentasNif.ascx" TagName="ctlPlanCuentasNif" TagPrefix="uc2" %>
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
                    <asp:Panel ID="panelLocal" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="80%">
                            <tr>
                                <td colspan="8" style="text-align: left">
                                    <span style="font-size: small; font-weight: bold">Local:</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">Cod.Cuenta&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvidentificacion" runat="server" Style="font-size: x-small"
                                    ControlToValidate="txtCodCuentaLocal" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                                    <asp:TextBox ID="txtCodCuentaLocal" runat="server" CssClass="textbox"
                                        MaxLength="20" />
                                </td>
                                <td colspan="5" style="text-align: left">Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvnombre" runat="server" ControlToValidate="txtNombre"
                                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" Style="font-size: x-small"
                                    ForeColor="Red" Display="Dynamic" /><br />
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="120" Width="500px" />
                                </td>
                                <td style="text-align: left">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: left">Depende de&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvloginConf" runat="server" Style="font-size: x-small"
                                    ControlToValidate="ddlDependede" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                                    <br />
                                    <asp:DropDownList ID="ddlDependede" runat="server" CssClass="textbox" Width="427px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDependede_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="0" width="80%">
                        <tr>
                            <td style="text-align: left; width: 30%">Moneda<br />
                                <asp:DropDownList ID="ddlMonedas" runat="server" Width="220px" CssClass="textbox">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 30%">Tipo&nbsp;<br />
                                <asp:DropDownList ID="ddlTipo" runat="server" Width="120px" CssClass="textbox">
                                    <asp:ListItem Value="D">Dèbito</asp:ListItem>
                                    <asp:ListItem Value="C">Crédito</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td style="text-align: left; width: 20%">Nivel<br />
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
                            <td style="text-align: left; width: 20%">Estado<br />
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
                            <td colspan="5" style="text-align: left">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkTerceros" runat="server" Text="Maneja Terceros" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkCentroCosto" runat="server" AutoPostBack="true" Text="Maneja Centro de Costo" Font-Size="X-Small" OnCheckedChanged="chkCentroCosto_CheckedChanged" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkCentroGestion" runat="server" Text="Maneja Centro de Gestión"
                                    Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left; width: 207px;">
                                <asp:CheckBox ID="chkCuentaPagar" runat="server" Text="Maneja Cuenta por Pagar" Font-Size="X-Small" />
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox ID="chkTrasladoSaldos" runat="server" Text="Maneja Traslado de Saldos a Terceros" Font-Size="X-Small" OnCheckedChanged="chkTrasladoSaldos_CheckedChanged"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: left">
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="90%">
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <asp:CheckBox ID="chkImpuestos" runat="server" Text="Maneja Impuesto" Font-Size="X-Small"
                                    OnCheckedChanged="chkImpuestos_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" OnClick="btnDetalle_Click"
                                    OnClientClick="btnDetalle_Click" Text="+ Adicionar Impuesto" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top" colspan="2">
                                <asp:Panel ID="panelGrilla" runat="server">
                                    <asp:GridView ID="gvImpuestos" runat="server" AutoGenerateColumns="false" HeaderStyle-Height="25px"
                                        BorderStyle="None" BorderWidth="0px" CellPadding="0" DataKeyNames="idimpuesto"
                                        ForeColor="Black" GridLines="None" OnRowDataBound="gvImpuestos_RowDataBound"
                                        OnRowDeleting="gvImpuestos_RowDeleting" ShowFooter="False"
                                        Style="font-size: xx-small">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("idimpuesto") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo de Impuesto" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoImpuesto" runat="server" Text='<%# Bind("cod_tipo_impuesto") %>'
                                                        Visible="false"></asp:Label>
                                                    <cc1:DropDownListGrid ID="ddlTipoImpuesto" runat="server" AppendDataBoundItems="True"
                                                        CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                        Width="200px">
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Base Mínima" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <uc1:decimales ID="txtBaseMinima" runat="server" Text='<%# Bind("base_minima") %>' Width="100px"></uc1:decimales>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Porcentaje Impuesto" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPorcentajeImpuesto" runat="server" CssClass="textbox" Width="80px"
                                                        Style="text-align: right" Text='<%# Bind("porcentaje_impuesto") %>' />
                                                    <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                        TargetControlID="txtPorcentajeImpuesto" ValidChars="," />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta_imp" runat="server" Width="100px"
                                                        CssClass="textbox" Text='<%# Bind("cod_cuenta_imp") %>'
                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>
                                                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                                        OnClick="btnListadoPlan_Click" CommandArgument='<%#Container.DataItemIndex %>' />
                                                    <uc1:ctlPlanCuentas ID="ctlListadoPlan" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Asumido" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:CheckBoxGrid ID="cbAsumido" runat="server" OnCheckedChanged="cbAsumido_CheckedChanged" AutoPostBack="true"
                                                        CommandArgument='<%#Container.DataItemIndex %>' Checked='<%#Convert.ToBoolean(Eval("asumido"))%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cod. Cuenta Asumido" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtCodCuenta_imps" runat="server" Width="100px"
                                                        CssClass="textbox" Text='<%# Bind("cod_cuenta_asumido") %>'
                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                    </cc1:TextBoxGrid>
                                                    <cc1:ButtonGrid ID="btnListadoPlans" CssClass="btnListado" runat="server" Text="..."
                                                        OnClick="btnListadoPlanAsumido_Click" CommandArgument='<%#Container.DataItemIndex %>' />
                                                    <uc1:ctlPlanCuentas ID="ctlListadoPlasn" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <asp:Panel ID="pnlCuentaContable" runat="server">
                                    <br />
                                    <hr />
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td style="text-align:left" colspan="3">
                                                        <strong>Cuentas de Compensación Movimientos Entre Centros de Costo</strong><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 130px; text-align: left">Cod Cuenta: 
                                                    <cc1:TextBoxGrid ID="txtCodCuentaContable" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                                        CssClass="textbox" OnTextChanged="txtCodCuentaContable_TextChanged" Style="text-align: left"
                                                        Width="120px"></cc1:TextBoxGrid>
                                                        <uc1:ctlPlanCuentas ID="ctlListadoPlanContable" runat="server" />
                                                    </td>
                                                    <td style="width: 35px; text-align: center">
                                                        <br />
                                                        <cc1:ButtonGrid ID="btnListadoPlanContable" runat="server" CssClass="btnListado" OnClick="btnListadoPlanContable_Click"
                                                            Width="95%" Text="..." />
                                                    </td>
                                                    <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                        <cc1:TextBoxGrid ID="txtNomCuentaContable" runat="server" CssClass="textbox" Enabled="false"
                                                            Width="190px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 50%">
                                <asp:Panel ID="pnlContraPartida" runat="server">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 130px; text-align: left">ContraPartida:
                                                    <cc1:TextBoxGrid ID="txtCodContrapartida" runat="server" AutoPostBack="True" BackColor="#F4F5FF"
                                                        CssClass="textbox" OnTextChanged="txtCodContrapartida_TextChanged" Style="text-align: left"
                                                        Width="120px"></cc1:TextBoxGrid>
                                                        <uc1:ctlPlanCuentas ID="ctlListadoPlanContraPartida" runat="server" />
                                                    </td>
                                                    <td style="width: 35px; text-align: center">
                                                        <br />
                                                        <cc1:ButtonGrid ID="btnListadoPlanContraPartida" runat="server" CssClass="btnListado" OnClick="btnListadoPlanContraPartida_Click"
                                                            Width="95%" Text="..." />
                                                    </td>
                                                    <td style="width: 230px; text-align: left">Nombre de la Cuenta<br />
                                                        <cc1:TextBoxGrid ID="txtNomCuentaContraPartida" runat="server" CssClass="textbox" Enabled="false"
                                                            Width="190px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left">
                                <br />
                                <hr />
                            </td>
                        
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top" colspan="2">
                                <strong>Homologación</strong><br />
                                <asp:GridView ID="gvHomologa" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                    BorderWidth="0px" CellPadding="0" DataKeyNames="idhomologa" ForeColor="Black"
                                    GridLines="None" Style="font-size: x-small; margin-bottom: 0px;"
                                    OnRowDeleting="gvHomologa_RowDeleting">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                        <asp:BoundField DataField="idhomologa" HeaderText="Tipo" Visible="false"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Cod Cuenta Niif">
                                            <ItemTemplate>
                                                <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                                                    BackColor="#F4F5FF" Width="90px" Text='<%# Bind("cod_cuenta_niif") %>' OnTextChanged="txtCodCuenta_TextChanged"
                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>  
                                                </cc1:TextBoxGrid>&nbsp;<cc1:ButtonGrid ID="btnListadoPlanHomo" CssClass="btnListado"
                                                    runat="server" Text="..." OnClick="btnListadoPlanHomo_Click" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                                <uc2:ctlPlanCuentasNif ID="ctlListadoPlanHomo" runat="server" />
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
                            </td>
                        </tr>
                       <td colspan="2" style="text-align: left">
                                <br />
                                <hr />
                               <strong>Homologación Cuentas DIAN</strong><br />
                            </td>
                        <tr>
                        
                                
                            <td style="text-align: left; vertical-align: top">
                                  Conceptos DIAN :
                                <br />
                            <asp:DropDownList ID="ddlConceptos" runat="server" CssClass="textbox"></asp:DropDownList> 
                                </td>
                            <td style="text-align: left; vertical-align: top">
                                Formatos DIAN :
                                <br />
                                <asp:DropDownList ID="ddlInformesDian" runat="server" CssClass="textbox"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"> 
                                <br />
                               <asp:Button ID="Button1" runat="server" CssClass="btn8" OnClick="btnImp_Click"
                                  Text=" Importar " />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlDependede" />
                    <asp:PostBackTrigger ControlID="chkImpuestos" />
                </Triggers>
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
                        <td style="text-align: center; font-size: large;">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
