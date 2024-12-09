﻿Imports ControleLivros.Models.ControleLivros.Models.Entities

Namespace ControleLivros.Services.Interfaces
    Public Interface ILivroService
        Function GetAllAsync() As Task(Of IEnumerable(Of Livro))
        Function GetByIdAsync(id As Integer) As Task(Of Livro)
        'Function AddAsync(livro As Livro) As Task
        Function AddAsync(livro As Livro, selectedAuthors As IEnumerable(Of Integer), selectedAssuntos As IEnumerable(Of Integer)) As Task
        Function UpdateAsync(livro As Livro) As Task
        Function DeleteAsync(id As Integer) As Task
        Function AddAuthorToLivroAsync(livroId As Integer, authorId As Integer) As Task ' Adicionando método
        Function AddAssuntoToLivroAsync(livroId As Integer, assuntoId As Integer) As Task ' Opcional, para adicionar assunto

    End Interface
End Namespace
