<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Page_Contabilidad_Conceptos_DIAN_Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <br />
            <br />
            <br />
            <br />
            <table  border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                
                    <td style="text-align: left; width: 150px">Código Concepto *<br />
                        <asp:TextBox ID="txtCodconvenio" runat="server" CssClass="textbox" Width="100px" ></asp:TextBox>
                    </td>
                   
                  </tr>
                <tr>
                     <td style="text-align: left; width: 280px" colspan="2">Nombre Concepto<br />
                        <asp:TextBox ID="txtnomconcepto" runat="server" C CssClass="textbox" MaxLength="128" Width="519px"></asp:TextBox>
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

