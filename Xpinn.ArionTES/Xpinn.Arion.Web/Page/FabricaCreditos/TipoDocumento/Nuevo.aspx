<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Tipos de Documento :." ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="~/General/Controles/CtlEditDocument.ascx" TagPrefix="uc1" TagName="CtlEditDocument" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScritpManager1" runat="server" EnablePageMethods="true" />
    
    <br />
    <br />

    <asp:MultiView ID="mvTipoDopcumento" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table border="0" cellpadding="1" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="text-align: left; width: 15%" colspan="2">Código*&nbsp;<asp:RequiredFieldValidator ID="rfvtipoliq" runat="server" ControlToValidate="txtTipoDocumento" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="textbox" Width="90%"
                            MaxLength="128" Enabled="False" />
                    </td>
                    <td class="tdI" style="text-align: left; width: 40%" colspan="2">Descripción&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" Width="95%" />
                    </td>
                    <td style="text-align: left; width: 20%">Tipo de Documento<br />
                        <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="textbox" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoDoc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 20%">Orden de Servicio<br />
                        <asp:CheckBox ID="chkOrdenServicio" runat="server" />

                    </td>
                    <td style="text-align: left; width: 25%"></td>

                </tr>
            </table>

            <uc1:CtlEditDocument runat="server" ID="CtlEditDocument" CtlEditDocument="true"/>

        </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Documento Grabado Correctamente"></asp:Label>
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
