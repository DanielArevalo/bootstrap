<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Líneas de Auxilios :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 780px">
        <tr>
            <td style="width: 20%; text-align: left">
                Código<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="140px" />
            </td>
            <td style="text-align: left; width: 80%">
                Descripción<br />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="250px" />
            </td>
        </tr>
    </table>    
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                <strong>Listado de Auxilios :</strong> <br />
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="cod_linea_auxilio" OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="cod_linea_auxilio" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomestado" HeaderText="Estado">
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_minimo" HeaderText="Monto Mínimo" 
                                DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto_maximo" HeaderText="Monto Máximo" 
                                DataFormatString="{0:c}">
                            </asp:BoundField>                           
                            <asp:BoundField DataField="nomperiodicidad" HeaderText="Periodicidad">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_persona" HeaderText="Tipo Persona">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_auxilios" 
                                HeaderText="Num Auxilios">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_desembolso" HeaderText="Días para Desembolso">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="retencion" HeaderText="Cobra Retención">
                            <ItemStyle HorizontalAlign="center" />
                            <ItemStyle HorizontalAlign="Center" />
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

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
