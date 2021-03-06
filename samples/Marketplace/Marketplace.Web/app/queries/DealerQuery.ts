// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../_references.ts" />

module App.Queries {
    'use strict';

    export class DealerQuery extends QueryRequest {
        Name: string;
        CityName: string;
        
        ProvinceId: number;
        CountryId: number;
        HasOrders: boolean;

        constructor() {
            super();

            this.SortField = "Name";
        }
    }
}  
