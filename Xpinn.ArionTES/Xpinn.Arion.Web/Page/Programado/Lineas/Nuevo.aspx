<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="ctlTasa" TagPrefix="ctl" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlPeriodicidad.ascx" TagName="ddlPeriodicidad" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlTipoMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <script type="text/javascript">
        
        
        $(window).load(function () {
            if ($("#chkTasaRenovacion").is(":checked")) {
                    $("#rbCalculoTasa_0").prop("disabled", false);
                    $("#rbCalculoTasa_1").prop("disabled", false);
                    $("#rbCalculoTasa_2").prop("disabled", false);
                    $("#rbCalculoTasa_3").prop("disabled", false);
                } else {
                    $("#rbCalculoTasa_0").prop("disabled", "disabled");
                    $("#rbCalculoTasa_1").prop("disabled", "disabled");
                    $("#rbCalculoTasa_2").prop("disabled", "disabled");
                    $("#rbCalculoTasa_3").prop("disabled", "disabled");
                }
        });

        $(document).ready(function (){
            $("#chkTasaRenovacion").change(function () {
                if ($(this).is(":checked")) {
                    $("#rbCalculoTasa_0").prop("disabled", false);
                    $("#rbCalculoTasa_1").prop("disabled", false);
                    $("#rbCalculoTasa_2").prop("disabled", false);
                    $("#rbCalculoTasa_3").prop("disabled", false);
                } else
                {
                    $("#rbCalculoTasa_0").prop("disabled", "disabled");
                    $("#rbCalculoTasa_1").prop("disabled", "disabled");
                    $("#rbCalculoTasa_2").prop("disabled", "disabled");
                    $("#rbCalculoTasa_3").prop("disabled", "disabled");
  
                }
            });

        });
        
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="text-align: center" border="0">
                <tr>
                    <td style="text-align: left">Código<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="120px"
                            OnTextChanged="txtCodigo_TextChanged" />
                        <asp:FilteredTextBoxExtender ID="fte5" runat="server" TargetControlID="txtCodigo"
                            FilterType="Custom, Numbers" ValidChars=",." />
                    </td>
                    <td colspan="2" style="text-align: left">Nombre<br />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="250px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Moneda<br />
                        <ctl:ddlMoneda ID="ddlMoneda" runat="server" Width="100px" />
                    </td>
                    <td style="text-align: left">Estado Activa<br />
                        <asp:CheckBox ID="chkEstado" runat="server" Text="" TextAlign="Right" />
                    </td>
                    <td style="text-align: left">Prioridad<br />
                        <asp:TextBox ID="txtPrioridad" runat="server" CssClass="textbox" Style="text-align: right" Width="80px" />
                        <asp:FilteredTextBoxExtender ID="txtPrioridad_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtPrioridad" ValidChars=".">
                        </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2"><strong>Datos Liquidación</strong></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">Saldo Base Cálculo Liquidación<br />
                        <asp:DropDownList ID="ddlTipoSaldoInt" runat="server" CssClass="textbox" Width="200px">
                            <asp:ListItem Text="Saldo Mínimo" Value="1" />
                            <asp:ListItem Text="Saldo Promedio" Value="2" />
                            <asp:ListItem Text="Saldo Final" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left">Periodicidad Liquidación<br />
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" AutoPostBack="False" Requerido="False" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">Cuota Mínima<br />
                        <uc1:decimales ID="txtCuotaMin" runat="server" />
                    </td>
                    <td style="text-align: left">Plazo Mínimo<br />
                        <asp:TextBox ID="txtPlazoMin" runat="server" CssClass="textbox" MaxLength="5" Style="text-align: right" Width="120px" />
                        <asp:FilteredTextBoxExtender ID="fte20" runat="server" FilterType="Custom, Numbers" TargetControlID="txtPlazoMin" ValidChars="" />
                    </td>
                    <td style="text-align: left">Saldo Mínimo<br />
                        <uc1:decimales ID="txtSaldoMin" runat="server" />
                    </td>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2"><strong>Tasa de Interés</strong> </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="6">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="3">
                        <asp:ImageButton ID="btnnuevatasa" runat="server" ImageUrl="~/Images/btnNuevo.jpg" OnClick="btnnuevatasa_Click" />
                        <asp:GridView ID="gvTasas" runat="server" AutoGenerateColumns="False" DataKeyNames="idrango" GridLines="Horizontal" OnRowDeleting="gvTasas_RowDeleting" OnSelectedIndexChanging="gvTasas_SelectedIndexChanging" 
                            PageSize="20" SelectedRowStyle-Font-Size="X-Small" ShowHeaderWhenEmpty="True" Style="font-size: small; margin-bottom: 0px;">
                            <Columns>
                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/gr_edit.jpg" ShowSelectButton="True" />
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:BoundField DataField="idrango" HeaderText="Código">
                                <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="left" Width="300px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                    <td style="text-align: left" colspan="3">
                        <asp:CheckBox ID="chkTasaRenovacion" Visible="true" runat="server" ClientIDMode="Static" Text="Tasa Renovación" />
                        <br />
                        <ctl:ctlTasa ID="ctlTasaInteresReno" runat="server" ClientIDMode="Static" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                    <asp:CheckBox ID="cbInteresPorCuenta" runat="server"
                            Text="Manejar Tasa de Interés por Cada Cuenta" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2"><strong>Otros</strong> </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="6">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkRetiroParcial" runat="server" AutoPostBack="true" OnCheckedChanged="chkRetiroParcial_CheckedChanged" Text="Permite Retiro Parcial" TextAlign="Right" />
                        <br />
                        % Retiro
                        <asp:TextBox ID="txtPorcRetiro" runat="server" CssClass="textbox" Width="55px" />
                        <asp:FilteredTextBoxExtender ID="txtPorcRetiro_FilteredTextBoxExtender" runat="server" FilterType="Custom, Numbers" TargetControlID="txtPorcRetiro" ValidChars=",." />
                    </td>
                    <td style="text-align: left">
                        Días Gracia<br />
                        <asp:TextBox ID="txtDiasGracia" runat="server" CssClass="textbox" Width="60px" />
                        <asp:FilteredTextBoxExtender ID="fte3" runat="server" FilterType="Custom, Numbers" TargetControlID="txtDiasGracia" ValidChars="" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">
                        <asp:CheckBox ID="chkAplicaReten" runat="server" AutoPostBack="true" OnCheckedChanged="chkAplicaReten_CheckedChanged" Text="Aplica Retención" TextAlign="Right" Width="150px" />
                    </td>
                    <td style="text-align: left">Cuota Nómina</td>
                    <td colspan="2" style="text-align: left">Vr. Máximo Retiro </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkCruza" runat="server" AutoPostBack="true" OnCheckedChanged="chkCruza_CheckedChanged" Text="Cruza" TextAlign="Right" Width="120px" />
                        % Cruce
                        <asp:TextBox ID="txtCruce" runat="server" CssClass="textbox" Width="50px" />
                        <asp:FilteredTextBoxExtender ID="fte2" runat="server" FilterType="Custom, Numbers" TargetControlID="txtCruce" ValidChars=",." />
                    </td>
                    <td style="text-align: left">% Retención
                        <asp:TextBox ID="txtPorcRetencion" runat="server" CssClass="textbox" Width="50px" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtPorcRetencion" ValidChars=",." />
                    </td>
                    <td style="text-align: left">
                        <uc1:decimales ID="txtCtaNomina" runat="server" />
                    </td>
                    <td style="text-align: left" colspan="2">
                        <uc1:decimales ID="txtVrMaxRetiro" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <strong>Cuotas Extras</strong>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="chkCuotasExtras" runat="server" AutoPostBack="true" OnCheckedChanged="chkCuotasExtras_CheckedChanged" Text="Maneja cuotas Extras" TextAlign="Right" Width="218px" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblcuotaminimaExtra" runat="server" Text="Cuota Mínima" Visible="false"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtCuotaMinExtra" runat="server" />
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblcuotamaximaExtra" runat="server" Text="Cuota Máxima" Visible="false"></asp:Label>
                        <br />
                        <uc1:decimales ID="txtCuotaMaxExtra" runat="server" />
                    </td>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>
            </table>
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
                        <td style="text-align: center; font-size: large; color: Red">La linea fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            ;correctamente.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwTasaRango" runat="server">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">Linea de Servicio
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblCodLineaProgramado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">Descripción Grupo<br />
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreGrupo" runat="server" CssClass="textbox" MaxLength="128"
                            Width="234px" />
                        ;
                    <asp:Label ID="lblCodRango" runat="server" Height="16px" Visible="False" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="text-align: left; width: 50%">
                        <asp:Button ID="btnDetalleTopes" OnClick="btnDetalleTopes_Click" runat="server" CssClass="btn8" Text="+ Adicionar Detalle" />
                        <br />
                        <asp:GridView ID="gvTopes" OnRowDeleting="gvTopes_RowDeleting" OnRowDataBound="gvTopes_RowDataBound" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black"
                            ShowFooter="True" Style="font-size: xx-small; margin-right: 0px;"
                            DataKeyNames="idrequisito">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                   <ItemTemplate>
                                         <asp:Label ID="lbltope" runat="server" Text='<%# Bind("idrequisito") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lbldescripciontope" runat="server" Text='<%# Bind("tipo_tope") %>' Visible="False"></asp:Label>
                                        <cc1:DropDownListGrid ID="ddlDescrpTope" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px"></cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    

                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tope Minimo" ItemStyle-HorizontalAlign="Left">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txttopeminimo" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" Text='<%# Bind("minimo") %>' Width="100px"></asp:TextBox>
                                        

                                    </ItemTemplate>
                                    

                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tope Maximo" ItemStyle-HorizontalAlign="Right">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txttopemaximo" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left" Text='<%# Bind("maximo") %>' Width="100px"></asp:TextBox>
                                        

                                    </ItemTemplate>
                                    

                                    <ItemStyle HorizontalAlign="Right" />
                                    

                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                    <td style="text-align: left;" colspan="3">
                        <ctl:ctlTasa ID="ctlTasaInteres" runat="server" Width="400px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button Text="Guardar" ID="btnGuardarTasa" OnClick="btnGuardarTasa_Click" runat="server" />
                        <asp:Button Text="Cancelar" ID="btnCancelarTasa" OnClick="btnCancelarTasa_Click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
