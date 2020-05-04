<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0">
                        <tr>
                            <td colspan="4" style="text-align: left">
                                <strong>Criterios de Búsqueda</strong>
                            </td>
                        </tr>
                        <tr>
                        <td style="text-align:left;width:200px">
                                Linea Auxilio<br />
                                <asp:DropDownList ID="ddlLineaAux" runat="server" Height="25px" Width="95%" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>  
                          
                            <td style="text-align:left;width:150px">
                                Cod Cuenta<br />
                                <cc1:textboxgrid id="txtCodCuenta" runat="server" autopostback="True" cssclass="textbox"
                                    style="text-align: left" backcolor="#F4F5FF" width="100px" ></cc1:textboxgrid>                                                               
                            </td>
                           
                            <td style="text-align:left;width:200px">
                                Tipo de Transacción<br />
                                <asp:DropDownList ID="ddltipotransaccion" runat="server" Height="25px" Width="95%" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>                           
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>                
        <tr>
            <td>
                <hr style="width: 100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <strong>Listado de Parámtros de Auxilios</strong>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" DataKeyNames="idparametro"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing" 
                    PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                            ShowEditButton="True" />
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                            ShowDeleteButton="True" />                        
                        <asp:BoundField DataField="idparametro" HeaderText="Id">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea_auxilio" HeaderText="Linea Auxilio">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_cuenta" HeaderText="Cod Cuenta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nom_cuenta" HeaderText="Nombre Cuenta">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tipo Transacción">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="nom_tipo_mov" HeaderText="Tipo Movimiento">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomestructura" HeaderText="Estructura">
                            <ItemStyle HorizontalAlign="Left" />
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

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>
