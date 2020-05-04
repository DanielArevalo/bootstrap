<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apertura de Deposito</title>
    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/JaLoAdmin.min.css" rel="stylesheet" type="text/css" />
    <%--<%=string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/Css/bootstrap.css"))%>--%>
    <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
    <script src="<%=ResolveUrl("~/Scripts/PCLBryan.js")%>"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="form-group">
            <div class="col-md-12">
                <div class="col-lg-2 col-md-1">
                </div>
                <div class="col-lg-8 col-md-10 col-xs-12" style="padding-top: 8px;">
                    <div class="col-xs-12">
                        <div class="col-xs-12">
                            <asp:Image ID="imgEmpresa" runat="server" ImageUrl="~/Imagenes/LogoEmpresa.jpg" Width="85px" />
                            &nbsp;<asp:Label ID="Label1" runat="server" CssClass="text-primary" Style="font-size: 30px"
                                Text="Apertura de Deposito"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 col-md-1">
                </div>
            </div>
        </div>
        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />

        <div class="col-md-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10 col-md-12 col-sm-12">
                <div class="col-xs-12">
                    <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 13px" />
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
        <asp:Panel ID="panelData" runat="server">
            <div class="col-md-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-sm-12">
                    <div class="col-md-12">
                        <div class="col-xs-12">
                            <div class="col-xs-7 text-left">
                                <table class="tableNormal">
                                    <tr>
                                        <td style="text-align: left; width: 150px">FECHA:
                                        </td>
                                        <td style="text-align: center">Día
                                        </td>
                                        <td style="text-align: left; width: 60px">
                                            <asp:TextBox ID="txtDiaEncabezado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center">Mes
                                        </td>
                                        <td style="text-align: left; width: 150px">
                                            <asp:DropDownList ID="ddlMesEncabezado" runat="server" CssClass="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: center">Año
                                        </td>
                                        <td style="text-align: left; width: 70px">
                                            <asp:TextBox ID="txtAnioEncabezado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblcod_persona" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td style="width:100px">
                                        </td>
                                        <td style="width:150px">Tipo de Ahorro: </td>
                                        <td style="width:200px"> 
                                            <asp:DropDownList ID="TipAhorro" runat="server" CssClass="form-control" OnSelectedIndexChanged="TipAhorro_SelectedIndexChanged"
                                              AutoPostBack="true">
                                                <asp:ListItem Text="Ahorro a la Vista" Value="1" Selected="true">  </asp:ListItem>
                                                <asp:ListItem Text="Ahorro Programado" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="CDATS" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-xs-5 text-right">
                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" Width="120px" ToolTip="Save"
                                    Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnGuardar_Click">
                                    <div class="pull-left" style="padding-left:10px">
                                    <span class="glyphicon glyphicon-floppy-disk"></span></div>Grabar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger" Width="120px" ToolTip="Cancel"
                                    Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnCancelar_Click">
                                    <div class="pull-left" style="padding-left:10px">
                                    <span class="glyphicon glyphicon-arrow-left"></span></div>Cancelar
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
            <div class="col-md-12" >
                <asp:Panel ID="PDatosFinan" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                    Width="100%">
                    <asp:Label ID="LbldatosPersonales" runat="server" Text="Datos de Ahorros a la Vista" Style="text-align: center;"></asp:Label>
                </asp:Panel>
            </div>
            <br />
            <br />
            <br />
          <asp:Panel ID="pnAhorroVista" runat="server" Visible="true">
               <br />
            <br />
   
            <div class="col-md-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-sm-12">
                    <table class="tableNormal" width="100%">
                        <tr >
                       
                     
                            <td style="text-align: left; width: 2%" > Linea de <asp:Label ID="lbltipAhorro" runat="server"></asp:Label>
                            
                                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
              
                                 <td style="text-align: left; width: 2%">Valor del Producto:
                          
                                <uc1:decimales ID="txtVrCredito" runat="server" Width_="100%" AutoPostBack_="false" />
                            </td>
                        </tr>
                        <tr>

                        </tr>
                        <tr >
                    
                           <td style="text-align: left; width: 20%">Periodicidad :
                            
                                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                  
                            <td style="text-align: left; width: 20%">Forma de Pago:
                            
                                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="true" Text="Caja" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Nomina" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 
                                Modalidad:
                    <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="true" Text="Individual" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Conjunta" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Alterna" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                              <td>
                                  Oficina:
                                  <asp:DropDownList ID="ddlOficina1" runat="server" CssClass="form-control"></asp:DropDownList>
                              </td>
                        </tr>
                        <tr>
                            <td>
                                Asesores: 
                                <asp:DropDownList ID="ddlAsesores1" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                   </div>
                      
                </div>
                <div class="col-lg-1">
                </div>
          
        </asp:Panel>
            <asp:Panel runat="server" ID="pnProgramados" Visible="false">
                   <br />
            <br />
                <div class="col-md-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-sm-12">
                    <table class="tableNormal" width="100%">
                        <tr>
                     
                            <td style="text-align: left; width: 2%">Linea de <asp:Label ID="lbltipAhorro1" runat="server"></asp:Label>
                           
                                <asp:DropDownList ID="ddlLinea1" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                              <td style="text-align: left; width: 2%">Valor del Producto:
                          
                                <uc1:decimales ID="Decimales1" runat="server" Width_="100%" AutoPostBack_="false" />
                            </td>
                        </tr>
                         <tr>

                        </tr>
                        <tr >
                    
                           <td style="text-align: left; width: 20%">Periodicidad :
                            
                                <asp:DropDownList ID="ddlPeriodicidad2" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                  
                            <td style="text-align: left; width: 20%">Forma de Pago:
                            
                                <asp:DropDownList ID="ddlFormaPago2" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="true" Text="Caja" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Nomina" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                              <td>
                                  Oficina:
                                  <asp:DropDownList ID="ddlOficina2" runat="server" CssClass="form-control"></asp:DropDownList>
                              </td>
                            <td>
                                Asesores: 
                                <asp:DropDownList ID="ddlAsesores2" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>

                                Plazo:
                                <asp:TextBox ID="txtPlazo1" runat="server" CssClass="form-control"></asp:TextBox>
                           
                            </td>
                        </tr>
                    </table>
                   </div>
                      
                </div>
                <div class="col-lg-1">
                </div>
          
            </asp:Panel>
          <asp:Panel ID="pnCDATS" runat="server" Visible="false">
                 <br />
            <br />
              <div class="col-md-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-sm-12">
                    <table class="tableNormal" width="100%">
                        <tr>
                         
                            <td style="text-align: left; width: 2%">Linea de <asp:Label ID="lbltipAhorro2" runat="server"></asp:Label>
                           
                                <asp:DropDownList ID="ddlLinea2" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                          <td style="text-align: left; width: 2%">Valor del Producto:
                          
                                <uc1:decimales ID="Decimales2" runat="server" Width_="100%" AutoPostBack_="false" />
                            </td>
                        </tr>
                         <tr>

                        </tr>
                        <tr >
                    
                           <td style="text-align: left; width: 20%">Periodicidad :
                            
                                <asp:DropDownList ID="ddlPeriodicidad3" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                  
                            <td style="text-align: left; width: 20%">Forma de Pago:
                            
                                <asp:DropDownList ID="ddlFormaPago3" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="true" Text="Caja" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Nomina" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <tr>
                            <td>
                             
                                Modalidad:
                    <asp:DropDownList ID="ddlModalidad2" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="true" Text="Individual" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Conjunta" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Alterna" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                              <td>
                                  Oficina:
                                  <asp:DropDownList ID="ddlOficina3" runat="server" CssClass="form-control"></asp:DropDownList>
                              </td>
                        </tr>
                        <tr>
                            <td>
                                Asesores: 
                                <asp:DropDownList ID="ddlAsesores3" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                            <td>
                                Plazo:
                                <asp:TextBox ID="txtPlazo2" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                   </div>
                      
                </div>
                <div class="col-lg-1">
                </div>
          
          </asp:Panel>
                    <asp:Panel ID="panelFinal" runat="server" Visible="false">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="Su solicitud de Afiliación se generó correctamente."
                            Style="color: #66757f; font-size: 28px;" />
                    </div>
                    <div class="col-xs-12">
                        <p style="margin-top: 36px">
                            Su solicitud de producto se registro correctamente con el código :
                            <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red" />. Tenga en
                            cuenta el código para cualquier inconveniente o acérquese a nuestras oficinas para
                            mayor información.
                        </p>
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:LinkButton ID="btnInicio" runat="server" CssClass="btn btn-primary" Width="170px" ToolTip="Home" Visible="false"
                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnInicio_Click">
                            <div class="pull-left" style="padding-left:10px">
                            <span class="fa fa-home"></span></div>&#160;&#160;Regresar al Inicio
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
        </asp:Panel>
        </asp:Panel>

        <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />
    </form>
</body>
</html>
