<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R08_Autorizacion.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%-- SECCION AUTORIZACION --%>
                        <div onclick="ocultarMostrarPanel('autorizacion')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">10. Autorizaci&oacute;n de descuentos<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="autorizacion">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    Yo,
                                <asp:TextBox runat="server" ID="txtNombreAutoriza" ReadOnly="true" CssClass="campo" Width="380px" />
                                    identificado con cedula N°
                                <asp:TextBox runat="server" ID="txtCedulaAutoriza" ReadOnly="true" CssClass="campo" Width="250px" />
                                    de 
                                <asp:TextBox runat="server" ID="txtCiudadAutoriza" ReadOnly="true" CssClass="campo" Width="250px" />
                                    declaro conocer los estatutos y reglamentos que rigen a FECEM por lo cual solicito mi afiliación de forma libre y voluntaria, autorizando al pagador de
                                <asp:TextBox runat="server" ID="txtPagadorAutoriza" CssClass="campo" Width="200px" />
                                    para descontar de mi salario y/o pensión la suma de $
                                <asp:TextBox runat="server" ID="txtValorAutoriza" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" CssClass="campo" Width="150px" />
                                    en forma mensual por concepto de aportes más ahorro permanente como asociado al Fondo de Empleados de Cemex Colombia "FECEM".
                                </div>
                            </div>
                            <br />
                            <small><asp:RequiredFieldValidator ControlToValidate="txtValorAutoriza" ErrorMessage="* Ingrese el valor autorizado " Display="Static" id="RequiredFieldValidator12" ForeColor="Red" runat="server"/></small>
                        </div>
                        <%-- FIN SECCION AUTORIZACION --%>
    <asp:Panel runat="server" ID="pnlAporte" Visible="false">
        <div class="row">
            <div class="col-sm-12">
                <img src="../../Imagenes/Precios.png" style="margin: 0 auto;display: -webkit-box;padding-bottom: 10px;" />
            </div>
        </div>
    </asp:Panel>    
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R07_Internacional.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

