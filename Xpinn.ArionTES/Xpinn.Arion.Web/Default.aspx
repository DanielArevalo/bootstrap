<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
<title>Expinn Technology</title>
<link href="Styles/LoginNuevo.css" rel="stylesheet" type="text/css" />
<link href="Styles/animate.min.css" rel="stylesheet" type="text/css" />
<link href="Styles/font-awesome.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
<style type="text/css">
			
		* {
			margin:0px;
			padding:0px;
		}
			
		#header {
			margin:auto;
			width:500px;
			font-family:Arial, Helvetica, sans-serif;
		}
			
		ul, ol {
			list-style:none;
		}
			
		.nav {
			width:500px; /*Le establecemos un ancho*/
			margin-left:-30%; /*Centramos automaticamente*/
		}
 
		.nav > li {
			float:left;
            text-align:center;
            width:150px;
		}
			
		.nav li .a {
			color:#fff;
			text-decoration:none;
			padding:0px 12px;
			display:block;
		}
			
		.nav li ul .a:hover {
            background-color:#d8d8d8;
            border-radius:5px;
		}
			
		.nav li ul {
            text-align:center;
			display:none;
			position:absolute;
            border-radius:5px;
			min-width:150px;
		}
			
		.trinagulo{
			display:none;
		}
		.nav li:hover > .trinagulo{
			display:block;
		}
		.nav li:hover > ul {
			display:block;
            background-color:#fff;
            text-align:center;
            color:#000;
		}
			
		.nav li ul li {
			position:relative;
            color:#000;
		}
			
		.nav li ul li ul {
			right:-140px;
			top:0px;
            color:#000;
		}
			

        #pruebas {
            color:#fff;
            font-style:italic;
            text-align:center;
            font-size:12px;
            background: rgba(242,31,31,1);
            background: -moz-linear-gradient(top, rgba(242,31,31,1) 0%, rgba(143,13,13,1) 100%);
            background: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(242,31,31,1)), color-stop(100%, rgba(143,13,13,1)));
            background: -webkit-linear-gradient(top, rgba(242,31,31,1) 0%, rgba(143,13,13,1) 100%);
            background: -o-linear-gradient(top, rgba(242,31,31,1) 0%, rgba(143,13,13,1) 100%);
            background: -ms-linear-gradient(top, rgba(242,31,31,1) 0%, rgba(143,13,13,1) 100%);
            background: linear-gradient(to bottom, rgba(242,31,31,1) 0%, rgba(143,13,13,1) 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f21f1f', endColorstr='#8f0d0d', GradientType=0 );
            width:100%;
            height:20px;
            margin-bottom:2px;
        }

        #produccion {
            color:#fff;
            font-style:italic;
            text-align:center;
            font-size:12px;
            background: rgba(16,88,150,1);
            background: -moz-linear-gradient(top, rgba(16,88,150,1) 0%, rgba(25,62,99,1) 100%);
            background: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(16,88,150,1)), color-stop(100%, rgba(25,62,99,1)));
            background: -webkit-linear-gradient(top, rgba(16,88,150,1) 0%, rgba(25,62,99,1) 100%);
            background: -o-linear-gradient(top, rgba(16,88,150,1) 0%, rgba(25,62,99,1) 100%);
            background: -ms-linear-gradient(top, rgba(16,88,150,1) 0%, rgba(25,62,99,1) 100%);
            background: linear-gradient(to bottom, rgba(16,88,150,1) 0%, rgba(25,62,99,1) 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#105896', endColorstr='#193e63', GradientType=0 );
            width:100%;
            height:20px;
            margin-bottom:2px;
        }
</style>
<script type="text/javascript">
    $(document).keydown(function (event) {
        if (event.keyCode == 123) { // Prevent F12
            return false;
        } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
            return false;
        }
    });
</script>

<script type="text/javascript" language="javascript">  
    function inhabilitar(){ 
   	    return false 
    } 

    document.oncontextmenu=inhabilitar 
</script>

