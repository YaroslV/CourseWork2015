(function (app) {
    app.controller('UpdateController', ['$scope', '$location', '$routeParams' ,'tutorService', function ($scope, $location, $routeParams ,tutorService) {
        var lectureId = $routeParams.id;
        $scope.ctrlBag = {
            lectureFileName: null,
            lectureId: null,
            lectureName: null
        };



        tutorService.getLectureById(lectureId)
            .success(function (data) {
                $scope.ctrlBag.lectureName = data.LectureText;
                $scope.ctrlBag.lectureSubject = data.Subject;
                $scope.ctrlBag.lectureFileName = data.FilePath;
                
            })
            .error(function (data) {

            })

        $scope.doUpdate =  function () {

            var dataToUpdate =
            {
                file: $scope.ctrlBag.fileToUpload,
                name: $scope.ctrlBag.lectureName,
                subject: $scope.ctrlBag.lectureSubject
            };

            tutorService.updateLecture(dataToUpdate, lectureId)
            .success(function (data) {
                $location.path('/list');
            })
            .error(function (data) {

            });
        };



    }]);
}(angular.module('atTutorLectures')));