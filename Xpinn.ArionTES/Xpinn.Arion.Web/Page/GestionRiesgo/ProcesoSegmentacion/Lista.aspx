﻿<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableSessionState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" Width="160px" Height="30px" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
    <asp:Panel ID="pFinal" runat="server" BackColor="White" Style="text-align: center;">
        <table style="width: 145%;">
            <tr>
                <td align="center" style="width: 70%">
                    <br />
                    <asp:Label ID="Label2" Text="Proceso de Cierre Histórico Terminado" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="btnSalir" runat="server" Text="Salir"
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

</asp:Content>

