<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlColorPicker.ascx.cs" Inherits="ctlColorPicker" %>

<link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/ui-lightness/jquery-ui.css">
    <link href="../../../Styles/evol-colorpicker.css" rel="stylesheet" type="text/css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../../../Scripts/evol-colorpicker.min.js" type="text/javascript"></script>

<asp:TextBox ID="txtcolor" runat="server" ClientIDMode="Static" OnTextChanged="txtcolor_TextChanged"
    AutoPostBack="True" Width="80px"></asp:TextBox>

<script type="text/javascript">
    $(document).ready(function () {
        $("#txtcolor").colorpicker();
    });
    </script>