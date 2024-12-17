Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Contexts
Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Models.Entities

Namespace ControleLivros.Data.Context
    Public Class ControleLivrosContext
        Inherits DbContext

        Public Sub New()
            MyBase.New("name=ControleLivrosContext")
        End Sub

        ' Construtor adicional para aceitar uma conexão DbConnection
        Public Sub New(connection As DbConnection)
            MyBase.New(connection, contextOwnsConnection:=True)
        End Sub

        Public Overridable Property Livros As DbSet(Of Livro)

        Public Property Autores As DbSet(Of Autor)
        Public Property LivroAutores As DbSet(Of LivroAutor)
        Public Property Assuntos As DbSet(Of Assunto)
        Public Property LivroAssuntos As DbSet(Of LivroAssunto)

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Entity(Of Livro)().
                HasMany(Function(l) l.Autores).
                WithMany(Function(a) a.Livros).
                Map(Sub(m)
                        m.ToTable("Livro_Autor")
                        m.MapLeftKey("Livro_Codl")
                        m.MapRightKey("Autor_CodAu")
                    End Sub)

            modelBuilder.Entity(Of Livro)().
                HasMany(Function(l) l.Assuntos).
                WithMany(Function(a) a.Livros).
                Map(Sub(m)
                        m.ToTable("Livro_Assunto")
                        m.MapLeftKey("Livro_Codl")
                        m.MapRightKey("Assunto_codAs")
                    End Sub)
            modelBuilder.Configurations.Add(New RelatorioModelConfiguration())
        End Sub

        Public Class RelatorioModelConfiguration
            Inherits EntityTypeConfiguration(Of RelatorioModel)

            Public Sub New()
                Me.HasKey(Function(r) New With {r.Nome, r.Titulo, r.Descricao})
                Me.ToTable("Relatorio")
            End Sub
        End Class

    End Class
End Namespace
