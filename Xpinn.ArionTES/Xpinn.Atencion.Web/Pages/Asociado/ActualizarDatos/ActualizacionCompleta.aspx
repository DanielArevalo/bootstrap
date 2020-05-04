<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="ActualizacionCompleta.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">        
    <link href="~/Css/bootstrap2.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/PCLBryan.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <%@ Register Src="~/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc2" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%-- SECCION ASOCIADO --%>
    <h6 style="text-align:center">2/11</h6>
                        <div onclick="ocultarMostrarPanel('asociado')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">1. Datos del Asociado<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="asociado">
                            <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
        <br />
                            <div class="row">
                                <div class="input-field col-xs-6 col-md-6 form-group">
                                    <label>Apellido </label>
                                    <asp:TextBox ID="txtApellido1" runat="server" ClientIDMode="Static" ReadOnly="true"
                                        class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
                                </div>
                                <div class="input-field col-xs-6 col-md-6 form-group">
                                    <label>Nombre </label>
                                    <asp:TextBox ID="txtNombre1" runat="server" ReadOnly="true" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
                                </div>
                            </div>
                            <br />                            
                        </div>
                        <%-- FIN SECCION ASOCIADO --%>
    <div onclick="ocultarMostrarPanel('contacto')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">2. Informaci&oacute;n contacto personal<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="contacto">
                            <br />
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
                                    </asp:DropDownList>                                    
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Ciudad de residencia *</label>
                                    <asp:DropDownList ID="ddlCiudad" runat="server" OnSelectedIndexChanged="ddlCiudad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" >
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <label>Barrio</label>                                    
                                    <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
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
                                        <asp:ListItem Value="">&nbsp;No informa&nbsp;</asp:ListItem>
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
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtTelefono" ErrorMessage="* indique un telefono de residencia" Display="Static" id="RequiredFieldValidator16" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label runat="server" for="txtCelular">Celular personal *</label>
                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtCelular" ErrorMessage="* indique su número de celular" Display="Static" id="RequiredFieldValidator17" ForeColor="Red" runat="server"/></small>                                    
                                </div>
                            </div>
                            <br />
                        </div>
                        <div onclick="ocultarMostrarPanel('laboral')" style='cursor: pointer;'>
                            <div class="col-md-12">
                                <a href="#!" class="accordion-titulo" style="">3. Informaci&oacute;n contacto laboral<span class="toggle-icon"></span></a>
                            </div>
                        </div>
                        <div class="col-md-12" id="laboral">
                            <div class="row">
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtDiaInicio" runat="server">Fecha de ingreso</label>
                                    <asp:TextBox ID="txtDiaInicio" runat="server" class="datepicker form-control" ClientIDMode="Static" type="text" />
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label runat="server">Cargo actual</label>
                                    <asp:DropDownList ID="ddlCargo" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <small><asp:RequiredFieldValidator ControlToValidate="ddlCargo" ErrorMessage="* seleccione su cargo" Display="Static" id="RequiredFieldValidator20" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Tipo de contrato</label>
                                    <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <small><asp:RequiredFieldValidator ControlToValidate="ddlTipoContrato" ErrorMessage="* seleccione un tipo de contrato" Display="Static" id="RequiredFieldValidator21" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label class="active" for="txtIngsalariomensual">salario básico</label>
                                    <uc1:decimales ID="txtIngsalariomensual" class="form-control" runat="server" Width_="100%" />
                                    
                                </div>
                            </div>
                            <br />
                            <div class="row">                                
                                <div class="col-sm-12 col-md-3">
                                    <label for="txtCorreoCorporativo" data-error="email no valido" data-success="right">Correo corporativo *</label>
                                    <asp:TextBox ID="txtCorreoCorporativo" runat="server" CssClass="form-control" type="email" placeholder="alguien@example.com"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="color: Red; font-weight: 200; font-size: 12px;" runat="server" ControlToValidate="txtCorreoCorporativo"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="email no valido"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label>Celular o teléfono corporativo</label>
                                    <asp:TextBox ID="txtTelefonolaboral" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="fte10" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonolaboral" ValidChars="-()" />
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    <label runat="server" for="txtProfesion">Profesión</label>
                                    <asp:TextBox ID="txtProfesion" runat="server" CssClass="form-control"></asp:TextBox>
                                    <small><asp:RequiredFieldValidator ControlToValidate="txtProfesion" ErrorMessage="* ingrese su profesion" Display="Static" id="RequiredFieldValidator23" ForeColor="Red" runat="server"/></small>
                                </div>
                                <div class="col-sm-12 col-md-3">
                                    &nbsp;
                                </div>
                            </div>
                            <br />                            
                        </div>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="../../../Default.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />                
            </div>
        </div>
    </div>
    <br />
    <script type="text/javascript" src="//code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="//www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(".datepicker").datepicker({
            maxDate: "0d"
        });

        $(".accordion-titulo").click(function () {

            var contenido = $(this).next(".accordion-content");

            if (contenido.css("display") == "none") { //open
                $(".accordion-titulo").removeClass("open");
                $(".accordion-content").slideUp(250);
                contenido.slideDown(250);
                $(this).addClass("open");
            }
            else { //close		
                contenido.slideUp(250);
                $(this).removeClass("open");
            }
        });
    </script>
</asp:Content>

