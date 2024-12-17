Imports Xunit
Imports Moq
Imports System.Data.Entity
Imports ControleLivros.Models
Imports ControleLivros.Data
Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Implementations
'Imports System.Data.Entity

Namespace ControleLivros.Tests.Repositories.Implementations
    Public Class LivroRepositoryTests

        ' Teste para o método GetAll
        <Fact>
        Public Sub GetAll_DeveRetornarTodosOsLivros()
            ' Arrange
            Dim dados As IQueryable(Of Livro) = New List(Of Livro) From {
            New Livro With {.Codl = 1, .Titulo = "Livro 1", .Editora = "Editora A"},
            New Livro With {.Codl = 2, .Titulo = "Livro 2", .Editora = "Editora B"}
        }.AsQueryable()

            Dim mockSet As New Mock(Of DbSet(Of Livro))()
            mockSet.As(Of IQueryable(Of Livro))().Setup(Function(m) m.Provider).Returns(dados.Provider)
            mockSet.As(Of IQueryable(Of Livro))().Setup(Function(m) m.Expression).Returns(dados.Expression)
            mockSet.As(Of IQueryable(Of Livro))().Setup(Function(m) m.ElementType).Returns(dados.ElementType)
            mockSet.As(Of IQueryable(Of Livro))().Setup(Function(m) m.GetEnumerator()).Returns(dados.GetEnumerator())

            Dim mockContext As New Mock(Of ControleLivrosContext)()
            mockContext.Setup(Function(c) c.Livros).Returns(mockSet.Object)

            Dim repository As New LivroRepository(mockContext.Object)

            ' Act
            Dim resultado = repository.GetAll()

            ' Assert
            Assert.NotNull(resultado)
            Assert.Equal(2, resultado.Count())
            Assert.Contains(resultado, Function(l) l.Titulo = "Livro 1")
        End Sub

        ' Teste para o método GetById
        <Fact>
        Public Sub GetById_DeveRetornarLivroCorreto()
            ' Arrange
            Dim livroEsperado As New Livro With {
        .Codl = 1,
        .Titulo = "Livro 1",
        .Editora = "Editora A",
        .Edicao = 1,
        .AnoPublicacao = 2020,
        .Valor = 49.99D
    }

            Dim mockSet As New Mock(Of DbSet(Of Livro))()
            mockSet.Setup(Function(m) m.Find(1)).Returns(livroEsperado)

            Dim mockContext As New Mock(Of ControleLivrosContext)()
            mockContext.Setup(Function(c) c.Livros).Returns(mockSet.Object)

            Dim repository As New LivroRepository(mockContext.Object)

            ' Act
            Dim resultado = repository.GetById(1)

            ' Assert
            Assert.NotNull(resultado) ' Verifica que o resultado não é nulo.
            Assert.Equal(livroEsperado.Codl, resultado.Codl) ' Verifica que o Codl é o esperado.
            Assert.Equal(livroEsperado.Titulo, resultado.Titulo) ' Verifica que o título é o esperado.
            Assert.Equal(livroEsperado.Editora, resultado.Editora) ' Verifica que a editora é a esperada.
        End Sub

        ' Teste para o método Add
        <Fact>
        Public Sub Add_DeveAdicionarNovoLivro()
            ' Arrange
            Dim novoLivro As New Livro With {
        .Titulo = "Livro Teste",
        .Editora = "Editora Teste",
        .Edicao = 1,
        .AnoPublicacao = "2024",
        .Valor = 99.99D
    }

            Dim mockSet As New Mock(Of DbSet(Of Livro))()
            Dim mockContext As New Mock(Of ControleLivrosContext)()

            ' Simula a geração do Codl
            mockSet.Setup(Sub(m) m.Add(It.IsAny(Of Livro))) _
           .Callback(Of Livro)(Sub(l) l.Codl = 101) ' Gera um Codl fictício.

            mockContext.Setup(Function(c) c.Livros).Returns(mockSet.Object)

            Dim repository As New LivroRepository(mockContext.Object)

            ' Act
            repository.Add(novoLivro)

            ' Assert
            Assert.Equal(101, novoLivro.Codl) ' Verifica se o Codl foi gerado.
            mockSet.Verify(Sub(m) m.Add(It.IsAny(Of Livro)), Times.Once)
            mockContext.Verify(Sub(c) c.SaveChanges(), Times.Once)
        End Sub

        ' Teste para o método Update
        <Fact>
        Public Sub Update_DeveAtualizarLivro()
            ' Arrange
            Dim livroExistente As New Livro With {
        .Codl = 1,
        .Titulo = "Livro Antigo",
        .Editora = "Editora Antiga",
        .Edicao = 1,
        .AnoPublicacao = 2000,
        .Valor = 29.99D
    }

            Dim livroAtualizado As New Livro With {
        .Codl = 1,
        .Titulo = "Livro Atualizado",
        .Editora = "Editora Atualizada",
        .Edicao = 2,
        .AnoPublicacao = 2023,
        .Valor = 39.99D
    }

            Dim mockSet As New Mock(Of DbSet(Of Livro))()
            Dim mockContext As New Mock(Of ControleLivrosContext)()

            mockContext.Setup(Function(c) c.Livros).Returns(mockSet.Object)
            mockContext.Setup(Function(c) c.Entry(It.IsAny(Of Livro))) _
        .Returns(Mock.Of(Of System.Data.Entity.Infrastructure.DbEntityEntry(Of Livro)))

            Dim repository As New LivroRepository(mockContext.Object)

            ' Act
            repository.Update(livroAtualizado)

            ' Assert
            mockContext.Verify(Sub(c) c.Entry(It.Is(Of Livro)(Function(l) l.Codl = livroAtualizado.Codl)), Times.Once)
            mockContext.Verify(Sub(c) c.Entry(livroAtualizado).State = EntityState.Modified, Times.Once)
            mockContext.Verify(Sub(c) c.SaveChanges(), Times.Once)
        End Sub

        ' Teste para o método Delete
        <Fact>
        Public Sub Delete_DeveRemoverLivroPorId()
            ' Arrange
            Dim livroParaExcluir As New Livro With {
        .Codl = 1,
        .Titulo = "Livro Teste",
        .Editora = "Editora Teste",
        .Edicao = 1,
        .AnoPublicacao = "2024",
        .Valor = 99.99D
    }

            Dim mockSet As New Mock(Of DbSet(Of Livro))()
            mockSet.Setup(Function(m) m.Find(1)).Returns(livroParaExcluir)

            Dim mockContext As New Mock(Of ControleLivrosContext)()
            mockContext.Setup(Function(c) c.Livros).Returns(mockSet.Object)

            Dim repository As New LivroRepository(mockContext.Object)

            ' Act
            repository.Delete(1)

            ' Assert
            mockSet.Verify(Sub(m) m.Remove(livroParaExcluir), Times.Once)
            mockContext.Verify(Sub(c) c.SaveChanges(), Times.Once)
        End Sub

    End Class

End Namespace