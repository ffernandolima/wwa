// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="PageRestService.ts" />

module App.Services {
    "use strict";

    export abstract class UpdateRestService<ModelType extends Models.Model, FilterType extends Queries.QueryRequest> extends PageRestService<ModelType, FilterType> {
        static $inject = ["$http", "Application"];

        constructor(
            protected $http: ng.IHttpService,
            protected Application: IApplication) {
            super($http, Application);
        }

        public Add(item: ModelType): ng.IHttpPromise<any> {
            var url = super.BuildUrl(this.ApiController);

            var promise = this.$http.post(url, item);
            return promise;
        }

        public Edit(item: ModelType): ng.IHttpPromise<any> {
            var url = super.BuildUrl(this.ApiController, item.Id);

            var promise = this.$http.put(url, item);
            return promise;
        }

        public Save(item: ModelType): ng.IHttpPromise<any> {
            if (item.Id > 0)
                return this.Edit(item);
            else
                return this.Add(item);
        }

        public Delete(id: number): ng.IHttpPromise<any> {
            var url = super.BuildUrl(this.ApiController, id);

            var promise = this.$http.delete(url);
            return promise;
        }
    }
}
