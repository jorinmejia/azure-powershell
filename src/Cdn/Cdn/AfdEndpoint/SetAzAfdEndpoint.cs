﻿// ----------------------------------------------------------------------------------
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Cdn.AfdHelpers;
using Microsoft.Azure.Commands.Cdn.AfdModels;
using Microsoft.Azure.Commands.Cdn.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.Cdn;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Cdn.AfdEndpoint
{
    [Cmdlet("Set", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AfdEndpoint", DefaultParameterSetName = FieldsParameterSet, SupportsShouldProcess = true), OutputType(typeof(PSAfdEndpoint))]
    public class SetAzAfdEndpoint : AzureCdnCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = HelpMessageConstants.AfdEndpointObject, ParameterSetName = ObjectParameterSet)]
        [ValidateNotNullOrEmpty]
        public PSAfdEndpoint Endpoint { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdEndpointName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string EndpointName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.AfdEndpointOriginResponseTimeoutSeconds, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public int OriginResponseTimeoutSeconds { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdProfileName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ProfileName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.ResourceGroupName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter()]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.TagsDescription, ParameterSetName = FieldsParameterSet)]
        public Hashtable Tags { get; set; }

        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ObjectParameterSet:
                    this.ObjectParameterSetCmdlet();
                    break;
            }

            ConfirmAction(AfdResourceProcessMessage.AfdEndpointUpdateMessage, this.EndpointName, this.UpdateAfdEndpoint);
        }

        private void UpdateAfdEndpoint()
        {
            try
            {
                Microsoft.Azure.Management.Cdn.Models.AFDEndpointUpdateParameters afdEndpointParameters = new Microsoft.Azure.Management.Cdn.Models.AFDEndpointUpdateParameters();
               
                if (this.OriginResponseTimeoutSeconds >= AfdResourceConstants.AfdEndpointOriginResponseTimeoutSecondsMin)
                {
                    afdEndpointParameters.OriginResponseTimeoutSeconds = this.OriginResponseTimeoutSeconds;
                }

                Dictionary<string, string> afdEndpointTags = TagsConversionHelper.CreateTagDictionary(this.Tags, true);
                afdEndpointParameters.Tags = afdEndpointTags;

                PSAfdEndpoint psAfdEndpoint = this.CdnManagementClient.AFDEndpoints.Update(this.ResourceGroupName, this.ProfileName, this.EndpointName, afdEndpointParameters).ToPSAfdEndpoint();

                WriteObject(psAfdEndpoint);
            }
            catch (Microsoft.Azure.Management.Cdn.Models.AfdErrorResponseException errorResponseException)
            {
                throw new PSArgumentException(errorResponseException.Response.Content);
            }
        }

        private void ObjectParameterSetCmdlet()
        {
            ResourceIdentifier parsedAfdEndpointResourceId = new ResourceIdentifier(this.Endpoint.Id);

            this.EndpointName = parsedAfdEndpointResourceId.ResourceName;
            this.ProfileName = parsedAfdEndpointResourceId.GetResourceName("profiles");
            this.ResourceGroupName = parsedAfdEndpointResourceId.ResourceGroupName;
            this.Tags = this.Endpoint.Tags;

            if (this.Endpoint.OriginResponseTimeoutSeconds != null)
            {
                this.OriginResponseTimeoutSeconds = (int)this.Endpoint.OriginResponseTimeoutSeconds;
            }
        }
    }
}