</head>
<body>
    <div id="pruebas"  runat="server"> Ambiente de pruebas
    </div>
    <div id="produccion"  runat="server">Ambiente de Produccion
    </div>
    <form id="wrapper" runat="server">
        <div style="margin-top:2%; width:25%; margin-left:90%; text-align:right; color:#fff;">
            <div id="header">
			        <nav> <!-- Aqui estamos iniciando la nueva etiqueta nav -->
				        <ul class="nav">
					<li style="color:#fff; background-color:none; text-align:center;"><asp:LinkButton runat="server" href="" style="color:#fff; background-color:none;"><asp:label runat="server" style="display:block; float:left; ">Idioma &nbsp<div style="transform:rotate(180deg); margin-top:4px; display:block; float:right; height: 0px; width: 0px; border-left:6px solid transparent; border-right:6px solid transparent; border-top:6px solid transparent; border-bottom:6px solid #fff;"></div><asp:Image runat="server" style="margin:0px 5px; margin-left:35px; margin-right:10px; height:15px; display:block; float:left;" src="Images/Español.png"/></asp:label></asp:LinkButton><br />
                            <div class="trinagulo" style="height: 0px; width: 0px; border-left:10px solid transparent; border-right:10px solid transparent; border-top:0px solid transparent; border-bottom:8px solid #fff; margin:0px auto;"></div>
						   <ul class="links" style="text-align:left;">
						
							        <li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton id="ES" CommandName ="es-co" runat="server" OnClick="Buscar" class="a" style="color:#000;"><asp:Image runat="server" style="margin-left:10px; margin-right:15px; margin-top:5px;"  src="Images/Español.png"/><asp:Label  runat="server"  Value ="es-co" Text="<%$  Resources:Resource,Español %>"></asp:Label></asp:LinkButton></li>
							        <li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton id="EN"  CommandName ="en-US" runat="server" OnClick="Buscar" class="a" style="color:#000;"><asp:Image runat="server" style="margin-left:10px; margin-right:15px; margin-top:5px;"  src="Images/Ingles.png"/><asp:Label runat="server" Value ="en-US" Text="<%$  Resources:Resource,Ingles %>"></asp:Label></asp:LinkButton></li>
							        <li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton runat="server" id="FR"   CommandName ="fr-FR"  OnClick="Buscar" class="a" style="color:#000;"><asp:Image runat="server" style="margin-left:10px; margin-right:15px; margin-top:5px;"  src="Images/Frances.png"/><asp:Label runat="server" Value ="fr-FR" Text="<%$  Resources:Resource,Frances %>"></asp:Label></asp:LinkButton></li>
						   </ul>        
                                 
					</li>
				        </ul>
			        </nav><!-- Aqui estamos cerrando la nueva etiqueta nav -->
            </div>
        </div>
    <div id="box" class="animated bounceIn">
    <div id="top_header" style="text-align:center; color:#808080;">
        <a href="#">
			<img class="logo" src="Images/logo.png" alt="logo" style="width:90px;" />
            </a>
					<asp:Literal runat="Server" ID ="Acceder" Text="<%$  Resources:Resource,Acceder%>" /> <br/>
     <asp:Literal runat="Server" ID ="Financial" Text="<%$  Resources:Resource,Financial%>" />.
        <div class="logoLogin fl-lt"></div>
    </div>
    <div id="inputs">
        <div class="form-control" style="text-align:center; margin-top:5px;">
            <label ID="lblUser" runat="server" text="<%$  Resources:Resource,Usuario %>" Width="60px"></label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$  Resources:Resource,Campo_Requerido %>" 
                    Display="Dynamic" Width="140px" ControlToValidate="txtUsuario" 
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                <br />
            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="50" placeholder="<%$  Resources:Resource,Usuario %>"></asp:TextBox>
            <i class="fa fa-user" style="color:#0099cc; margin-top:15px; font-size:30px;"></i>
        </div>
        <div class="form-control" style="text-align:center; margin-top:2px;">
            <label ID="lblPass" runat="server" text="<%$  Resources:Resource,Contraseña %>" Width="90px"></label>
                <asp:RequiredFieldValidator ID="rfvClave" runat="server" ErrorMessage="<%$  Resources:Resource,Campo_Requerido %>" 
                    Display="Dynamic" Width="140px" ControlToValidate="txtPassword" 
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" placeholder="<%$  Resources:Resource,Contraseña %>" MaxLength="50" TextMode="Password" oncopy="return false;" onpaste="return false;" oncut="return false;"></asp:TextBox>
            <i class="fa fa-key" style="color:#0099cc; margin-top:15px; font-size:30px;"></i>
            <div class="fl-rt">
                <asp:Button ID="btnIngresar" runat="server" Text="<%$  Resources:Resource,Ingresar %>" 
                    onclick="btnIngresar_Click" ValidationGroup="vgLogin" />
            </div>
            <div class="clear">
                <asp:Label ID="lblInfo" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div class="clear" style="margin-top:15px;">
                <asp:HyperLink ID="OlvideContraseña" runat="server" href="#" text="<%$  Resources:Resource,Olvide_contraseña %>"></asp:HyperLink>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
<script>
    window.name = "*ukn*";
</script>

