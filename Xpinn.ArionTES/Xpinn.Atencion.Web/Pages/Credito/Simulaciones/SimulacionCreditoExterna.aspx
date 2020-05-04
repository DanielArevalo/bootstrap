<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SimulacionCreditoExterna.aspx.cs" Inherits="SimulacionCreditoExterna" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" type="text/css" href="../../../Css/bootstrap.css">
    <link rel="stylesheet" href="../../../Css/estilos.css">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <style>
        .consulta {
            width: 30%;
            background-color: #5BA554;
            color: #fff;
        }

            .consulta:hover {
                background-color: #24813C;
            }
    </style>
    <title title=".: Simulación :."></title>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="../../../Scripts/flexslider.js"></script>
    <script type="text/javascript" charset="utf-8">
        $(window).load(function () {
            $('.flexslider').flexslider({
                touch: true,
                pauseOnAction: false,
                pauseOnHover: false,
            });
        });
    </script>
    <script src="../../../Scripts/PCLBryan.js"></script>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!--/#navigation-->
        <section id="home-section">
            <div class="colmd-12" style="height: 5px; background: rgba(100,166,93,1); background: -moz-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -webkit-gradient(left top, right top, color-stop(0%, rgba(100,166,93,1)), color-stop(100%, rgba(0,77,17,1))); background: -webkit-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -o-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -ms-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: linear-gradient(to right, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#64a65d', endColorstr='#004d11', GradientType=1 );">
            </div>
            <div class="container">
                <div class="col-md-3" style="margin-bottom: 1%; margin-top: 5px;">
                    <img src="../../../Imagenes/logo.png" style="width: 120%;">
                </div>
                <div class="col-md-9" style="margin-top: 5px;">
                    <h1 style="text-align: center;">Simulador de créditos de COOFINANCIAMOS</h1>
                </div>
            </div>
            <div class="colmd-12" style="height: 10px; background: rgba(100,166,93,1); background: -moz-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -webkit-gradient(left top, right top, color-stop(0%, rgba(100,166,93,1)), color-stop(100%, rgba(0,77,17,1))); background: -webkit-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -o-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: -ms-linear-gradient(left, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); background: linear-gradient(to right, rgba(100,166,93,1) 0%, rgba(0,77,17,1) 100%); filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#64a65d', endColorstr='#004d11', GradientType=1 );">
            </div>
            <div class="flexslider">
                <ul class="slides">
                    <li>
                        <img src="../../../Imagenes/convenios.png" alt="">
                    </li>
                    <li>
                        <img src="../../../Imagenes/nosotros.png" alt="">
                    </li>
                    <li>
                        <img src="../../../Imagenes/servicios.png" alt="">
                    </li>
                </ul>
            </div>
        </section>
        <!--/#home-section-->
        <section>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="container" style="margin: 4% auto;">
                        <div class="form">
                            <div class="row" style="background-color: #24813C; border-top-right-radius: 10px; border-top-left-radius: 10px;">
                                <h1 style="text-align: center; color: #fff; margin-top: 7px;">Simulador de crédito</h1>
                            </div>
                            <div class="row" style="text-align: center; margin-top: 2%;">
                                <label>Completa los espacios para ver el plan de pagos de tu crédito</label>
                            </div>
                            <div class="container">
                                <div class="col-md-6" style="text-align: left;">
                                    <h2>Monto solicitado</h2>
                                    <asp:Label runat="server" ID="lblMonto" Style="text-align: center; color: #ff0000;" Visible="false" Text="Monto solicitado invalido!"></asp:Label><br />
                                    <asp:TextBox ID="txtMontoSolicitado" onkeypress="return isNumber(event)" Style="width: 80%;" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6" style="text-align: left;">
                                    <h2>Tasa de Interés</h2>
                                    <asp:Label runat="server" ID="lblTasa" Style="text-align: center; color: #ff0000;" Visible="false" Text="Tasa de interés invalido!"></asp:Label><br />
                                    <asp:TextBox ID="txtTasaInteres" onkeypress="return isDecimalNumber(event)" Style="width: 80%;" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="container">
                                <div class="col-md-6" style="text-align: left;">
                                    <h2>Numero de cuotas</h2>
                                    <asp:Label runat="server" ID="lblCuotas" Style="text-align: center; color: #ff0000;" Visible="false" Text="Numero de cuotas invalido!"></asp:Label>
                                    <asp:TextBox ID="txtNumeroCuotas" onkeypress="return isNumber(event)" Style="width: 80%;" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6" style="text-align: left;">
                                    <h2>Periodicidad</h2>
                                    <asp:Label runat="server" ID="lblPeriodicidad" Style="text-align: center; color: #ff0000;" Visible="false" Text="Selecciona una periodicidad valida!"></asp:Label>
                                    <div class="dropdown">
                                        <asp:DropDownList class="btn btn-default dropdown-toggle" ID="ddlPeriodicidad" runat="server" Style="width: 80%;"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center; margin-top: 5%;">
                                        <asp:Button ID="btnConsultar" CssClass="col-md-6 btn btn-default consulta" runat="server" Text="Calcular" Style="margin-left: 10%;" OnClick="btnConsultar_Click" />
                                        <asp:Button ID="btnLimpiar" CssClass="col-md-6 btn btn-default consulta" runat="server" Text="Limpiar" Style="margin-left: 10%;" OnClick="btnLimpiar_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:UpdatePanel ID="updLineas" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <asp:Panel ID="panelLineas" runat="server">
                                                        <div class="text-left"><strong>Datos de créditos a solicitar</strong></div>
                                                        <div style="overflow: scroll; max-width: 100%;">
                                                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table"
                                                                RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="Smaller" GridLines="Horizontal"
                                                                RowStyle-CssClass="table" PageSize="12" AllowPaging="true" OnPageIndexChanging="gvLista_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField DataField="numerocuota" HeaderText="No. Cuota"
                                                                        ItemStyle-HorizontalAlign="center">
                                                                        <ItemStyle HorizontalAlign="center" Width="50px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="valorcuota" HeaderText="Valor Cuota"
                                                                        ItemStyle-HorizontalAlign="center" DataFormatString="{0:c}">
                                                                        <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="capital" HeaderText="Capital"
                                                                        ItemStyle-HorizontalAlign="center" DataFormatString="{0:c}">
                                                                        <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="interes" HeaderText="Interes Corriente" DataFormatString="{0:c}"
                                                                        ItemStyle-HorizontalAlign="center">
                                                                        <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sal_pendiente" HeaderText="Saldo Final"
                                                                        ItemStyle-HorizontalAlign="center" DataFormatString="{0:c}">
                                                                        <ItemStyle HorizontalAlign="center" Width="150px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <PagerTemplate>
                                                                    &nbsp;
                                    <asp:ImageButton ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pág"
                                        CommandArgument="First" ImageUrl="~/Imagenes/first.png" />
                                                                    <asp:ImageButton ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                                        CommandArgument="Prev" ImageUrl="~/Imagenes/previous.png" />
                                                                    <asp:ImageButton ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                                        CommandArgument="Next" ImageUrl="~/Imagenes/next.png" />
                                                                    <asp:ImageButton ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pág"
                                                                        CommandArgument="Last" ImageUrl="~/Imagenes/last.png" />
                                                                </PagerTemplate>
                                                                <HeaderStyle BackColor="#5BA554" Font-Bold="True" ForeColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                        <div style="text-align: center; width: 100%;">
                                                            <asp:Label ID="lblTotReg" runat="server" Visible="false" />
                                                            <asp:Label ID="lblInfo" runat="server" Visible="false" Text="Su consulta no obtuvo ningún resultado." />
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID ="btnLimpiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
        <footer id="footer" style="background-color: #125427; margin: 0px; padding: 0px">
            <div class="container" style="margin-bottom: 0px; margin-top: 0px;">
                <div class="row" style="margin: 15px 0px;">
                    <div class="logo col-sm-4 col-xs-6" style="margin-top: -15px;">
                        <h1 style="color: #fff;">COOFINANCIAMOS</h1>
                    </div>
                    <div class="col-sm-8 col-xs-6">
                        <h3 style="color: #fff; text-align: right;">Simulador de créditos</h3>
                    </div>
                </div>
            </div>
        </footer>
        <div style="text-align: center; background-color: #19351C; height: 80px; margin: 0px;">
            <div class="container" style="padding: 0px; margin-bottom: 0px; margin-top: 0px;">
                <div class="row" style="margin: 0px auto; margin-top: 2%;">
                    <div class="col-sm-12">
                        <p style="color: #fff; margin-top: 5px; text-align: center;">Todos los derechos reservados - CooFinanciamos 2017</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
