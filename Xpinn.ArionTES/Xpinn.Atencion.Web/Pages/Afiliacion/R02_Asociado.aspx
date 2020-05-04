<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Afiliacion/Register.master" AutoEventWireup="true" CodeFile="R02_Asociado.aspx.cs" Inherits="Pages_Afiliacion_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .grid-head {
            background-color: hsl(182,25%,50%);
            text-align: center;
            font-size: small;
        }

        .gridItem > td > input, select {
            text-align: center;
            border: none;
            width: 90%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%-- SECCION ASOCIADO --%>
    <%-- 2/11 --%>
    <div onclick="ocultarMostrarPanel('asociado')" style='cursor: pointer;'>
        <div class="col-md-12">
            <a href="#!" class="accordion-titulo" style="">Datos del Asociado<span class="toggle-icon"></span></a>
        </div>
    </div>
    <div class="col-md-12" id="asociado">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 15px; text-align: center; display: block;" />
            </div>
        </div>
        <div class="row" style="display: none">
            <div class="input-field col-xs-6 col-md-3 form-group">
                <label>Primer apellido *</label>
                <asp:TextBox ID="txtApellido1" runat="server" ClientIDMode="Static" ReadOnly="true"
                    class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
            </div>
            <div class="input-field col-xs-6 col-md-3 form-group">
                <label>Segundo Apellido</label>
                <asp:TextBox ID="txtApellido2" runat="server" ClientIDMode="Static" ReadOnly="true"
                    class="form-control" type="text" placeholder="Ingrese aquí su apellido"></asp:TextBox>
            </div>
            <div class="input-field col-xs-6 col-md-3 form-group">
                <label>Primer Nombre *</label>
                <asp:TextBox ID="txtNombre1" runat="server" ReadOnly="true" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
            </div>
            <div class="input-field col-xs-6 col-md-3 form-group">
                <label>Segundo Nombre</label>
                <asp:TextBox ID="txtNombre2" ReadOnly="true" runat="server" class="form-control" type="text" placeholder="Ingrese aquí su nombre"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-3">
                <label for="inputState">Tipo de documento *</label>
                <asp:DropDownList ID="ddlDocumento" ReadOnly="true" runat="server" class="form-control"
                    ClientIDMode="static">
                    <asp:ListItem Selected="True" Value="">Seleccione un item</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Número de identificación *</label>
                <asp:TextBox ID="txtNumeroDocumento" runat="server" class="form-control" type="text" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12">
                <label>Ciudad de Expedición *</label>
                <asp:DropDownList ID="ddlCiudadExpedicion" runat="server" class="form-control">
                </asp:DropDownList>
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="ddlCiudadExpedicion" ErrorMessage="* seleccione la ciudad de expedición" Display="Static" ID="RequiredFieldValidator5" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-md-3 col-sm-12">
                <label>Fecha de expedición*</label>
                <asp:TextBox ID="txtDia" runat="server" class="datepicker form-control" type="text" placeholder="fecha de expedición" />
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="txtDia" ErrorMessage="* seleccione la fecha de expedición" Display="Static" ID="RequiredFieldValidator6" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Ciudad de Nacimiento *</label>
                <asp:DropDownList ID="ddlCiudadNacimiento" runat="server" class="form-control">
                </asp:DropDownList>
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="ddlCiudadNacimiento" ErrorMessage="* seleccione la ciudad de nacimiento" Display="Static" ID="RequiredFieldValidator8" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Fecha de Nacimiento *</label>
                <asp:TextBox ID="txtDianacimiento" runat="server" class="datepicker form-control" placeholder="fecha de nacimiento" type="text" OnTextChanged="txtDianacimiento_TextChanged" AutoPostBack="True" />
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="txtDianacimiento" ErrorMessage="* seleccione la fecha de nacimiento" Display="Static" ID="RequiredFieldValidator9" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Nacionalidad *</label>
                <asp:DropDownList ID="ddlNacionalidad" runat="server" class="form-control">
                </asp:DropDownList>
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="ddlNacionalidad" ErrorMessage="* seleccione nacionalidad" Display="Static" ID="RequiredFieldValidator10" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Genero</label>
                <asp:RadioButtonList ID="cblSexoPer" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%">
                    <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160Hombre&#160&#160&#160&#160</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160Mujer&#160&#160&#160</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="col-sm-12 col-md-3">
                <label runat="server">Estado Civil</label>
                <asp:DropDownList ID="cblEstadoCivil" runat="server" class="form-control">
                    <asp:ListItem Selected="true" Value="">Seleccione un item</asp:ListItem>
                </asp:DropDownList>
                <small>
                    <asp:RequiredFieldValidator ControlToValidate="cblEstadoCivil" ErrorMessage="* seleccione su estado civil" Display="Static" ID="RequiredFieldValidator11" ForeColor="Red" runat="server" /></small>
            </div>
            <div class="col-sm-12 col-md-3">
                <label>Familiares o personas dependientes</label>
                <asp:RadioButtonList ID="cblCabezaFamilia" runat="server" CellPadding="1" CellSpacing="1" RepeatColumns="2" RepeatLayout="Flow" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="cblCabezaFamilia_SelectedIndexChanged">
                    <asp:ListItem Value="1" style="margin-right: 3px; margin-top: 2%;">&#160&#160SI&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160&#160</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0" style="margin-top: 2%;">&#160NO&#160&#160&#160</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <br />
        <div class="row">
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-3" style="display: none">
                <label for="txtPersonaCargo">Personas a cargo</label>
                <asp:TextBox ID="txtPersonaCargo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-6"></div>
        </div>
        <div runat="server" id="dependientes" visible="false">
            <asp:UpdatePanel runat="server" ID="updBenef">
                <ContentTemplate>
                    <asp:Button ID="btnAddRow" runat="server" class="btn btn-info" TabIndex="21" OnClick="btnAddRow_Click"
                        Text="+ Agregar familiar o persona dependiente" UseSubmitBehavior="false" />
                    <asp:GridView ID="gvBeneficiarios"
                        runat="server" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="cod_beneficiario"
                        ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                        PageSize="10" ShowFooter="false" Style="font-size: xx-small; padding-top: 10px;" Width="100%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Imagenes/gr_elim.jpg" ShowDeleteButton="True" />

                            <asp:TemplateField HeaderText="Nombres" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="text-align: left"
                                        Text='<%# Bind("nombres") %>'> </asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Apellidos" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Style="text-align: left"
                                        Text='<%# Bind("apellidos") %>'> </asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo identificación" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbltipoid" runat="server" Text='<%# Bind("tipo_id") %>'
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoId" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Style="text-align: left"
                                        Text='<%# Bind("identificacion") %>' CommandArgument="<%#Container.DataItemIndex %>"> </asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sexo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSexo" runat="server" Text='<%# Bind("sexo") %>'
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlSexo" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFechaNac" runat="server" class="datepicker" Style="font-size: xx-small; text-align: center"
                                        Text='<%# Eval("fecha_nac", "{0:d}") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ocupación" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOcupacion" runat="server" CssClass="textbox" Style="text-align: left"
                                        Text='<%# Bind("ocupacion") %>' CommandArgument="<%#Container.DataItemIndex %>"> </asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nivel educativo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNivel" runat="server" Text='<%# Bind("nivel_educativo") %>'
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlNivel" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("codparentesco") %>'
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlParentesco" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle CssClass="grid-head" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <br />
    <%-- FIN SECCION ASOCIADO --%>
    <%-- BOTONES --%>
    <div class="row">
        <div class="col-12">
            <div class="col-sm-12 text-center">
                <a class="btn btn-danger" href="R01_Identificacion.aspx" style="padding: 3px 15px; width: 110px" role="button">volver</a>
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

