angular.module("umbraco").controller("VersionEditorController", function($scope, $element) {
	if (!$scope.model.config) {
		$scope.model.config = {
			usePatch: 1
		}
	}
	
	if (!$scope.model.value) {
		$scope.model.value = {
			major: 1,
			minor: 0,
			patch: ($scope.model.config.usePatch == 1) ? 0 : null
		}
	}
})
