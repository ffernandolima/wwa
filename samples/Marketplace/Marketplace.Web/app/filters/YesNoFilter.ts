// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

/// <reference path="../_references.ts" />

module App.Filters {
    'use strict';

    export function YesNoFilter(input: boolean, trueText?: string, falseText?: string) {
        if (Utils.IsEmpty(trueText))
            trueText = "Yes";

        if (Utils.IsEmpty(falseText))
            falseText = "No";

        return input ? trueText : falseText;
    }

    YesNoFilter.$inject = [];
}
