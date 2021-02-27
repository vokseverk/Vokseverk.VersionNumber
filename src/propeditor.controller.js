angular.module("umbraco").controller("VersionEditorController", function($scope, $element) {
	if ($scope.model.config == null) {
		$scope.model.config = {
			usePatch: 1
		}
	}
	
	if ($scope.model.value == "" || $scope.model.value == null) {
		$scope.model.value = {
			major: 1,
			minor: 0,
			patch: $scope.model.config.usePatch ? 0 : null
		}
	}
})
