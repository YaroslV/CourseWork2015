(function (app) {
    app.controller("StudentController", ["$http", "$scope", 'lectureService', function ($http, $scope, lectureService) {

        var lectureStorage;
        $scope.lectures;
        $scope.itemsperpage = 3;
        $scope.allDisciplines;
        
        $scope.orderby = 0;

        $scope.test = "2323232";
        
        var page = 1;
        var itemsPerPage = 3;
        var orderBy = 0;



        lectureService.getAllDisciplines()
            .success(function (data) {
                $scope.allDisciplines = data;
            })
            .error(function (data) {
            });
        

        $scope.doSearch = function () {
            //service for search 
            //I need service which will consider tutor, discipline and search text
        };

        $scope.doFilter = function (selected) {
            var filteredLectures = [];
            for (var i = 0; i < lectureStorage.length; i++) {
                if (lectureStorage[i].Subject == selected)
                    filteredLectures.push(lectureStorage[i]);
            }
            $scope.lectures = filteredLectures;
        };

        $scope.moreLectures = function () {
            page = page + 1;
            lectureService.getLectures(page, itemsPerPage, orderBy)
                .success(function (data) {
                    var currLectures = $scope.lectures;                    
                    $scope.lectures = currLectures.concat(data);
                }).error(function (data){

                });
        };
        
        lectureService.getLectures(page,itemsPerPage, orderBy)
                .success(function (data) {
                    lectureStorage = data;
                    $scope.lectures = lectureStorage;
                }).error(function (data) {
                    $scope.error = "Can't connect tot server";
                });
    }]);


}(angular.module("atLectures")));