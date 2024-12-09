Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities
Namespace ControleLivros.Services.Interfaces
    Public Interface IRelatorioService
        Function GetRelatorio() As Task(Of IEnumerable(Of RelatorioModel))
    End Interface
End Namespace
