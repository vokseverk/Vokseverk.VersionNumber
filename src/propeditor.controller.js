angular.module("umbraco").controller("VersionEditorController", function($scope, $element) {
	if (!$scope.config) {
		$scope.config = {
			usePatch: true
		}
	}
	
	if ($scope.config.usePatch == null) {
		$scope.config.usePatch = true
	}
	
	if (!$scope.model.value) {
		$scope.model.value = {
			major: 1,
			minor: 0,
			patch: ($scope.config.usePatch ? 0 : null)
		}
	}
	
	// Set the property editor's value...
	// $scope.model.value = 'Some computed value'
})
