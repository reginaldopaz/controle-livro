

CREATE DATABASE ControlesLivroDB

GO

USE ControlesLivroDB
GO

CREATE TABLE Livro (
    Codl INT PRIMARY KEY IDENTITY,
    Titulo VARCHAR(40) NOT NULL,
    Editora VARCHAR(40),
    Edicao INT,
    AnoPublicacao VARCHAR(4),
    Valor DECIMAL(18, 2),
	[DataPublicacao] [date] NULL,
);
GO
CREATE TABLE Autor (
    CodAu INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(40) NOT NULL
);
GO
CREATE TABLE Livro_Autor (
    Livro_Codl INT NOT NULL,
    Autor_CodAu INT NOT NULL,
    PRIMARY KEY (Livro_Codl, Autor_CodAu),
    FOREIGN KEY (Livro_Codl) REFERENCES Livro(Codl),
    FOREIGN KEY (Autor_CodAu) REFERENCES Autor(CodAu)
);

CREATE INDEX Livro_autor_FKIndex1 ON Livro_Autor (Livro_Codl);
CREATE INDEX Autor_CodAu_FKIndex2 ON Livro_Autor (Autor_CodAu);
GO
CREATE TABLE Assunto (
    CodAs INT PRIMARY KEY IDENTITY,
    Descricao VARCHAR(20) NOT NULL
);
GO
CREATE TABLE Livro_Assunto (
    Livro_Codl INT NOT NULL,
    Assunto_codAs INT NOT NULL,
    PRIMARY KEY (Livro_Codl, Assunto_codAs),
    FOREIGN KEY (Livro_Codl) REFERENCES Livro(Codl),
    FOREIGN KEY (Assunto_codAs) REFERENCES Assunto(CodAs)
);

CREATE INDEX Livro_Assunto_FKIndex1 ON Livro_Assunto (Livro_Codl);
CREATE INDEX Livro_Assunto_FKIndex2 ON Livro_Assunto (Assunto_codAs);

GO
CREATE VIEW [dbo].[Relatorio] AS
SELECT A.Nome,L.Titulo, S.Descricao
FROM Livro L
JOIN Livro_Autor LA ON L.Codl = LA.Livro_Codl
JOIN Autor A ON A.CodAu = LA.Autor_CodAu
JOIN Livro_Assunto LS ON L.Codl = LS.Livro_Codl
JOIN Assunto S ON S.CodAs = LS.Assunto_CodAs
GROUP BY A.CodAu, L.Titulo, A.Nome, S.Descricao