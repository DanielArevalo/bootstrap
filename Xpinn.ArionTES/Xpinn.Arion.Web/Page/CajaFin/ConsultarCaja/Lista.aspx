<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Nuevo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
<br/>
 <td class="tdI" style="width: 130px" align="left">
    <asp:Label ID="Label1" runat="server" Text="Consulta de Arqueos" ></asp:Label>
     </td>
    <br/>
<div id="gvDiv">

       <table cellpadding="5" cellspacing="0" style="width: 100%">
           <tr>
               <td class="tdI" style="width: 130px">
                  
                    Fecha Arqueo<ucFecha:fecha ID="txtFechaIni" runat="server" CssClass="textbox" />
                  
               </td>
               <td class="tdI">
                   <asp:Label ID="Labelejecutivos" runat="server" Text="Cajeros"></asp:Label>
                   <br />
                   <asp:DropDownList ID="ddlAsesores" runat="server" AutoPostBack="True" 
                       CssClass="dropdown" Height="18px" 
                       onselectedindexchanged="ddlAsesores_SelectedIndexChanged" Width="220px">
                   </asp:DropDownList>
                   <br/>
               </td>
               
           </tr>
           <tr>
               <td colspan="3" valign="top">
                   <asp:GridView ID="gvdetalles" runat="server" AllowPaging="True" 
                       AutoGenerateColumns="False" DataKeyNames="cod_cajero" GridLines="Horizontal" 
                       HeaderStyle-CssClass="gridHeader" 
                       OnPageIndexChanging="gvGarantias_PageIndexChanging" 
                       onrowediting="gvGarantias_RowEditing" 
                       
                       PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" 
                       Width="100%">
                       <Columns>
                           <asp:BoundField DataField="cod_cajero" HeaderStyle-CssClass="gridColNo" 
                               ItemStyle-CssClass="gridColNo" />
                           <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                              </asp:TemplateField>
                           <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                               <ItemTemplate>
                                   <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" 
                                       ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                               </ItemTemplate>
                           </asp:TemplateField>
                          
                           <asp:BoundField DataField="fechaCierre" 
                               HeaderText="Fecha de Arqueo" />
                           <asp:BoundField DataField="cod_caja"  
                               HeaderText="Caja" />
                           <asp:BoundField DataField="nombre" HeaderText="Cajero" />
                           <asp:BoundField DataField="nom_moneda"  HeaderText="Modena">
                           <ItemStyle HorizontalAlign="center" />
                           </asp:BoundField>
                           <asp:BoundField DataField="efectivo" HeaderText="Saldo Efectivo" DataFormatString="{0:N0}"  />
                           <asp:BoundField DataField="cheque" DataFormatString="{0:N0}" 
                               HeaderText="Saldo Cheque" HtmlEncode="false" />
                          
                       </Columns>


                      
                   </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Text=""></asp:Label>
               </td>
               <td>
               </td>
           </tr>
       </table>
    </div>
    </div>
    </div>
    </div>
    </div>
</asp:Content>