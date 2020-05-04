<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

      <script type="text/javascript">   
        $(document).ready(function ()
        {
            $("#btn_actualizar").click(function () {
               
                $("#gvProductos > tbody > tr ").each(function (i, val)
                {
                    if(i>0){
                        var col = $(this).children().length;
                    if (col>1)
                    {
                                              
                        this.children[4].children[0].selectedIndex = 0;
                        
                            var oficina = $("#ddlOficina")[0].selectedIndex;
                            this.children[4].children[0].selectedIndex = oficina;
 
                       
                    }

                }
                })
            });
        });

    </script>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View runat="server" ID="vwBusqueda">
            <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                    </td>
                                </tr>
                            </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwData" runat="server" >
            <asp:Panel ID="pConsulta" runat="server">
                <br />
                <br />
                <asp:Panel ID="pDatos_busqueda" runat="server" Height="100%">
                    <table cellpadding="0" cellspacing="0" style="width: 606px">
                        
                        <tr>
                            <td style="text-align: left;">
                                Código<br />
                                <asp:TextBox ID="txtCodigo" Enabled="false" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                Identificación<br />
                                <asp:TextBox ID="txtNumeIdentificacion" Enabled="false" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                Nombres<br />
                                <asp:TextBox ID="txtNombres" Enabled="false" runat="server" CssClass="textbox"  Width="229px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: left;">
                                <br />
                                DATOS DE LA OFICINA
                                <br />
                            </td>
                        </tr>
                        <tr>
                            
                            
                             <td style="text-align: left;" colspan="2">
                                 <asp:Panel ID="pOficina" runat="server">
                                 <asp:Label ID="lblOficina" runat="server" Text="Oficina" /><br />
                                                    <asp:DropDownList ID="ddlOficina" ClientIDMode="Static" runat="server" Width="180px" CssClass="textbox" >
                                                    </asp:DropDownList>
                                     </asp:Panel>
                            </td>
                            <td style="text-align:left;">
                                <input type="button" value="Actualizar" ID="btn_actualizar" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:left;">
                                 <br />
                    PRODUCTOS
                    <br />
                            </td>
                        </tr>
                    </table>
                   
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="pBusqueda"
                    ExpandControlID="pEncBusqueda" CollapseControlID="pEncBusqueda" Collapsed="False"
                    TextLabelID="lblMostrarDetalles" ImageControlID="imgExpand" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                    CollapsedText="(Click Aqui para Mostrar Detalles...)" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" SuppressPostBack="true" SkinID="CollapsiblePanelDemo"
                    ExpandedSize="80" />
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvProductos" ClientIDMode="Static" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                           OnRowDataBound="gvProductos_RowDataBound"  Style="font-size: x-small">
                            <Columns>
                                <asp:BoundField DataField="nom_tipo_producto" HeaderText="Tipo Producto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="num_producto" HeaderText="Num. Producto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Línea"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                 <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Oficina">
                                    <ItemTemplate>
                                       <asp:DropDownList ID="ddlOficinaPro" ClientIDMode="Static" runat="server" Width="180px" CssClass="textbox">
                                       </asp:DropDownList>
                                        <asp:Label ID="lblLinea" Visible="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <br />
        </asp:View>

        </asp:MultiView>
    <asp:Panel ID="pProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
      <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>

