<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" 
    Inherits="Lista"  Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server" >

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 936px">
        <tr>
            <td style="text-align: left; width: 152px;">
                &nbsp;</td>
            <td style="text-align: left" colspan="4">
                <strong>Criterios de Búsqueda</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 152px">Num. Crédito<br />
                <asp:TextBox ID="txtNumCred" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 140px">Identificación<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 172px">Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="250px" />
            </td>
            <td style="text-align: left; ">Fec. Aprobación<br />
                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
            </td>
            <td style="text-align: left; ">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 152px">Código nómina<br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 140px">&nbsp;</td>
            <td style="text-align: left; ">Linea<br />
                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="250px" />
            </td>
            <td style="text-align: left; ">Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="250px" />
            </td>
            <td style="text-align: left; ">&nbsp;</td>
        </tr>
    </table>

        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Créditos rotativos activos :</strong><br />
                </td>
            </tr>
            <tr>
                <td>                   
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                        <Columns>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/gr_edit.jpg" ShowEditButton="False" ShowSelectButton="True" />
                            <asp:BoundField DataField="numero_radicacion" HeaderText="Num. Crédito">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomlinea" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_deudor" HeaderText="Cod. Deudor">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="idavance" HeaderText="Número Avance">
                                <ItemStyle HorizontalAlign="center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cupototal" HeaderText="Cupo Total">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cupodisponible" HeaderText="Cupo Disponible" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
   
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
   
    <br />
    <br />


</asp:Content>
