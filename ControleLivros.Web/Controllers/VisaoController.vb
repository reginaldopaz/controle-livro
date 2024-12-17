Imports System.Web.Mvc
Imports System.Threading.Tasks
Imports ControleLivros.Models
Imports ControleLivros.Services
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces

Public Class VisaoController
    Inherits Controller

    Private ReadOnly _relatorioService As IRelatorioService

    Public Sub New(relatorioService As IRelatorioService)
        _relatorioService = relatorioService
    End Sub

    ' GET: Visao
    Public Async Function Index(Optional ByVal nomeFiltro As String = "") As Task(Of ActionResult)
        Try
            Dim relatorio = Await _relatorioService.GetRelatorio()

            If Not String.IsNullOrEmpty(nomeFiltro) Then
                relatorio = relatorio.Where(Function(r) r.Nome.IndexOf(nomeFiltro, StringComparison.OrdinalIgnoreCase) >= 0).ToList()
            End If

            If relatorio IsNot Nothing AndAlso relatorio.Any() Then
                Console.WriteLine("Número de itens no relatório: " & relatorio.Count().ToString())
            Else
                Console.WriteLine("Nenhum dado retornado.")
            End If

            Return View(relatorio)
        Catch ex As Exception
            ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o relatório." & ex.Message)
            Return View(New List(Of RelatorioModel))
        End Try
    End Function



End Class
