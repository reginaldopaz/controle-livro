Imports System.Net
Imports System.Web.Mvc
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports System.Threading.Tasks
Imports ControleLivros.Models
Imports System.Data.Entity.Infrastructure


Namespace Controllers
    Public Class AssuntoController
        Inherits Controller

        Private ReadOnly _assuntoService As IAssuntoService

        Public Sub New(assuntoService As IAssuntoService)
            _assuntoService = assuntoService
        End Sub

        ' GET: Assunto
        Async Function Index() As Task(Of ActionResult)
            Try
                Dim assuntos = Await Task.Run(Function() _assuntoService.GetAll())
                Return View(assuntos)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar a lista de assuntos.")
                Return View(New List(Of Assunto))
            End Try
        End Function

        ' GET: Assunto/Details/5
        Async Function Details(CodAs As Integer?) As Task(Of ActionResult)
            If CodAs Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim assunto = Await Task.Run(Function() _assuntoService.GetById(CodAs.Value))
                If assunto Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(assunto)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar os detalhes do assunto.")
                Return View()
            End Try
        End Function

        ' GET: Assunto/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Assunto/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Create(ByVal assunto As Assunto) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Await Task.Run(Sub() _assuntoService.Add(assunto))
                    Return RedirectToAction("Index")
                Catch ex As Exception
                    ModelState.AddModelError(String.Empty, "Ocorreu um erro ao criar o assunto.")
                End Try
            End If

            Return View(assunto)
        End Function

        ' GET: Assunto/Edit/5
        Async Function Edit(CodAs As Integer?) As Task(Of ActionResult)
            If CodAs Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim assunto = Await Task.Run(Function() _assuntoService.GetById(CodAs.Value))
                If assunto Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(assunto)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o assunto para edição.")
                Return View()
            End Try
        End Function

        ' POST: Assunto/Edit/5
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Edit(ByVal assunto As Assunto) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Await Task.Run(Sub() _assuntoService.Update(assunto))
                    Return RedirectToAction("Index")
                Catch ex As Exception
                    ModelState.AddModelError(String.Empty, "Ocorreu um erro ao atualizar o assunto.")
                End Try
            End If

            Return View(assunto)
        End Function

        ' GET: Assunto/Delete/5
        Async Function Delete(CodAs As Integer?) As Task(Of ActionResult)
            If CodAs Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim assunto = Await Task.Run(Function() _assuntoService.GetById(CodAs.Value))
                If assunto Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(assunto)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o assunto para exclusão.")
                Return View()
            End Try
        End Function

        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Async Function DeleteConfirmed(CodAs As Integer) As Task(Of ActionResult)
            Try
                Await Task.Run(Sub() _assuntoService.Delete(CodAs))
                Return RedirectToAction("Index")
            Catch ex As DbUpdateException
                ' Exceção específica para violação de chave estrangeira
                ModelState.AddModelError(String.Empty, "Não é possível excluir o assunto, pois existem registros associados.")
            Catch ex As Exception
                ' Captura qualquer outra exceção
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao excluir o assunto.")
            End Try

            ' Retorna à View de exclusão com a mensagem de erro
            Dim assunto = _assuntoService.GetById(CodAs)
            Return View("Delete", assunto)
        End Function

    End Class
End Namespace
