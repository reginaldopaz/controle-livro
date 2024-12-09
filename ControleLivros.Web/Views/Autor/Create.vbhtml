@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Autor

@Code
    ViewData("Title") = "Criar Novo Autor"
End Code

<h2>@ViewData("Title")</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4> Autor</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Nome, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Nome, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Nome, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Salvar" class="btn btn-primary" />
                <a href="@Url.Action("Index", "Autor")" class="btn btn-secondary"> 
                <i class="fa fa-arrow-left"></i> Voltar </a>
                               
            </div>
        </div>
    </div>
    End Using
