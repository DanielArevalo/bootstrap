<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Parametrización Contable Créditos :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="70%">
                   <tr>
                       <td class="columnForm50">
                           Línea<br/>                           
                           <asp:DropDownList ID="ddlLineaCredito" runat="server" Height="25px" 
                               Width="300px" CssClass="dropdown" AppendDataBoundItems="True">
                               <asp:ListItem Value=""></asp:ListItem>  
                            </asp:DropDownList>
                       </td>
                       <td class="ctrlLogin" style="width: 223px">
                           Atributo<br/>
                           <asp:DropDownList ID="ddlAtributo" runat="server" Height="25px" Width="200px" 
                               CssClass="dropdown" AppendDataBoundItems="True"> 
                               <asp:ListItem Value=""></asp:ListItem> 
                            </asp:DropDownList>
                       </td>
                       <td class="tdD">
                           Tipo Cuenta<br/>
                           <asp:DropDownList ID="ddlTipoCuenta" runat="server" Height="26px" Width="110px" 
                               CssClass="dropdown">
                               <asp:ListItem Value=""></asp:ListItem> 
                               <asp:ListItem Value="1">Normal</asp:ListItem>
                               <asp:ListItem Value="2">Causado</asp:ListItem>
                               <asp:ListItem Value="3">Orden</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       <td class="tdD">
                           Tipo Parametro<br/>
                           <asp:DropDownList ID="ddlTipo" runat="server" Height="25px" Width="110px" 
                               CssClass="dropdown"> 
                               <asp:ListItem Value=""></asp:ListItem>
                               <asp:ListItem Value="0">Operaciones</asp:ListItem>
                               <asp:ListItem Value="1">Clasificación</asp:ListItem>
                               <asp:ListItem Value="2">Provisión</asp:ListItem>
                               <asp:ListItem Value="3">Causación</asp:ListItem>
                               <asp:ListItem Value="4">Provisión General</asp:ListItem>
                               <asp:ListItem Value="5">Garantias</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       <td class="tdD">
                           Categoria<br/>
                           <asp:DropDownList ID="ddlCategoria" runat="server" Height="26px" 
                               Width="100px" CssClass="dropdown" AppendDataBoundItems="True"> 
                               <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       <td class="tdD">
                           &nbsp;    
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" 
                    AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" 
                    AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" 
                    onrowediting="gvLista_RowEditing" PageSize="20" DataKeyNames="idparametro" style="font-size: x-small" >
                    <Columns>
                        <asp:BoundField DataField="idparametro"  >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>                  
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Cod Linea" >
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_linea_credito" HeaderText="Linea" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_ATR" HeaderText="Cod.Atr" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO_CUENTA" HeaderText="Tipo Cta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COD_CATEGORIA" HeaderText="Categoria" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_LIBRANZA" HeaderText="Libranza" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_GARANTIA" HeaderText="Tipo Garantía" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COD_EST_DET" HeaderText="Estruc.Detalle" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COD_CUENTA" HeaderText="Cod.Cuenta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO_MOV" HeaderText="Tipo Mov." >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_TIPO_TRAN" HeaderText="Tipo Transaccion" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>


    <asp:HiddenField ID="HiddenField1" runat="server" />    
    <asp:ModalPopupExtender ID="mpeCopiar" runat="server" 
        PopupControlID="PanelCopiar" TargetControlID="HiddenField1"
        BackgroundCssClass="modalBackground" > 
    </asp:ModalPopupExtender>

    <asp:Panel ID="PanelCopiar" runat="server" BackColor="White" CssClass="pnlBackGround" Width="450px">
           <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
            <table width="450px">
                <tr>
                    <td colspan="2" style="text-align:center;width:100%">
                        &nbsp;<strong>Copiar Parámetros de Linea</strong>         
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;width:120px">
                       Desde
                    </td>
                    <td style="text-align:left;width:230px">
                       <asp:DropDownList ID="ddlLineaDesde" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">
                        Atributo
                    </td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlAtributoDesde" runat="server" Height="26px" Width="90%" CssClass="dropdown"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">
                       Tipo Parametro
                    </td>
                    <td style="text-align:left;">
                       <asp:DropDownList ID="ddlTipoDesde" runat="server" Height="26px" Width="90%" CssClass="dropdown">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="0">Operaciones</asp:ListItem>
                            <asp:ListItem Value="1">Clasificación</asp:ListItem>
                            <asp:ListItem Value="2">Provisión</asp:ListItem>
                            <asp:ListItem Value="3">Causación</asp:ListItem>
                            <asp:ListItem Value="4">Provisión General</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>      
                <tr>
                    <td style="text-align:right;width:70px" colspan="2">
                       <hr />
                    </td>
                </tr>                          
                <tr>
                    <td style="text-align:right;width:70px">
                       A la Linea
                    </td>
                    <td style="text-align:left;width:230px">
                       <asp:DropDownList ID="ddlLineaHasta" runat="server" CssClass="textbox" Width="270px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2">
                        &nbsp;<asp:Label ID="lblMsj" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center" colspan="2">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Agregar" CssClass="btn8" 
                            Width="180px" Height="27px" onclick="btnConfirmar_Click" />
                            &#160;&#160;&#160;
                        <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn8" 
                            Width="180px" Height="27px" onclick="btnCerrar_Click" />
                    </td>
                </tr>
            </table>  
             </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="Click" />
                   <asp:AsyncPostBackTrigger ControlID="btnConfirmar" EventName="Click" />
               </Triggers>
            </asp:UpdatePanel>            
    </asp:Panel>


    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
</script>

</asp:Content>