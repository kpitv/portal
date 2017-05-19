$(document).ready(function () {
    initEvents();
});

function initEvents() {
    $(".remove-number-input-button").click(removeInput);
    $("#addNumberInputButton").click(addInput);

    function addInput() {
        var inputHtml = '<div><div class="form-group phone-number"><input type="tel" name="PhoneNumber" class="form-control" /><div class="btn glyphicon glyphicon-minus remove-number-input-button"></div><span class="text-danger"></span></div></div>';
        $(this).before(inputHtml);
        $(".remove-number-input-button:last").click(removeInput);
    }

    function removeInput() {
        if ($(".remove-number-input-button").length !== 1) {
            $(".remove-number-input-button").remove();
            $(this).parent().remove();
        }
    }
}

function createMember() {
    var member = collectMember();

    $.ajax({
        url: "/Members/Create",
        method: "POST",
        data: member,
        success: function (data) {
            $("#formContainer").html(data);
            initEvents();
        }
    });
}

function updateMember() {
    var member = collectMember();

    $.ajax({
        url: "/Members/Edit",
        method: "POST",
        data: member,
        success: function (data) {
            $("#formContainer").html(data);
            initEvents();
        }
    });
}

function collectMember() {
    var phoneNumbers = [];
    $("#phoneNumbers input").each(function () {
        phoneNumbers.push($(this).val());
    });
    var roles = [];
    $("#roles input").each(function () {
        roles.push({
            key: $(this).attr("data-role"),
            value: $(this).prop("checked")
        });
    });
    var contactLinks = [];
    $("#contactLinks input")
        .each(function () {
            contactLinks.push({
                key: $(this).attr("name"),
                value: $(this).val()
            });
        });

    var member = {
        firstNameInRussian: $("#FirstNameInRussian").val(),
        secondNameInRussian: $("#SecondNameInRussian").val(),
        lastNameInRussian: $("#LastNameInRussian").val(),
        firstNameInEnglish: $("#FirstNameInEnglish").val(),
        lastNameInEnglish: $("#LastNameInEnglish").val(),
        firstNameInUkrainian: $("#FirstNameInUkrainian").val(),
        secondNameInUkrainian: $("#SecondNameInUkrainian").val(),
        lastNameInUkrainian: $("#LastNameInUkrainian").val(),
        email: $("#Email").val(),
        phoneNumbers: phoneNumbers,
        roles: roles,
        contactLinks: contactLinks,
        about: $("#About").val(),
        id: $("#Id").val()
    };
    return member;
}