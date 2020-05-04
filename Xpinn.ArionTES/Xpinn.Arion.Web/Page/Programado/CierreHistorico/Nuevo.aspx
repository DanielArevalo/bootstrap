<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagName="ctlFecha" TagPrefix="ucf" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlCierreHistoricoError.ascx" TagName="gvLista" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <style type="text/css">
        .ctlFecha {
            width: 162px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 100%;">
            <tr>
                <td align="center"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="LabelFecha" runat="server" Text="Fecha de Corte"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="upFechaCorte" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown"
                                Width="160px" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    Estado del Proceso:
                    <asp:RadioButtonList ID="rbEstado" runat="server" align="center"
                        RepeatDirection="Horizontal" Width="185px">
                        <asp:ListItem Selected="True" Value="1">Pruebas</asp:ListItem>
                        <asp:ListItem Value="2">Definitivo</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnEjecutar" runat="server" CssClass="btn8" OnClick="BtnEjecutar_Click"
                        Text="Ejecutar" Width="182px" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:HiddenField ID="HF1" runat="server" />
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
        PopupControlID="panelActividadReg" TargetControlID="HF1"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right;">
        <div id="popupcontainer" style="width: 120%">
            <div class="row">
                <div class="cell popupcontainercell">
                    <div id="ordereditcontainer">
                        <asp:UpdatePanel ID="upActividadReg" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="cell ordereditcell" style="width: 120%">
                                        <br>
                                    </div>
                                    <div class="cell" style="width: 120%" align="center">
                                        <div class="cell">
                                            Este proceso tardará varios minutos. <strong>Esta seguro de continuar?</strong><br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell ordereditcell" style="width: 120%">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" style="width: 120%">
                            </div>
                            <div class="cell" style="text-align: center; width: 120%;">
                                <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                                    CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn8"
                                    Width="182px" OnClick="btnCancelar_Click" />
                            </div>
                            <div class="cell" style="width: 120%">
                                <br />
                            </div>
                            <div class="cell" style="width: 120%; text-align: center;">
                                Luego de oprimir el botón continuar deberá esperar hasta que termine el proceso.<br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando" TargetControlID="HF2" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;">
        <table style="width: 145%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loading.gif" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Espere un Momento Mientras Termina el Proceso"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:HiddenField ID="HF3" runat="server" />
    <asp:ModalPopupExtender ID="mpeFinal" runat="server" PopupControlID="pFinal" TargetControlID="HF3" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pFinal" runat="server" BackColor="White" Style="text-align: right; width: 50%">
        <table style="width: 100%;">
            <tr>
                <td align="center" style="width: 70%">
                    <br />
                    <asp:Label ID="Label2"  ForeColor="Red" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="btnSalir" runat="server" Text="Salir"
                        CssClass="btn8" Width="100px" OnClick="btnSalir_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnErrores" runat="server" Text="Consultar Errores" CssClass="btn8" Width="200px" OnClick="btnErrores_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HF4" runat="server" />
    <asp:ModalPopupExtender ID="mpeErrores" runat="server" PopupControlID="pnErrores" TargetControlID="HF4" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnErrores" runat="server" ackColor="White" Style="text-align: right; width: 100%">
        <table style="width: 100%;">
            <tr>
                <td align="center" style="width: 100%">
                    <uc1:gvLista ID="error" runat="server" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Salir"
                        CssClass="btn8" Width="100px" OnClick="btnSalir_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="text-align: center; width: 70%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" OnTick="Timer1_Tick">
            </asp:Timer>
            <br />
            <asp:Label ID="lblError" runat="server" Text=""
                Style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(window).load(function () {
            $("#cphMain_ctlFecha_txtFecha").removeAttr('width');
        });


    </script>
</asp:Content>
