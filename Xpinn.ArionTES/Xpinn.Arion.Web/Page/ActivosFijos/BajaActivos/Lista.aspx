<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="5" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: left;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="height: 15px; text-align: left;">
                    Id.Encargado<br />
                    <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" 
                        Width="157px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowEditing="gvLista_RowEditing" OnRowDataBound="gvLista_RowDataBound" DataKeyNames="consecutivo" 
                    style="font-size: xx-small">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="consecutivo" HeaderText="Consec." >
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
                        <asp:BoundField DataField="estado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="serial" HeaderText="Serial" Visible="False" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_encargado" HeaderText="Encargado" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_compra" HeaderText="F.Compra"  DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_compra" HeaderText="Vr.Compra" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_avaluo" HeaderText="Vr.Avaluo" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_salvamen" HeaderText="Vr.Salvamento" DataFormatString="{0:N2}">
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
                        <asp:BoundField DataField="acumulado_depreciacion" HeaderText="Deprec.Acumulada" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_por_depreciar" HeaderText="SaldoXDepreciar" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
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
</asp:Content>
