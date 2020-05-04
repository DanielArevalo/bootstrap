<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - IngresosFamilia :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCod_ingreso" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" Visible="False" />
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtIngresos" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" >0</asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Negocio&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtNegocio" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" />
                        <asp:maskededitextender ID="msktxtNegocio" runat="server" targetcontrolid="txtNegocio" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                       </td>
                       <td class="tdD">
                       Conyuge&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtConyuge" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtConyuge" runat="server" targetcontrolid="txtConyuge" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
           
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Hijos&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtHijos" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtHijos" runat="server" targetcontrolid="txtHijos" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
        
                       </td>
                       <td class="tdD">
                       Arriendos&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtArriendos" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtArriendos" runat="server" targetcontrolid="txtArriendos" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
        
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Pension&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtPension" runat="server" CssClass="textbox" MaxLength="128" />
                       <asp:maskededitextender ID="msktxtPension" runat="server" targetcontrolid="txtPension" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
        
                       </td>
                       <td class="tdD">
                       Otros&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtOtros" runat="server" CssClass="textbox" MaxLength="128" />
                        <asp:maskededitextender ID="msktxtOtros" runat="server" targetcontrolid="txtOtros" mask="999,999,999" messagevalidatortip="true"
                            masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"  errortooltipenabled="True" />
       
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       
                       </td>
                       <td class="tdD">
                       &nbsp;</td>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_INGRESO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>