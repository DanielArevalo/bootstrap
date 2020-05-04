<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="~/General/Controles/fechaeditable.ascx" tagname="fecha" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
             <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="logo" style="width: 148px; text-align:left">
                           Fecha Aprobación
                       </td>
                       <td style="width: 148px; text-align:left" class="logo">
                           Oficina</td>
                       <td style="text-align:left">
                           <strong>
                           </strong>
                       </td>
                   </tr>
                    <tr>
                        <td class="logo" style="width: 148px; text-align:left">
                            <uc1:fecha ID="txtFechaaprobacion" runat="server" />                         
                        </td>
                        <td style="width: 138px; text-align:left">                           
                            <asp:DropDownList ID="ddlOficinas" runat="server" AutoPostBack="True" 
                                CssClass="textbox" Height="30px" 
                                onselectedindexchanged="ddlOficinas_SelectedIndexChanged" Width="191px" 
                                onload="ddlOficinas_Load">
                                <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                            </asp:DropDownList>                          
                            <br />
                        </td>
                        <td style="text-align:left">
                            &nbsp;<asp:Label ID="LblMensaje" runat="server" ForeColor="Red" Text="Label" 
                                Visible="False"></asp:Label>
                            &nbsp;
                            <asp:Label ID="Lblrestructurado" runat="server" Text="Restructurado"></asp:Label>
                            <asp:CheckBox ID="ChkRestructurado" runat="server" 
                                oncheckedchanged="ChkRestructurado_CheckedChanged" AutoPostBack="True" />
                            <br />
                        </td>
                    </tr>
                </table>
              </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" noshade></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" 
                    AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" 
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" 
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    onselectedindexchanged="gvLista_SelectedIndexChanged" 
                    onrowediting="gvLista_RowEditing" HeaderStyle-CssClass="gridHeader" 
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  
                    DataKeyNames="numero_radicacion" >
                    <Columns>
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}"  >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tasa" HeaderText="Tasa" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desc_tasa" HeaderText="Tipo Tasa" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="periodicidad" HeaderText="Amortización" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreAsesor" HeaderText="Asesor" >
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Codeudor" HeaderText="Identificación Codeudor" />
                        <asp:BoundField DataField="NombreCodeudor" HeaderText="Nombre Codeudor" />                        
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                <asp:MultiView ID="mvReporte" runat="server">
                    <asp:View ID="vReporte" runat="server">
                        <rsweb:ReportViewer ID="ReportViewActa" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)"  Width="100%" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="500px"> 
                        <localreport reportpath="Page\FabricaCreditos\Actas\ReporteActas.rdlc"></localreport></rsweb:ReportViewer>
                        <br />
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>    
</asp:Content>