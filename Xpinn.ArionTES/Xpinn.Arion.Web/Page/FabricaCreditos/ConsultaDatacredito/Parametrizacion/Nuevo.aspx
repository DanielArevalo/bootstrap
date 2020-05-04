<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvNuevo" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwMensaje" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center">
                        <br />
                        <br />
                        <asp:RadioButtonList ID="rblParametria" runat="server" Style="text-align: center"
                            Width="100%">
                            <asp:ListItem Selected="True">Parámetro</asp:ListItem>
                            <asp:ListItem Value="Central">Central de riesgo</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <br />
                        <asp:ImageButton ID="btnAceptarInsercion" runat="server" ImageUrl="~/Images/btnAceptar.jpg"
                            OnClick="btnAceptar_Click" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwNuevoParametro" runat="server">
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Mínimo * <br />                       
                        <uc1:decimales ID="txtMinimo" runat="server" CssClass="textbox" ValidationGroup="vgGuardar"/><br />
                    </td>
                    <td style="text-align:left">
                        Máximo *
                        <br />
                        <uc1:decimales ID="txtMaximo" runat="server" CssClass="textbox" ValidationGroup="vgGuardar" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Aprueba<br />
                        <asp:RadioButtonList ID="chklAprueba" runat="server" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align:left">
                        Muestra<br />
                        <asp:RadioButtonList ID="chklMuestra" runat="server" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Value="S">Si</asp:ListItem>
                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="2">
                        Mensaje *<asp:RequiredFieldValidator ID="rfvMensaje" runat="server" ErrorMessage="Campo Requerido"
                            ControlToValidate="txtMensaje" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
                        &nbsp;<br />
                        <asp:TextBox ID="txtMensaje" runat="server" CssClass="textbox" MaxLength="50" Width="400px"
                            ValidationGroup="vgGuardar" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwNuevaCentral" runat="server">
            <table cellpadding="5" cellspacing="0">
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Central *                        
                        <asp:RadioButtonList ID="rblCentral" runat="server" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Selected="True" Value="Cifin">Cifin</asp:ListItem>
                            <asp:ListItem Value="Datacredito">Datacredito</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                    </td>
                    <td style="text-align:left">
                        Valor *<br />
                        <uc1:decimales ID="txtValor" runat="server" CssClass="textbox" MaxLength="4" />                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Cobra IVA<br />
                        <asp:RadioButtonList ID="rblCobra" runat="server" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align:left">
                        Porcentaje IVA<br />
                        <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" style="text-align:right" MaxLength="3" Width="130px" />
                        <asp:FilteredTextBoxExtender ID="txtPorcentaje_FilteredTextBoxExtender" 
                            runat="server" FilterType="Numbers, Custom" TargetControlID="txtPorcentaje" ValidChars=".,">
                        </asp:FilteredTextBoxExtender>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
           
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
