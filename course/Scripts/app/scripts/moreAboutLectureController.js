(function (app) {
    app.controller('aboutLectureController', ['$routeParams', '$scope', function ($routeParams, $scope) {
        var id = $routeParams.id;
        $scope.downloadfile = function () {
            window.open("/api/lectures/getfile/" + id, "_blank", "");
        };
    }]);

}(angular.module('atLectures')));