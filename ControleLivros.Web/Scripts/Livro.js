

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