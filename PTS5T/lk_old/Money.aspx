<%@ Page Title="" Language="C#" MasterPageFile="~/lk/mpLK.master" AutoEventWireup="true" CodeFile="Money.aspx.cs" Inherits="lk_Money" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLK" Runat="Server">
    <div class="col-9 sub">
                <h1>Личный кабинет</h1>                
                <p class="till fs-36"><asp:Literal ID="lMoney" runat="server" /> Б&#8381;<a href="addMoney.aspx">пополнить</a></p>
                <h2>Транзакции</h2>
                <asp:Literal ID="lTrans" runat="server" />
            </div>  
</asp:Content>

