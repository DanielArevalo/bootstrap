<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Credito :." %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0">
                     <tr>
                        <td class="tdI" style="text-align:left">
                            Codigo Operación<br/>
                            <asp:TextBox ID="txtoperacion" runat="server" CssClass="textbox" 
                                MaxLength="20" Width="158px" />
                            <br />
                        </td>                        
                        <td class="tdD" style="text-align:left">
                            Tipo&nbsp; Operación<br />
                            <asp:DropDownList ID="ddloperacion" runat="server" CssClass="textbox" 
                                Width="174px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td class="tdD" style="text-align:left">
                           Numero de Comprobante<br/>
                           <asp:TextBox ID="txtcomprobante" runat="server" CssClass="textbox" 
                               MaxLength="128" Width="152px" />
                       </td>
                    </tr>                   
                    <tr>
                        <td class="tdD" style="text-align:left">
                            Fecha Operación <br />
                            Desde
                            <ucFecha:fecha id="ucFechaInicial" runat="server" 
                               style="text-align: center" />
                            Hasta
                            <ucFecha:fecha id="ucFechaFinal" runat="server" 
                               style="text-align: center" />
                        </td>
                        <td class="tdD" style="text-align:left">
                            Tipo Comprobante&nbsp;<br/>
                            <asp:DropDownList ID="ddlcomprobantes" runat="server" CssClass="textbox" 
                                Width="174px">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td class="tdD" style="text-align:left">
                            Número de Lista<br />
                            <asp:TextBox ID="txtNumLista" runat="server" CssClass="textbox" 
                               MaxLength="128" Width="152px" />
                        </td>
                    </tr>               
                </table>
                </asp:Panel>
            </td>
        </tr>
         <tr>
            <td><hr width="100%" /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" 
                    AutoGenerateColumns="False" onrowediting="gvLista_RowEditing"
                    AllowPaging="True"  PageSize="20" HeaderStyle-CssClass="gridHeader" 
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  
                    DataKeyNames="cod_referencia" onpageindexchanging="gvLista_PageIndexChanging" >
                    <Columns>                 
                        <asp:BoundField DataField="COD_OPE" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" > <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>             
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="COD_OPE" HeaderText="Código Operación"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="TIPO_OPE" HeaderText="Tipo Operación"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="FECHA_OPER" HeaderText="Fehca Operación"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="FECHA_REAL" HeaderText="Fecha Real"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="NUM_COMP" HeaderText="Numero Comprobante"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="TIPO_COMP" HeaderText="Tipo Comprobante"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="NUM_LISTA" HeaderText="Numero Lista"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                        <asp:BoundField DataField="NOMESTADO" HeaderText="Estado"  > <ItemStyle HorizontalAlign="center" /> </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
</asp:Content>