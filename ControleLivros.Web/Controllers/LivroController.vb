Imports System.Data.Entity.Infrastructure
Imports System.Net
Imports System.Threading.Tasks
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces


Namespace Controllers
    Public Class LivroController
        Inherits Controller

        Private ReadOnly _livroService As ILivroService
        Private ReadOnly _autorService As IAutorService
        Private ReadOnly _assuntoService As IAssuntoService

        Public Sub New(livroService As ILivroService, autorService As IAutorService, assuntoService As IAssuntoService)
            _livroService = livroService
            _autorService = autorService
            _assuntoService = assuntoService
        End Sub

        ' GET: Livro
        Async Function Index() As Task(Of ActionResult)
            Try
                Dim livros = Await _livroService.GetAllAsync()
                Return View(livros)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar a lista de livros.")
                Return View(New List(Of Livro))
            End Try
        End Function

        ' GET: Livro/Details/5
        Async Function Details(Codl As Integer?) As Task(Of ActionResult)

            Try
                Dim livro = Await _livroService.GetByIdAsync(Codl.Value)
                If livro Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(livro)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar os detalhes do livro." & ex.Message)
                Return View()
            End Try
        End Function

        ' GET: Livro/Create
        Async Function Create() As Task(Of ActionResult)

            ViewBag.Autores = _autorService.GetAll().OrderBy(Function(a) a.Nome).Select(Function(a) New SelectListItem With {
                                                                                            .Value = a.CodAu.ToString(), .Text = a.Nome
                           }).ToList()

            ViewBag.Assuntos = _assuntoService.GetAll().Select(Function(a) New SelectListItem With {
                               .Value = a.CodAs.ToString(),
                                .Text = a.Descricao}).ToList()


            Return View()
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Create(ByVal livro As Livro) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try

                    Dim existingLivro = Await _livroService.GetByIdAsync(livro.Codl)

                    If existingLivro Is Nothing Then

                        Await _livroService.AddAsync(livro, Enumerable.Empty(Of Integer)(), Enumerable.Empty(Of Integer)())

                        ViewBag.ShowAddSection = True
                        ViewBag.LivroId = livro.Codl

                        ViewBag.Autores = _autorService.GetAll().Select(Function(a) New SelectListItem With {.Value = a.CodAu.ToString(), .Text = a.Nome}).ToList()
                        ViewBag.Assuntos = _assuntoService.GetAll().Select(Function(a) New SelectListItem With {.Value = a.CodAs.ToString(), .Text = a.Descricao}).ToList()

                        Return View(livro)
                    Else
                        ModelState.AddModelError(String.Empty, "O livro já existe.")
                    End If
                Catch ex As Exception

                    ModelState.AddModelError(String.Empty, "Erro ao salvar o livro: " & ex.Message)

                End Try
            End If
            ViewBag.Autores = _autorService.GetAll().Select(Function(a) New SelectListItem With {.Value = a.CodAu.ToString(), .Text = a.Nome}).ToList()
            ViewBag.Assuntos = _assuntoService.GetAll().Select(Function(a) New SelectListItem With {.Value = a.CodAs.ToString(), .Text = a.Descricao}).ToList()
            Return View(livro)
        End Function


        <HttpPost>
        <ValidateAntiForgeryToken>
        Async Function AddAuthorAssunto(livroId As Integer, selectedAuthor As Integer, selectedAssunto As Integer) As Task(Of JsonResult)
            Try
                ' Supondo que os serviços estejam configurados corretamente
                If selectedAuthor > 0 Then
                    Await _livroService.AddAuthorToLivroAsync(livroId, selectedAuthor)
                End If

                If selectedAssunto > 0 Then
                    Await _livroService.AddAssuntoToLivroAsync(livroId, selectedAssunto)
                End If

                ' Retornar informações para atualizar a grid
                Dim autorNome = (_autorService.GetById(selectedAuthor))?.Nome
                Dim assuntoDescricao = (_assuntoService.GetById(selectedAssunto))?.Descricao

                Return Json(New With {
            .success = True,
            .autorNome = autorNome,
            .assuntoDescricao = assuntoDescricao
        })
            Catch ex As Exception
                Return Json(New With {
            .success = False,
            .message = "Erro ao adicionar autor e assunto: " & ex.Message
        })
            End Try
        End Function

        ' GET: Livro/Edit/5
        Async Function Edit(Codl As Integer?) As Task(Of ActionResult)
            'If id Is Nothing Then
            '    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            'End If

            Try
                Dim livro = Await _livroService.GetByIdAsync(Codl.Value)
                If livro Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(livro)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o livro para edição." & ex.Message)
                Return View()
            End Try
        End Function

        ' POST: Livro/Edit/5
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Edit(<Bind(Include:="Id,Titulo,Editora,Edicao,AnoPublicacao,Valor")> ByVal livro As Livro) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Await _livroService.UpdateAsync(livro)
                    Return RedirectToAction("Index")
                Catch ex As Exception
                    ModelState.AddModelError(String.Empty, "Ocorreu um erro ao atualizar o livro." & ex.Message)
                End Try
            End If
            Return View(livro)
        End Function

        ' GET: Livro/Delete/5
        Async Function Delete(Codl As Integer?) As Task(Of ActionResult)
            If Codl Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim livro = Await _livroService.GetByIdAsync(Codl.Value)
                If livro Is Nothing Then
                    Return HttpNotFound()
                End If

                Return View(livro)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o livro para exclusão." & ex.Message)
                Return View()
            End Try
        End Function

        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Async Function DeleteConfirmed(Codl As Integer) As Task(Of ActionResult)
            Try
                Await _livroService.DeleteAsync(Codl)
                Return RedirectToAction("Index")
            Catch ex As DbUpdateException
                ' Exceção específica para violação de chave estrangeira
                ModelState.AddModelError(String.Empty, "Não é possível excluir o livro, pois existem registros associados." & ex.Message)
            Catch ex As Exception
                ' Captura qualquer outra exceção
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao excluir o livro." & ex.Message)
            End Try

            ' Retorna à View de exclusão com a mensagem de erro
            Dim livro = Await _livroService.GetByIdAsync(Codl)
            Return View("Delete", livro)
        End Function

    End Class
End Namespace
