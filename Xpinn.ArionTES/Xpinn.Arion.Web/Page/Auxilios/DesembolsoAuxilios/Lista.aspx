<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 945px">
            <tr>
                <td style="text-align: left" colspan="5">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: left">Num. Auxilio<br />
                    <asp:TextBox ID="txtNumAux" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width: 135px">Fec. Solicitud<br />
                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left; width: 180px">Linea de Auxilio<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="97%" />
                </td>
                <td style="text-align: left; width: 120px">Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width: 220px">Nombre<br />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width: 170px">Ordenar por<br />
                    <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width: 170px"><%--Código de nómina--%><br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="90%" visible="false"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Auxilios :</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_auxilio" Style="font-size: small; margin-bottom: 0px;">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                            <asp:BoundField DataField="numero_auxilio" HeaderText="Num. Auxilio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Linea">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" Visible="false">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_solicitado" HeaderText="Valor Solicitante" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_aprobado" HeaderText="Valor Aprobado" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observacion" HeaderText="Observación" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="left" />
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
    </asp:Panel>
    <br />
    <br />

</asp:Content>
