<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
        </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pEncabezado" runat="server">
        <table style="width: 690px">
            <tr>
                <td style="width: 140px; text-align: left">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="text-align: left; width: 350px">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="95%" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align:left">
                <strong>Listado :</strong> <br />
                    <asp:GridView ID="gvLista" runat="server" Width="90%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" RowStyle-Font-Size="Small" DataKeyNames="idescalafon,grado" OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="idescalafon" HeaderText="idescalafón" Visible="false"/>
                            <asp:BoundField DataField="grado" HeaderText="Grado Escalafón">
                                <ItemStyle HorizontalAlign="left" Width="23%"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="asignacion_mensual" HeaderText="Asignación Basica Mensual" DataFormatString={0:N0}>
                                <ItemStyle HorizontalAlign="left" Width="70%"/>
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
            </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
   

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
