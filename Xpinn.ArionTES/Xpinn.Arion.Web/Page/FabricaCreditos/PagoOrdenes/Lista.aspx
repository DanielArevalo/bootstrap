<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlValidarBiometria.ascx" TagName="validarBiometria" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
   <%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>
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
                <table cellpadding="5" cellspacing="0" style="width: 95%; margin-right: 0px;">
                    <tr>
                        <td style="height: 15px; text-align: left;">Fecha Del Reporte<br />
                            <ucFecha:fecha ID="txtAprobacion_fin" runat="server" AutoPostBack="True"
                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="110px" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTitIdenProveedor" runat="server" Text="Identificación" />
                            <br />
                            <asp:TextBox ID="txtIdentificacionprov" runat="server" AutoPostBack="true"
                                CssClass="textbox" MaxLength="10"
                                OnTextChanged="txtIdentificacionprov_TextChanged" Width="80px"></asp:TextBox>
                            <cc1:ButtonGrid ID="btnListadoPersona" runat="server" CssClass="btnListado"
                                OnClick="btnListadoPersona_Click" Text="..." />
                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTitNomProveedor" runat="server" Text="Nombre " />
                            <br />
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox"
                                Enabled="false" MaxLength="200" Width="150px"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbloficina" runat="server" Text="Oficina" />
                            <br />
                            <asp:DropDownList ID="ddloficina" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;">Fecha Inicial
                            <br />
                            <ucFecha:fecha ID="txtFechaInicial" runat="server" CssClass="textbox" MaxLength="1" Width="100px" />
                        </td>
                        <td style="text-align: left;" >Fecha Final
                            <br />
                            <ucFecha:fecha ID="txtFechaFinal" runat="server" CssClass="textbox" MaxLength="1" Width="100px" />
                        </td>
                        </tr>
                    <tr>                        <td style="text-align: left;" colspan ="4">
                                                <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
                         </td>
                    </tr>
                    
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" Visible="false"
                            OnClick="btnExportar_Click" Text="Exportar a Excel" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLista" runat="server"  AutoGenerateColumns="False"
                            AllowPaging="False" GridLines="Horizontal" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="idordenservicio"
                            Style="font-size: xx-small">
                            <Columns>
                                <asp:TemplateField HeaderText="Pagar">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" Checked="True" Width="10px"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <cc1:CheckBoxGrid ID="chbcreacion" runat="server" Checked="True" Width="10px" AutoPostBack="True" OnCheckedChanged="chbcreacion_CheckedChanged" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" Width="10px"/>
                                    <ItemStyle HorizontalAlign="left" Width="20px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="idordenservicio" HeaderText="Id.Servicio">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="idproveedor" HeaderText="Id.Proveedor">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="nomproveedor" HeaderText="Nom.Proveedor">
                                    <ItemStyle HorizontalAlign="left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_preimpreso" HeaderText="No.Orden">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_persona" HeaderText="cod.persona">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_radicacion" HeaderText="Numero Radicación">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nom_linea" HeaderText="Línea">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_solicitud" HeaderText="F.Solicitud" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_desembolso" HeaderText="F.Desembolso" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto_aprobado" HeaderText="Monto" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_auxilio" HeaderText="Vr.Auxilio" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Vr.Cuota" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="left" />
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
            </table>
            <table>
                <tr>
                    <td style="width: 750px">
                        <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Monto Aprobado&nbsp;&nbsp;</strong>
                        <asp:TextBox ID="TXTmONTO" runat="server" CssClass="textbox" Enabled="false" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr style="width: 105%" />
            <table>
                <tr>
                    <td style="text-align: left;">
                        <asp:Label ID="labelgiro" runat="server" Text="Datos para el Giro "
                            Visible="false" Style="font-weight: 700" /><br />
                    </td>

                </tr>
                <tr>
                    <td style="text-align: left;">
                        <uc5:ctlgiro ID="ctlGiros" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
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
                            <asp:Label ID="lblMensaje" runat="server"
                                Text="Datos Grabados Correctamente" Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <ctl:mensajegrabar ID="ctlMensaje" runat="server" />
    <script type="text/javascript" language="javascript">
        function CheckAllEmp(Checkbox) {
            var gvLista = document.getElementById("<%=gvLista.ClientID %>");
            for (i = 1; i < gvLista.rows.length; i++) {
                gvLista.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
</asp:Content>
