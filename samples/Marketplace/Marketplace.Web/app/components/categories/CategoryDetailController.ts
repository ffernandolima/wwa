// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../../_references.ts" />

module App.Controllers {
    'use strict';

    export class CategoryDetailController extends Controller {
        
        // $inject: Avoid injection errors caused by minification
        static $inject = ['$scope', '$location', '$stateParams', 'ToastService', 'CategoryService'];

        // Injected dependencies
        constructor(
            public $scope: Scopes.CategoryFormScope,
            private $location: ng.ILocationService,
            private $stateParams: App.StateParameters,
            private ToastService: Services.ToastService,
            private CategoryService: Services.CategoryService
        ) {
            // Invoke parent constructor
            super($scope);

            // Gets the route parameter
            $scope.Id = $stateParams.Id;
        }

        public Init() {
            // Load from api
            this.Load();
        }

        public Load() {
            
            var scope = this.$scope;
            var promise = this.CategoryService.Get(scope.Id);

            this.Wait();

            promise
                .then((result: Models.Category) => {
                    scope.Form = result;
                })
                .catch((error: Models.ErrorResult) => {
                    scope.Rule = error.Rule;
                    this.ToastService.Warning(error.Text);
                })
                .finally(() => {
                    this.Done();
                });
        }
    }
} 
