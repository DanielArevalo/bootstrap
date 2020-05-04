<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>
<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 300;
            }
            else {
                return 1000;
            }
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">     
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 80%; margin-right: 0px;">
                    <tr>
                        <td style="height: 15px; text-align: left; width: 150px;">
                            Fecha Inicial<br />
                            <ucFecha:fecha ID="fechainicial" runat="server" AutoPostBack="True" CssClass="textbox"
                                MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                        <td style="height: 15px; text-align: left; width: 150px;">
                            Fecha final<br />
                            <ucFecha:fecha ID="fechafinal" runat="server" AutoPostBack="True" CssClass="textbox"
                                MaxLength="1" ValidationGroup="vgGuardar" Width="148px" />
                        </td>
                        <td style="text-align: left; height: 15px; width: 99px;">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" Width="200px " CssClass="textbox"
                                AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 15px;" colspan="2">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server" Width="77%" AutoGenerateColumns="False"
                            AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowDeleting="gvLista_RowDeleting" OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            OnRowDataBound="gvLista_RowDataBound" Style="font-size: xx-small" 
                            ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Operación" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" Width="50px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_operacion" HeaderText="Numero Operación">
                                    <ItemStyle HorizontalAlign="Center" Width="50px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_transaccion" HeaderText="Numero Transacción">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_cuenta" HeaderText="Numero Cuenta">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_transaccion" HeaderText="Tipo Transacción">
                                    <ItemStyle HorizontalAlign="Center"  Width="100px"/>
                                </asp:BoundField>
                                   <asp:BoundField DataField="valor_base" HeaderText="Valor Base Gmf"  DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Center"  Width="100px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_gmf" HeaderText="Valor GMF" DataFormatString="{0:c}">                             
                                </asp:BoundField>                              
                                <asp:BoundField DataField="nombre_producto" HeaderText="Producto">                             
                                </asp:BoundField>
                                  <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">                             
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="identificacion">                                                                 
                                </asp:BoundField>
                                   <asp:BoundField DataField="nombres" HeaderText="Nombres">                                                                 
                                </asp:BoundField>
                                   <asp:BoundField DataField="apellidos" HeaderText="Apellidos">                                                                 
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
               
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />            
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVrTotal" runat="server" Visible="False" Text="Valor Total" />&nbsp;&nbsp;
                        <uc1:decimales ID="txtTotal" runat="server" Enabled="false" />
                    </td>
                     <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <uc3:ctlgiro ID="ctlGiro" runat="server" />
                    </td>
                </tr>   
            </table>                
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <uc2:procesoContable ID="ctlproceso" runat="server" />
        </asp:View>
    </asp:MultiView>
      
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>  
      


