// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../../_references.ts" />

module App.Scopes {
    'use strict';

    export interface UserDetailScope extends ViewScope {
        Id: number;
        Item: Models.User;
    }
}  
