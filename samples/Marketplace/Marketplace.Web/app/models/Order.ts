// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../_references.ts" />

module App.Models {
    'use strict';

    export class Order extends ActiveModel {
        public Date: Date;
        public TotalAmount: number;

        public StatusId: number;
        public CustomerId: number;
        public DealerId: number;

        public StatusName: string;
        public CustomerName: string;
        public DealerName: string;

        public Items: OrderItem[];

        constructor() {
            super();
            this.Items = [];
            this.TotalAmount = 0;
        }
    }
} 
