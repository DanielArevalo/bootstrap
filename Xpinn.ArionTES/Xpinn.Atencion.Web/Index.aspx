<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
      #carousel {
  width: 100%;
  height: 480px;
  margin: 0 auto;
  overflow: hidden;
}

.slides {
  position: relative;
  width: 100%;
}
.slide {
  position: absolute;
  top: 0;
  left: 0;
  display: none;
}
.slide.visible {
  display: block;
}
.slide > img{
    width:900px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- SECCION DE PRUEBAS INICIO --%>
    <%--<div style="text-align: center;">
        <h5 style="margin-bottom: 5px;">
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="text-Link" NavigateUrl="~/Pages/Convenio/Servicios.aspx"> Servicios </asp:HyperLink></h5>
    </div>--%>
    <%-- SECCIÓN DE PRUEBA FIN --%>
    <%-- <div class="">
        <div id="carousel">
            <div class="slides">
                <div class="slide visible">
                  <img src="http://fecem.com/images/001banner.jpg" />
                </div>
                <div class="slide">
                  <img src="http://fecem.com/images/002banner.jpg" />
                </div>
                <div class="slide">
                  <img src="http://fecem.com/images/003banner.jpg" />
                </div>
                <div class="slide">
                  <img src="http://fecem.com/images/004banner.jpg" />
                </div>
                <div class="slide">
                  <img src="http://fecem.com/images/005banner.jpg" />
                </div>
            </div> <!-- /slides -->
        </div> <!-- /carousel --> --%>       
    <div class="">
      <div id="carousel">
        <div class="slides">
            <div class="slide visible">
                <img src="Imagenes/inicio1.jpg" />
            </div>
            <div class="slide">
                <img src="Imagenes/inicio2.jpg" />
            </div>
    <%--<div class="slide">
      <img src="Imagenes/inicio3.jpg" />
    </div>--%>
        </div> <!-- /slides -->
    </div> <!-- /carousel -->        

  <%-- OPCIONES DE PÁGINA DE INICIO --%>  
        <%--
  <div class="card1">
  	<asp:HyperLink ID="hplEstadoCuenta" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Estado de cuenta</h3></div>
  		    <div class="div1 card1-icon"><i class="fa fa-credit-card icon" aria-hidden="true"></i></div>
	    </div>
    </asp:HyperLink>
  </div>
    --%>
    <%-- 
  <div class="card2">
      <asp:HyperLink ID="hplActualizarData" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Actualizar datos</h3></div>
  		    <div class="div1 card2-icon"><i class="fa fa-address-book-o icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>      
  </div>
    --%> 
    <%--
  <div class="card3">
      <a href="">
        <div class="option">
  		    <div class="div1"><h3>Notificaciones</h3></div>
  		    <div class="div1 card3-icon"><i class="fa fa-bell icon" aria-hidden="true"></i></div>
	    </div>
      </a>
  </div>
    --%>

    <%--
  <div class="card4">
      <asp:HyperLink ID="hplModificarProduc" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Modificar productos</h3></div>
  		    <div class="div1 card4-icon"><i class="fa fa-money icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>
    --%>

    <%--
  <div class="card5">
      <asp:HyperLink ID="hplPlanPagos" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Plan pagos</h3></div>
  		    <div class="div1 card5-icon"><i class="fa fa-university icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>
    --%>
    <%--
  <div class="card6">
      <asp:HyperLink ID="hplSimulacion" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Simulación</h3></div>
  		    <div class="div1 card6-icon"><i class="fa fa-clipboard icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>
    --%>

    <%--
      <div class="card7">
      <asp:HyperLink ID="hplCertificacion" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Certificación</h3></div>
  		    <div class="div1 card7-icon"><i class="fa fa-address-card-o icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>
   --%>

    <%--
  <div class="card8">
      <asp:HyperLink ID="hplCambiarClave" runat="server">
        <div class="option">
  		    <div class="div1"><h3>Cambiar clave</h3></div>
  		    <div class="div1 card8-icon"><i class="fa fa-unlock-alt icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>
    --%>

  <%--<div class="card9">
      <asp:HyperLink ID="b" runat="server">
        <div class="card-main">
  		    <div class="cardleft"><h2>opción</h2></div>
  		    <div class="cardrigth card9-icon"><i class="fa fa-address-book-o icon" aria-hidden="true"></i></div>
	    </div>
      </asp:HyperLink>
  </div>--%>
    <%--
  <div class="cardEmpty">
      <div class="card-main">  		
	  </div>
  </div>         
    --%>
</div>
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(function () {
                var $visible = $('.slide.visible');
                var $next = $visible.next('.slide');

                if (!$next.length) {
                    $next = $('.slides .slide:first-child');
                }

                $visible.hide(0).removeClass('visible');
                $next.show(0).addClass('visible');
            }, 5000);
        });
    </script>
</asp:Content>
