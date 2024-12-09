Imports System.Net
Imports System.Web.Mvc
Imports PagedList
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports System.Threading.Tasks
Imports System.Linq
Imports System.Data.Entity.Infrastructure

Namespace Controllers
    Public Class AutorController
        Inherits Controller

        Private ReadOnly _autorService As IAutorService

        Public Sub New(autorService As IAutorService)
            _autorService = autorService
        End Sub

        ' GET: Autor
        Async Function Index(page As Integer?, pageSize As Integer?) As Task(Of ActionResult)
            Try

                Dim pageNumber = If(page, 1)
                Dim pageSizeNumber = If(pageSize, 10)

                Dim autores = Await Task.Run(Function() _autorService.GetAll().OrderBy(Function(a) a.Nome).ToList())
                Dim pagedList = autores.ToPagedList(pageNumber, pageSizeNumber)
                Return View(pagedList)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar a lista de autores.")

                Return View(New StaticPagedList(Of Autor)(New List(Of Autor)(), 1, 1, 0))

            End Try
        End Function

        ' GET: Autor/Details/5
        Async Function Details(CodAu As Integer?) As Task(Of ActionResult)
            If CodAu Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim autor = Await Task.Run(Function() _autorService.GetById(CodAu.Value))
                If autor Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(autor)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar os detalhes do autor.")
                Return View()
            End Try
        End Function

        ' GET: Autor/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Autor/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Create(ByVal autor As Autor) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Await Task.Run(Sub() _autorService.Add(autor))
                    Return RedirectToAction("Index")
                Catch ex As Exception
                    ModelState.AddModelError(String.Empty, "Ocorreu um erro ao criar o autor.")
                End Try
            End If

            Return View(autor)
        End Function

        ' GET: Autor/Edit/5
        Async Function Edit(CodAu As Integer?) As Task(Of ActionResult)
            'If CodAu Is Nothing Then
            '    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            'End If

            Try
                Dim autor = Await Task.Run(Function() _autorService.GetById(CodAu.Value))
                If autor Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(autor)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o autor para edição." & ex.Message)
                Return View()
            End Try
        End Function

        ' POST: Autor/Edit/5
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Edit(ByVal autor As Autor) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Await Task.Run(Sub() _autorService.Update(autor))
                    Return RedirectToAction("Index")
                Catch ex As Exception
                    ModelState.AddModelError(String.Empty, "Ocorreu um erro ao atualizar o autor.")
                End Try
            End If

            Return View(autor)
        End Function

        ' GET: Autor/Delete/5
        Async Function Delete(CodAu As Integer?) As Task(Of ActionResult)
            If CodAu Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim autor = Await Task.Run(Function() _autorService.GetById(CodAu.Value))
                If autor Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(autor)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o autor para exclusão.")
                Return View()
            End Try
        End Function

        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Async Function DeleteConfirmed(CodAu As Integer) As Task(Of ActionResult)
            Try
                Await Task.Run(Sub() _autorService.Delete(CodAu))
                Return RedirectToAction("Index")
            Catch ex As DbUpdateException
                ' Exceção específica para violação de chave estrangeira, ajuste conforme sua implementação
                ModelState.AddModelError(String.Empty, "Não é possível excluir o autor, pois existem registros associados." & ex.Message)
            Catch ex As Exception
                ' Captura qualquer outra exceção
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao excluir o autor." & ex.Message)
            End Try

            ' Retorna à View de exclusão com a mensagem de erro

            Dim autor = _autorService.GetById(CodAu)
            Return View("Delete", autor)
        End Function

    End Class
End Namespace
