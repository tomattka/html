<%@ Page Title="" Language="C#" MasterPageFile="~/mpPage.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sph" Runat="Server">
    <!-- adaptation in page.less -->
     <article>
        <h1><asp:Literal ID="lTitle" runat="server" /></h1>
        <asp:Literal ID="lText" runat="server" />
         <div class="contactpage" runat="server">
             <asp:TextBox ID="tbName" runat="server" placeholder="Имя" />
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Введите ваше имя!" ControlToValidate="tbName" Display="Dynamic" SetFocusOnError="True"><p style="color:red; margin:0;">Введите ваше имя!</p></asp:RequiredFieldValidator>
             <asp:TextBox ID="tbMail" runat="server" placeholder="E-Mail" />   
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Укажите ваш E-mail!" ControlToValidate="tbMail" Display="Dynamic" SetFocusOnError="True"><p style="color:red; margin:0;">Укажите E-mail!</p></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="Укажите корректный E-mail!" Display="Dynamic" ControlToValidate="tbMail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><p style="color:red; margin:0;">Укажите корректный E-mail!</p></asp:RegularExpressionValidator>
             <asp:TextBox ID="tbMessage" runat="server" TextMode="MultiLine" placeholder="Ваше сообщение" />
             <asp:RequiredFieldValidator ID="rfvText" runat="server" ErrorMessage="Введите текст сообщения!" ControlToValidate="tbMessage" Display="Dynamic" SetFocusOnError="True"><p style="color:red; margin:0;">Введите текст сообщения</p></asp:RequiredFieldValidator>
             <asp:Literal ID="lErr" runat="server" />
             <asp:LinkButton ID="lbSend" runat="server" Text="Отправить сообщение" CssClass="sendbtn" OnClick="lbSend_Click" />
        </div>
     </article>
</asp:Content>

