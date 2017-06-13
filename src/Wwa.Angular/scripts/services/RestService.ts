// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="Service.ts" />

module App.Services {
    "use strict";

    export abstract class RestService<ModelType extends Models.Model> extends Service {
        static $inject = ["$http", "Application"];

        public ApiController: string;

        constructor(
            protected $http: ng.IHttpService,
            protected Application: IApplication) {

            super($http, Application);
        }

        public All(): ng.IHttpPromise<ModelType[]> {
            
            var url = super.BuildUrl(this.ApiController);
            var promise = this.$http.get(url);

            return promise;
        }

        public Get(id: number): ng.IHttpPromise<ModelType> {
            var url = super.BuildUrl(this.ApiController, id);

            var promise = this.$http.get(url);
            return promise;
        }
    }
}
