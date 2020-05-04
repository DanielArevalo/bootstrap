<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Presupuesto :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>    
        
    <script type="text/javascript"> 
        function pageLoad() {
            $('#<%=gvProyeccion.ClientID%>').gridviewScroll({
                width: 980,
                height: 400,               
                freezesize: 2,
                headerrowcount: 2,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"  
            });
        }

    </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../../../Scripts/PopUp.js" />
        </Scripts>
    </asp:ScriptManager>

    <ajaxToolkit:ModalPopupExtender ID="mpeLoading" runat="server" BehaviorID="idmpeLoading"
        PopupControlID="pnlLoading" TargetControlID="btnLoading" EnableViewState="false"
        BackgroundCssClass="ModalBackground" />
    <asp:Panel ID="pnlLoading" runat="server" Width="200" Height="100" HorizontalAlign="Center"
        CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <asp:Image ID="Image1" ImageUrl="../../../Images/loading.gif" runat="server"/>
        <br />Generando el Presupuesto Ejecutado...        
    </asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">          
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>          
                <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="80%">
                    <tr>
                        <td style="width: 169px; text-align:left" colspan="2">
                            Código<br/>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="104px" 
                                Enabled="False" />
                        </td>
                        <td colspan="2" style="text-align:left">
                            Tipo de Presupuesto<br />
                            <asp:DropDownList ID="ddlTipoPresupuesto" runat="server" CssClass="dropdown" 
                                Width="180px" Height="25px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" colspan="2" style="text-align:left">
                            Centro de Costo<br />
                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" 
                                Width="180px" Height="25px" AppendDataBoundItems="True" Enabled="False">
                                <asp:ListItem Selected="True" Value="0">Consolidado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdI" colspan="6" style="text-align:left">
                            Descripción<br />
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" 
                                Width="380px" Enabled="False" ReadOnly="True" Wrap="False" />
                            <br/>
                        </td>
                    </tr>
                </table>
                <table id="tbPeriodo" border="0" cellpadding="5" cellspacing="0" width="70%">
                    <tr>
                        <td style="text-align:left; ">
                            Periodo<br />
                            <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="dropdown" 
                                Height="25px" Width="329px" 
                                DataTextField= 'fecha_periodo' DataValueField="numero_periodo" 
                                AutoPostBack="True" onselectedindexchanged="ddlPeriodo_SelectedIndexChanged">
                            </asp:DropDownList>                        
                            <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte" 
                                OnClick="btnGenerar_Click" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>                
                <br />
                <hr />         
                <asp:GridView ID="gvProyeccion" runat="server" AutoGenerateColumns="False" OnRowCreated="gvProyeccion_RowCreated"
                    Font-Size="XX-Small" PageSize="20" Style="font-size: xx-small" Width="960">
                    <Columns>
                        <asp:BoundField DataField="cod_cuenta" HeaderText="Código"><ItemStyle HorizontalAlign="Left" Width="40px" /></asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Cuentas"><ControlStyle Width="180px" /><ItemStyle HorizontalAlign="Left" Width="180px"/></asp:BoundField>
                        <asp:BoundField DataField="depende_de" HeaderText="Depende De" Visible="False"><ItemStyle HorizontalAlign="Left" Width="80px" /></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False"><ItemStyle HorizontalAlign="Left" Width="40px" /></asp:BoundField>
                        <asp:BoundField DataField="valor_presupuestado" DataFormatString="{0:N}" HeaderText="Vr.Presupuesto" ><ItemStyle HorizontalAlign="Right" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="valor_ejecutado" DataFormatString="{0:N}" HeaderText="Vr.Ejecutado" ><ItemStyle HorizontalAlign="Right" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="diferencia" DataFormatString="{0:N2}" HeaderText="Vr.Diferencia" ><ItemStyle HorizontalAlign="Right" Width="80px" /></asp:BoundField>
                        <asp:BoundField DataField="porcentaje" DataFormatString="{0:N2}" HeaderText="% Dif." ><ItemStyle HorizontalAlign="Right" Width="70px" /></asp:BoundField>
                        <asp:BoundField DataField="acumulado_presupuestado" DataFormatString="{0:N}" HeaderText="Acumulado Presupuesto" ><ItemStyle HorizontalAlign="Right" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="acumulado_ejecutado" DataFormatString="{0:N}" HeaderText="Acumulado Ejecutado" ><ItemStyle HorizontalAlign="Right" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="acumulado_diferencia" DataFormatString="{0:N2}" HeaderText="Acum. Diferencia" ><ItemStyle HorizontalAlign="Right" Width="80px" /></asp:BoundField>
                        <asp:BoundField DataField="acumulado_porcentaje" DataFormatString="{0:N2}" HeaderText="% Acum. Dif." ><ItemStyle HorizontalAlign="Right" Width="70px" /></asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" /> 
                    <SelectedRowStyle Font-Size="XX-Small" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="text-align:left">
        <asp:Button ID="btnExpPresupuesto" runat="server" CssClass="btn8" 
            onclick="btnExpPresupuesto_Click" onclientclick="btnExpPresupuesto_Click" 
            Text="Exportar a excel" />
        </div>   

        </asp:View>

    </asp:MultiView>


</asp:Content>