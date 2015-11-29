(function (app) {
    app.controller('AdminController',['lectureService','$scope', function (lectureService, $scope) {
        $scope.ctrlData = {requests: null};




        function loadRequests() {
            lectureService.getTutorRequests()
                .success(function (data) {
                    $scope.ctrlData.requests = data;
                }).error(function (data) {
                    $scope.ctrlData.err_msg = "Проблеми із з'єднанням";
                });
        };

        loadRequests();

        $scope.activateTutor = function (tutorId) {
            lectureService.activateTutor(tutorId)
                .success(function () {
                    loadRequests();
                })
                .error(function (data) {
                    $scope.ctrlData.err_msg = "Не вдалось підтведити викладача через проблема із з'єднанням";
                });
        };
        
    }]);
}(angular.module('atLectures')));