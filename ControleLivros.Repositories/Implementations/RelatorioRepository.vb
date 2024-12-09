Imports System.Data.Entity
Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Namespace ControleLivros.Repositories.Implementations
    Public Class RelatorioRepository
        Implements IRelatorioRepository

        Private ReadOnly _dbContext As DbContext

        Public Sub New(dbContext As DbContext)
            _dbContext = dbContext
        End Sub

        Public Async Function GetRelatorio() As Task(Of IEnumerable(Of RelatorioModel)) Implements IRelatorioRepository.GetRelatorio
            Dim query = "SELECT Nome, Titulo, Descricao FROM Relatorio"
            Dim relatorios = Await _dbContext.Database.SqlQuery(Of RelatorioModel)(query).ToListAsync()
            'Console.WriteLine("Número de registros retornados: " & relatorios.Count())
            Return relatorios

        End Function

    End Class
End Namespace