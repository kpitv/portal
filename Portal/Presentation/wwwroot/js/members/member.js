$(document).ready(function () {
    $("form").on("submit", function () {
        //$(this).validate();
        createMember();
    });
});

function createMember() {

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
        .filter(function () { return $(this).val() !== ""; })
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
        about: $("#About").val()
    };

    $.ajax({
        url: "/Members/Create",
        method: "POST",
        data: member,
        success: function (data) {
            $("#formContainer").load(data);
            alert("TAK!");
        }
    });
}