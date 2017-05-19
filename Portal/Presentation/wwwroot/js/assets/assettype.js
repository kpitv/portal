function AddProperty() {
    $("#assetTypeProperties").append('<div><div class="form-group"></div></div>');
    $("#assetTypeProperties .form-group:last").append('<input class="form-control"><button class="btn btn-default removeProperty glyphicon glyphicon-remove" type="button"></button>');
    $("#assetTypeProperties .form-group:last .removeProperty").click(RemoveProperty);
}

function SaveAssetType(route) {
    var assetType = {
        Id: $("#Id").val(),
        Name: $("#assetTypeName > input").val(),
        Properties: []
    };
    $("#assetTypeProperties input").each(function () {
        assetType.Properties.push($(this).val());
    });
    $.ajax({
        url: "/Assets/" + route + "Type",
        type: "post",
        data: assetType,
        success: function (data) {
            $("#createAssetTypeForm").html(data);
            initEvents();
        }
    });
}

function RemoveProperty() {
    $(this).parent().remove();
}

function CreateAssetType() {
    SaveAssetType("Create");
}

function EditAssetType() {
    SaveAssetType("Edit");
}

var editProperties = {
    editName: function () {
        var name = $(this).prev().val();

        $.ajax({
            url: "/Assets/EditName",
            type: "post",
            data: {
                assetTypeId: $("#Id").val(),
                name: name
            }
        });
    },
    renameProperty: function () {
        var url = "";
        if ($(this).attr("data-type") === "add")
            addProperty();
        else {
            var newName = $(this).prev().val();
            var property = $(this).prev().attr("data-old-name");
            $.ajax({
                url: "/Assets/RenameProperty",
                type: "post",
                data: {
                    assetTypeId: $("#Id").val(),
                    property: property,
                    newName: newName
                }
            });
        }
        $(this).attr("data-old-name", newName);
    },
    removeProperty: function () {
        var property = $(this).prev().prev().val();

        $.ajax({
            url: "/Assets/RemoveProperty",
            type: "post",
            data: {
                assetTypeId: $("#Id").val(),
                property: property
            }
        });
        RemoveProperty();
    },
    addProperty: function () {
        var property = $(this).prev().val();
        $.ajax({
            url: "/Assets/AddProperty",
            type: "post",
            data: {
                assetTypeId: $("#Id").val(),
                property: property
            }
        });
        $(this).attr("data-type", "edit");
    },
    addPropertyInput: function () {
        $("#assetTypeProperties").append('<div class="form-group">');
        $("#assetTypeProperties .form-group:last").append('<input class="form-control"><button data-type="add" class="btn btn-default renameProperty glyphicon glyphicon-ok"></button><button class="btn btn-default removeProperty glyphicon glyphicon-remove" type="button"></button>');
        $("#assetTypeProperties .form-group:last .removeProperty").click(RemoveProperty);
    }
}

$(document).ready(function () {
    initEvents();
    $("#createAssetType").click(CreateAssetType);
    $("#editAssetType").click(EditAssetType);
});

function initEvents() {
    $("#addPropertyButton").click(AddProperty);
    $("#createAssetTypeForm .removeProperty").click(RemoveProperty);
    $("#editAssetTypeForm .renameProperty").click(editProperties.renameProperty);
    $("#editAssetTypeForm .removeProperty").click(editProperties.removeProperty);
    $("#editAssetTypeForm .addProperty").click(editProperties.addPropertyInput);
    $("#editAssetTypeForm .editName").click(editProperties.editName);
}