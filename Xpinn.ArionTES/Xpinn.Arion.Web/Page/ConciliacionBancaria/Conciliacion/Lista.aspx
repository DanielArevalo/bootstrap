<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <table style="width: 600px">
        <tr>
            <td style="text-align: left" colspan="4">
                <strong>Filtrar por</strong>
            </td>
        </tr>
        <tr>
            <td class="logo" style="width: 160px; text-align: left">
                Cuenta Bancaria :<br />
                <asp:DropDownList ID="ddlCuentaBanc" runat="server" CssClass="textbox" 
                    Width="95%" AppendDataBoundItems="True" />
            </td>
            <td style="width: 160px; text-align: left">
                Cod. Cuenta Contable<br />
                <asp:DropDownList ID="ddlCuentaCont" runat="server" CssClass="textbox" 
                    Width="95%" AppendDataBoundItems="True" />
            </td>
            <td style="text-align: left; width:140px">
                Fecha Inicial<br />
                <ucFecha:fecha ID="txtIngresoIni" runat="server" CssClass="textbox"/>
            </td>
            <td style="text-align: left; width:140px">
                Fecha Final<br />
                <ucFecha:fecha ID="txtIngresoFin" runat="server" CssClass="textbox"/>          
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <strong>Listado de Conciliación</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="idconciliacion" 
                        onrowdeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                                ShowEditButton="True" />
                            <%--<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                ShowDeleteButton="True" />--%>
                            <asp:BoundField DataField="idconciliacion" HeaderText="Código">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numcuenta" HeaderText="Cta Bancaria">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombrebanco" HeaderText="Banco">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomtipocuenta" HeaderText="Tipo Cta">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_cuenta" HeaderText="Cta Contable">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Cuenta">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_inicial" HeaderText="Fec. Inicial" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_final" HeaderText="Fec. Final" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usuarioelabora" HeaderText="Elaborado Por">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomestado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>                    
                </td>                
            </tr>
            <tr>
            <td style="text-align:center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                                Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
