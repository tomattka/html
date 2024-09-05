<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="lk_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLK" Runat="Server">
    <div class="col-9 main">
                <h1>Личный кабинет</h1>
                <table>
                    <tr>
                        <td class="left">
                            <p class="name"><asp:Literal ID="lUserName" runat="server" /> <a href="info.aspx">изменить</a></p>
                            <p class="mail"><b><asp:Literal ID="lUserMail" runat="server" /></b></p>
                            <p class="subscr"><b>Подписка:</b> <asp:Literal ID="lSubTill" runat="server" /></p>
                            <a href="subscription.aspx" class="prolong"><asp:Literal ID="lSubLinkText" runat="server" /></a>
                        </td>
                        <td class="right">
                            <p><asp:Literal ID="lUserMoney" runat="server" /> Б&#8381;</p>
                            <a href="addMoney.aspx">пополнить</a>
                        </td>
                    </tr>
                </table>
            </div>        
</asp:Content>

