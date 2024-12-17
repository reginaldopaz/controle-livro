@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Editar Livro"
End Code

<h2>Editar Livro</h2>

@Using Html.BeginForm("Edit", "Livro", FormMethod.Post, New With {.Codl = Model.Codl})
    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(model) model.Codl)
    @<div class="form-horizontal">
        
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <table class="table">
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.Titulo, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Titulo, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Titulo, "", New With {.class = "text-danger"})
                </td>
                <td>
                    @Html.LabelFor(Function(model) model.Editora, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Editora, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Editora, "", New With {.class = "text-danger"})
                </td>
                <td>
                    @Html.LabelFor(Function(model) model.Edicao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Edicao, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Edicao, "", New With {.class = "text-danger"})
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.AnoPublicacao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.AnoPublicacao, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.AnoPublicacao, "", New With {.class = "text-danger"})
                </td>
                <td>
                    @Html.LabelFor(Function(model) model.Valor, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Valor, New With {.htmlAttributes = New With {.class = "form-control", .id = "valor", .data_val = "false", .data_val_number = "false"}})
                    @Html.ValidationMessageFor(Function(model) model.Valor, "", New With {.class = "text-danger"})
                </td>
                <td>
                <td>
                    @Html.LabelFor(Function(model) model.DataPublicacao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DataPublicacao, New With {
                        .htmlAttributes = New With {
                            .class = "form-control",
                            .type = "date",
                            .value = If(Model.DataPublicacao.HasValue, Model.DataPublicacao.Value.ToString("yyyy-MM-dd"), String.Empty)
                        }
                    })
                    @Html.ValidationMessageFor(Function(model) model.DataPublicacao, "", New With {.class = "text-danger"})
                </td>

                </td>
            </tr>
        </table>

        <!-- Adicionar controles para seleção de Autores e Assuntos -->
        @Code
            Dim autores = If(TryCast(ViewBag.AutoresAssociados, List(Of String)), New List(Of String)())
            Dim assuntos = If(TryCast(ViewBag.AssuntosAssociados, List(Of String)), New List(Of String)())
            Dim maxRows = Math.Max(autores.Count, assuntos.Count)
        End Code

        <h4>Autores e Assuntos Associados</h4>
        <table class="table">
            <thead>
                <tr>
                    <th>Autores</th>
                    <th>Assuntos</th>
                </tr>
            </thead>
            <tbody>
                @For i As Integer = 0 To Math.Max(DirectCast(ViewBag.AutoresAssociados, List(Of String)).Count, DirectCast(ViewBag.AssuntosAssociados, List(Of String)).Count) - 1
                    @<tr>
                        <td>
                            @Html.DropDownList(
                              "selectedAuthors",
                              New SelectList(
                                  DirectCast(ViewBag.Autores, IEnumerable(Of SelectListItem)),
                                  "Value",
                                  "Text",
                                  If(i < DirectCast(ViewBag.AutoresAssociados, List(Of String)).Count, DirectCast(ViewBag.AutoresAssociados, List(Of String))(i), "")
                              ),
                              "Selecione um Autor",
                              New With {.class = "form-control"}
                          )

                        </td>
                        <td>
                            @Html.DropDownList(
                                "selectedAssuntos",
                                New SelectList(
                                    DirectCast(ViewBag.Assuntos, IEnumerable(Of SelectListItem)),
                                    "Value",
                                    "Text",
                                    If(DirectCast(ViewBag.AssuntosAssociados, List(Of String)).Any(), DirectCast(ViewBag.AssuntosAssociados, List(Of String))(0), "")
                                ),
                                "Selecione um Assunto",
                                New With {.class = "form-control"}
                            )
                        </td>

                    </tr>


                Next

            </tbody>

        </table>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Salvar" class="btn btn-primary" />
                @Html.ActionLink("Cancelar", "Index", Nothing, New With {.class = "btn btn-default"})
            </div>
        </div>

    </div>

    @<div Class="form-group d-flex align-items-center">
        <div class="me-2">
            <select id="selectedAuthor" class="form-control">
                <option value="">Selecione um Autor</option>
                @For Each author As SelectListItem In ViewBag.Autores
                    @<option value="@author.Value">@author.Text</option>
                Next
            </select>
        </div>

        <div class="me-2">
            <select id="selectedAssunto" class="form-control">
                <option value="">Selecione um Assunto</option>
                @For Each assunto As SelectListItem In ViewBag.Assuntos
                    @<option value="@assunto.Value">@assunto.Text</option>
                Next
            </select>
        </div>

        <button type="button" class="btn btn-secondary" id="adicionar">
            <i class="fa fa-plus"></i> Adicionar
        </button>

    </div>


End Using

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
<script>

       $(document).ready(function () {
    $('#adicionar').click(function (e) {
        e.preventDefault();
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("AddAuthorAssunto", "Livro")',
            data: {
                __RequestVerificationToken: token,
                livroId: $('input[name="Codl"]').val(),
                selectedAuthor: $('#selectedAuthor').val(),
                selectedAssunto: $('#selectedAssunto').val()
            },
            success: function (response) {
                if (response.success) {
                    // Redireciona para a mesma página para recarregar dados
                    location.reload();
                } else {
                    alert(response.message || 'Erro ao adicionar autor e assunto.');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Erro ao tentar adicionar autor e assunto: ' + textStatus + ' - ' + errorThrown);
                console.log(jqXHR.responseText);
            }
        });
    });
});

</script>

    } end section
