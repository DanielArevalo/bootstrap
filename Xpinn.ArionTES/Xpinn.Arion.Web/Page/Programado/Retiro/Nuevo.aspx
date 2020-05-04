<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
    <%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimal" TagPrefix="ucdecimal" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fe" TagPrefix="ucfec" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlTasa.ascx" TagName="tasa" TagPrefix="uctasa" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <style type="text/css">
        .ccsla, #rbCalculoTasa
        {
            display: block;
            width: 393px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="text-align: center; width: 700px" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left">
                        Fecha Retiro<br />
                        <ucfec:fe ID="txtFecha" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td
                        colspan="5" style="text-align: left; font-weight: 700">
                        Datos Titular de la cuenta<hr />
                    </td>
                </tr>
                <tr> 
                <td style="text-align: left; width: 198px; display: table">
                            Identificación<br /> 
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                                ReadOnly="True" Visible="true" Width="150px" />
                        </td>
                        <td style="text-align: left; width: 140px;">
                            Tipo. Identificación<br />
                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" 
                                ClientIDMode="Static" CssClass="textbox" ReadOnly="True">
                            </asp:DropDownList>
                        </td>
                   
                    <td colspan="3" style="text-align: left; width: 120px;">
                        Nombre<br />
                        <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" 
                            ReadOnly="True" Width="250px" />
                        <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" 
                            ControlToValidate="txtNomPersona" Display="Dynamic" 
                            ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0" 
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: 700; text-align: left">
                        Datos de la cuenta:
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 280px">
                        Cuenta<br />
                        <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" ReadOnly="True" Width="160px"
                            AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 300px">
                        Fecha Apertura<br />
                        <ucFecha:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" Width="240px" />
                    </td>
                    <td style="text-align: left; ">
                        Oficina<br />
                        <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" ClientIDMode="Static"
                            Width="140px" ReadOnly="true"></asp:DropDownList>
                    </td>
                    <td style="text-align: left;">
                        Linea<br />
                        <asp:DropDownList ID="ddLinea" runat="server" CssClass="textbox" ClientIDMode="Static"
                            Width="210px" ReadOnly="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">
                        Saldo Total<br />
                        <asp:TextBox ID="txtSaldoTotal" ClientIDMode="Static" CssClass="textbox" runat="server"
                            Width="140px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fec. Próximo Pago<br />
                        <ucFecha:fecha ID="txtFechaProximoPago" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left;">
                        Cuota
                        <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" ReadOnly="True" Width="90%" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" TargetControlID="txtCuota"
                            FilterType="Custom, Numbers" ValidChars="+-=/*()." />
                    </td>
                    <td style="text-align: left;">
                        Plazo<br />
                        <asp:TextBox ID="txtPlazo" ClientIDMode="Static" ReadOnly="True" runat="server" CssClass="textbox"
                            Width="140px"></asp:TextBox>
                        <asp:TextBox ID="txtPorcentaje" runat="server" ClientIDMode="Static" 
                            CssClass="textbox" Visible="false"  ReadOnly="True" Width="10px"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        Periodicidad<br />
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" ReadOnly="True" CssClass="textbox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%-- Datos de Intereses Liquidados:--%>
                    </td>
                    <td style="position: relative; top: 50px; left: -217px">
                        &nbsp;</td>
                    <caption>
                        <tr>
                            </td>
                            <caption>
                                <br />
                                <tr>
                                    <td style="text-align: left;">
                                        Valor del Retiro<br />
                                        <uc1:decimales ID="txtDecimales" runat="server" cssClass="textbox" />
                                    </td>
                                </tr>
                            </caption>
                        </tr>
                    </caption>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr style="width: 100%" />
                    </td>
                </tr>
                </caption>
            </table>
            
           <asp:UpdatePanel ID="upFormaDesembolso" runat="server">
            <ContentTemplate>
            <asp:Panel ID="Panel5" runat="server" Width="100%">
                <table style="width: 583px;">
                    <tr>
                        <td style="width: 179px; text-align: left">
                            <strong>Forma de Pago</strong>
                        </td>
                        <td colspan="3" style="width: 404px">
                            <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px;
                                text-align: left" Width="84%" Height="28px" CssClass="textbox" AutoPostBack="True" 
                                OnSelectedIndexChanged="DropDownFormaDesembolso_TextChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 179px; text-align: left">
                            <asp:Label ID="lblEntidadOrigen" runat="server" Text="Banco de donde se Gira" Style="text-align: left"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                Width="84%" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px; text-align: left; height: 25px;">
                            <asp:Label ID="lblNumCuentaOrigen" runat="server" Text="Cuenta de donde se Gira"
                                Style="text-align: left"></asp:Label>
                        </td>
                        <td colspan="3" style="height: 25px">
                            <asp:DropDownList ID="ddlCuentaOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                Width="84%" CssClass="textbox" OnSelectedIndexChanged="ddlCuentaOrigen_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 25px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px; text-align: left">
                            <asp:Label ID="lblEntidad" runat="server" Text="Entidad" Style="text-align: left"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                                Width="84%" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px; text-align: left">
                            <asp:Label ID="lblNumCuenta" runat="server" Text="Numero de Cuenta" Style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 143px">
                            <asp:TextBox ID="txtnumcuenta" runat="server" Width="129px" CssClass="textbox" Style="text-align: left"></asp:TextBox>
                        </td>
                        <td style="width: 110px; text-align: left">
                            <asp:Label ID="lblTipoCuenta" runat="server" Text="Tipo Cuenta" Style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 151px">
                            <asp:DropDownList ID="ddlTipo_cuenta" runat="server" Style="margin-left: 0px; text-align: left"
                                Width="102%" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:View>
       
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</asp:Content>
