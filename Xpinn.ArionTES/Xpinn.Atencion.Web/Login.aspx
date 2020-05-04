<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="~/Css/bootstrap.css" />    
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
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
<body>        
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
        <div class="xpinn_formulario">
          <div class="xpinn_identificacion">
               <asp:TextBox ID="login__username" CssClass="xpinn_txtidentificacion" runat="server" placeholder="Identificación"/>
          </div>
          <div class="xpinn_password">
               <asp:TextBox ID="login__password" CssClass="xpinn_txtpassword" runat="server" placeholder="Contraseña" TextMode="password"/>
          </div>
          <div class="xpinn_login">
              <asp:Button ID="btnIniciar" runat="server" CssClass="xpinn_btnlogin" Text="Iniciar Sesión" OnClick="btnIniciar_Click" />
          </div>
          <div class="xpinn_error">
            <asp:Label ID="lblMsj" runat="server" CssClass="xpinn_txterror"/>
          </div>
        </div> 
    </form>
</body>
</html>
