<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SolicitudCredito.aspx.cs" Inherits="SolicitudCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="panelInicial" runat="server">

</asp:Panel>
    <asp:Panel ID="panelDataPersona" runat="server">
        <div class="form-group">
            <div class="col-sm-12 text-primary" style="font-weight: bold;">
                Datos del Deudor<br />
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2 text-left">
                    Identificación :<br />
                    <asp:Label ID="lblCod_persona" runat="server" Visible="false" />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" Width="95%"
                        Enabled="false"></asp:TextBox>
                </div>
                <div class="col-sm-3 text-left">
                    Tipo Identificación :<br />
                    <asp:DropDownList ID="ddlTipoIdent" runat="server" CssClass="form-control" Width="100%"
                        Enabled="false">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 text-left">
                    Nombres :<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-sm-3 text-left">
                    Apellidos :<br />
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Width="100%"
                        Enabled="false"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                </div>
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12">
                <div class="col-sm-5 text-left">
                    Direccion :
                    <br />
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Width="100%"
                        Enabled="false"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    Teléfono
                    <br />
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Width="100%"
                        Enabled="false"></asp:TextBox>
                </div>
                <div class="col-sm-5">
                </div>
            </div>
            <div class="col-sm-12">
                <hr style="width: 100%" />
            </div>
        </div>        
    </asp:Panel>
    <asp:Panel ID="panelEducativo" runat="server">
        <div class="form-group">

        </div>
    </asp:Panel>
    <asp:Panel ID="panelNormal" runat="server">
        <div class="form-group">
            <div class="col-sm-12 text-primary" style="font-weight: bold;">
                Condiciones Solicitadas del Crédito
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2 text-left">
                    Fecha Solicitud<br />
                    <uc2:fecha ID="txtFechaSolicitud" runat="server" CssClass="form-control" Width="100%" />
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-4">
                            Línea de Crédito:<br />
                            <asp:DropDownList ID="ddlLineaCredito" OnSelectedIndexChanged="ddlLineaCredito_SelectedIndexChanged"
                                AutoPostBack="true" runat="server" CssClass="form-control" Width="100%">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            Monto Máximo:<br />
                            <uc1:decimales ID="txtMontoMaximo" runat="server" Enabled="false" Width_="100%" AutoPostBack_="false"/>
                        </div>
                        <div class="col-sm-2">
                            Plazo Máximo:<br />
                            <asp:TextBox ID="txtPlazoMaximo" runat="server" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtPlazoMaximo" ValidChars="" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-2">
                </div>
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2">
                    Valor del Crédito:<br />
                    <uc1:decimales ID="txtVrCredito" runat="server" Width_="100%" AutoPostBack_="false"/>
                </div>
                <div class="col-sm-2">
                    Plazo:<br />
                    <asp:TextBox ID="txtPlazo" runat="server" CssClass="form-control text-right" MaxLength="8" />
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                        FilterType="Numbers, Custom" TargetControlID="txtPlazo" ValidChars="" />
                </div>
                <div class="col-sm-4">
                    Periodicidad:<br />
                    <asp:DropDownList ID="ddlperiodicidad" runat="server" CssClass="form-control" />
                </div>
                <div class="col-sm-4">
                <asp:DropDownList ID="ddlMedio" runat="server" CssClass="form-control" Visible="false" />
                </div>
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12">
                <asp:UpdatePanel ID="updFormaPago" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-2">
                            Forma de Pago:<br />
                            <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            <asp:Label ID="lblPagaduri" runat="server" Text="Pagaduria:"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"
                                AutoPostBack="true" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-6">
                    <br />
                    <asp:Button ID="btnSolicitar" runat="server" Text="  Solicitar  " Width="60%" OnClick="btnSolicitar_Click"
                        CssClass="btn btn-primary" Style="height: 30px; padding: 0px; border-radius: 0px" />
                </div>
            </div>
            <div class="col-sm-12">
                <br />
                <br />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelFinal" runat="server">
        <div class="form-group col-lg-12 text-center">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <strong>
                <asp:Label ID="lblmsjFinal" runat="server" Style="color: Red" /></strong>
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </asp:Panel>

</asp:Content>

