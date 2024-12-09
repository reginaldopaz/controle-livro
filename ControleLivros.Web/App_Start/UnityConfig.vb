Imports Unity
Imports Unity.AspNet.Mvc
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports ControleLivros.Services.ControleLivros.Services.Implementations
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports ControleLivros.Repositories.ControleLivros.Repositories.Implementations
Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Repositories
Imports ControleLivros.Services
Imports System.Data.Entity

Public Class UnityConfig
    Public Shared Sub RegisterComponents()
        Dim container = New UnityContainer()

        ' Registre o DbContext para ser injetado
        container.RegisterType(Of DbContext, ControleLivrosContext)()

        ' Registre os serviços e repositórios
        container.RegisterType(Of ILivroService, LivroService)()
        container.RegisterType(Of ILivroRepository, LivroRepository)()
        container.RegisterType(Of IAssuntoService, AssuntoService)()
        container.RegisterType(Of IAssuntoRepository, AssuntoRepository)()
        container.RegisterType(Of IAutorService, AutorService)()
        container.RegisterType(Of IAutorRepository, AutorRepository)()
        container.RegisterType(Of IRelatorioService, RelatorioService)()
        container.RegisterType(Of IRelatorioRepository, RelatorioRepository)()

        DependencyResolver.SetResolver(New UnityDependencyResolver(container))
    End Sub
End Class
