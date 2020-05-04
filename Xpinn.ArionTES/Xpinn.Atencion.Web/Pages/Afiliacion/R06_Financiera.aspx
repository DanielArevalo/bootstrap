<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R06_Financiera.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:UpdatePanel ID="upCalculos" runat="server">
        <ContentTemplate>
            <%-- SECCION FINANCIERA --%>
            <div onclick="ocultarMostrarPanel('financiera')" style='cursor: pointer;'>
                <div class="col-md-12">
                    <a href="#!" class="accordion-titulo" style="">5. Informaci&oacute;n financiera<span class="toggle-icon"></span></a>
                </div>
            </div>
            <div class="col-md-12" id="financiera">
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-3">
                        <label class="active" for="txtIngsalariomensualCopia">Ingresos mensuales </label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="txtIngsalariomensualCopia" AutoPostBack="true" Width="100%" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <label for="TxtTotalActivos">Activos (bienes que posee) *</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="TxtTotalActivos" required AutoPostBack="true" Width="100%" class="form-control" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" runat="server" Enabled="true"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-3">
                        <label class="active" for="txtOtrosing">Otros ingresos</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="txtOtrosing" runat="server" class="form-control" AutoPostBack="true" Width="100%" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" OnTextChanged="txtOtrosing_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <label for="TxtTotalPasivos">Pasivos(deuda) *</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="TxtTotalPasivos" required AutoPostBack="true" Width="100%" class="form-control" runat="server" Enabled="true" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-3">
                        <label for="txtTotalIng">Total Ingresos *</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="txtTotalIng" runat="server" required CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <label for="TxtTotalPatrimonio">Total Patrimonio *</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="TxtTotalPatrimonio" runat="server" required CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-3">
                        <label class="active" for="txtDeducciones">Total Egresos Mensuales *</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="txtDeducciones" CssClass="form-control" required runat="server" AutoPostBack="true" Width="100%" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" />
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <label for="txtEmpresaAnterior">Detalle otros ingresos mensuales que percibe</label>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <asp:TextBox ID="txtDetalleIngresos" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
            </div>
            <%-- FIN SECCION FINANCIERA --%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R05_Pep.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

