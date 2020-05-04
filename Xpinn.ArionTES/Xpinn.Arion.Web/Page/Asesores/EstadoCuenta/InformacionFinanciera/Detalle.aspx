<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Page_Asesores_EstadoCuenta_InformacionFinanciera_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:ImageButton runat="server" ImageUrl="~/Images/btnRegresar.jpg" ID="imgBtnVolver"
                    OnClick="imgBtnVolverHandler" />
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 100px;" runat="server" id="contentTable">
        <tr>
         <td style="text-align: center; width: 9%">
                Tipo Identificación
                <br />
                <asp:TextBox ID="txtTipoIdentificacion" runat="server" CssClass="textbox" Enabled="False"
                    Width="70%"></asp:TextBox>
                <br />
            </td>
            <td style="width: 9%" class="columnForm50">
                Número Identificación<br />
                <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" Enabled="False" Width="73%"
                    align="center" Height="20px"></asp:TextBox>
            </td>
           
                <td style="text-align:left; width: 33%">
                Nombre&nbsp;Completo
                <br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="False" 
                    Width="50%" aling="left"></asp:TextBox>
                <br />
            </td>
        </tr>
    </table>
    <table style="width: 75%; height: 100px;" runat="server" >
        <tr>
          <td style="text-align: center; width: 23%">
                Total Ingresos<br />
                <asp:TextBox ID="txtIngresos" runat="server" CssClass="textbox" Enabled="False" Width="70%"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                    Mask="999,999,999" MaskType="Number" TargetControlID="txtIngresos" />
            </td>
            <td style="text-align: center; width: 23%">
                Total Egresos
                <br />
                <asp:TextBox ID="txtEgresos" runat="server" CssClass="textbox" Enabled="False" Width="70%"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                                Mask="999,999,999" MaskType="Number" TargetControlID="txtEgresos" />
            </td>
            <td style="text-align: center; width: 24%">
                Total Activos<br />
                <asp:TextBox ID="txtactivos" runat="server" CssClass="textbox" Enabled="False" Width="70%"></asp:TextBox>
                  <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                                Mask="999,999,999" MaskType="Number" TargetControlID="txtactivos" />
            </td>
            <td style="text-align: center; width: 24%">
                Total Pasivos<br />
                <asp:TextBox ID="txtPasivos" runat="server" CssClass="textbox" Enabled="False" OnTextChanged="txtAntiguedad_TextChanged"
                    Width="70%" ></asp:TextBox>
                  <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" DisplayMoney="Left"
                                                Mask="999,999,999" MaskType="Number" TargetControlID="txtPasivos" />
                <br />
            </td>
             <td>&nbsp; &nbsp;&nbsp;</td>
        </tr>
    </table>
    <table style="width: 70%; height: 100px;" runat="server" >
        <tr>
            <td style="text-align: center; width: 50%">
                Total Disponible<br />
                <asp:TextBox ID="txtDisponible" runat="server" CssClass="textbox" Enabled="False"
                    Width="30%"></asp:TextBox>
                      <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" DisplayMoney="Left"
                                                Mask="999,999,999" MaskType="Number" TargetControlID="txtDisponible" />
            </td>
            <td style="text-align: center; width: 50%">
                Total Patrimonio<br />
                <asp:TextBox ID="txtPatrimonio" runat="server" CssClass="textbox" Enabled="False"
                    Width="30%"></asp:TextBox>
                     <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" DisplayMoney="Left"
                                                Mask="999,999,999" MaskType="Number" TargetControlID="txtPatrimonio" />
            </td>
         
        </tr>
    </table>
</asp:Content>
