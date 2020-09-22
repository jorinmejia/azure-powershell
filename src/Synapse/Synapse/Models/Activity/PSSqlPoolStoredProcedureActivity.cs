// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Commands.Synapse.Models
{
    using global::Azure.Analytics.Synapse.Artifacts.Models;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.WindowsAzure.Commands.Utilities.Common;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Execute SQL pool stored procedure activity.
    /// </summary>
    [Newtonsoft.Json.JsonObject("SqlPoolStoredProcedure")]
    [Rest.Serialization.JsonTransformation]
    public partial class PSSqlPoolStoredProcedureActivity : PSActivity
    {
        /// <summary>
        /// Initializes a new instance of the SqlPoolStoredProcedureActivity
        /// class.
        /// </summary>
        public PSSqlPoolStoredProcedureActivity()
        {
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets SQL pool stored procedure reference.
        /// </summary>
        [JsonProperty(PropertyName = "sqlPool")]
        public SqlPoolReference SqlPool { get; set; }

        /// <summary>
        /// Gets or sets stored procedure name. Type: string (or Expression
        /// with resultType string).
        /// </summary>
        [JsonProperty(PropertyName = "typeProperties.storedProcedureName")]
        public object StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets value and type setting for stored procedure
        /// parameters. Example: "{Parameter1: {value: "1", type: "int"}}".
        /// </summary>
        [JsonProperty(PropertyName = "typeProperties.storedProcedureParameters")]
        public IDictionary<string, StoredProcedureParameter> StoredProcedureParameters { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public override void Validate()
        {
            base.Validate();
            if (SqlPool == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "SqlPool");
            }
            if (StoredProcedureName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "StoredProcedureName");
            }
        }

        public override Activity ToSdkObject()
        {
            var activity = new SqlPoolStoredProcedureActivity(this.Name, this.SqlPool, this.StoredProcedureName);
            this.StoredProcedureParameters?.ForEach(item => activity.StoredProcedureParameters.Add(item));
            SetProperties(activity);
            return activity;
        }
    }
}

