angular.module("umbraco").controller("VersionEditorController", function($scope, $element) {
	if ($scope.model.config == null) {
		$scope.model.config = {
			useMinor: true,
			usePatch: true
		}
	}
	
	if ($scope.model.value == null) {
		$scope.model.value = {
			major: 1,
			minor: $scope.model.config.useMinor ? 0 : null,
			patch: ($scope.model.config.useMinor && $scope.model.config.usePatch) ? 0 : null
		}
	}
})
