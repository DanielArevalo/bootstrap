<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R03_Contacto.aspx.cs" Inherits="Pages_Afiliacion_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <%@ Register Src="~/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc2" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%-- SECCION CONTACTO --%>
                        <div onclick="ocultarMostrarPanel('contacto')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">2. Informaci&oacute;n contacto personal<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="contacto">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <label>Dirección de residencia *</label>
                                    <uc2:Direccion ID="txtDireccion" runat="server" Width="90%" CssClass="form-control required"
                                            TabIndex="11" required="required"></uc2:Direccion>                                    
                                </div>                                                                            
                            </div>
                            <br />
                            <div class="row">                                
                                <div class="col-sm-12 col-md-4">
                                    <label>Departamento de residencia *</label>
                                    <asp:DropDownList ID="ddlDepartamento" runat="server" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="0">Pendiente</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Ciudad de residencia *</label>
                                    <asp:DropDownList ID="ddlCiudad" runat="server" OnSelectedIndexChanged="ddlCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" >
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="0">Pendiente</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Barrio</label>                                    
                                    <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="0">Pendiente</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>                            
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label>Tipo de vivienda *</label>
                                    <asp:DropDownList ID="ddlTipoVivienda" runat="server"
                                        CssClass="form-control"
                                        TextAlign="Left" Width="80%">
                                        <asp:ListItem Selected="True" Value="">&nbsp;Seleccione&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="P">&nbsp;Propia&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="A">&nbsp;Arrendada&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="F">&nbsp;Familiar&nbsp;</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><asp:RequiredFieldValidator ControlToValidate="ddlTipoVivienda" ErrorMessage="* seleccione un tipo de vivienda" Display="Static" id="RequiredFieldValidator14" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Estrato social</label>
                                    <asp:DropDownList ID="cblEstrato" runat="server" class="form-control">
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><asp:RequiredFieldValidator ControlToValidate="cblEstrato" ErrorMessage="* seleccione un estrato" Display="Static" id="RequiredFieldValidator15" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Afecta vivienda familiar</label>
                                    <asp:RadioButtonList ID="cbAfectaVivienda" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                                        <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Tiempo en la vivienda</label>
                                    <div class="row">
                                        <div class="col-sm-6 col-md-6">
                                            <asp:TextBox ID="txtAñosVivienda" runat="server" class="form-control" type="text" placeholder="Años" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6 col-md-6">
                                            <asp:TextBox ID="txtMesesVivienda" runat="server" class="form-control" type="text" placeholder="Meses" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" TargetControlID="txtAñosVivienda" ValidChars="-." />
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" TargetControlID="txtMesesVivienda" ValidChars="-." />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <label>Email *</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                        placeholder="alguien@example.com" type="email"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="* indique un email valido" Display="Static" id="RequiredFieldValidator1" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Teléfono fijo de residencia</label>
                                    <asp:TextBox ID="txtTelefono" onkeypress="return ValidaNum(event);" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label runat="server" for="txtCelular">Celular personal *</label>
                                    <asp:TextBox ID="txtCelular" runat="server" onkeypress="return ValidaNum(event);" CssClass="form-control"></asp:TextBox>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtCelular" ErrorMessage="* indique su número de celular" Display="Static" id="RequiredFieldValidator17" ForeColor="Red" runat="server"/></small>                                    
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- FIN SECCION CONTACTO --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R02_Asociado.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>

