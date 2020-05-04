<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Cartera_ReportHistoricoClasifi_Lista" %>


<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvBalance" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
              <asp:Panel ID="pConsulta" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left">
             <asp:DropDownList ID="ddlFechaInicio" runat="server" CssClass="textbox" 
                                Width="150px">
                            </asp:DropDownList>
            </td>
            <td style="text-align: left">
               <asp:DropDownList ID="ddlFechaFinal" runat="server" CssClass="textbox" 
                                Width="150px">
                            </asp:DropDownList>
            </td>

            <td style="text-align: left">Oficina:
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>
            <%--<td style="text-align: left">Categoria:<br />
                <ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>
            <td style="text-align: left">Linea de Crédito:<br />
                <ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>--%>
           
        </tr>
    </table>
                  </asp:Panel>
              <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td>
                        <hr style="width: 100%; text-align: left" />                        
                    </td>
                </tr>
                <tr>
                    <td>     
                        <br />   
                          <div style="overflow: scroll; height:400px; width:900px; margin-right: 0px;">                           
                        <asp:GridView ID="gvLista" runat="server" OnPageIndexChanging="gvLista_PageIndexChanging"
                            AutoGenerateColumns="False" 
                            PageSize="100" AllowPaging="true"
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="60%" style="font-size: xx-small"
                            OnDataBound = "OnDataBound" >
                            <Columns>                        
                               
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                              </div>
                        &nbsp;
                        </td>
                </tr>
            </table>
            </asp:View>

        </asp:MultiView>
</asp:Content>

