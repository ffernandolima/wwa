// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../../_references.ts" />

module App.Controllers {
    'use strict';

    export class CustomerFormController extends Controller {
        
        // $inject: Avoid injection errors caused by minification
        static $inject = ['$scope', '$location', '$stateParams', 'ToastService', 'ProvinceService', 'CountryService', 'CustomerService'];

        // Injected dependencies
        constructor(
            public $scope: Scopes.CustomerFormScope,
            private $location: ng.ILocationService,
            private $stateParams: App.StateParameters,
            private ToastService: Services.ToastService,
            private ProvinceService: Services.ProvinceService,
            private CountryService: Services.CountryService,
            private CustomerService: Services.CustomerService
        ) {
            // Invoke parent constructor
            super($scope);

            // Gets the route parameter
            this.$scope.Id = $stateParams.Id;
        }

        public Init() {
            // Scope initialization
            this.$scope.Form = new Models.Customer();

            // Load lists
            this.LoadCountries();

            if (!this.IsNew()) {
                // Load from api
                this.Load();
            }
            
            // Reload child list
            this.$scope.$watch((i: Scopes.CustomerFormScope) => i.Form.CountryId, (newValue: number, oldValue: number, scope: Scopes.CustomerFormScope) => {
                if (!Utils.IsUndefined(newValue) && newValue != oldValue)
                    this.LoadProvinces(newValue);
            });
        }

        public NameRemaining() {
            if (Utils.IsUndefined(this.$scope.Form))
                return 100;

            return super.Remainder(100, this.$scope.Form.Name);
        }

        public IsNew() {
            return Utils.IsUndefined(this.$scope.Id) || this.$scope.Id <= 0;
        }

        public Back() {
            this.$location.url("/customers");
        }

        public Save() {
            
            var scope = this.$scope;
            var location = this.$location;

            this.Wait();

            // Sends out data for insert/update
            var promise = this.CustomerService.Save(scope.Form);

            promise
                .then((result: any) => {
                    location.url('/customers');
                })
                .catch((error: Models.ErrorResult) => {
                    scope.Rule = error.Rule;
                    this.ToastService.Warning(error.Text);
                })
                .finally(() => {
                    this.Done();
                });
        }

        public Load() {
            // Load from api
            var scope = this.$scope;
            var promise = this.CustomerService.Get(scope.Id);

            this.Wait();

            promise
                .then((result: Models.Customer) => {
                    scope.Form = result;
                })
                .catch((error: Models.ErrorResult) => {
                    if (error.Rule != null) 
                        scope.Rule = error.Rule;

                    this.ToastService.Warning(error.Text);
                })
                .finally(() => {
                    this.Done();
                });
        }

        private LoadCountries() {
            var scope = this.$scope;
            var promise = this.CountryService.All();

            this.Wait();

            promise
                .then((result: Models.Country[]) => {
                    scope.Countries = result;
                })
                .catch((error: Models.ErrorResult) => {
                    scope.Rule = error.Rule;
                    this.ToastService.Warning(error.Text);
                })
                .finally(() => {
                    this.Done();
                });
        }

        private LoadProvinces(countryId: number) {
            
            var scope = this.$scope;

            var filter = new Queries.ProvinceQuery();
            filter.CountryId = countryId;

            var promise = this.ProvinceService.Search(filter);

            this.Wait();

            promise
                .then((result: Queries.PagedList<Models.Province>) => {
                    scope.Provinces = result.Items;
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
