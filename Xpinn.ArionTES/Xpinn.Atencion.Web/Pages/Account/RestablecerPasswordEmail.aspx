<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestablecerPasswordEmail.aspx.cs" Inherits="RestablecerPasswordEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.: Restablecimiento de contraseña :.</title>
    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
     <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
</head>
<body>
    
        <form id="form1" class="form-horizontal" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="form-group">
            <div class="container">
                <div class="col-lg-12">
                    <div class="col-lg-2 col-md-1">
                    </div>
                    <div class="col-lg-8 col-md-10 col-xs-12" style="padding-top: 8px;">
                        <div class="col-xs-12">
                            <div class="col-xs-12">
                                <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Imagenes/LogoEmpresa.jpg" Width="85px" />
                                &nbsp;<asp:Label ID="lbltitulo" runat="server" Text="Restablecimiento de contraseña"
                                    CssClass="text-primary" style="font-size:16px" />
                            </div>                            
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-1">
                    </div>
                </div>
            </div>
            <hr style="width:100%;margin-top:4px;box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);"/>
            <div class="container">
                <div class="col-lg-12">
                    <div class="col-lg-2 col-md-1">
                    </div>
                    <div class="col-lg-8 col-md-10 col-xs-12" style="padding-top: 8px;">
                        <div class="col-xs-12">
                            <asp:Label ID="lblError" runat="server" Style="text-align: left; font-size: small"
                                Visible="False" Width="100%" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-1">
                    </div>
                </div>
            </div>            
            <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwInfo1" runat="server">
                    <div class="container">
                        <div class="col-lg-12">
                            <div class="col-lg-2 col-md-1">
                            </div>
                            <div class="col-lg-8 col-md-10 col-xs-12">
                                <div class="col-xs-12" style="margin-top: 27px">
                                    <asp:Label ID="Label1" runat="server" Text="Encuentra tu cuenta registrada con nosotros"
                                        Style="color: #66757f; font-size: 28px;" />
                                </div>
                                <div class="col-xs-12">
                                    <p style="margin-top: 36px">
                                        Ingresa tu correo electrónico, de no encontrarla comuníquese con nosotros</p>
                                </div>
                                <div class="col-xs-12">
                                    <asp:TextBox ID="txtEmailRecuperacion" runat="server" CssClass="form-control" Width="80%" />
                                    <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmailRecuperacion"
                                        Display="Dynamic" ErrorMessage="Necesitamos esta información para encontrar tu cuenta." ForeColor="Red" SetFocusOnError="True"
                                        Style="font-size: x-small" ValidationGroup="vgGuardar" />
                                    <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmailRecuperacion"
                                        ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="vgGuardar" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-xs-12">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" CssClass="btn btn-primary"
                                        Style="padding-bottom: 8px; padding-top: 8px; margin-top: 20px; border-radius: 3px"
                                        OnClick="btnBuscar_Click" ValidationGroup="vgGuardar"/>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-1">
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwInfo2" runat="server">
                    <div class="container">
                        <div class="col-lg-12">
                            <div class="col-lg-2 col-md-1">
                            </div>
                            <div class="col-lg-8 col-md-10 col-xs-12">
                                <div class="col-xs-12" style="margin-top: 27px">
                                    <asp:Label ID="lblSub" runat="server" Text="¿Cómo deseas restablecer tu contraseña?"
                                        Style="color: #66757f; font-size: 28px;" />
                                </div>
                                <div class="col-xs-12">
                                    <p style="margin-top:36px">
                                        Hemos encontrado la siguiente información asociada a tu cuenta.</p>
                                </div>
                                <div class="col-xs-12">
                                    <asp:RadioButton ID="rbUbicacion" runat="server" />
                                    <asp:Label ID="lblNombre" runat="server" Visible="false"/>
                                    <asp:Label ID="lblCod_Persona" runat="server" Visible="false"/>
                                </div>
                                <div class="col-xs-12">
                                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" Width="150px" CssClass="btn btn-primary"
                                        
                                        Style="padding-bottom: 8px; padding-top: 8px; margin-top: 20px; border-radius: 3px" 
                                        onclick="btnContinuar_Click" />
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-1">
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwMensajeEnvio" runat="server">
                    <div class="container">
                        <div class="col-lg-12">
                            <div class="col-lg-2 col-md-1">
                            </div>
                            <div class="col-lg-8 col-md-10 col-xs-12">
                                <div class="col-xs-12" style="margin-top: 27px">
                                    <asp:Label ID="Label2" runat="server" Text="Revisa tu correo electrónico"
                                        Style="color: #66757f; font-size: 28px;" />
                                </div>
                                <div class="col-xs-12">
                                    <p style="margin-top: 36px">
                                        Hemos enviado un correo electrónico a
                                        <asp:Label ID="lblEmailEncontrado" runat="server" />. Haz clic en el enlace dentro
                                        del correo electrónico para restablecer tu contraseña.</p>
                                    <p style="margin-top: 10px">
                                        Si no ves el correo electrónico en tu bandeja de entrada, revisa otros lugares donde
                                        podría estar, como tus carpetas de correo no deseado, sociales u otras.
                                    </p>
                                </div>                                
                            </div>
                            <div class="col-lg-2 col-md-1">
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            

            <asp:Panel ID="panelEnvio" runat="server">
                <div style="padding:15px;padding: 30px 80px;margin-bottom: 30px;background-color: #e6e6e6">
                    <div class="container">
                        <asp:Panel ID="panel1" runat="server" Style="background-color: White;padding:10px">
                            <div class="form-group">
                                <div class="container" style="padding:15px;">
                                    <div class="col-lg-12">
                                        <div class="col-lg-2 col-md-2">
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-xs-12" style="padding-top: 8px;">
                                            <div class="col-xs-12">
                                                Hola,&nbsp;<asp:Label ID="lblNomApe" runat="server"></asp:Label>:
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                        </div>
                                    </div>
                                    <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
                                    <div class="col-lg-12">
                                        <div class="col-lg-2 col-md-2">
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-xs-12" style="padding-top: 8px;">
                                            <div class="col-xs-12" style="margin-top: 27px">
                                                <label style="color: #66757f; font-size: 28px;">
                                                    Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</label>
                                            </div>
                                            <div class="col-xs-12">
                                                <p style="margin-top: 36px;font-size: 15px;">
                                                    Si solicitaste restablecer tu contraseña, haz clic en el enlace de abajo. Si no
                                                    hiciste esta solicitud, por favor, ignora este correo electrónico.</p>
                                            </div>
                                            <div class="col-xs-12">
                                                <asp:HyperLink ID="hlEnvio" runat="server" style="text-decoration: none; color:#2780e3; font-weight: 600; font-size:17px" target="_blank">
                                                    Restablecer Contraseña
                                                </asp:HyperLink>
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>


        </div>
        </form>    
</body>
</html>
