<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <div class="form-group">
                <div class="col-sm-12">
                    <div class="col-sm-6">
                        Primer nombre
                        <asp:TextBox ID="txtNombre1" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:FilteredTextBoxExtender ID="fte50" runat="server" Enabled="True" TargetControlID="txtNombre1"
                            ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ" />
                    </div>
                    <div class="col-sm-6">
                        Segundo nombre
                        <asp:TextBox ID="txtNombre2" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                            TargetControlID="txtNombre2" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-6">
                        Primer apellido<br />
                        <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                            TargetControlID="txtApellido1" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ" />
                    </div>
                    <div class="col-sm-6">
                        Segundo apellido<br />
                        <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                            TargetControlID="txtApellido2" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div>
                <div class="col-sm-12">
                <div class="col-sm-12">
                    Dirección de residencia *<br />
                    <uc2:Direccion ID="txtDireccionResid" runat="server" Width="90%" CssClass="form-control required"
                            TabIndex="11" required="required"></uc2:Direccion>                                    
                </div>  
                    </div>
                <div class="col-sm-12">
                    <br />
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-3">
                        Ciudad de residencia<br />
                        <asp:DropDownList ID="ddlCiudadResid" runat="server" CssClass="form-control" Width="100%" />
                    </div>
                    <div class="col-sm-3">
                        Teléfono<br />
                        <asp:TextBox ID="txtTelefonoResid" runat="server" CssClass="form-control" MaxLength="12" />
                        <asp:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                            TargetControlID="txtTelefonoResid" ValidChars="" />
                    </div>
                    <div class="col-sm-3">
                        Celular<br />
                        <asp:TextBox ID="txtCelularResid" runat="server" CssClass="form-control" MaxLength="12" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers, Custom"
                            TargetControlID="txtCelularResid" ValidChars="" />
                    </div>
                    <div class="col-sm-3">
                        Ciudad de oficina<br />
                        <asp:DropDownList ID="ddlCiudadOfi" runat="server" CssClass="form-control" Width="100%" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                </div>                
                <div class="col-sm-12">
                    
                    <div class="col-sm-3">
                        Dirección de oficina<br />
                        <asp:TextBox ID="txtDireccionOfi" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-3">
                        Teléfono de oficina<br />
                        <asp:TextBox ID="txtTelefonoOfi" runat="server" CssClass="form-control" MaxLength="12" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                            FilterType="Numbers, Custom" TargetControlID="txtTelefonoOfi" ValidChars="" />
                    </div>
                    <div class="col-sm-6">
                        Correo electrónico personal<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="alguien@example.com" />
                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgGuardar"></asp:RegularExpressionValidator>
                    </div>
                    
                </div>
                <div class="col-sm-12">
                    <br />
                </div>
                <div class="col-sm-12">                    
                    <div class="col-sm-6" style="display:none">
                        Correo electrónico laboral<br />
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="alguien@example.com" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="vgGuardar"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-12">
                                    <br />
                <br />
                    <asp:Button ID="btnRegistrar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar" OnClick="btnRegistrar_Click" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="form-group col-lg-12 text-center">
               <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="La actualización esta pendiente por autorización."
                            Style="color: #66757f; font-size: 28px;" />
                        <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: 28px;" />
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:Button ID="btnVolver" CssClass="btn btn-primary" runat="server" Text="Volver" Style="padding: 3px 15px; width: 110px; margin: 0 auto" OnClick="btnVolver_Click" />
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:View>
    </asp:MultiView>
</asp:Content>
