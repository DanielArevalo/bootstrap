<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <asp:Panel ID="pConsulta" runat="server">
        <table>
            <tr>
                <td style="text-align: left" colspan="5">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Num. Servicio<br />
                    <asp:TextBox ID="txtNumServ" runat="server" CssClass="textbox" Width="110px" />
                </td>
                <td style="text-align: left;">
                    Fec. Solicitud<br />
                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left;">
                    Linea de Servicio<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="230px" />
                </td>
                <td style="text-align: left;">
                    Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="100px" />
                </td>
                <td style="text-align: left;">
                    Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="150px" />
                </td>
                <td style="text-align: left;">
                    Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr style="width:100%" />
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
            <td style="text-align:left">
                <strong>Listado de Servicios :</strong>                
            </td>
        </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_servicio"  style="font-size: x-small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                            <asp:BoundField HeaderText="F. 1erCuota" DataField="fecha_primera_cuota" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                            <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}" />
                            <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                            <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                            <asp:BoundField HeaderText="Ident. Titular" DataField="identificacion_titular" />
                            <asp:BoundField HeaderText="Nom. Titular" DataField="nombre_titular" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="False" />                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    

</asp:Content>
