<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R04_Laboral.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <script runat="server">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%-- SECCION LABORAL --%>
    <div onclick="ocultarMostrarPanel('laboral')" style='cursor: pointer;'>
        <div class="col-md-12">
            <a href="#!" class="accordion-titulo" style="">3. Informaci&oacute;n contacto laboral<span class="toggle-icon"></span></a>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updLaboral">
        <ContentTemplate>
            <div class="col-md-12" id="laboral">
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-6">
                        Ocupación
                                    <asp:RadioButtonList runat="server" ID="chkOcupacion" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkOcupacion_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">Empleado &nbsp &nbsp &nbsp</asp:ListItem>
                                        <asp:ListItem Value="2">Independiente &nbsp &nbsp &nbsp</asp:ListItem>
                                        <asp:ListItem Value="3">Pensionado &nbsp &nbsp &nbsp</asp:ListItem>
                                    </asp:RadioButtonList>
                    </div>
                    <div class="col-sm-12 col-md-3">
                        <label for="txtDireccionLaboral" runat="server">Zona *</label>
                        <asp:DropDownList ID="ddlZonaLaboral" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <small>
                            <asp:RequiredFieldValidator ControlToValidate="ddlZonaLaboral" ErrorMessage="* seleccione una zona" Display="Static" ID="RequiredFieldValidator19" ForeColor="Red" runat="server" /></small>
                    </div>
                </div>
                <br />
                <asp:Panel runat="server" ID="pnlEmpleado">
                    <div class="row">
                        <div class="col-sm-12 col-md-3">
                            <label for="ddlEmpresa" runat="server">Empresa *</label>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                            <small>
                                <asp:RequiredFieldValidator ControlToValidate="ddlEmpresa" ErrorMessage="* seleccione una empresa" Display="Static" ID="ddlEmpresavalidator" ForeColor="Red" runat="server" Enabled="false" /></small>
                            <small>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmpresa" ErrorMessage="* ingrese la empresa" Display="Static" ID="txtEmpresaValidator" ForeColor="Red" runat="server" Enabled="false" /></small>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label>Departamento empresa *</label>
                            <asp:DropDownList ID="ddlDepartamentoLaboral" OnSelectedIndexChanged="ddlDepartamentoLaboral_SelectedIndexChanged" AutoPostBack="true" runat="server" required CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label>Ciudad empresa *</label>
                            <asp:DropDownList ID="ddlCiudadLaboral" required runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12 col-md-3" style="display: none">
                            <label for="txtNit" runat="server">Nit de la empresa en la que labora</label>
                            <asp:TextBox ID="txtNit" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label for="txtDiaInicio" runat="server">Fecha de ingreso *</label>
                            <asp:TextBox ID="txtDiaInicio" runat="server" class="datepicker form-control" Type="Date" placeholder="fecha de expedición" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 col-md-3">
                            <label runat="server">Cargo actual *</label>
                            <asp:DropDownList ID="ddlCargo" required runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <small>
                                <asp:RequiredFieldValidator ControlToValidate="ddlCargo" ErrorMessage="* seleccione su cargo" Display="Static" ID="RequiredFieldValidator20" ForeColor="Red" runat="server" /></small>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label>Tipo de contrato *</label>
                            <asp:DropDownList ID="ddlTipoContrato" required runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <small>
                                <asp:RequiredFieldValidator ControlToValidate="ddlTipoContrato" ErrorMessage="* seleccione un tipo de contrato" Display="Static" ID="RequiredFieldValidator21" ForeColor="Red" runat="server" /></small>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label class="active" for="txtIngsalariomensual">salario básico *</label>
                            <uc1:decimales ID="txtIngsalariomensual" class="form-control" runat="server" required Width_="100%" />
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label for="txtCodNomina" runat="server">Código de nómina</label>
                            <asp:TextBox ID="txtCodNomina" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 col-md-3">
                            <label for="txtCorreoCorporativo" data-error="email no valido" data-success="right">Correo corporativo</label>
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
                            <label runat="server" for="txtProfesion">Profesión *</label>
                            <asp:TextBox ID="txtProfesion" runat="server" required CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <label>Nivel Academico *</label>
                            <asp:DropDownList ID="cbNivelAcademico" required runat="server" class="form-control">
                            </asp:DropDownList>
                            <small>
                                <asp:RequiredFieldValidator ControlToValidate="cbNivelAcademico" ErrorMessage="* seleccione su nivel de escolaridad" Display="Static" ID="RequiredFieldValidator24" ForeColor="Red" runat="server" /></small>
                        </div>
                    </div>
                    <br />


                    <%--  <div class="row">
                        <div class="message">
                            Si su ocupación adicional es independiente, profesional independiente, comerciante, ganadero, agricultor o rentista de capital, por favor diligencie esta información.
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 col-md-8">
                            <label runat="server" for="TxtDescripcionEconomica">Detalle de la actividad económica especial</label>
                            <asp:TextBox ID="TxtDescripcionEconomica" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <label runat="server" for="TxtCiiu">Código CIIU</label>
                            <asp:TextBox ID="TxtCiiu" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <label runat="server" for="txtNumEmpleados">N° empleados</label>
                            <asp:TextBox ID="txtNumEmpleados" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txtNumEmpleados" ValidChars="-." />
                        </div>
                    </div>--%>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelIndependiente">
                    <div class="row">
                        <div class="message">
                            Si su ocupación  es independiente, profesional independiente, comerciante, ganadero, agricultor o rentista de capital, por favor diligencie esta información.
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 col-md-8">
                            <label runat="server" for="TxtDescripcionEconomica">Detalle de la actividad económica especial</label>
                            <asp:TextBox ID="TxtDescripcionEconomica" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <label runat="server" for="TxtCiiu">Código CIIU</label>
                            <asp:TextBox ID="TxtCiiu" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <label runat="server" for="txtNumEmpleados">N° empleados</label>
                            <asp:TextBox ID="txtNumEmpleados" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txtNumEmpleados" ValidChars="-." />
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <%-- BOTONES --%>
                <div class="row">
                    <div class="col-12">
                        <div class="col-sm-12 text-center">
                            <a class="btn btn-danger" href="R03_Contacto.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
                            <asp:Button ID="btnContinuar" CssClass="btn btn-success" Style="padding: 3px 15px; width: 110px" runat="server" Text="continuar" />
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
 
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

