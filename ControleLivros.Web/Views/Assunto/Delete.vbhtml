@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Assunto

@Code
    ViewData("Title") = "Excluir Assunto"
End Code

<h2>Excluir Assunto</h2>

@Html.ValidationSummary(True, "", New With {.class = "text-danger"})

<h3>Tem certeza de que deseja excluir este assunto?</h3>
<div>
    <h4>Assunto</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>@Html.DisplayNameFor(Function(model) model.CodAs)</dt>
        <dd>@Html.DisplayFor(Function(model) model.CodAs)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Descricao)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Descricao)</dd>
    </dl>
</div>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<div class="form-group">
    <input type = "submit" value="Excluir" class="btn btn-danger" />
    <a href = "@Url.Action("Index")" class="btn btn-secondary">Cancelar</a>
</div>
End Using

<div>
    <a href="@Url.Action("Index", "Assunto")" class="btn btn-secondary">
        <i class="fas fa-arrow-left"></i> Voltar à Lista
    </a>
</div>
