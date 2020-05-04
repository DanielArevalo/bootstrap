<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reporte Movimientos :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table width="100%">
            <tr>
                <td style="text-align: left;" colspan="5">
                    <strong>Criterios de B�squeda</strong>
                </td>
            </tr>
            <tr>
                <td class="logo" style="text-align: left">
                    Num Cuenta<br />
                    <asp:TextBox ID="txtNumCta" runat="server" CssClass="textbox" Width="120px" />
                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtNumCta" ValidChars="-" />
                </td>
                <td style="text-align: left">
                    L�nea<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="200px" />
                </td>
                <td style="text-align: left">
                    Fecha Apertura<br />
                    <ucFecha:fecha ID="txtFechaApertura" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left">
                    Forma Pago<br />
                    <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="textbox" Width="170px" />
                </td>
                <td style="text-align: left">
                    Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="170px" />
                </td>
            </tr>
            <tr>
                <td class="logo" style="text-align: left">
                    Cod. Persona<br />
                    <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" Width="120px" />
                    <asp:FilteredTextBoxExtender ID="ftb2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                    TargetControlID="txtCodPersona" ValidChars="-" />
                </td>
                <td style="text-align: left">
                    Identificaci�n<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="140px" />
                </td>
                <td style="text-align: left">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left">
                    C�digo de n�mina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="180px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left" colspan="5">
                    <hr style="width: 100%" />
                </td>
            </tr>
        </table>
    </asp:Panel>
        
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Panel ID="panelGrilla" runat="server">
                    <strong>Listado de Cuentas</strong>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" 
                        PagerStyle-CssClass="gridPager" RowStyle-Font-Size="X-Small"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_programado">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" />
                            <asp:BoundField DataField="numero_programado" HeaderText="Num Cuenta">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomlinea" HeaderText="L�nea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificaci�n">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Titular">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina" HeaderText="C�digo de n�mina">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total"  DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota"  DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ning�n resultado"
                    Visible="False" />
            </td>
        </tr>
    </table>
    
</asp:Content>
