Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities

Namespace ControleLivros.Repositories.Interfaces
    Public Interface IRelatorioRepository
        Function GetRelatorio() As Task(Of IEnumerable(Of RelatorioModel))
    End Interface
End Namespace
