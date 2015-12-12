(function (app) {
    app.controller('UpdateController', ['$scope', '$location', '$routeParams' ,'tutorService', function ($scope, $location, $routeParams ,tutorService) {
        var lectureId = $routeParams.id;
        

        $scope.doUpdate =  function () {

            var dataToUpdate =
            {
                file: $scope.ctrlBag.fileToUpload,
                name: $scope.ctrlBag.lectureName,
                subject: $scope.ctrlBag.lectureSubject
            };

            tutorService.updateLecture(dataToUpdate, lectureId)
            .success(function (data) {

            })
            .error(function (data) {

            });
        };



    }]);



}(angular.module('atTutorLectures')));