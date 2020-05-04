<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tnumero.ascx.cs" Inherits="TextBoxValor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<script type="text/javascript">
    function clearTextBox() 
    {
        document.getElementById("MEE").disabled = true;
    }
    function disable() {
        var behavior = $find("MEE"); // "MaskedEditExtenderEx" - BehaviorID
        // to prevent the base dispose() method call - it removes the behavior from components list
        //------------------------------------------------------------------------
        var savedDispose = AjaxControlToolkit.MaskedEditBehavior.callBaseMethod;
        AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = function (instance, name) {
        };
        //------------------------------------------------------------------------
        behavior.dispose();
        // restore the base dispose() method
        AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = savedDispose;
    }

    function enable() { // enable it again
        var behavior = $find("MEE"); // "MaskedEditExtenderEx" - BehaviorID
        behavior.initialize();
    }

    function blur(Valor) 
    {
        document.getElementById('<%= TxtValor.ClientID %>').value = Valor + "11";
    }

</script>
<p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="TxtValor" runat="server"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>  
</p>
<p>
&nbsp;
</p>








