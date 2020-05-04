<%@ Page Title=".: Retiro de Asociado :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RetiroAsociado.aspx.cs" Inherits="RetiroAsociado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .cssClass1 {
            background: Red;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass2 {
            background: Gray;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass3 {
            background: orange;
            color: black;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass4 {
            background: blue;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .cssClass5 {
            background: Green;
            color: White;
            font-weight: bold;
            font-size: x-small;
        }

        .BarBorder {
            border-style: solid;
            border-width: 1px;
            width: 180px;
            padding: 2px;
        }

        .answer{
            background-color: #337ab7;
            color: white;
            padding: 2px 3px;
            margin: auto;
            font-weight: 600;
        }

        label{
            font-weight: normal;
        }
    </style>


    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server">
            <asp:Label ID="txtCodPersona" runat="server" Visible="false" />
            <asp:Label ID="txtCambio" runat="server" Visible="false" />
            <asp:FormView ID="frvData" runat="server" Width="100%" OnDataBound="frvData_DataBound" visible="false">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td>
                                <h5 class="text-primary text-left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Información del Asociado</h5>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Identificación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtIdentificacion" runat="server" Text='<%# Eval("NumeroDocumento") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Nombre
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtNombre" runat="server" Text='<%# Eval("PrimerNombre") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Ciudad
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtCiudad" runat="server" Text='<%# Eval("SegundoNombre") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Dirección
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtDireccion" runat="server" Text='<%# Eval("Direccion") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Fecha Afiliación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="lblFechaAfiliacion" runat="server" Text='<%# Eval("FechaAfiliacion", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Tipo Cliente
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblTipoCliente" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                        <asp:Label ID="lblMotivo" Visible="false" runat="server" Text='<%# Eval("nommotivo") %>' />
                                        <asp:Label ID="Ciudad" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEstado" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <div style="text-align: center">
                <asp:Panel ID="pnlRetiroAsociado" Visible="true" runat="server" BackColor="White" Style="text-align: left; margin: auto auto" Width="100%">
                        <div class="col-sm-12 container">
                            <center>
                            <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                            </center>                            

                        <asp:Panel runat="server" ID="pnldatosRetiro">
                        <div class="col-sm-6">
                            <div class="col-sm-6 text-left">
                                Fecha Solicitud
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtFechaSolicitud" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="col-sm-6 text-left">
                                Motivo Retiro
                            </div>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="form-control"></asp:DropDownList>                                
                            </div>
                        </div> 
                            <%-- Preguntas finales --%>
                            <%-- pregunta 1 --%>
                            <div class="col-sm-12 answer">
                                <div class="col-sm-12">
                                    1. <asp:Label ID="lblPregunta1" runat="server" Text="¿Cuáles considera Usted son los aspectos positivos de FECEM?"></asp:Label>                            
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <br />
                                <div class="col-sm-12">
                                    <asp:RadioButtonList ID="rbPregunta1" runat="server">
                                        <asp:ListItem>Diversas opciones de ahorro</asp:ListItem>
                                        <asp:ListItem>Variedad en Líneas de crédito</asp:ListItem>
                                        <asp:ListItem>Servicios y convenios</asp:ListItem>
                                        <asp:ListItem>Facilidad de acceso al crédito y de Pago (Descuento de Nómina)</asp:ListItem>
                                        <asp:ListItem>Atención al Cliente y Gestión Comercial</asp:ListItem>
                                        <asp:ListItem>Garantías  y Beneficios (Tasa Interés Baja de Crédito, cero gastos estudio de crédito, pólizas de vida, tasa de interes de ahorros competitivas)</asp:ListItem>
                                        <asp:ListItem>Ninguno</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br />
                                </div>                            
                            </div>
                            <%-- pregunta 2 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-12">
                                2. <asp:Label ID="lblPregunta2" runat="server" Text="¿Cuáles considera usted son los aspectos por mejorar de FECEM?"></asp:Label>                            
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rbPregunta2" runat="server">
                                    <asp:ListItem>Mejorar el portafolio de Productos, Servicios y Convenios</asp:ListItem>
                                    <asp:ListItem>Disminuir Requisitos y tramitología de solicitud de crédito</asp:ListItem>
                                    <asp:ListItem>Mejorar la Atención al Cliente y la Gestión Comercial</asp:ListItem>
                                    <asp:ListItem>Fortalecer la información y servicios de accesibilidad ofrecidos en la página WEB</asp:ListItem>
                                    <asp:ListItem>Disminuir el tiempo para volver a afiliarse</asp:ListItem>
                                    <asp:ListItem>Ninguno</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </div>                            
                        </div>
                            <%-- pregunta 3 --%>

                            <div class="col-sm-12 answer">
                            <div class="col-sm-12">
                                3. <asp:Label ID="lblPregunta3" runat="server" Text="¿Cuáles eran sus expectativas al afiliarse a FECEM?"></asp:Label>                            
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rbPregunta3" runat="server">
                                    <asp:ListItem>Ahorrar y capitalizar los ahorros</asp:ListItem>
                                    <asp:ListItem>Obtener beneficios como Asociado</asp:ListItem>
                                    <asp:ListItem>Acceder a Crédito para suplir mis necesidades, sueños y alcanzar mis metas</asp:ListItem>
                                    <asp:ListItem>Ninguno</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </div>
                        </div>

                            <%-- pregunta 4 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-10">
                                4. <asp:Label ID="lblPregunta4" runat="server" Text="¿Cuál es el motivo real de su retiro de FECEM?"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rbPregunta4" runat="server">
                                    <asp:ListItem>Liberar Capacidad de Pago/Pago de Deudas</asp:ListItem>
                                    <asp:ListItem>Pensión/Jubilación</asp:ListItem>
                                    <asp:ListItem>Inconformidad</asp:ListItem>
                                    <asp:ListItem>Licencias/Incapacidades</asp:ListItem>
                                    <asp:ListItem>Compra de Vivienda</asp:ListItem>                                    
                                    <asp:ListItem>Ninguno</asp:ListItem>
                                    <asp:ListItem>Otras inversiones</asp:ListItem>
                                </asp:RadioButtonList>
                                ¿Cuáles?  <asp:TextBox runat="server" CssClass="form-control" style="width: 50%;" ID="txtOtras"></asp:TextBox>
                                <br />
                            </div>
                        </div>

                            <%-- pregunta 5 --%>

                            <div class="col-sm-12 answer">
                            <div class="col-sm-10">
                                5. <asp:Label ID="lblPregunta5" runat="server" Text="¿Califique su experiencia en FECEM de 1 a 10, siendo 1 la más baja y 10 la más alta?"></asp:Label>                                                            
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-6 col-lg-4 col-md-3">
                                <asp:DropDownList ID="ddlPregunta5" runat="server" CssClass="checkbox form-control">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </div>                            
                        </div>
                            <%-- pregunta 6 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-12">
                                6. <asp:Label ID="lblPregunta6" runat="server" Text="¿Tiene pólizas vigentes con FECEM?"></asp:Label>                            
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-4">
                                <asp:RadioButtonList id="rbPregunta6" runat="server">
                                   <asp:ListItem>Si</asp:ListItem>
                                   <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </div>
                        </div>

                            <%-- pregunta 7 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-12">
                                7. <asp:Label ID="lblPregunta7" runat="server" Text="¿Desea continuar con las pólizas vigentes?"></asp:Label>                            
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-4">
                                <asp:RadioButtonList id="rbPregunta7" runat="server">
                                   <asp:ListItem>Si</asp:ListItem>
                                   <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </div>
                        </div>
                            <%-- pregunta 8 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-12">
                                8. <asp:Label ID="lblPregunta8" runat="server" Text="¿Acepta reafiliarse automáticamente a FECEM vencido el periodo de 3 meses?"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                                <asp:RadioButtonList id="rbPregunta8" runat="server">
                                   <asp:ListItem>Si</asp:ListItem>
                                   <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                        </div>

                            <%-- pregunta 9 --%>
                            <div class="col-sm-12 answer">
                            <div class="col-sm-10">
                                9. <asp:Label ID="lblPregunta9" runat="server" Text="¿Cuál sería su nuevo aporte obligatorio (Cuota mínima según rango salarial)?"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-6 col-lg-4 col-md-3">
                                <asp:RadioButtonList id="rbPregunta9" runat="server">
                                    <asp:ListItem>Menor o igual a 3 SMLV $29.800</asp:ListItem>
                                    <asp:ListItem>Entre 3 SMLV y 6 SMLV $59.600</asp:ListItem>
                                    <asp:ListItem>Mayor a 6 SMLV $89.400</asp:ListItem>
                                    <asp:ListItem>Otro</asp:ListItem>    
                                </asp:RadioButtonList>
                                 ¿Cuál?<asp:TextBox runat="server" CssClass="form-control" style="width: 50%;" ID="txtOtroValor"></asp:TextBox>
                                <br />
                            </div>                            
                        </div>
                            <%-- Fin Preguntas finales --%>                            
                                 
                                                
                            <br />
                            <br />
                        <div class="col-sm-12 center">
                            <div class="col-sm-10 text-center">
                                <br />
                            <asp:Button ID="btnGuardarSolicitud" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar"
                                OnClick="btnGuardarSolicitud_Click" />
                            </div>
                        </div>
                        <div class="modal-footer">                            
                        </div>
                            </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

