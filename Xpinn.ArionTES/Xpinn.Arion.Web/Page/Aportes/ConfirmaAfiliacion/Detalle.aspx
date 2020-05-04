<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelDataBasic" runat="server" BorderWidth="2" BorderColor="#0066FF">
        <div style="text-align: center; color: #FFFFFF; background-color: #0066FF; width: 100%">
            <strong>Datos Personales</strong>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Visible="false"/>
        </div>
        <table cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="text-align: left; width: 50%">
                    <table width="100%">
                        <tr>
                            <td style="text-align: left; width: 30%">
                                Fecha de Solicitud
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblFechaSoli" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: left; width: 50%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 50%">
                    <table width="100%">
                        <tr>
                            <td style="text-align: left; width: 20%">
                                Nombres
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblNombres" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: left; width: 50%">
                    <table width="100%">
                        <tr>
                            <td style="text-align: left; width: 20%">
                                Apellidos
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="lblApellidos" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="text-align: left; width: 13%">
                    Tipo Identificación
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblTipoIdentificacion" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Identificación
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblIdentificacion" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Sexo
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblSexo" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Fecha Expedición
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblFechaExp" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Ciudad Expedición
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCiudadExp" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Estado Civil
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblEstadoCiv" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Fecha Nacimiento
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblFechaNac" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Ciudad Nacimiento
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCiudadNac" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Cabeza de Familia
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCabezaFam" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Personas a Cargo
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblPersonasCargo" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Nivel Académico
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblNivelAcad" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Profesión
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblProfesion" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Dirección
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblDireccion" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Ciudad
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCiudad" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Email
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Barrio
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblBarrio" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Teléfono
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblTelefono" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Celular
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCelular" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelDataLaboral" runat="server" BorderWidth="2" BorderColor="#0066FF">
        <div style="text-align: center; color: #FFFFFF; background-color: #0066FF; width: 100%">
            <strong>Información Laboral</strong>
        </div>
        <table cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="text-align: left; width: 13%">
                    Empresa
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblEmpresa" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Nit
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblNit" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Teléfono
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblTeleLabor" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Dirección
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblDireccionEmp" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Ciudad
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCiudadEmp" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Tipo de Contrato
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblTipoContrato" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Cargo Contacto</td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblCargo" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Fecha Inicio
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblFecInicioLab" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Fecha Ult Liquidación
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblFecUltLiqui" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 13%">
                    Salario
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblSalario" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Otros Ing
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblOtrosIng" runat="server" />
                </td>
                <td style="text-align: left; width: 12%">
                    Deducciones
                </td>
                <td style="text-align: left; width: 21%">
                    <asp:Label ID="lblDeduccion" runat="server" />
                </td>
            </tr>            
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:View ID="mvFinal" runat="server">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;" >                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensajeGrabar" runat="server" Text="Registros modificados correctamente"></asp:Label><br />                           
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
        </asp:View> 
    </asp:Panel>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
