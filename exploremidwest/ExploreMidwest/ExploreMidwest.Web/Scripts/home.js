var num = 5;
var set = 0;
var cap = 0;

$('#nextblog').click(function () {
    if (cap == 0) {
        set += 1;
    }
    loadData();
    updateArea();
})

$('#prev').click(function () {
    if (set > 0)
    {
        set--;
        cap = 0;
    }
    loadData();
    updateArea();
}) 

$(document).ready(function () {
    set = 0;
    loadData();
    updateArea();
});

function updateArea() {
    getNumber(num, set);
}

function loadData() {
    $('#BlogsArea').show();
    $('#details').hide();
    $('.next').show();
    $('.divTop').show();
    $('#editdelete').hide();
    $('#newCategory').hide();
    $('#reset').hide();
    newCategory();
    getPages();
}

$('#navigation').click(function () {
    var paramChangeBoxes = $('input:checkbox.change');
    if ($(this).is(':checked')) {
        paramChangeBoxes.attr('disabled', 'disabled');
        $('#finished').attr('disabled', 'disabled');
    }
    else {
        paramChangeBoxes.removeAttr('disabled');
        $('#finished').removeAttr('disabled');
    }
});

function getNumber(number, sets) {
    $.ajax({
        url: 'http://localhost:8080/blogs/' + number + '/' + sets,
        type: 'GET',
        success: function (blogs) {
            var output = "";
            var i = 0;

            //<img src="ExploreMidwest.Web/images/' + blogs[i].ImageLocation + '"

            if (blogs.length == 0) {
                output += '<img src="images/EMWLogo.PNG" width="100%" height="100%" alt="">'
            } else {

                for (i; i < blogs.length; i++) {
                    if (blogs[i].IsFinished) {
                        output += '<div class="col-xs-2" style="height:135px; margin-top:8px"><img src="' + blogs[i].ImageLocation + '" width="100%" height="100%" alt=""></div>'
                        output += '<div class="col-xs-10 blogDiv"><div class="col-xs-3"><div class="titleDiv"><h4>'
                        output += blogs[i].Title + '</h4></div><h5>'
                        output += blogs[i].BlogCategory.CategoryType + '</h5><br />'
                        output += blogs[i].Date.slice(0, 10) + '</div>'
                        output += '<div class="col-xs-9 innerDiv">'
                        output += blogs[i].Body
                        output += '</div><div style="text-align:end"><button type="button" class="btn btn-default" onclick="FullArticle(' + blogs[i].BlogId + ')">Read full article</button></div></div>'
                    }
                }
            }
            if (i < num - 1) {
                cap = 1;
            }
            $('#BlogsArea').html(output);
        },
        error: function (jqxhr, techstatus, errorthrow) {

        }
    })
}

function newCategory() {
    $('#Category_CategoryId').on('change', function () {
        if ($(this).val() == "0") {
            $('#newCategory').show();
        }
        else {
            $('#newCategory').hide();
        }
    });

    //$('#newCategory').hide();
}

function getPages() {
    $.ajax({
        url: 'http://localhost:8080/pages',
        type: 'GET',
        success: function (pages) {
            var output = "";
            var i = 0;

            for (i; i < pages.length; i++) {
                if (pages[i].IsInNavigation) {
                    output += '<li><a href="http://localhost:8080/Home/page/' + pages[i].PageId + '">'
                    output += pages[i].Title
                    output += '</a></li>'
                }
            }
            $('#staticpages').html(output);
        },
        error: function (jqxhr, techstatus, errorthrow) {

        }
    })
}

function search() {
    var para = $('#searchTerm').val();
    var type = $('#searchCategory').val();
    if (para[0] == '#') {
        para = para.substring(1);
    }
    para = para.replace(/\//g, '-')
    $('.next').hide();
    $.ajax({
        url: 'http://localhost:8080/blog/' + type + '/' + para,
        type: 'GET',
        success: function (blogs) {
            var output = "";
            var i = 0;

            for (i; i < blogs.length; i++) {
                if (blogs[i].IsFinished) {
                    output += '<div class="col-xs-2" style="height:135px; margin-top:8px"><img src="' + blogs[i].ImageLocation + '" width="100%" height="100%" alt=""></div>'
                    output += '<div class="col-xs-10 blogDiv"><div class="col-xs-3"><div class="titleDiv"><h4>'
                    output += blogs[i].Title + '</h4></div><h5>'
                    output += blogs[i].BlogCategory.CategoryType + '</h5><br />'
                    output += blogs[i].Date.slice(0, 10) + '</div>'
                    output += '<div class="col-xs-9 innerDiv">'
                    output += blogs[i].Body
                    output += '</div><div style="text-align:end"><button type="button" class="btn btn-default" onclick="FullArticle(' + blogs[i].BlogId + ')">Read full article</button></div></div>'
                }
            }
            $('#BlogsArea').html(output);
        },
        error: function (jqxhr, techstatus, errorthrow) {

        }
    })
}
function FullArticle(id) {
    $('#details').show("slow");
    $('#editdelete').show();
    $('#BlogsArea').hide();
    $('.next').hide();
    $('.divTop').hide();
    $('#reset').show();
    $.ajax({
        url: 'http://localhost:8080/blog/' + id,
        type: 'GET',
        success: function (blog) {
            var output = "";
            var editdelete = "";

            var i = 0;
            output += '<div class="col-xs-2" style="height:150px"><img src="' + blog.ImageLocation + '" width="100%" height="100%" alt=""></div>' 
            output += '<div class="col-xs-9 detailDiv"><h3>' + blog.Title
            output += '</h3 ><h4>' + blog.BlogCategory.CategoryType
            output += '</h4 ><h4>' + blog.Date.slice(0, 10)
            output += '</h4 ></div><div class="col-xs-9 detailDiv"><p>' + blog.Body
            output += '</p ></div><div class="col-xs-offset-2 col-xs-9 detailDiv"><p>'
            output += '</p ></div>'

            editdelete += '<a href="/Manager/EditBlog/' + id + '">Edit</a> | '
            editdelete += '<a href="/Admin/DeleteBlog/' + id + '">Delete</a>'

            $('#details').html(output);
            $('#editdelete').html(editdelete);
        },
        error: function (jqxhr, techstatus, errorthrow) {

        }
    })
}

$('#clearSearch').click(function(){
    $('#searchTerm').val("");
    $('#searchCategory').val("");
    set = 0;
    loadData();
    updateArea();
})
