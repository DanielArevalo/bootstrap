<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>  
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="width: 80%" cellspacing="2" cellpadding="2">
                        <tr>
                            <td style="text-align: left; width: 15%">
                                Código<br />
                                &nbsp;<asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="70%"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 65%">
                                Nombre<br />
                                &nbsp;<asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="60%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                Tipo de Archivo:
                            </td>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rblTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="rblTipoArchivo_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Excel</asp:ListItem>
                                    <asp:ListItem Value="1">Texto</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td rowspan="3" style="text-align: left; width: 15%">
                                <strong>Tipo de Datos</strong> &nbsp;
                                <asp:RadioButtonList ID="rblTipoDato" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblTipoDato_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Delimitados</asp:ListItem>
                                    <asp:ListItem Value="1">De Ancho Fijo</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td rowspan="3" style="text-align: left; width: 30%">
                                <strong>Separador de Campo</strong><br />
                                <asp:RadioButtonList ID="chkSeparaCampo" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="3">
                                    <asp:ListItem Value="0">Tabulación</asp:ListItem>
                                    <asp:ListItem Value="1">Coma</asp:ListItem>
                                    <asp:ListItem Value="2">Punto y Coma</asp:ListItem>
                                    <asp:ListItem Value="3">Espacio</asp:ListItem>
                                    <asp:ListItem Value="4">Otro</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left; width: 10%; vertical-align: top">
                                #Filas de Encabezado&nbsp;
                            </td>
                            <td style="text-align: left; width: 10%; vertical-align: top">
                                #Filas de Final &nbsp;
                            </td>
                            <td style="text-align: left; width: 35%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 15%; vertical-align: top">
                                <asp:TextBox ID="txtEncabezado" runat="server" CssClass="textbox" Width="30px" MaxLength="10"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte5" runat="server" TargetControlID="txtEncabezado"
                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." /> 
                                <asp:DropDownList ID="ddlEstructura" runat="server" CssClass="textbox" Width="110px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 15%; vertical-align: top">
                                <asp:TextBox ID="txtFinal" runat="server" CssClass="textbox" Width="30px" MaxLength="10"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="fte6" runat="server" TargetControlID="txtFinal"
                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 20%">
                            </td>
                            <td style="text-align: left; width: 15%">
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="text-align: left; width: 15%">
                                Formato de fecha
                                <br />
                                <asp:DropDownList ID="ddlFormatoFecha" runat="server" CssClass="textbox" Width="90%">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width: 20%">
                                Separador de Decimales:
                                <br />
                                <asp:TextBox ID="txtSepDecimales" runat="server" CssClass="textbox" Width="40px"
                                    MaxLength="4">.</asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 20%">
                                Separador de Miles:<br />
                                <asp:TextBox ID="txtSepMiles" runat="server" CssClass="textbox" Width="40px" MaxLength="4">,</asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 45%">
                                <br />
                                <asp:CheckBox ID="cbTotalizar" runat="server" Text="Totalizar Según la Estructura" />
                            </td>
                            <td style="text-align: left; width: 45%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left; width: 100%">
                                <hr style="width: 100%" />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="4">
                                <strong>Campo de cada Registro :</strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left; width: 100%">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" OnClick="btnAgregar_Click"
                                    Text="+ Adicionar Detalle" />
                                <asp:GridView ID="gvDetalle" runat="server" Width="100%" PageSize="20" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                    margin-bottom: 0px;" OnRowDataBound="gvDetalle_RowDataBound" OnRowDeleting="gvDetalle_RowDeleting"
                                    DataKeyNames="cod_estructura_detalle">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                            <ItemStyle HorizontalAlign="center" Width="4%" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcod_estructura_detalle" runat="server" Text='<%# Bind("cod_estructura_detalle") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre de campo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodigo_campo" runat="server" Text='<%# Bind("codigo_campo") %>'
                                                    Visible="false"></asp:Label>
                                                <cc1:DropDownListGrid ID="ddlcodigo_campo" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                    CssClass="textbox" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlcodigo_campo_SelectedIndexChanged">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Num Columna">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumero_columna" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("numero_columna") %>' Width="80%"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtnumero_columna"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*(). " />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Posición Inicial">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtposicion_inicial" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("posicion_inicial") %>' Width="80%"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fte2" runat="server" TargetControlID="txtposicion_inicial"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longitud">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtlongitud" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("longitud") %>' Width="80%"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fte3" runat="server" TargetControlID="txtlongitud"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Justificación">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljustificacion" runat="server" Text='<%# Bind("justificacion") %>'
                                                    Visible="false">
                                                </asp:Label>
                                                <cc1:DropDownListGrid ID="ddljustificacion" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                    CssClass="textbox" Width="90%">
                                                </cc1:DropDownListGrid>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Justificador">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtjustificador" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("justificador") %>' Width="80%" MaxLength="1"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fte4" runat="server" TargetControlID="txtjustificador"
                                                    FilterType="Custom, Numbers" ValidChars="+-=/*(). " />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Campo Fijo">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVrCampoFijo" runat="server" CssClass="textbox" Style="font-size: xx-small;
                                                    text-align: left" Text='<%# Bind("vr_campo_fijo") %>' Width="80%" Visible="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                </asp:GridView>
                                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblTipoArchivo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rblTipoDato" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gvDetalle" EventName="RowDeleting" />                    
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Se ha
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente la Estructura</td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Continuar" 
                                onclick="btnFinal_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
     
     <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>