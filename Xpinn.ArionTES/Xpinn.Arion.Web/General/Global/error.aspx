<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="General_Global_error" %>

<%@ Register Src="~/General/Controles/EnviarCorreo.ascx" TagPrefix="uc1" TagName="EnviarCorreo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Expinn Technology</title>
    <link rel="stylesheet" href="../../Styles/Styles.css" />
</head>
<body class="modulo">
    <header>
        <div class="logo fl-lt"><img alt="logo"  src="../../Images/logoInterna.jpg" /></div>
    </header>
    <form id="form1" class="form-horizontal" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="clear"></div>
        <div class="contentModulos">

            <h1><asp:Label style="color: #FFFFFF; font-weight: bold" runat="Server" ID ="ErrorIneseperado" Text="<%$  Resources:Resource,Error_Inesperado%>" /></h1>
            <br />
            <h3><asp:Label style="color: #FFFFFF; font-weight: bold" runat="Server" ID ="Literal1" Text="<%$  Resources:Resource,Mensaje_ErrorExplicacion%>" /></h3>
            <br />
            <asp:Button runat="server" ID="EnviarCorreoError" OnClick="EnviarError_Click" Text="<%$  Resources:Resource,Enviar_Error%>" />
            <br />
            <br />
            <asp:Button runat="server" ID="VolverPaginaAnterior" OnClick="btnVolver_OnClick" Text="<%$  Resources:Resource,Mensaje_volver%>" />
            <br />
            <br />
            <asp:Button runat="server" ID="VerFormulario" Text="<%$  Resources:Resource,Ver_Error%>" OnClick="VerFormulario_Click" />
            <div class="fl-rt">
            </div>
        </div>
        <asp:Panel runat="server" Visible="false" ID="pnlError">
            <div style="background-color: #fff; margin-top: 10px; width: 60%" runat="server">
                <br />
                <h1 style="color: #0099cc; font-size: 25px; text-align: center; margin-top: 20px;"><asp:Literal runat="Server" ID ="Literal2" Text="<%$  Resources:Resource,Mensaje_Error_Inesperado%>" /></h1>
                <label style="color: #0099cc; text-align: center;"><asp:Literal runat="Server" ID ="Literal3" Text="<%$  Resources:Resource,Revisar_Codigo%>" /></label>
                <div>
                    <div class="input-group">
                        <br />
                        <table runat="server" width="90%" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <h4 style="color: #0099ff; font-weight: normal; text-align: left;"><asp:Literal runat="Server" ID ="Literal4" Text="<%$  Resources:Resource,Nombre_Empresa%>" /></h4>
                                    <label runat="server" text="Nombre Empresa" style="color: #0099cc;"></label>
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNombreEmpresa" runat="server" Style="width: 80%; height: 3em; margin: 0px auto;" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <table runat="server" width="90%" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <h4 style="color: #0099ff; font-weight: normal; text-align: left;">URL Error</h4>
                                    <label runat="server" text="Nombre Empresa" style="color: #0099cc;"></label>
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtUrlError" runat="server" Style="width: 80%; height: 3em; margin: 0px auto;" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <br />
                        <table width="90%" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <h4 style="color: #0099ff; font-weight: normal; text-align: left;"><span><asp:Literal runat="Server" ID ="Literal5" Text="<%$  Resources:Resource,Descripcion%>" /></span>
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDescripcion" Style="width: 80%; height: 5em; margin: 0px auto;" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="90%" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <h4 style="color: #0099ff; font-weight: normal; text-align: left;"><span>Exception Details</span>
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDetalleExcepcion" Style="width: 80%; height: 10em; margin: 0px auto;" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />

                        <table width="90%" style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <h4 style="color: #0099ff; font-weight: normal; text-align: left;"><span>Strack Trace</span>
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtStackTrace" Style="width: 80%; height: 12em; margin: 0px auto;" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
    <div class="clear space"></div>
    <footer>
    ©2016 Expinn Technology
    </footer>
</body>
</html>
