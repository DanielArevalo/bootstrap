<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - NIF CostoAmortizado :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

<asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex=0>
<asp:View ID="vwData" runat="server">

    <table style="width: 100%">
        <tr>            
            <td style="text-align: left">
               Fecha de Costeo<br />
            <ucFecha:fecha ID="txtFecha" runat="server" />
            </td>            
        </tr>
        <tr>
            <td>
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>    
                
        <table style="width: 100%">
            <tr>
                <td style="text-align:left"> 
                <asp:Panel ID="panelGrilla" runat="server">              
                <div style="overflow:scroll; width:100%"> 
                    <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" 
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codobligacion" 
                        onrowdatabound="gvLista_RowDataBound" ShowFooter="True">
                        <Columns>                           
                            <asp:BoundField DataField="codobligacion" HeaderText="Num Obligación"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField HeaderText="Entidad" DataField="nomentidad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField HeaderText="Monto" DataField="monto" 
                                DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="tasa" HeaderText="Tasa"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField HeaderText="Saldo Total" DataField="saldo" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="plazo_faltante"  HeaderText="Plazo Faltante"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="tasa_mercado" HeaderText="Tasa Mercado"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField DataField="tir" HeaderText="TIR"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField HeaderText="Valor Presente" DataField="valor_presente" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                            <asp:BoundField HeaderText="Ajuste" DataField="valor_ajuste" DataFormatString="{0:n}"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>                                                        
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    </div>
                    </asp:Panel>                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" />
                </td>
            </tr>
        </table>
    

    </asp:View>
    <asp:View ID="vwFinal" runat="server">
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
                <td style="text-align: center; font-size: large; color:Red;">
                    Se Grabaron los Datos Correctamente
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                    <br />
                    <asp:Button id="btnRegresar" runat="server" CssClass="btn8" Text="  REGRESAR  " 
                    Height="30px" OnClick="btnRegresar_Click"/>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: large;">
                 <br />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>

    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
