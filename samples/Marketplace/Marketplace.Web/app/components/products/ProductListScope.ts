// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../../_references.ts" />

module App.Scopes {
    'use strict';

    export interface ProductListScope extends QueryScope<Models.Product, Queries.ProductQuery> {
        Categories: Models.Category[];
        Dealers: Models.Dealer[];

        Dealer: Models.AutoCompleteConfig<Models.Dealer>;
    }
}  
