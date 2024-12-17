Imports System.Data.Entity.Infrastructure
Imports System.Net
Imports System.Threading.Tasks
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports PagedList.Mvc
Imports PagedList
Imports System.Web.Mvc

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
        Async Function Index(Optional pageNumber As Integer = 1, Optional pageSize As Integer = 10, Optional codlFiltro As String = Nothing) As Task(Of ActionResult)
            Try
                Dim livros = Await _livroService.GetAllAsync()

                If Not String.IsNullOrEmpty(codlFiltro) Then
                    Dim codl As Integer
                    If Integer.TryParse(codlFiltro, codl) Then
                        livros = livros.Where(Function(l) l.Codl = codl).ToList()
                    End If
                End If

                Dim paginatedLivros = livros.ToPagedList(pageNumber, pageSize)

                Return View(paginatedLivros)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar a lista de livros." & ex.Message)
                Return View(New PagedList(Of Livro)(New List(Of Livro), pageNumber, pageSize))
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

        ' Método GET: Livro/Edit/5
        Async Function Edit(Codl As Integer?) As Task(Of ActionResult)
            If Not Codl.HasValue Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                Dim livro = Await _livroService.GetByIdAsync(Codl.Value)
                If livro Is Nothing Then
                    Return HttpNotFound()
                End If

                ViewBag.Autores = _autorService.GetAll().Select(Function(a) New SelectListItem With {
                                  .Value = a.CodAu.ToString(),
                                  .Text = a.Nome
                                  }).ToList()

                ViewBag.Assuntos = _assuntoService.GetAll().Select(Function(a) New SelectListItem With {
                                  .Value = a.CodAs.ToString(),
                                  .Text = a.Descricao
                                  }).ToList()


                'Dim autoresAssociados = livro.Autores.Select(Function(a) a.Nome).ToList()
                'Dim assuntosAssociados = livro.Assuntos.Select(Function(a) a.Descricao).ToList()

                Dim autoresAssociados = livro.Autores.Select(Function(a) a.CodAu.ToString()).ToList()
                Dim assuntosAssociados = livro.Assuntos.Select(Function(a) a.CodAs.ToString()).ToList()

                ViewBag.AutoresAssociados = autoresAssociados
                ViewBag.AssuntosAssociados = assuntosAssociados

                Return View(livro)
            Catch ex As Exception
                ModelState.AddModelError(String.Empty, "Ocorreu um erro ao carregar o livro para edição." & ex.Message)
                Return View()
            End Try
        End Function


        ' POST: Livro/Edit/5
        <HttpPost>
        <ValidateAntiForgeryToken>
        Async Function Edit(<Bind(Include:="Codl,Titulo,Editora,Edicao,AnoPublicacao,Valor,DataPublicacao")> ByVal livro As Livro, selectedAuthors As String(), selectedAssuntos As String()) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    ' Atualize o livro
                    Await _livroService.UpdateAsync(livro)

                    ' Atualize as associações de autores
                    Await _livroService.UpdateAuthorsAsync(livro.Codl, selectedAuthors)

                    ' Atualize as associações de assuntos
                    Await _livroService.UpdateAssuntosAsync(livro.Codl, selectedAssuntos)

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
