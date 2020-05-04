<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="ctlmensaje" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 950,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        } 
       
    </script>

    <asp:Panel ID="pDatos" runat="server" HorizontalAlign="Center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align:Center">
            <tr>
                <td style="text-align: Center; width:90px">Fecha Comprobante<br/>
                    <uc:fecha ID="txtFechaComprobante" runat="server" cssClass="textbox" Height="16px" Enabled="false"/>
                </td>
                <td style="text-align: Center;" class="gridIco">Código de Cuenta<br/>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCodCuenta" runat="server" Width="60px" CssClass="textbox" enable="false"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNomCuenta" runat="server" Width="180px" CssClass="textbox" enable="false"/>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: Center; width:100px">Saldo<br/>
                    <asp:TextBox ID="txtSaldo" runat="server" CssClass="textbox" enable="false"/>
                </td>
                <td style="text-align: Center; width:100px">Naturaleza de Cuenta<br/>
                    <asp:TextBox ID="txtNaturaleza" runat="server" CssClass="textbox" enable="false"/>
                </td>
                <td style="text-align: Center; width:90px">Fecha<br/>
                    <uc:fecha ID="txtFecha" runat="server" cssClass="textbox" Height="16px" OneventoCambiar="txtFecha_eventoCambiar" />
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 50%; text-align:Center">
            <tr>
                <td style="text-align: Center;" class="gridIco">Identificación<br/>
                    <asp:TextBox ID="txtIdentificacion" MaxLength="20" runat="server" CssClass="textbox" Width="100px" OnTextChanged="txtIdPersona_TextChanged"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="txtIdentificacion" Display="Dynamic" 
                        ForeColor="Red" ValidationGroup="vgGuardar" Font-Size="Small" />
                </td>
                <td style="text-align: Center" colspan="4">Tercero a Trasladar<br/>
                    <asp:TextBox ID="txtNomTercero" runat="server" CssClass="textbox" Width="280px" Enabled="true"></asp:TextBox>
                    <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px" OnClick="btnConsultaPersonas_Click" />
                    <asp:TextBox ID="txtCodTercero" MaxLength="20" runat="server" CssClass="textbox" Width="120px" Visible="false"></asp:TextBox>      
                    <br />
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="txtNomTercero" Display="Dynamic" 
                        ForeColor="Red" ValidationGroup="vgGuardar" Font-Size="Small" />
                </td>
            </tr>                    
        </table>
        <br/>
    </asp:Panel>

    <asp:Panel ID="pListado" runat="server" ChildrenAsTriggers="true">
        <table style="text-align: center; width:100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" AllowPaging="False" HorizontalAlign="Center"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" DataKeyNames="codtercero"
                        RowStyle-CssClass="gridItem" Style="font-size:small" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="fechaini" DataFormatString="{0:d}" HeaderText="Fecha Mov."/>
                            <asp:BoundField DataField="nombretercero" HeaderText="Tercero"/>
                            <asp:BoundField DataField="identercero" HeaderText="Identificación"/>
                            <asp:BoundField DataField="centro_costo" HeaderText="C/C"/>
                            <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Movimiento"/>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:N0}" HeaderText="Valor"/>                            
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False"  /><br/>    
                    <asp:Label ID="lblTraslado" runat="server" Width="120px" Text="Total a trasladar" Visible="True"></asp:Label>  
                    <asp:Label ID="lblTotalTraslado" runat="server" CssClass="textbox" Width="120px" Visible="True"></asp:Label>                 
                </td>
            </tr>
        </table>
        <span style="font-size: small">
            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
        &nbsp; </span>      
    </asp:Panel>

    <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
    <uc2:ctlmensaje ID="ctlmensaje" runat="server"/>
     

</asp:Content>

