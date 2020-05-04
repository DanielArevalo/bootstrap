<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - PRESUPUESTOEMPRESARIAL :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">                    
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                Total Activo</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtTotalactivo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Total Pasivo</td>
            <td class="tdD">
               
                <uc1:decimales ID="txtTotalpasivo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Total Patrimonio</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtTotalpatrimonio" runat="server" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Venta Mensual</td>
            <td class="tdD">
               
                <uc1:decimales ID="txtVentamensual" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Costo Total</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtCostototal" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Utilidad</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtUtilidad" runat="server" Enabled="false"  />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 148px">
                <br />
            </td>
            <td class="tdD" style="height: 148px">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod_persona&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_PERSONA" runat="server"
                        ControlToValidate="txtCod_persona" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                        ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                    <asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona"
                        ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                        SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                        Display="Dynamic" />
                    <br />
                    <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox>
                    <br />
                    Cod_presupuesto&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_presupuesto" runat="server" CssClass="textbox" Enabled="False"
                        MaxLength="128" />
                </asp:Panel>
                <br />
            </td>
        </tr>
    </table>
    <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_PRESUPUESTO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
