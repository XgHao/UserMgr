window.onload = function () {
    //获取当前请求动作-加载对应表格
    var url = window.location.pathname;
    var action = "TableInit_" + url.substring(url.lastIndexOf('/') + 1, url.length);
    var func = this.eval(action);
    try {
        new func().Init();
    } catch{}
};



//URL
var TableInit_AccessMgr = function () {
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
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UAM,  //传递参数
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
                Dbclick_UAM(row);
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
                    events: operateEvents_UAM,
                    formatter: operateFormatter_UAM,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UAM = function (params) {
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
    Dbclick_UAM = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_UAM(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UAM" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_UAM = {
        'click #btnEdit_UAM': function (e, value, row, index) {
            window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
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

    return TableInit;
};

//用户组
var TableInit_UserGroupMgr = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserGroupMgr').bootstrapTable('destroy');
        //设置表格数据
        $('#UserGroupMgr').bootstrapTable({
            url: '/API/TableData/UGroupMgr',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UGP,  //传递参数
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
                Dbclick_UGP(row);
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
                    field: 'UserGroupNo',     //数据键
                    title: '编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupClass',     //数据键
                    title: '访问权限等级',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupDesc',     //数据键
                    title: '描述',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: '',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_UGP,
                    formatter: operateFormatter_UGP,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UGP = function (params) {
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
    Dbclick_UGP = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(row);
        //console.log(row["PageID"]);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/UserGroup?Id=" + row.UserGroupID;
    };


    //按钮定义
    function operateFormatter_UGP(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UGP" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelete_UGP" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_UGP = {
        'click #btnEdit_UGP': function (e, value, row, index) {
            window.location.href = "/EditEntity/UserGroup?Id=" + row.UserGroupID;
        },
        'click #btnDelete_UGP': function (e, value, row, index) {
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

    return TableInit;
};

//审核用户列表
var TableInit_CheckUser = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#CheckUser').bootstrapTable('destroy');
        //设置表格数据
        $('#CheckUser').bootstrapTable({
            url: '/API/TableData/CheckUserMgr',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_CU,  //传递参数
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
                Dbclick_CU(row);
            },
            columns: [
                {
                    field: 'UserID',     //数据键
                    title: '用户ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'UserGroupName',     //数据键
                    title: '所属用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserName',     //数据键
                    title: '用户名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserNo',     //数据键
                    title: '用户编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserPhoneNum',     //数据键
                    title: '手机号码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserEmail',     //数据键
                    title: '注册邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserDesc',     //数据键
                    title: '用户说明',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserCode',     //数据键
                    title: '用户编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_CU,
                    formatter: operateFormatter_CU,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_CU = function (params) {
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
    Dbclick_CU = function (row) {
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_CU(value, row, index) {
        return [
            //'<div class="btn-group">',
            '<button id="btnYES_CU" title="审核" class="btn btn-success btn-circle" singleSelected=true>',
            '<i class="fa fa-check"></i>',
            '</button>',
            '<button id="btnNO_CU" title="拒绝" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-close"></i>',
            '</button>',
            //'</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_CU = {
        'click #btnYES_CU': function (e, value, row, index) {
            //审核通过
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/API/AJAX/CheckUser",
                data: {
                    "UserID": row['UserID']
                },
                error: function (msg) {
                    alert("操作失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#CheckUser').bootstrapTable('remove', {
                            field: 'UserID',
                            values: [row.UserID]
                        });
                        Notiy("用户 " + row['UserName'] + " 已通过审核", "success");
                    }
                    else if (res == "Error") {
                        Notiy("审核失败", "danger");
                    } else {
                        Notiy("你没有权限执行该操作", "warning");
                    }
                }
            });
        },
        'click #btnNO_CU': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/API/AJAX/DenyUser",
                data: {
                    "UserID": row['UserID']
                },
                error: function (msg) {
                    alert("操作失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#CheckUser').bootstrapTable('remove', {
                            field: 'UserID',
                            values: [row.UserID]
                        });
                        Notiy("用户 " + row['UserName'] + " 注册请求已被拒绝", "success");
                    }
                    else if (res == "Error") {
                        Notiy("拒绝失败", "danger");
                    }
                    else {
                        Notiy("你没有权限执行该操作", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//用户列表
var TableInit_UserList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserList').bootstrapTable('destroy');
        //设置表格数据
        $('#UserList').bootstrapTable({
            url: '/API/TableData/UserList',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UL,  //传递参数
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
                Dbclick_UL(row);
            },
            columns: [
                {
                    field: 'UserID',     //数据键
                    title: '用户ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'UserName',     //数据键
                    title: '用户名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserNo',     //数据键
                    title: '用户编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupName',     //数据键
                    title: '所属用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserDesc',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserPhoneNum',     //数据键
                    title: '手机号码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserEmail',     //数据键
                    title: '注册邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserCode',     //数据键
                    title: '用户编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'IsChecked',     //数据键
                    title: '是否通过审核',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    formatter: operateFormatter_ULCheck,
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_UL,
                    formatter: operateFormatter_UL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UL = function (params) {
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
    Dbclick_UL = function (row) {
        window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
    };


    //按钮定义
    function operateFormatter_UL(value, row, index) {
        return [
            '<button id="btnEdit_UL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            //'</div>'
        ].join('');
    };

    //用户审核状态
    function operateFormatter_ULCheck(value, row, index) {
        if (row.IsChecked) {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-check text-success"></i>',
                '</span>',
            ].join('');
        }
        else {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-close text-danger"></i>',
                '</span>',
            ].join('');
        }
    }

    //按钮事件定义
    window.operateEvents_UL = {
        'click #btnEdit_UL': function (e, value, row, index) {
            if (row.IsChecked) {
                window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
            }
            else {
                Notiy("用户还未审核", "warning");
            }
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
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

    return TableInit;
};

//供应商
var TableInit_Supplier = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#SupplierList').bootstrapTable('destroy');
        //设置表格数据
        $('#SupplierList').bootstrapTable({
            url: '/API/TableData/Supplier',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_SL,  //传递参数
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
                Dbclick_SL(row);
            },
            columns: [
                {
                    field: 'SupplierID',     //数据键
                    title: '供应商ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierNo',     //数据键
                    title: '供应商编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierName',     //数据键
                    title: '供应商名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierPhoNum',     //数据键
                    title: '联系方式',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierEmail',     //数据键
                    title: '电子邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierRemark',     //数据键
                    title: '备注信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_SL,
                    formatter: operateFormatter_SL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_SL = function (params) {
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
    Dbclick_SL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
    };

    //按钮定义
    function operateFormatter_SL(value, row, index) {
        //console.log(row);
        return [
            '<button id="btnEdit_SL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnTrash_SL" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_SL = {
        'click #btnEdit_SL': function (e, value, row, index) {
            window.location.href = "/EditEntity/Supplier?Id=" + row.SupplierID;
        },
        'click #btnTrash_SL': function (e, value, row, index) {
            if (confirm("要删除 [" + row.SupplierName + "] 吗?")) {
                //移除该项
                $.ajax({
                    type: "POST",
                    dataType: "text",
                    url: "/API/AJAX/DeleteSupplier",
                    data: {
                        "SupplierID": row.SupplierID
                    },
                    error: function (msg) {
                        alert("删除失败，错误原因：" + msg);
                    },
                    success: function (res) {
                        if (res == "OK") {
                            $('#SupplierList').bootstrapTable('remove', {
                                field: 'SupplierID',
                                values: [row.SupplierID]
                            });
                            Notiy("供应商" + row.SupplierName + "已删除", "succedd");
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
        }
    };

    return TableInit;
};

//物资种类
var TableInit_MaterialsType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#MaterialsType').bootstrapTable('destroy');
        //设置表格数据
        $('#MaterialsType').bootstrapTable({
            url: '/API/TableData/MaterialsType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_MT,  //传递参数
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
                Dbclick_MT(row);
            },
            columns: [
                {
                    field: 'MaterialTypeID',     //数据键
                    title: '物资种类ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'MaterialTypeNo',     //数据键
                    title: '物资种类编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypeName',     //数据键
                    title: '物资种类名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypeRoot',     //数据键
                    title: '父类',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Supplier',     //数据键
                    title: '供应商',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypePrice',     //数据键
                    title: '价格',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_MT,
                    formatter: operateFormatter_MT,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_MT = function (params) {
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
    Dbclick_MT = function (row) {
        window.location.href = "/EditEntity/MaterialType?Id=" + row.MaterialTypeID;
    };


    //按钮定义
    function operateFormatter_MT(value, row, index) {
        return [
            '<button id="btnEdit_MT" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnTrash_MT" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_MT = {
        'click #btnEdit_MT': function (e, value, row, index) {
            window.location.href = "/EditEntity/MaterialType?Id=" + row.MaterialTypeID;
        },
        'click #btnTrash_MT': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/API/AJAX/DeleteMaterialType",
                data: {
                    "MaterialTypeID": row.MaterialTypeID
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#MaterialsType').bootstrapTable('remove', {
                            field: 'MaterialTypeID',
                            values: [row.MaterialTypeID]
                        });
                        Notiy("物资种类 " + row['MaterialTypeName'] + " 删除成功", "success");
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

    return TableInit;
};

//所有物资列表
var TableInit_MaterialList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#MaterialList').bootstrapTable('destroy');
        //设置表格数据
        $('#MaterialList').bootstrapTable({
            url: '/API/TableData/MaterialList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_ML,  //传递参数
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
                Dbclick_ML(row);
            },
            columns: [
                {
                    field: 'MaterialSizeID',     //数据键
                    title: '物资规格ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'MaterialType',     //数据键
                    title: '物资种类',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SizeCode',     //数据键
                    title: '规格代码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Detail',     //数据键
                    title: '详细描述',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Unit',     //数据键
                    title: '单位',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'LWH',     //数据键
                    title: '长(mm)*宽(mm)*高(mm)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Weight',     //数据键
                    title: '质量(g)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialDensity',     //数据键
                    title: '密度(g/(mm^3))',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialModel',     //数据键
                    title: '物资型号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ParcelUnit',     //数据键
                    title: '小件单位',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ParcelMeasure',     //数据键
                    title: '小件计量',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialMin',     //数据键
                    title: '物料最低值',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialMax',     //数据键
                    title: '物料最高值',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ContainerName',     //数据键
                    title: '容器',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'IsCKD',     //数据键
                    title: '是否组合商品',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_ML,
                    formatter: operateFormatter_ML,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_ML = function (params) {
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
    Dbclick_ML = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_ML(value, row, index) {
        //console.log(row);
        return [
            //'<div class="btn-group">',
            '<button id="btnEdit_UAM" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            //'</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_ML = {
        'click #btnEdit_UAM': function (e, value, row, index) {
            window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
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

    return TableInit;
};

//仓库列表
var TableInit_WarehouseList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#WarehouseList').bootstrapTable('destroy');
        //设置表格数据
        $('#WarehouseList').bootstrapTable({
            url: '/API/TableData/WarehouseList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_WL,  //传递参数
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
                Dbclick_WL(row);
            },
            columns: [
                {
                    field: 'WarehouseID',     //数据键
                    title: '仓库ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'WarehouseNo',     //数据键
                    title: '仓库编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WarehouseName',     //数据键
                    title: '仓库名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Enable',     //数据键
                    title: '是否启用',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    formatter: operateFormatter_WLEnable,
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WarehouseType',     //数据键
                    title: '仓库类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OtherInfo',     //数据键
                    title: '其他信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_WL,
                    formatter: operateFormatter_WL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_WL = function (params) {
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
    Dbclick_WL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        console.log(row);
        console.log(row.WarehouseID);
        window.location.href = "/EditEntity/Warehouse?Id=" + row.WarehouseID;
    };


    //按钮定义
    function operateFormatter_WL(value, row, index) {
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_WL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };


    //是否开放状态
    function operateFormatter_WLEnable(value, row, index) {
        if (row.Enable) {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-check text-success"></i>',
                '</span>',
            ].join('');
        }
        else {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-close text-danger"></i>',
                '</span>',
            ].join('');
        }
    }


    //按钮事件定义
    window.operateEvents_WL = {
        'click #btnEdit_WL': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/EditEntity/Warehouse?Id=" + row.WarehouseID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
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

    return TableInit;
};

//库区列表
var TableInit_InventoryArea = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#InventoryAreaList').bootstrapTable('destroy');
        //设置表格数据
        $('#InventoryAreaList').bootstrapTable({
            url: '/API/TableData/InventoryAreaList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_IA,  //传递参数
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
                Dbclick_IA(row);
            },
            columns: [
                {
                    field: 'InventoryAreaID',     //数据键
                    title: '库区ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'InventoryAreaName',     //数据键
                    title: '库区名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryAreaNo',     //数据键
                    title: '库区编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryAreaType',     //数据键
                    title: '库区类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Warehouse',     //数据键
                    title: '仓库名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Enable',     //数据键
                    title: '是否开放',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    formatter: operateFormatter_IAEnable,
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OtherInfo',     //数据键
                    title: '其他信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_IA,
                    formatter: operateFormatter_IA,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_IA = function (params) {
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
    Dbclick_IA = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        console.log(row);
        console.log(row.WarehouseID);
        window.location.href = "/EditEntity/InventoryArea?Id=" + row.InventoryAreaID;
    };


    //按钮定义
    function operateFormatter_IA(value, row, index) {
        return [
            //'<div class="btn-group">',
            '<button id="btnEdit_IA" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            //'</div>'
        ].join('');
    };

    //是否开放状态
    function operateFormatter_IAEnable(value, row, index) {
        if (row.Enable) {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-check text-success"></i>',
                '</span>',
            ].join('');
        }
        else {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-close text-danger"></i>',
                '</span>',
            ].join('');
        }
    }

    //按钮事件定义
    window.operateEvents_IA = {
        'click #btnEdit_IA': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/EditEntity/InventoryArea?Id=" + row.InventoryAreaID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
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

    return TableInit;
};

//库位列表
var TableInit_InventoryLocation = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#InventoryLocationList').bootstrapTable('destroy');
        //设置表格数据
        $('#InventoryLocationList').bootstrapTable({
            url: '/API/TableData/InventoryLocationList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_IL,  //传递参数
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
                Dbclick_IL(row);
            },
            columns: [
                {
                    field: 'InventoryLocationID',     //数据键
                    title: '库位ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'InventoryLocationName',     //数据键
                    title: '库位名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryLocationNo',     //数据键
                    title: '库位编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryLocationType',     //数据键
                    title: '库位类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'LWH',     //数据键
                    title: '长(mm) * 宽(mm) * 高(mm)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PLC',     //数据键
                    title: '排 - 列 - 层',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryArea',     //数据键
                    title: '库区',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryLocationGroupName',     //数据键
                    title: '库位分组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'NarrowName',     //数据键
                    title: '库区巷道',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'EnterDistance',     //数据键
                    title: '入口距离',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ExitDistance',     //数据键
                    title: '出口距离',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'FrontAndBack',     //数据键
                    title: '前后',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ContainerName',     //数据键
                    title: '容器',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Enable',     //数据键
                    title: '是否开放',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    formatter: operateFormatter_ILEnable,
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_IL,
                    formatter: operateFormatter_IL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_IL = function (params) {
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
    Dbclick_IL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        console.log(row);
        console.log(row.WarehouseID);
        window.location.href = "/AddEntity/InventoryLocation?Id=" + row.InventoryLocationID;
    };


    //按钮定义
    function operateFormatter_IL(value, row, index) {
        return [
            //'<div class="btn-group">',
            '<button id="btnEdit_IA" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnAllocation_IA" class="btn btn-success btn-circle" singleSelected=true>',
            '<i class="fa fa-inbox"></i>',
            '</button>',
            //'</div>'
        ].join('');
    };


    //是否开放状态
    function operateFormatter_ILEnable(value, row, index) {
        if (row.Enable) {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-check text-success"></i>',
                '</span>',
            ].join('');
        }
        else {
            return [
                '<span class="fa-stack fa-lg" aria-hidden="true">',
                '<i class="fa fa-close text-danger"></i>',
                '</span>',
            ].join('');
        }
    }


    //按钮事件定义
    window.operateEvents_IL = {
        'click #btnEdit_IA': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/EditEntity/InventoryLocation?Id=" + row.InventoryLocationID;
        },
        'click #btnAllocation_IA': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/AddEntity/InventoryAllocation?Id=" + row.InventoryLocationID;
        }
    };

    return TableInit;
};

//托盘列表
var TableInit_Tray = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#TrayList').bootstrapTable('destroy');
        //设置表格数据
        $('#TrayList').bootstrapTable({
            url: '/API/TableData/Tray',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_TR,  //传递参数
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
                Dbclick_TR(row);
            },
            columns: [
                {
                    field: 'TrayID',     //数据键
                    title: '托盘ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'TrayType',     //数据键
                    title: '托盘类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'TrayNo',     //数据键
                    title: '托盘编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'TrayCode',     //数据键
                    title: '托盘条码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ContainerName',     //数据键
                    title: '容器',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Weight',     //数据键
                    title: '重量',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Height',     //数据键
                    title: '高度',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'StatusName',     //数据键
                    title: '状态',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InboundTime',     //数据键
                    title: '入库时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTimr',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'TrayDetailID',     //数据键
                    title: '托盘细节单',    //列名
                    sortable: false,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_TR,
                    formatter: operateFormatter_TR,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_TR = function (params) {
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
    Dbclick_TR = function (row) {
        window.location.href = "/AddEntity/TrayDetail?tdid=" + row.TrayID;
    };


    //按钮定义
    function operateFormatter_TR(value, row, index) {
        //根据是否存在托盘细节单，返回不同的按钮
        if (row.TrayDetailID == null) {
            return [
                '<button id="btnEdit_TR" class="btn btn-info btn-circle" singleSelected=true>',
                '<i class="fa fa-pencil"></i>',
                '</button>',
                '<button id="btnDetail_TR" class="btn btn-warning btn-circle" title="增加托盘细节" singleSelected=true>',
                '<i class="fa fa-share"></i>',
                '</button>',
            ].join('');
        }
        else {
            return [
                '<button id="btnEdit_TR" class="btn btn-info btn-circle" singleSelected=true>',
                '<i class="fa fa-pencil"></i>',
                '</button>',
                '<button id="btnDetail_TR" class="btn btn-success btn-circle" title="查看托盘细节" singleSelected=true>',
                '<i class="fa fa-eye"></i>',
                '</button>',
            ].join('');
        }
    };

    //按钮事件定义
    window.operateEvents_TR = {
        'click #btnEdit_TR': function (e, value, row, index) {
            window.location.href = "/EditEntity/Tray?Id=" + row.TrayID;
        },
        'click #btnDetail_TR': function (e, value, row, index) {
            window.location.href = (row.TrayDetailID == null ? "/AddEntity/TrayDetail?tdid=" : "Warehouse/TrayDetail?tdid=") + row.TrayID;
        }
    };

    return TableInit;
};

//托盘明细
var TableInit_TrayDetail = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#TrayDetail').bootstrapTable('destroy');
        //设置表格数据
        $('#TrayDetail').bootstrapTable({
            url: '/API/TableData/TrayDetail',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_TE,  //传递参数
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
                Dbclick_TE(row);
            },
            columns: [
                {
                    field: 'TrayDetailID',     //数据键
                    title: '托盘明细ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'TrayNo',     //数据键
                    title: '托盘编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'TrayCode',     //数据键
                    title: '托盘条码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'TrayCode',     //数据键
                    title: '托盘条码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialSizeInfo',     //数据键
                    title: '物资规格信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InboundTaskNo',     //数据键
                    title: '入库任务明细单编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'GroupTrayOrder',     //数据键
                    title: '组盘顺序',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ParcelMeasure',     //数据键
                    title: '小件数量',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ContainerName',     //数据键
                    title: '容器',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Weight',     //数据键
                    title: '重量',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialSN',     //数据键
                    title: '物料SN',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialDateVersion',     //数据键
                    title: '物资数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InboundPostMark',     //数据键
                    title: '入库过账标识',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OutboundPostMark',     //数据键
                    title: '出库过账标识',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ProductionDate',     //数据键
                    title: '生产日期',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'StatusName',     //数据键
                    title: '状态',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTimr',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_TE,
                    formatter: operateFormatter_TE,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_TE = function (params) {
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
    Dbclick_TR = function (row) {
        window.location.href = "/EditEntity/Tray?Id=" + row.TrayID;
    };


    //添加按钮
    function operateFormatter_TE(value, row, index) {
        return [
            '<button id="btnEdit_TR" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_TR" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_TE = {
        'click #btnEdit_TR': function (e, value, row, index) {
            window.location.href = "/EditEntity/TrayDetail?Id=" + row.TrayDetailID;
        },
        'click #btnRefresh_TR': function (e, value, row, index) {
        }
    };

    return TableInit;
};

//库存清单
var TableInit_InventoryList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#InventoryList').bootstrapTable('destroy');
        //设置表格数据
        $('#InventoryList').bootstrapTable({
            url: '/API/TableData/InventoryList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_IL,  //传递参数
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
                Dbclick_IL(row);
            },
            columns: [
                {
                    field: 'InventoryListID',     //数据键
                    title: '库存清单ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'InboundTaskNo',     //数据键
                    title: '入库任务单编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OutboundTaskNo',     //数据键
                    title: '出库任务单编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryType',     //数据键
                    title: '库存类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'TrayNoAndCode',     //数据键
                    title: '托盘编号 & 编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryLocationNo',     //数据键
                    title: '库位编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'StatusName',     //数据键
                    title: '状态',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTimr',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_IL,
                    formatter: operateFormatter_IL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_IL = function (params) {
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
    Dbclick_IL = function (row) {
        window.location.href = "/EditEntity/InventoryList?Id=" + row.InventoryListID;
    };


    //添加按钮
    function operateFormatter_IL(value, row, index) {
        return [
            '<button id="btnEdit_TR" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_TR" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_IL = {
        'click #btnEdit_TR': function (e, value, row, index) {
            window.location.href = "/EditEntity/InventoryList?Id=" + row.InventoryListID;
        },
        'click #btnRefresh_TR': function (e, value, row, index) {
        }
    };

    return TableInit;
};

//波次单
var TableInit_WavePicking = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#WavePicking').bootstrapTable('destroy');
        //设置表格数据
        $('#WavePicking').bootstrapTable({
            url: '/API/TableData/WavePicking',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_WP,  //传递参数
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
                Dbclick_WP(row);
            },
            columns: [
                {
                    field: 'WavePickingID',     //数据键
                    title: '波次ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'WavePickingNo',     //数据键
                    title: '波次编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WavePickingTypeName',     //数据键
                    title: '波次类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PickingName',     //数据键
                    title: '拣货类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'StatusName',     //数据键
                    title: '状态',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTimr',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'WavePickingDetailID',     //数据键
                    title: '波次明细ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_WP,
                    formatter: operateFormatter_WP,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_WP = function (params) {
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
    Dbclick_WP = function (row) {
        window.location.href = "/EditEntity/WavePicking?Id=" + row.WavePickingID;
    };


    //添加按钮
    function operateFormatter_WP(value, row, index) {
        //根据是否存在波次明细，显示不同的按钮
        if (row.WavePickingDetailID == null) {
            return [
                '<button id="btnEdit_WP" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
                '<i class="fa fa-pencil"></i>',
                '</button>',
                '<button id="btnDetail_WP" class="btn btn-warning btn-circle" title="完善波次细节" singleSelected=true>',
                '<i class="fa fa-share"></i>',
                '</button>',
            ].join('');
        } else {
            return [
                '<button id="btnEdit_WP" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
                '<i class="fa fa-pencil"></i>',
                '</button>',
                '<button id="btnDetail_WP" class="btn btn-success btn-circle" title="查看波次细节" singleSelected=true>',
                '<i class="fa fa-eye"></i>',
                '</button>',
            ].join('');
        }
        
    };

    //按钮事件定义
    window.operateEvents_WP = {
        'click #btnEdit_WP': function (e, value, row, index) {
            window.location.href = "/EditEntity/WavePicking?Id=" + row.WavePickingID;
        },
        'click #btnDetail_WP': function (e, value, row, index) {
            window.location.href = (row.WavePickingDetailID == null ? "/AddEntity/WavePickingDetail?id=" : "Warehouse/WavePickingDetail?id=") + row.WavePickingID;  
        }
    };

    return TableInit;
};


//
//==============================基础资料========================
//


//基础资料-入库类型
var TableInit_InboundType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDInboundType').bootstrapTable('destroy');
        //设置表格数据
        $('#BDInboundType').bootstrapTable({
            url: '/API/TableData/BDInboundType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_ITBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_ITBD(row);
            },
            columns: [
                {
                    field: 'InboundTypeID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'InboundTypeName',     //数据键
                    title: '入库类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_ITBD,
                    formatter: operateFormatter_ITBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_ITBD = function (params) {
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
    Dbclick_ITBD = function (row) {
        EditEntity(row.InboundTypeID, "InboundType");
    };


    //添加按钮
    function operateFormatter_ITBD(value, row, index) {
        return [
            '<button id="btnEdit_ITBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_ITBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_ITBD = {
        'click #btnEdit_ITBD': function (e, value, row, index) {
            EditEntity(row.InboundTypeID, "InboundType");
        },
        'click #btnDelate_ITBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.InboundTypeName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeleteInboundType",
                        data: {
                            "InboundTypeID": row['InboundTypeID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDInboundType').bootstrapTable('remove', {
                                    field: 'InboundTypeID',
                                    values: [row.InboundTypeID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};

//基础资料-出库类型
var TableInit_OutboundType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDOutboundType').bootstrapTable('destroy');
        //设置表格数据
        $('#BDOutboundType').bootstrapTable({
            url: '/API/TableData/BDOutboundType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_OTBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_OTBD(row);
            },
            columns: [
                {
                    field: 'OutboundTypeID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'OutboundTypeName',     //数据键
                    title: '入库类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_OTBD,
                    formatter: operateFormatter_OTBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_OTBD = function (params) {
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
    Dbclick_OTBD = function (row) {
        EditEntity(row.OutboundTypeID, "OutboundType");
    };


    //添加按钮
    function operateFormatter_OTBD(value, row, index) {
        return [
            '<button id="btnEdit_OTBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_OTBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_OTBD = {
        'click #btnEdit_OTBD': function (e, value, row, index) {
            EditEntity(row.OutboundTypeID, "OutboundType");
        },
        'click #btnDelate_OTBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.OutboundTypeName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeleteOutboundType",
                        data: {
                            "OutboundTypeID": row['OutboundTypeID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDOutboundType').bootstrapTable('remove', {
                                    field: 'OutboundTypeID',
                                    values: [row.OutboundTypeID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};

//基础资料-容器
var TableInit_Container = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDContainer').bootstrapTable('destroy');
        //设置表格数据
        $('#BDContainer').bootstrapTable({
            url: '/API/TableData/BDContainer',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_CBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_CBD(row);
            },
            columns: [
                {
                    field: 'ContainerID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ContainerName',     //数据键
                    title: '容器名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_CBD,
                    formatter: operateFormatter_CBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_CBD = function (params) {
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
    Dbclick_CBD = function (row) {
        EditEntity(row.ContainerID, "Container");
    };


    //添加按钮
    function operateFormatter_CBD(value, row, index) {
        return [
            '<button id="btnEdit_CBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_CBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_CBD = {
        'click #btnEdit_CBD': function (e, value, row, index) {
            EditEntity(row.ContainerID, "Container");
        },
        'click #btnDelate_CBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.ContainerName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeleteContainer",
                        data: {
                            "ContainerID": row['ContainerID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDContainer').bootstrapTable('remove', {
                                    field: 'ContainerID',
                                    values: [row.ContainerID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};

//基础资料-巷道
var TableInit_Narrow = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDNarrow').bootstrapTable('destroy');
        //设置表格数据
        $('#BDNarrow').bootstrapTable({
            url: '/API/TableData/BDNarrow',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_NBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_NBD(row);
            },
            columns: [
                {
                    field: 'NarrowID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'NarrowName',     //数据键
                    title: '巷道名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_NBD,
                    formatter: operateFormatter_NBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_NBD = function (params) {
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
    Dbclick_NBD = function (row) {
        EditEntity(row.NarrowID, "Narrow");
    };


    //添加按钮
    function operateFormatter_NBD(value, row, index) {
        return [
            '<button id="btnEdit_NBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_NBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_NBD = {
        'click #btnEdit_NBD': function (e, value, row, index) {
            EditEntity(row.NarrowID, "Narrow");
        },
        'click #btnDelate_NBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.NarrowName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeleteNarrow",
                        data: {
                            "NarrowID": row['NarrowID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDNarrow').bootstrapTable('remove', {
                                    field: 'NarrowID',
                                    values: [row.NarrowID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};

//基础资料-拣货类型
var TableInit_PickingType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDPickingType').bootstrapTable('destroy');
        //设置表格数据
        $('#BDPickingType').bootstrapTable({
            url: '/API/TableData/BDPickingType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_PTBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_PTBD(row);
            },
            columns: [
                {
                    field: 'PickingTypeID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'PickingTypeName',     //数据键
                    title: '拣货类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_PTBD,
                    formatter: operateFormatter_PTBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_PTBD = function (params) {
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
    Dbclick_PTBD = function (row) {
        EditEntity(row.PickingTypeID, "PickingType");
    };


    //添加按钮
    function operateFormatter_PTBD(value, row, index) {
        return [
            '<button id="btnEdit_PTBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_PTBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_PTBD = {
        'click #btnEdit_PTBD': function (e, value, row, index) {
            EditEntity(row.PickingTypeID, "PickingType");
        },
        'click #btnDelate_PTBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.PickingTypeName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeletePickingType",
                        data: {
                            "PickingTypeID": row['PickingTypeID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDPickingType').bootstrapTable('remove', {
                                    field: 'PickingTypeID',
                                    values: [row.PickingTypeID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};

//基础资料-销售类型
var TableInit_SaleType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#BDSaleType').bootstrapTable('destroy');
        //设置表格数据
        $('#BDSaleType').bootstrapTable({
            url: '/API/TableData/BDSaleType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: '[10, 25, 50, All]',    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_STBD,  //传递参数
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
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_STBD(row);
            },
            columns: [
                {
                    field: 'SaleTypeID',     //数据键
                    title: 'ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'SaleTypeName',     //数据键
                    title: '销售类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreaterName',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangerName',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_STBD,
                    formatter: operateFormatter_STBD,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_STBD = function (params) {
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
    Dbclick_STBD = function (row) {
        EditEntity(row.SaleTypeID, "SaleType");
    };


    //添加按钮
    function operateFormatter_STBD(value, row, index) {
        return [
            '<button id="btnEdit_STBD" class="btn btn-info btn-circle" title="编辑" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelate_STBD" class="btn btn-danger btn-circle" title="删除" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_STBD = {
        'click #btnEdit_STBD': function (e, value, row, index) {
            EditEntity(row.SaleTypeID, "SaleType");
        },
        'click #btnDelate_STBD': function (e, value, row, index) {
            layer.confirm("确定删除 [" + row.SaleTypeName + "] 这一项吗？",
                {
                    btn: ['是的', '我再想想']
                },
                function () {
                    //移除该项
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/API/AJAX/DeleteSaleType",
                        data: {
                            "SaleTypeID": row['SaleTypeID']
                        },
                        error: function (msg) {
                            layer.msg('请求失败' + msg, { shade: 0.3 });
                        },
                        success: function (res) {
                            layer.msg(res, { shade: 0.3 });
                            if (res == "删除成功") {
                                $('#BDSaleType').bootstrapTable('remove', {
                                    field: 'SaleTypeID',
                                    values: [row.SaleTypeID]
                                });
                            }
                        }
                    });
                },
                function () {
                });
        }
    };

    return TableInit;
};






//通知消息
function Notiy(msg, type) {
    var notiy = "<div class='alert alert-" + type + " alert-dismissable'><button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" + msg + "</div>";

    $("#notiy").html(notiy);
};