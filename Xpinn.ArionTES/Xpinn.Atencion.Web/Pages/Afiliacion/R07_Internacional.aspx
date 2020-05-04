<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R07_Internacional.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
     <%-- SECCION INTERNACIONAL --%>
                        <div onclick="ocultarMostrarPanel('internacional')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">6. Informaci&oacute;n operaciones internacionales <span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="internacional">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label>Operaciones en moneda extrajera *</label>
                                    <asp:RadioButtonList ID="ChkOperaciones" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtBanco">Nombre de la entidad</label>
                                    <asp:TextBox ID="TxtBanco" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="ddlTipoProductoInt">Tipo de producto</label>
                                    <asp:DropDownList ID="ddlTipoProductoInt" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="1">Cuenta de ahorros</asp:ListItem>
                                        <asp:ListItem Value="2">Cuenta corriente</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtPorcBenef2">N° de cuenta</label>
                                    <asp:TextBox ID="TxtNumeroCuenta" runat="server" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                        TargetControlID="TxtNumeroCuenta" ValidChars=",." />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtPromedio">Moneda</label>
                                    <uc1:decimales ID="txtPromedio" class="form-control" runat="server" />
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtMoneda">Moneda</label>
                                    <asp:TextBox ID="TxtMoneda" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtCiudad">Ciudad</label>
                                    <asp:TextBox ID="TxtCiudad" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label for="TxtPaís">País</label>
                                    <asp:TextBox ID="TxtPaís" runat="server" CssClass="form-control" sStyle="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION INTERNACIONAL --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R06_Financiera.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

