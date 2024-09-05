<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }

    public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)  
    {  
        routes.MapPageRoute("pageRoute", "page/{psevdoName}", "~/page.aspx");
        routes.MapPageRoute("newsRoute", "news/{psevdoName}", "~/newsItem.aspx");
        routes.MapPageRoute("newsRoute2", "news", "~/news.aspx");
        routes.MapPageRoute("articleRoute", "article/{psevdoName}", "~/article.aspx");
        routes.MapPageRoute("artCatRoute", "articles/{psevdoName}", "~/articles.aspx");
        routes.MapPageRoute("artCatRoute2", "articles", "~/articles.aspx");
        routes.MapPageRoute("theoryPageRoute", "theory-page/{psevdoName}", "~/theoryPage.aspx");
        routes.MapPageRoute("theoryCatRoute", "theory/{psevdoName}", "~/theory.aspx");
        routes.MapPageRoute("theoryCatRoute2", "theory", "~/theory.aspx");
        routes.MapPageRoute("contactsRoute", "contacts", "~/contacts.aspx");
        routes.MapPageRoute("searchRoute", "search/", "~/search.aspx");
    }  
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Код, выполняемый при завершении работы приложения

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Код, выполняемый при возникновении необрабатываемой ошибки

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске нового сеанса

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения. 
        // Примечание: Событие Session_End вызывается только в том случае, если для режима sessionstate
        // задано значение InProc в файле Web.config. Если для режима сеанса задано значение StateServer 
        // или SQLServer, событие не порождается.

    }
       
</script>
