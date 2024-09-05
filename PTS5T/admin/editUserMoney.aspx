<%@ Page Title="" Language="C#" MasterPageFile="~/admin/mpAdmin.master" AutoEventWireup="true" CodeFile="editUserMoney.aspx.cs" Inherits="admin_editUserMoney" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" Runat="Server">
    <h1>Cчёт пользователя <asp:Literal ID="lUser" runat="server" /></h1>
    <div class="allmoney"><asp:Literal ID="lAmount" runat="server" /> Б&#8381;</div>
    <asp:DropDownList ID="ddlAction" runat="server" Width="144">
        <asp:ListItem Value="Добавить" />
        <asp:ListItem Value="Вычесть" />
    </asp:DropDownList>    
    <asp:TextBox ID="tbAmount" runat="server" Width="150" CausesValidation="true" ToolTip="Сумма" TabIndex="1" /> <asp:Button ID="bGo" runat="server" Text=">" ToolTip="Выполнить действия" TabIndex="2" OnClick="bGo_Click" /><br />
    <asp:RangeValidator ID="rvAmount" runat="server" ErrorMessage="Сумма указана неверно <br /><br />" MaximumValue="1000000" MinimumValue="-1000000" ControlToValidate="tbAmount" SetFocusOnError="True" Display="Dynamic" Font-Size="14px" Type="Integer"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="rfAmount" runat="server" ErrorMessage="Укажите сумму <br /><br />" ControlToValidate="tbAmount" Display="Dynamic" SetFocusOnError="True" Font-Size="14px"></asp:RequiredFieldValidator>
    <label style="font-size: 14px; color: #232323; margin-top: 5px; display: block;">Комментарий:<br /><asp:TextBox ID="tbComment" runat="server" Width="323" TextMode="MultiLine" TabIndex="1" MaxLength="2000" /></label>
    <p>&nbsp</p>
    <p><b>Последние транзакции:</b></p>
    <asp:Literal ID="lTrans" runat="server" />


    	<script>
        window.onload = function () {
            const urlParams = new URLSearchParams(window.location.search);
            const status = urlParams.get('result');
            switch (status) {
                case "success": alert("Транзакция успешно совершена!"); break;
            }
        }
    </script>
</asp:Content>

