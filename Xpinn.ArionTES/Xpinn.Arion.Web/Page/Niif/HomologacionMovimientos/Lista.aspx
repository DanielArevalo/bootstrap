<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableSessionState="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table cellpadding="2">
            <tr>
                <td align="left" colspan="3">
                    Periodo
                </td>
            </tr>
            <tr>
                <td align="left">
                    <ucFecha:fecha ID="txtFechaIni" runat="server" />
                </td>
                <td align="left">
                    a
                </td>
                <td align="left">
                    <ucFecha:fecha ID="txtFechaFin" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    Tipo de Comprobante<br />
                    <asp:DropDownList ID="ddlTipoComprobante" runat="server" 
                        AppendDataBoundItems="True" CssClass="textbox" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando" TargetControlID="HF2" BackgroundCssClass="modalBackground" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;"  CssClass="pnlBackGround">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1"  runat="server" ImageUrl="~/Images/loading.gif" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Espere un Momento Mientras Termina el Proceso"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:HiddenField ID="HF3" runat="server" />
    <asp:ModalPopupExtender ID="mpeFinal" runat="server" PopupControlID="pFinal" TargetControlID="HF3" BackgroundCssClass="modalBackground"  >
    </asp:ModalPopupExtender>
    <asp:Panel ID="pFinal" runat="server" BackColor="White" Style="text-align: right;" CssClass="pnlBackGround">
        <table style="width: 100%;">
            <tr>
                <td align="center" style="width: 100%">
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Proceso de Homologación NIIF Terminado Correctamente"></asp:Label>
                    <br />
                    <asp:Button ID="btnSalir" runat="server" Text="Salir"
                      CssClass="btn8"  Width="100px" onclick="btnSalir_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  Style="text-align: center; width:100%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" ontick="Timer1_Tick" >
            </asp:Timer>
            <br />
            <asp:Label ID="lblError" runat="server" Text="" 
                style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
