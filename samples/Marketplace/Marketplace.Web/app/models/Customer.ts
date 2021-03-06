// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../_references.ts" />

module App.Models {
    'use strict';

    export class Customer extends ActiveNamedModel {
        public PhoneNumber: string;
        public Address: string;
        public City: string;
        public ZipCode: string;

        public ProvinceId: number;
        public CountryId: number;
        public UserId: number;

        public ProvinceAbbreviation: string;
        public ProvinceName: string;
        public CountryName: string;
        public UserName: string;
    }
} 
