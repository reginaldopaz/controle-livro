@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Cadastrar Livro"
End Code

<h2>@ViewData("Title")</h2>

<div id="mensagem" class="alert alert-success" style="display:none;">
    Livro salvo com sucesso! Agora você pode adicionar autores e assuntos.
</div>

@Using Html.BeginForm("Create", "Livro", FormMethod.Post, New With {.id = "livroForm"})

    @Html.AntiForgeryToken()
    @<input type="hidden" id="livroId" name="livroId" value="@ViewBag.LivroId" />

    @<div class="form-horizontal">
        <h4>Livro</h4>
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
                    @Html.LabelFor(Function(model) model.DataPublicacao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.DataPublicacao, New With {.htmlAttributes = New With {.class = "form-control", .type = "date"}})
                    @Html.ValidationMessageFor(Function(model) model.DataPublicacao, "", New With {.class = "text-danger"})
                </td>

            </tr>
        </table>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <button type="submit" class="btn btn-primary">
                    <i class="fa fa-save"></i> Salvar
                </button>

                <a href="@Url.Action("Index", "Livro")" class="btn btn-secondary">
                    <i class="fa fa-list"></i> Voltar
                </a>
            </div>
        </div>
    </div>

End Using

<div id="addSection" style="display:@(If(ViewBag.ShowAddSection, "block", "none"));">
    <h4>Adicionar Autor e Assunto</h4>

    @Using Html.BeginForm("AddAuthorAssunto", "Livro", FormMethod.Post, New With {.id = "addForm"})

        @Html.AntiForgeryToken()

        @<input type="hidden" id="livroId" name="livroId" value="@ViewBag.LivroId" />

        @<div class="form-group">
            <table>
                <tr>
                    <td style="padding-right: 10px; width: 250px;">
                        <div>
                            @Html.DropDownList("selectedAuthor", New SelectList(DirectCast(ViewBag.Autores, IEnumerable(Of SelectListItem)), "Value", "Text"), "Selecione um Autor", New With {.class = "form-control"})
                        </div>
                    </td>
                    <td style="padding-right: 10px; width: 250px;">
                        <div>
                            @Html.DropDownList("selectedAssunto", New SelectList(DirectCast(ViewBag.Assuntos, IEnumerable(Of SelectListItem)), "Value", "Text"), "Selecione um Assunto", New With {.class = "form-control"})
                        </div>
                    </td>
                    <td>
                        <button type="button" class="btn btn-secondary" id="adicionar">
                            <i class="fa fa-plus"></i> Adicionar
                        </button>
                    </td>
                </tr>
            </table>
        </div>

    End Using

    <div id="gridViewSection">

        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>Autor</th>
                    <th>Assunto</th>
                </tr>
            </thead>
            <tbody id="gridViewBody">
                @* Linhas serão preenchidas dinamicamente *@
            </tbody>
        </table>
    </div>

</div>

@section Scripts
    
    <script>

             $(document).ready(function () {
                if ($.fn.inputmask) {
                    $('#valor').inputmask({
                        alias: 'decimal',
                        groupSeparator: '.',
                        radixPoint: ',',
                        autoGroup: true,
                        digits: 2,
                        digitsOptional: false,
                        placeholder: '0',
                        removeMaskOnSubmit: true,
                        rightAlign: false,
                        showMaskOnHover: false,
                        showMaskOnFocus: true
                    });
                } else {
                    console.error("Inputmask não está disponível.");
                }

                // Ajuste o método de validação de número para aceitar vírgula
                if ($.validator && $.validator.methods) {
                    $.validator.methods.number = function (value, element) {
                        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)?(?:,\d+)?$/.test(value);
                    };
                }

                // Configuração global para adicionar o token antifalsificação a todas as requisições POST
                $.ajaxSetup({
                    beforeSend: function (xhr, settings) {
                        if (settings.type === 'POST') {
                            var token = $('input[name="__RequestVerificationToken"]').val();
                            xhr.setRequestHeader('RequestVerificationToken', token);
                        }
                    }
                });

                // Ação para salvar o livro
                $('#salvarLivro').click(function (e) {
                    e.preventDefault();
                    $.ajax({
                        type: 'POST',
                        url: '@Url.Action("Create", "Livro")',
                        data: $('#livroForm').serialize(),
                        success: function (response) {
                            if (response.success) {
                                $('#mensagem').show();
                                $('#addSection').show();
                                $('#livroId').val(response.livroId); // Armazena o ID do livro salvo
                                $('#salvarLivro').prop('disabled', true);
                            } else {
                                alert(response.message || 'Erro ao salvar o livro.');
                            }
                        },
                        error: function () {
                            alert('Erro ao tentar salvar o livro.');
                        }
                    });
                });

                // Ação para adicionar autor e assunto
                $('#adicionar').click(function (e) {
                    e.preventDefault();
                    var token = $('input[name="__RequestVerificationToken"]').val();
                    $.ajax({
                        type: 'POST',
                        url: '@Url.Action("AddAuthorAssunto", "Livro")',
                        data: {
                            __RequestVerificationToken: token, // Inclua o token no data
                            livroId: $('#livroId').val(),
                            selectedAuthor: $('#selectedAuthor').val(),
                            selectedAssunto: $('#selectedAssunto').val()
                        },
                        success: function (response) {
                            if (response.success) {
                                $('#gridViewBody').append('<tr><td>' + response.autorNome + '</td><td>' + response.assuntoDescricao + '</td></tr>');
                            } else {
                                alert(response.message || 'Erro ao adicionar autor e assunto aqui.');
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            alert('Erro ao tentar adicionar autor e assunto Erro: ' + textStatus + ' - ' + errorThrown);
                            console.log(jqXHR.responseText);
                        }
                    });
                });
            });
        </script>

    <script type="text/javascript">

    </script>
End Section

