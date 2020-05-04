<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Diligencias.aspx.cs" Inherits="Detalle" MasterPageFile="~/General/Master/site.master"%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
           <table style="width:100%; margin-right: 109px;">
            <tr>
                <td style="width: 463px; height: 7px; font-size: small; text-align: center;">
                    <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox" Width="89px" 
                        Enabled="False" Height="15px" Visible="False"></asp:TextBox>
                </td>
                <td style="height: 7px; text-align: center;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvListaDiligencias" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="codigo_diligencia" 
                    GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" 
                    OnPageIndexChanging="gvListaDiligencias_PageIndexChanging" 
                    onselectedindexchanged="gvListaDiligencias_SelectedIndexChanged"  
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                    Width="100%" onpageindexchanged="gvListaDiligencias_PageIndexChanged" 
                        onselectedindexchanging="gvListaDiligencias_SelectedIndexChanging1" 
                        ViewStateMode="Enabled" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="fecha_diligencia" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="tipo_diligencia_consulta" HeaderText="Tipo Diligencia" />
                        <asp:BoundField DataField="tipo_contacto_consulta" HeaderText="Tipo Contacto" />
                        <asp:BoundField DataField="atendio" HeaderText="Quien atendió?" />
                        <asp:BoundField DataField="respuesta" HeaderText="Respuesta" />
                        <asp:BoundField DataField="acuerdo_consulta" HeaderText="Acuerdo" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    </Columns>

<HeaderStyle CssClass="gridHeader"></HeaderStyle>

<PagerStyle CssClass="gridPager"></PagerStyle>

<RowStyle CssClass="gridItem"></RowStyle>

                </asp:GridView>
                </td>
                </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" 
                        style="font-size: x-small" />
                    

                </td>
                </tr>
            <tr>
                <td colspan="2">
                        <hr noshade width="100%" style="height: -12px">
                </td>
                </tr>
                      
               </table>
             <script type="text/javascript">
                 function NewWindow() {
                     document.forms[0].target = "_blank";
                 }

                   

                </script>
               
       
</asp:Content>