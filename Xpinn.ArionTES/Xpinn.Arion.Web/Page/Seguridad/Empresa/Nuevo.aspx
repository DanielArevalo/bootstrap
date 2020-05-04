<%@ Page Title=".: Datos De Empresa :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagPrefix="uc1" TagName="ctlFecha" %>
<%@ Register Src="~/General/Controles/ctlDireccion.ascx" TagPrefix="uc1" TagName="ctlDireccion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        var imgControlID = '<%= imgFoto.ClientID  %>';
        var uploadFileID = '<%= avatarUpload.ClientID  %>';
        var hiddenFieldID = '<%= hiddenFieldImageData.ClientID  %>';
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewDatos">
            <table style="width: 100%;">
                <tr>
                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px;">
                        <strong>Datos De La Empresa</strong>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: left; width: 50px">Razon Social:
               <br />
                        <asp:TextBox runat="server" ID="txtrazonsocial" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 50px">NIT:
                    <br />
                        <asp:TextBox runat="server" ID="txtnit" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 50px">Telefono:
              <br />
                        <asp:TextBox runat="server" ID="txttelefono" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 170px">Fecha De Fundacion:
            <br />
                        <uc1:ctlFecha runat="server" ID="ctlFecha" />
                    </td>

                    <td rowspan="6" style="text-align: center; width: 30px">
                        <br />
                        <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                        <asp:HiddenField ID="hiddenFieldImageData" ClientIDMode="Static" runat="server" />
                        <br />
                        <input id="avatarUpload" type="file" name="file" onchange="javascript:PrevisualizarArchivoCargadoYGuardarEnHidden(imgControlID, uploadFileID, hiddenFieldID);" runat="server" />
                        <asp:TextBox
                            ID="txtCod_persona"
                            runat="server"
                            CssClass="textbox"
                            Enabled="False"
                            MaxLength="80"
                            Visible="False"
                            Width="120px" />

                    </td>

                </tr>
                <tr>
                    <td style="text-align: left">Sigla:
              <br />
                        <asp:TextBox runat="server" ID="txtsigla" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Ciudad:
                <br />
                        <asp:DropDownList runat="server" ID="ddlciudad" CssClass="textbox"></asp:DropDownList>
                    </td>
                    <td style="text-align: left;" colspan="2">Nombre Del Logo De la Empresa:
                            <br />
                        <asp:TextBox ID="txtnomimg" runat="server" CssClass="textbox" Width="77%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 60px; text-align: left">Correo:
                <br />
                        <asp:TextBox runat="server" ID="txtcorreo" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Clave de E-mail:
               <br />
                        <asp:TextBox runat="server" ID="txtpassword" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="height: 60px; text-align: left">Representante Legal:
               <br />
                        <asp:TextBox runat="server" ID="txtrepresentante" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Contador:
               <br />
                        <asp:TextBox runat="server" ID="txtcontador" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 60px; text-align: left">N° Tajeta Profesional:
               <br />
                        <asp:TextBox runat="server" ID="txttarjetacontador" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Tipo:
              <br />
                        <asp:TextBox runat="server" ID="txtxtipo" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="height: 60px; text-align: left">Reporte De Egreso:
                <br />
                        <asp:TextBox runat="server" ID="txtxegreso" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Revisor:
                <br />
                        <asp:TextBox runat="server" ID="txtxrevisor" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 60px; text-align: left">Codigo Persona:
              <br />
                        <asp:TextBox runat="server" ID="txtcodpersona" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Reporte de Ingreso:
              <br />
                        <asp:TextBox runat="server" ID="txtingreso" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="height: 60px; text-align: left">Codigo UIAF:
              <br />
                        <asp:TextBox runat="server" ID="txtxcodigoUIAF" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">Manejo De Sincornizacion:
               <br />
                        <asp:DropDownList runat="server" ID="ddlManejo" CssClass="textbox">
                            <asp:ListItem Value="0"> NO </asp:ListItem>
                            <asp:ListItem Value="1"> Si </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>                    
                    <td style="text-align: left">Tipo empresa:
                    <br />
                        <asp:DropDownList runat="server" ID="ddlTipoEmp" CssClass="textbox">
                            <asp:ListItem Value=""> Seleccione </asp:ListItem>
                            <asp:ListItem Value="0"> Abierta </asp:ListItem>
                            <asp:ListItem Value="1"> Cerrada </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <table>
                <tr>
                    <td style="text-align: left">Direccion
                    </td>
                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td>
                        <uc1:direccion ID="txtdir" runat="server"></uc1:direccion>
                    </td>
                </tr>
            </table>



            <table style="width: 100%;">
                <tr>
                    <td style="height: 17px; text-align: left;" colspan="4">
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdI" style="color: #FFFFFF; background-color: #0066FF; height: 20px; text-align: center;">Datos de facturación </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 60px; text-align: left">Descripción Regimen:
                        <br />
                        <asp:TextBox ID="txtdescripcionRegimen" runat="server"  width= "300px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="height: 60px; text-align: left">&nbsp;</td>
                    <td style="text-align: left">Resolución facturación:
                        <br />
                        <asp:TextBox ID="txtResolucionfacturacion" runat="server"   width= "300px"  CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px;">&nbsp;</td>
                </tr>
            </table>

        </asp:View>

        <asp:View ID="vFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos Empresa Actualizados Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

</asp:Content>
