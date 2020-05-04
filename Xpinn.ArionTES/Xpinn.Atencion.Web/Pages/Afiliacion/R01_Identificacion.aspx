<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R01_Identificacion.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <h6 style="text-align:center">1/11</h6>
    <%-- SECCION IDENTIFICACION --%>
    <div onclick="ocultarMostrarPanel('identificacion')" style='cursor: pointer;'>
        <div class="col-md-12">
            <a href="#!" class="accordion-titulo" style="">Identificación<span class="toggle-icon"></span></a>
        </div>
    </div>
    <div class="col-md-12" id="identificacion">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 col-md-6">
                <label for="inputState">Tipo de documento *</label>
                <asp:DropDownList ID="ddlDocumento" runat="server" class="form-control"
                    ClientIDMode="static" OnSelectedIndexChanged="ddlDocumento_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                </asp:DropDownList>                                
                <small><asp:RequiredFieldValidator id="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlDocumento" Display="Static" ErrorMessage="* Seleccione un tipo de identificación" runat="server"/></small>
            </div>
            <div class="col-sm-12 col-md-6">
                <label>Número de identificación *</label>
                <asp:TextBox ID="txtNumero" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su N° Identificación" OnTextChanged="txtNumero_TextChanged" onkeypress="return isNumber(event)" AutoPostBack="True"></asp:TextBox>
                <small><asp:RequiredFieldValidator id="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtNumero" Display="Static" ErrorMessage="* Ingrese su número de identificación" runat="server"/></small>
            </div>
        </div>
                            <div class="row">
                                <div class="col-xs-6 col-md-3">
                                    <label>Primer apellido *</label>
                                    <asp:TextBox ID="txtApellido1" runat="server" ClientIDMode="Static"
                                        class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>                                    
                                    <small><asp:RequiredFieldValidator id="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtApellido1" ErrorMessage="* ingrese su primer apellido" runat="server"/></small>
                                </div>
                                <div class="col-xs-6 col-md-3">
                                    <label>Segundo apellido</label>
                                    <asp:TextBox ID="txtApellido2" runat="server" ClientIDMode="Static"
                                        class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
                                    <small>&nbsp</small>                                    
                                </div>
                                <div class="col-xs-6 col-md-3">
                                    <label>Primer nombre *</label>
                                    <asp:TextBox ID="txtNombre1" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtNombre1" ErrorMessage="* ingrese su primer nombre" Display="Static" id="RequiredFieldValidator4" ForeColor="Red" runat="server"/></small>                                    
                                </div>
                                <div class="col-xs-6 col-md-3">
                                    <label>Segundo nombre</label>
                                    <asp:TextBox ID="txtNombre2" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
                                    <small>&nbsp</small>
                                </div>
                            </div>
                            <br />
    </div>
    <%-- BOTONES --%>
    <div class="row">
            <div class="col-12">                
                <div class="col-sm-12 text-center">       
                    <a class="btn btn-danger" href="../../Default.aspx" Style="padding: 3px 15px; width: 110px" role="button">volver</a>
                    <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
                </div>
            </div>
        </div>
        <br />
</asp:Content>

