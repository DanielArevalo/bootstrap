<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="~/Css/bootstrap.css" />
    <link rel="stylesheet" href="~/Css/style.css" />
    <link rel="stylesheet" href="~/Css/flexslider.css" />
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script type="" src="Scripts/jquery.flexslider.js"></script>
	<script type="text/javascript" charset="utf-8">
      $(window).load(function() {
        $('.flexslider').flexslider({
    	    touch: true,
    	    pauseOnAction: false,
    	    pauseOnHover: false,
        });
      });
    </script>
    <style type="text/css">
        .fondoLog {
            background-color:transparent;
            background-position:center;
            background-size:cover;
            background-repeat:no-repeat;
            height:100%;
            margin:0px;
        }
    </style>
    <style type="text/css">
        /* Form */
.form {
  position: relative;
  z-index: 1;
  background: rgba(193,193,193,.8);
  max-width: 400px;
  margin: 30px auto 100px;
  padding: 20px 50px;
  border-top-left-radius: 3px;
  border-top-right-radius: 3px;
  border-bottom-left-radius: 3px;
  border-bottom-right-radius: 3px;
  text-align: center;
}
.image {
  width: 60%; height: auto;
  margin: 10% auto;
}
.form .thumbnail img {
  display: block;
  width: 100%;
}
.form input {
  outline: 0;
  width: 100%;
  border: 0;
  margin: 0 0 15px;
  padding: 15px;
  border-top-left-radius: 3px;
  border-top-right-radius: 3px;
  border-bottom-left-radius: 3px;
  border-bottom-right-radius: 3px;
  box-sizing: border-box;
  font-size: 14px;
}
.button {
  outline: 0;
  background: #456;
  width: 100%;
  border: 0;
  padding: 15px;
  border-top-left-radius: 3px;
  border-top-right-radius: 3px;
  border-bottom-left-radius: 3px;
  border-bottom-right-radius: 3px;
  color: #FFFFFF;
  font-size: 14px;
  transition: all 0.3 ease;
  cursor: pointer;
}
.form .message {
  margin: 15px 0 0;
  color: #b3b3b3;
  font-size: 12px;
}
.form .message a {
  color: #456;
  text-decoration: none;
}
.form .register-form {
  display: none;
}

.container {
  position: relative;
  z-index: 1;
  max-width: 300px;
  margin: 0 auto;
}
.container:before, .container:after {
  content: "";
  display: block;
  clear: both;
}
.container .info {
  margin: 50px auto 0;
  text-align: center;
}
.container .info h1 {
  margin: 0 0 15px;
  padding: 0;
  font-size: 36px;
  font-weight: 300;
  color: #1a1a1a;
}
.container .info span {
  color: #4d4d4d;
  font-size: 12px;
}
.container .info span a {
  color: #000000;
  text-decoration: none;
}
.container .info span .fa {
  color: #456;
}

/* END Form */
/* Demo Purposes */
body {  
  background: #ccc;
  font-family: "Roboto", sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background: url('Imagenes/fondoLog.jpg') no-repeat center center fixed;
-webkit-background-size: cover;
-moz-background-size: cover;
-o-background-size: cover;
background-size: cover;
}
body:before {
  content: "";
  position: fixed;
  top: 0;
  left: 0;
  display: block;
  width: 100%;
  height: 100%;
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

<script type="text/javascript">  
    function inhabilitar() {
        return false
    }
    document.oncontextmenu = inhabilitar 
</script>

<script type="'text/javascript'">
    (function(){
    var _z = console;
    Object.defineProperty( window, "console", {
    get : function(){
        if( _z._commandLineAPI ){
        throw "Sorry, Can't exceute scripts!";
            }
        return _z; 
    },
    set : function(val){
        _z = val;
    }
    }); 
    })();
</script>

</head>
<body style="margin: 0px;">        
  <form class="login-form" runat="server">
<div class="form">
    <asp:Panel runat="server" ID="log">
        <h1 style="font-weight:bold">Oficina Virtual</h1>
  <div><asp:Image ID="Image3" runat="server" ImageUrl="Imagenes/LogoEmpresa.png" CssClass="image" /></div>
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
      <div class="form__field">
            <i class="fontawesome-user" style="font-size: 2em;margin: 0;color: gray;padding: 0 10px;"></i>
            <asp:TextBox ID="login__username" runat="server" placeholder="Identificación" required style="background-color:#494949 !important" />
      </div>
      <div class="form__field">
            <i class="fontawesome-lock" style="font-size: 2em;margin: 0;color: gray;padding: 0 10px;"></i>
                <asp:TextBox ID="login__password" runat="server" placeholder="Contraseña" TextMode="password" required style="background-color:#494949 !important"/>
            </div>    
            <asp:Button ID="btnIniciar" runat="server" CssClass="button" Text="Iniciar Sesión" OnClick="btnIniciar_Click" />
            <div style="text-align: center;">                
                <h5 style="text-align:center; margin-bottom:5px;">
                    <asp:HyperLink ID="HyperLink4" runat="server" CssClass="text-Link" NavigateUrl="~/Pages/Account/RestablecerPasswordEmail.aspx">¿Has olvidado tu clave?</asp:HyperLink>
                </h5>
            </div>
            <div style="text-align: center;">
                <h5 style="margin-bottom: 5px;">¿No eres asociado? 
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="text-Link" NavigateUrl="~/Pages/Afiliacion/R01_Identificacion.aspx"> Inicie aquí el registro</asp:HyperLink></h5>
            </div>
            <div style="text-align: center; margin-bottom: 5px;">
                <h5>¿Eres asociado y no tienes cuenta?&nbsp;&nbsp;<asp:HyperLink ID="HyperLink3" runat="server"
                    CssClass="text-Link" NavigateUrl="Pages/Account/Registro.aspx"> Solicítala Aquí</asp:HyperLink></h5>
            </div>
            <center>
                <b>
                    <asp:Label ID="lblMsj" runat="server" ForeColor="Red" Visible="false" /></b>
            </center>
            <div style="text-align: center; padding-bottom: 25px;">
                <a href="http://www.expinn.com.co/inicio/" target="_blank">
                    <asp:Image ID="Image2" runat="server" ImageUrl="Imagenes/logointerna2.png" Style="width: 25%; height: auto; margin-top: 5%;" />
                </a>
       </div>
    </asp:Panel>    
    <asp:Panel runat="server" ID="noAccess" Visible="false">
        <h1>No tiene acceso a esta página</h1>
    </asp:Panel>
</div> 
    </form>
</body>
</html>
