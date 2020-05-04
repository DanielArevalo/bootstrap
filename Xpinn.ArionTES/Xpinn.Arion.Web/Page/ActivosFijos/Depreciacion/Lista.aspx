<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>

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
                freezesize: 3,
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

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 70%">
            <tr>
                <td style="height: 15px; text-align: left;">
                    Fecha A Depreciar
                    <br />
                    <uc:fecha ID="txtFecha" runat="server" CssClass="textbox" Width="85px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="95%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    DataKeyNames="consecutivo" 
                    style="font-size: xx-small">
                    <Columns>
                        <asp:BoundField DataField="consecutivo" HeaderText="Consec." Visible="False">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_act" HeaderText="Código" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomclase" HeaderText="Clase" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomtipo" HeaderText="Tipo" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomubica" HeaderText="Ubicación" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomcosto" HeaderText="C/C" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="anos_util" HeaderText="Vida Util" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="serial" HeaderText="Serial" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_encargado" HeaderText="Encargado" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_compra" HeaderText="F.Compra"  DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_compra" HeaderText="Vr.Compra" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_avaluo" HeaderText="Vr.Avaluo" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_salvamen" HeaderText="Vr.Salvamento" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="num_factura" HeaderText="Num.Factura" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_proveedor" HeaderText="Proveedor" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomoficina" HeaderText="Oficina" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_ult_depre" HeaderText="F.Ult.Deprec."  DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="acumulado_depreciacion" HeaderText="Deprec.Acumulada" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_por_depreciar" HeaderText="SaldoXDepreciar" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_a_depreciar" HeaderText="Valor a Depreciar" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dias_a_depreciar" HeaderText="Dias Deprec." DataFormatString="{0:N}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta_depreciacion" HeaderText="Cod.Cuenta" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomcuenta_depreciacion" HeaderText="Nom.Cuenta" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta_depreciacion_gasto" HeaderText="Cod.Cuenta Gasto" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomcuenta_depreciacion_gasto" HeaderText="Nom.Cuenta Gasto" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechacreacion" HeaderText="F.Creación"  DataFormatString="{0:d}" Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="usuariocreacion" HeaderText="Usuario" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
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

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
