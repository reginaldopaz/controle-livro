Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing
Imports System.Globalization
Imports System.Threading

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        UnityConfig.RegisterComponents() ' Adicione esta linha para registrar os componentes do Unity
        ModelBinders.Binders.Add(GetType(Decimal), New DecimalModelBinder())
        Dim cultura As New CultureInfo("pt-BR") ' Define cultura brasileira
        Thread.CurrentThread.CurrentCulture = cultura
        Thread.CurrentThread.CurrentUICulture = cultura

    End Sub
End Class
