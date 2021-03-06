﻿/**
 * Copyright 2017 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Diagnostics.Contracts;

namespace Net.Appclusive.Api
{
    [ContractClass(typeof(ContractClassForIOdataActionExecutor))]
    interface IOdataActionExecutor
    {
        void InvokeEntitySetActionWithVoidResult(object entity, string actionName, object inputParameters);

        void InvokeEntitySetActionWithVoidResult(string entitySetName, string actionName, object inputParameters);

        T InvokeEntitySetActionWithSingleResult<T>(string entitySetName, string actionName, object inputParameters);

        T InvokeEntitySetActionWithSingleResult<T>(object entity, string actionName, object inputParameters);

        object InvokeEntitySetActionWithSingleResult(string entitySetName, string actionName, object type, object inputParameters);

        object InvokeEntitySetActionWithSingleResult(object entity, string actionName, object type, object inputParameters);

        object InvokeEntitySetActionWithSingleResult(string entitySetName, string actionName, Type type, object inputParameters);

        object InvokeEntitySetActionWithSingleResult(object entity, string actionName, Type type, object inputParameters);

        object InvokeEntitySetActionWithListResult(string entitySetName, string actionName, Type type, object inputParameters);

        object InvokeEntitySetActionWithListResult(object entity, string actionName, Type type, object inputParameters);

        object InvokeEntitySetActionWithListResult(string entitySetName, string actionName, object type, object inputParameters);

        object InvokeEntitySetActionWithListResult(object entity, string actionName, object type, object inputParameters);

        IEnumerable<T> InvokeEntitySetActionWithListResult<T>(string entitySetName, string actionName, object inputParameters);

        IEnumerable<T> InvokeEntitySetActionWithListResult<T>(object entity, string actionName, object inputParameters);

        void InvokeEntityActionWithVoidResult(object entity, string actionName, object inputParameters);

        void InvokeEntityActionWithVoidResult(string entitySetName, long id, string actionName, object inputParameters);

        void InvokeEntityActionWithVoidResult(string entitySetName, object id, string actionName, object inputParameters);

        T InvokeEntityActionWithSingleResult<T>(string entitySetName, long id, string actionName, object inputParameters);

        T InvokeEntityActionWithSingleResult<T>(string entitySetName, object id, string actionName, object inputParameters);

        T InvokeEntityActionWithSingleResult<T>(object entity, string actionName, object inputParameters);

        object InvokeEntityActionWithSingleResult(string entitySetName, long id, string actionName, object type, object inputParameters);

        object InvokeEntityActionWithSingleResult(object entity, string actionName, object type, object inputParameters);

        object InvokeEntityActionWithSingleResult(string entitySetName, long id, string actionName, Type type, object inputParameters);

        object InvokeEntityActionWithSingleResult(object entity, string actionName, Type type, object inputParameters);

        object InvokeEntityActionWithListResult(string entitySetName, long id, string actionName, Type type, object inputParameters);

        object InvokeEntityActionWithListResult(object entity, string actionName, Type type, object inputParameters);

        object InvokeEntityActionWithListResult(string entitySetName, long id, string actionName, object type, object inputParameters);

        object InvokeEntityActionWithListResult(object entity, string actionName, object type, object inputParameters);

        IEnumerable<T> InvokeEntityActionWithListResult<T>(string entitySetName, long id, string actionName, object inputParameters);

        IEnumerable<T> InvokeEntityActionWithListResult<T>(string entitySetName, object id, string actionName, object inputParameters);

        IEnumerable<T> InvokeEntityActionWithListResult<T>(object entity, string actionName, object inputParameters);

        BodyOperationParameter[] GetBodyOperationParametersFromObject(object input);

        BodyOperationParameter[] GetBodyOperationParametersFromHashtable(Hashtable input);

        BodyOperationParameter[] GetBodyOperationParametersFromDictionary(Dictionary<string, object> input);
    }
}
