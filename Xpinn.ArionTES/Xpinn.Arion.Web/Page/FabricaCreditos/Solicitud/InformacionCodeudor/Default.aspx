<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Page_FabricaCreditos_Solicitud_InformacionCodeudor_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
               <iframe frameborder="0" height="2400px" scrolling="no" src="Codeudor/Lista.aspx" 
                    width="100%">
               
               </iframe>

            </td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
               <iframe id="I1" frameborder="0" height="1200px" name="I1" scrolling="no" 
                    src="ConyugeCodeudor/Lista.aspx" width="100%">
               </iframe>

            </td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <br />
            </td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

