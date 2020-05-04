<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionCodeudor_Presupuesto_Default" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<asp:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager2" />--%>
    <table width="100%">
        <tr>
            <td style="height: 414px">
                <asp:TabContainer runat="server" ID="Tabs" ActiveTabIndex="1" Width="100%">
                    <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="Datos">
                        <HeaderTemplate>                
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            Presupuesto Fmailiar del Codeudor
                        </HeaderTemplate>            
                        <ContentTemplate>                
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <iframe id="I1" frameborder="0" height="600px" name="I1" scrolling="no" 
                                src="PresupuestoFamiliar/Lista.aspx" width="600px"></iframe>                
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                         </ContentTemplate>            
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="Datos">
                        <HeaderTemplate>                
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            Presupuesto Empresarial del Codeudor
                        </HeaderTemplate>            
                        <ContentTemplate>                         
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         
                            <iframe frameborder="0" height="600px" scrolling="no" 
                                src="PresupuestoEmpresarial/Lista.aspx" width="600px" id="I2" name="I2"></iframe>                      
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 
                        </ContentTemplate>            
                    </asp:TabPanel>
                </asp:TabContainer>
            </td>
        </tr>
        </table>
</asp:Content>

