@ModelType IEnumerable(Of ControleLivros.Models.ControleLivros.Models.Entities.RelatorioModel)

<h2>Lista Agrupada por Autor</h2>

<a href="@Url.Action("Index", "Livro")" class="btn btn-primary mb-3">
    <i class="fas fa-arrow-left"></i> Voltar para Livros
</a>

<form method="get" action="@Url.Action("Index", "Visao")" class="mb-3">
    <div class="form-group">
        <label for="nomeFiltro">Filtrar por Nome:</label>
        <input type="text" id="nomeFiltro" name="nomeFiltro" class="form-control" value="@Request.QueryString("nomeFiltro")" />
    </div>
    <button type="submit" class="btn btn-secondary">Filtrar</button>
</form>

<div class="table-responsive">
    <table class="table table-bordered table-striped">
        <thead class="thead-dark">
            <tr>
                <th>Nome</th>
                <th>Título</th>
                <th>Descrição</th>
            </tr>
        </thead>
        <tbody>
            @For Each item In Model
                @<tr>
                    <td>@Html.DisplayFor(Function(m) item.Nome)</td>
                    <td>@Html.DisplayFor(Function(m) item.Titulo)</td>
                    <td>@Html.DisplayFor(Function(m) item.Descricao)</td>
                </tr>
            Next
        </tbody>
    </table>
</div>
