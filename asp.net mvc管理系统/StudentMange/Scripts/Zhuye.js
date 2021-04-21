/////
////调试方法
////
$.get("http://localhost:62650/Student/School").then((data) => console.log(data));
var cid = 1;
var s = "http://localhost:62650/Student/College/" + cid;
$.get(s).then((data) => console.log(data));
$(document).click(function (e) {//获取点击的id
    var v_id = $(e.target).attr('id');
    console.log(v_id);
})
//下拉框三级联动
////////
////////
$.get("http://localhost:62650/Student/School").then((data) => {
    var ddl = $("#xx");

    var result = data;

    $(result).each(function (key) {

        var opt = $("<option></option>").text(result[key].name).val(result[key].id);
        ddl.append(opt);


    });


});
function changeschool() {
    $("#xy").empty();
    $("#xy").append('<option value="0" >请选择你的学院---</option>');
    $("#bj").empty();
    $("#bj").append('<option value="0" >请选择你的班级---</option>');

    console.log("1");
    var sid = $("#xx").val();
    var getstring = "http://localhost:62650/Student/College/" + sid;
    $.get(getstring).then((data) => {
        var result = data;
        var dd = $("#xy");
        $(result).each(function (key) {

            var opt = $("<option></option>").text(result[key].name).val(result[key].id);
            dd.append(opt);


        });

    });


}
function changcollege() {
    $("#bj").empty();
    $("#bj").append('<option value="0" >请选择你的班级---</option>');


    var sid = $("#xy").val();
    var getstring = "http://localhost:62650/Student/Class/" + sid;
    $.get(getstring).then((data) => {
        var result = data;
        var dd = $("#bj");
        $(result).each(function (key) {

            var opt = $("<option></option>").text(result[key].name).val(result[key].id);
            dd.append(opt);


        });

    });


}
//
////////
////////
////////各种查询办法
function SearchAll() {

    $('#dg').datagrid('load', {
        searchstr: $("#textquery").val()

    })

}//搜索查询方法
function Searchclass() {

    $('#dg').datagrid('load', {
        searchstr: $("#bj").val()

    })

}//下拉框查询方法

$(document).ready(function getdata() {
    $('#dg').datagrid({

        url: 'http://localhost:62650/Student/Dgraid',

        methord: 'post',

        fitColumns: true,
        sortName: 'Id',
        sortOrder: 'desc',
        idField: 'Id',
        pageSize: 5,
        pageList: [5, 10, 20, 30, 40],
        pagination: true,
        striped: true, //奇偶行是否区分
        singleSelect: true,//单选模式
        rownumbers: true,//行号
        toolbar: '#tb',
        onClickRow: function () {
           /* var selected = $('#dg').datagrid('getSelected');
            if (!selected) {
                return;//为防止意外情况可以选择加上此判断
            }

            var id = selected.Id;
            console.log(id);
            //var str1 = "/Student/Delete/" + id;
            var str2 = "/Student/Edit/" + id;
            //$('#remove').attr('href', str1);
            $('#edit').attr('href', str2);*/
        },

        columns: [[
            { field: 'Id', title: 'ID', width: 100 },
            { field: 'Name', title: '姓名', width: 100 },
            { field: 'Myclass', title: '班级', width: 100 },
            { field: 'Chinesescore', title: '语文', width: 100 },
            { field: 'Mathscore', title: '数学', width: 100 },
            { field: 'Englishscore', title: '英语', width: 100 },
            { field: 'Computerscore', title: '计算机', width: 100, align: 'right' }

        ]]


    });
    var p = $('#dg').datagrid('getPager');//刷新页面所有数据
    if (p) {
        $(p).pagination({ //设置分页功能栏
            //分页功能可以通过Pagination的事件调用后台分页功能来实现
            onRefresh: function () {
                $('#dg').datagrid('load', {
                    searchstr: ""
                })
            }
        });

    }


})//表格数据加载和刷新 数据删除
////////////
//////////////
/////////////删除时的消息提示
function isOk() {
    var selected = $('#dg').datagrid('getSelected');
    if (selected) {
        $.messager.confirm('信息提示', '确定要删除该信息吗?', function (r) {
            if (!r) {
                return;
            }
            var id = selected.Id;
            var url = "/Student/Delete/" + id;
            $.get(url, {}, function (result) {
                if ("success" == result) {
                    $.messager.alert("信息提示", "删除成功！");
                    $("#dg").datagrid('reload');
                }
                else {
                    $.messager.alert("信息提示", result);
                }
            })
        })
    }
    else {
        $.messager.alert("信息提示", "请先选中要删除的信息！");
    }
}
//////
/////////
////////新增和编辑的窗体以及提示消息
function frameReturnByClose() {
    $("#modalwindow").window('close');
}
//iframe 返回并刷新
function frameReturnByReload(flag) {
    if (flag)
        $("#dg").datagrid('load');
    else
        $("#dg").datagrid('reload');
}
//输出信息
function frameReturnByMes(mes) {
    $.messageBox5s('提示', mes);
}
$("#append").click(function () {
    $("#modalwindow").html("<iframe width='100%' height='98%' scrolling='yes' frameborder='0'' src='/Student/Creat'></iframe>");
    $("#modalwindow").window({ title: '新增', width: 700, height: 400, iconCls: 'icon-add' }).window('open');
});
$("#edit").click(function () {
    var row = $('#dg').datagrid('getSelected');
    if (row != null) {
        $("#modalwindow").html("<iframe width='100%' height='99%'  frameborder='0' src='/Student/Edit?id=" + row.Id + "'></iframe>");
        $("#modalwindow").window({ title: '编辑', width: 700, height: 430, iconCls: 'icon-edit' }).window('open');
    } else { $.messageBox5s('提示', '请选择要操作的记录'); }
});
/////
//////
////////////
///菜单栏的生成
///////////
/*$('#tt').tree({
     url: '/Student/GetTree'
        });

$('#tt').tree({
    onClick: function (node) {
        //alert(node.text);  // alert node text property when clicked
        // add a new tab panel   这里使用exits判断tab是否已经存在
        if ($('#tabs').tabs('exists', node.text)) {
            $('#tabs').tabs('select', node.text);
        }
        else {
            $('#tabs').tabs('add', {
                title: node.text,
                href: node.attributes,
                closable: true,
            });
        }
    }
});*/
 