(function () {
    var app = angular.module('atTutorLectures', ['ngRoute']);

    app.directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function() {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };        
    }]);

    app.factory('tutorService', ['$http', function ($http) {
        var uploadNewLecture = function (formData) {
            return $http({
                method: 'POST',
                url: '/api/lectures/addlecture',
                data: formData,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: function (data) {
                    var fd = new FormData();
                    fd.append("subject", data.subject);
                    fd.append("name", data.name);
                    fd.append("file", data.file);

                    return fd;
                }
            });
        };

        var getTutorLectures = function () {
            return $http.get('/api/lectures/all/tutor');
        };

        return {
            uploadLecture: uploadNewLecture,
            getTutorLectures: getTutorLectures
        };

    }]);

    var config = function ($routeProvider) {
        $routeProvider
        .when('/list',
        { templateUrl: '/Scripts/app/views/tutorLectures.html' })
        .when('/add',
        { templateUrl: '/Scripts/app/views/addNewLecture.html' })
        .otherwise(
        { redirectTo: '/list'});
    };

    app.config(config);

    //*********************************
    //          CONTROLLER
    //*********************************
    app.controller('TutorController', ['$scope', '$location', 'tutorService', function ($scope, $location, tutorService) {
        function checkFile(file) {
            //TODO checkfile
        };

        $scope.publishLecture = function () {            
            
            var dataToUpload = 
            {
                file: $scope.ctrlBag.fileToUpload,
                name: $scope.ctrlBag.lectureName,
                subject: $scope.ctrlBag.lectureSubject
            };
            tutorService.uploadLecture(dataToUpload)
                .success(function (data) {
                    //TODO
                    //file upload congratulations
                })
                .error(function (data) {
                    //TODO
                    //uploading failure alert
                });
        
            $location.path('/list')
        };

        tutorService.getTutorLectures()
                        .success(function (data) {
                            $scope.allTutorLectures = data;
                        })
                        .error(function (data) {
                            $scope.errormsg = "Проблеми із з'єднанням";
                        });
                        

    }]);

}());