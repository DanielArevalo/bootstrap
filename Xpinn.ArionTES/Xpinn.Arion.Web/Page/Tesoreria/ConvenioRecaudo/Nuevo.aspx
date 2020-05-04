<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 100%; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 180px">Fecha Convenio<br />
                        <ucFecha:fecha ID="Fecha_convenio" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 180px">Código Convenio *<br />
                        <asp:TextBox ID="txtCod_convenio" runat="server" CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 300px">Nombre Convenio<br />
                        <asp:TextBox ID="txt_nom_convenio" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 160px">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Identificación *<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="50px" Visible="false" />
                        <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                            Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                        <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                            OnClick="btnConsultaPersonas_Click" Text="..." />
                    </td>
                    <td style="text-align: left;" colspan="2">Nombre<br />
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                        <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="85%" />
                        <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                            Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="text-align: left;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Tipo Producto *<br />
                        <asp:DropDownList ID="ddlTipo_producto" runat="server" CssClass="textbox" Width="90%"
                             OnSelectedIndexChanged="ddlTipo_producto_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left;">Número Producto<br />
                        <asp:DropDownList ID="ddl_NumeroProducto" runat="server" CssClass="textbox" Width="90%"
                            ></asp:DropDownList>
                    <%--    <asp:TextBox ID="txtnum_producto" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>--%>
                    </td>
                    <td style="text-align: left;">Código Tipo Transacción<br />
                        <asp:DropDownList ID="ddlTipo_tran" runat="server" CssClass="textbox" Width="90%"></asp:DropDownList>
                     <%--   <asp:TextBox ID="txtCod_tipo_tran" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>--%>
                    </td>
                    <td style="text-align: left;">Código EAN<br />
                       <asp:TextBox ID="txtEAN" runat="server" CssClass="textbox"  Width="85%" />
                     <%--   <asp:TextBox ID="txtCod_tipo_tran" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Naturaleza de Convenio *<br />
                        <asp:DropDownList ID="ddlNaturalezaConvenio" runat="server" CssClass="textbox" Width="90%"></asp:DropDownList>
                    </td>
                    <td style="text-align: left;">
                        <br />
                        <asp:CheckBox ID="chkCuentaPropia" runat="server" Text="Es Cuenta Propia?" />
                    </td>
                    <td style="text-align: left;">
                        <br />
                        <asp:CheckBox ID="chkContratoFirmado" runat="server" Text="Existe Contrato Firmado?" />
                    </td>
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
                        <td style="text-align: center; font-size: large; color: Red">El Convenio fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.</td>
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
    </asp:MultiView>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
