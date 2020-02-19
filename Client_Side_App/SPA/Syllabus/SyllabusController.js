App.controller("SyllabusController", function ($scope, $http, Upload, $timeout) {
    $scope.Syllabus_GetDynamic = function () {
        var query = "1=1";
        if ($scope.ddlsearchTradeId != undefined && $scope.ddlsearchTradeId != null && $scope.ddlsearchTradeId != '') {
            query += " and slb.[TradeId] =" + $scope.ddlsearchTradeId.TradeId + " ";
        }
        if ($scope.ddlsearchLevelId != undefined && $scope.ddlsearchLevelId != null && $scope.ddlsearchLevelId != '') {
            query += " and slb.[LevelId] =" + $scope.ddlsearchLevelId.LevelId + " ";
        }

        $http({
            url: "http://localhost:25329/api/Syllabus/Syllabus_GetDynamic?WhereCondition=" + query,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
        }).success(function (data) {
            $scope.SyllabusList = JSON.parse(data.SyllabusList);
        });
    }
    load();
    function load() {
        $scope.Syllabus = { SyllabusId: 0, lstSyllabusDetails:[] };
        $scope.ddlTrade = { TradeId: 0 };
        $scope.Savebtnlbl = 'Save';
        $http({
            url: "http://localhost:25329/api/Syllabus/GetAllLanguageTradeLevelList",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
        }).success(function (data) {
            if (data != 'False') {
                $scope.LanguageList = JSON.parse( data.LanguageList);
                $scope.TradeList = JSON.parse( data.TradeList);
                $scope.LevelList = JSON.parse(data.LevelList);
            }
        }).error(function(err){
            console.log(err)
        });

        $scope.Syllabus_GetDynamic();
    }

    $scope.UploadSylebusFiles = function (files) {
        $scope.SelectedFiles = files;
        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
            Upload.upload({
                url: "http://localhost:25329/api/Syllabus/UploadFiles",
                data: {
                    files: $scope.SelectedFiles
                }
            }).then(function (response) {
               // $timeout(function () {
                //$scope.Result = response.data;
                if (response.data != "File Extension Problem") {
                    $scope.Syllabus.SyllabusFileName = $scope.SelectedFiles[0].name;
                    $scope.Syllabus.SyllabusFileLocName = response.data;
                }
                else {
                    $scope.Syllabus.SyllabusFileName ='';
                    $scope.Syllabus.SyllabusFileLocName = '';
                    alert(response.data);
                }
               // });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            }, function (evt) {
           //     alert(JSON.parse( evt));
           //     var element = angular.element(document.querySelector('#dvProgress'));
             //       $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            //    element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
            });
        }
    }

    $scope.UploadTestplanFiles = function (files) {
        $scope.SelectedFiles = files;
        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
            Upload.upload({
                url: "http://localhost:25329/api/Syllabus/UploadFiles",
                data: {
                    files: $scope.SelectedFiles
                }
            }).then(function (response) {
                if (response.data != "File Extension Problem") {
                    $scope.Syllabus.TestplanFileName = $scope.SelectedFiles[0].name;
                    $scope.Syllabus.TestplanFileLocName = response.data;
                }
                else {
                    $scope.Syllabus.TestplanFileName ='';
                    $scope.Syllabus.TestplanFileLocName = '';
                    alert(response.data);
                }
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            }, function (evt) {
            });
        }
    }

    $scope.Save = function () {
        $scope.Syllabus.lstSyllabusDetails = Enumerable.From($scope.LanguageList).Where("$.Checked ==true").ToArray()

        if ($scope.Syllabus.SyllabusName == undefined || $scope.Syllabus.SyllabusName == null) {
            alertify.error('Enter Syllabus Name');
        } else if ($scope.ddlTrade == undefined || $scope.ddlTrade == null || $scope.ddlTrade.TradeId == 0) {
            alertify.error('Select Trade');
        } else if ($scope.ddlLevel == undefined || $scope.ddlLevel == null || $scope.ddlLevel.LevelId == 0) {
            alertify.error('Select Level');
        } else if ($scope.Syllabus.lstSyllabusDetails.length == 0) {
            alertify.error('Select Language');
        } else if ($scope.Syllabus.DevelopmentOfficer == undefined || $scope.Syllabus.DevelopmentOfficer == null) {
            alertify.error('Enter Development Officer');
        } else if ($scope.Syllabus.Manager == undefined || $scope.Syllabus.Manager == null) {
            alertify.error('Enter Manager');
        } else if ($scope.Syllabus.ActiveDate == undefined || $scope.Syllabus.ActiveDate == null) {
            alertify.error('Enter Active Date');
        }
        else {
            $scope.Syllabus.TradeId=$scope.ddlTrade.TradeId;
            $scope.Syllabus.LevelId = $scope.ddlLevel.LevelId;
            $.ajax({
                type: "POST",
                url: "http://localhost:25329/api/Syllabus/Add",
                data: '=' +JSON.stringify($scope.Syllabus),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
                success: function (data) {
                    if (data) {
                        alertify.success('Saved Successfully');
                        //load();
                      //  $scope.Books_GetDynamic();
                    } else { alertify.error('Failed, Try again'); }
                },
                error: function (data) {
                    alertify.error('Failed, Try again');
                }
            });
        }
    }
});