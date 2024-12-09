Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports ControleLivros.Services.ControleLivros.Services.Interfaces

Namespace ControleLivros.Services.Implementations
    Public Class RelatorioService
        Implements IRelatorioService

        Private ReadOnly _relatorioRepository As IRelatorioRepository

        Public Sub New(relatorioRepository As IRelatorioRepository)
            _relatorioRepository = relatorioRepository
        End Sub

        Public Async Function GetRelatorio() As Task(Of IEnumerable(Of RelatorioModel)) Implements IRelatorioService.GetRelatorio
            Return Await _relatorioRepository.GetRelatorio()
        End Function

    End Class
End Namespace