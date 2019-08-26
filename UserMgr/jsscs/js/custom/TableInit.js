window.onload = function () {
    //初始化表格
    var Table_Url = new TableInit_Url();
    Table_Url.Init();
    var Table_UserGroup = new TableInit_UserGroup();
    Table_UserGroup.Init();

    //一些控件事件
    $("#AddUserGroup").click(function () {
        window.location.href = "/AddEntity/UserGroup";
    });
};



//URL
var TableInit_Url = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserAccessMgr').bootstrapTable('destroy');
        //设置表格数据
        $('#UserAccessMgr').bootstrapTable({
            url: '/API/TableData/UrlMgr',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick(row);
            },
            columns: [
                {
                    field: 'PageName',     //数据键
                    title: '页面名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PageUrl',     //数据键
                    title: '页面URL',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PageClass',     //数据键
                    title: '可访问用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'BlackList',     //数据键
                    title: '访问黑名单',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WhiteList',     //数据键
                    title: '访问白名单',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents,
                    formatter: operateFormatter,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.Id);
        window.location.href = "/Home/UrlAccessDetail?Id=" + row.Id;
    };


    //按钮定义
    function operateFormatter(value, row, index) {
        console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnChooseUser" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDeleteUser" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents = {
        'click #btnChooseUser': function (e, value, row, index) {
            window.location.href = "/Home/UserRole?Id=" + row.Id;
        },
        'click #btnDeleteUser': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    function Notiy(msg, type) {
        var notiy = "<div class='alert alert-" + type + " alert-dismissable'><button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" + msg + "</div>";

        $("#notiy").html(notiy);
    };

    return TableInit;
};

//用户组
var TableInit_UserGroup = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserGroupMgr').bootstrapTable('destroy');
        //设置表格数据
        $('#UserGroupMgr').bootstrapTable({
            url: '/API/TableData/uGroupMgr',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick(row);
            },
            columns: [
                {
                    field: 'UserGroupID',     //数据键
                    title: '用户组ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupName',     //数据键
                    title: '名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupCode',     //数据键
                    title: '编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupClass',     //数据键
                    title: '访问权限等级',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MenuGroupCode',     //数据键
                    title: '编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupDesc',     //数据键
                    title: '描述',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupCreater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupCreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    //formatter: DateFormatter
                }, {
                    field: '',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents,
                    formatter: operateFormatter,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.Id);
        window.location.href = "/Home/UrlAccessDetail?Id=" + row.Id;
    };


    //按钮定义
    function operateFormatter(value, row, index) {
        console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnChooseUser" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDeleteUser" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents = {
        'click #btnChooseUser': function (e, value, row, index) {
            window.location.href = "/Home/UserRole?Id=" + row.Id;
        },
        'click #btnDeleteUser': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    function Notiy(msg, type) {
        var notiy = "<div class='alert alert-" + type + " alert-dismissable'><button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" + msg + "</div>";

        $("#notiy").html(notiy);
    };

    return TableInit;
};



