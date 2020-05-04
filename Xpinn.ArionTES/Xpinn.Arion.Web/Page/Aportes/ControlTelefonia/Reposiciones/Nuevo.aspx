<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="width: 740px; text-align: center">
                        <tr>
                            <td style="text-align: left; width: 140px" colspan="4">Fec. Reposición<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 246px">Titular<br />
                                <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                                <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true" Enabled="false"
                                    Width="80%" OnTextChanged="txtIdPersona_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="fte121" runat="server" TargetControlID="txtIdPersona"
                                    FilterType="Custom, Numbers" ValidChars="-" />
                                <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px" Visible="false"
                                    OnClick="btnConsultaPersonas_Click" Text="..." />
                            </td>
                            <td style="text-align: left; width: 370px" colspan="2">Nombre titular<br />
                                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" OneventotxtIdentificacion_TextChanged="txtIdPersona_TextChanged" />
                                <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True" Enabled="false"
                                    Width="350px" />
                                <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                                    Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                                    Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                            </td>

                        </tr>
                        <tr>

                            <td style="text-align: center; width: 185px">Número Linea Telefónica<br />
                                <asp:TextBox ID="txtNumeroLineaTel" runat="server" CssClass="textbox" Width="80%" Enabled="false" />
                            </td>

                            <td style="text-align: center; width: 185px">Valor Reposición<br />
                                <asp:TextBox ID="txtValorRep" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtValorRep"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; width: 185px">Observaciones<br />
                                <asp:TextBox ID="txtobservaciones" runat="server" CssClass="textbox" Width="80%" Style="text-align: right" TextMode="MultiLine" Rows="5" MaxLength="500"/>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtIdentificacionTitu" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreTit" runat="server" CssClass="textbox" ReadOnly="True"
                                    Width="50px" Visible="false" />
                            </td>

                        </tr>

                    </table>

                </ContentTemplate>
                <Triggers>
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
                        <td style="text-align: center; font-size: large; color: Red">Reposición
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br />
                            Nro. de linea telefónica :
                            <asp:Label ID="lblNroMsj" runat="server"></asp:Label>
                            <br />
                            <asp:Button ID="btnDesembolso" runat="server" Text="ir a Desembolso"
                                OnClick="btnDesembolso_Click" />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
