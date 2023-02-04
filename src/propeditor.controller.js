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
			build: ($scope.model.config.usePatch == 1) ? 0 : null
		}
	} else if (typeof($scope.model.value) == "string") {
		var value = $scope.model.value
		var usePatch = $scope.model.config.usePatch == 1
		var versionRE = /^\s*v?(\d+)\.(\d+)(?:\.(\d+))?\s*$/;
		var matches = versionRE.exec(value)
		if (matches != null && matches.length > 1) {
			var maj = matches[1]
			var min = matches[2] || 0
			var rev = usePatch ? (matches[3] || 0) : null
			$scope.model.value = {
				major: maj,
				minor: min,
				build: rev
			}
		}
	}
})
