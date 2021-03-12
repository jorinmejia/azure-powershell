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
using Microsoft.Azure.Management.Cdn.Models;
using System.Collections;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Cdn.AfdEndpoint
{
    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AfdEndpoint", DefaultParameterSetName = FieldsParameterSet, SupportsShouldProcess = true), OutputType(typeof(PSAfdEndpoint))]
    public class NewAzAfdEndpoint : AzureCdnCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdEndpointName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string EndpointName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.AfdEndpointOriginResponseTimeoutSeconds, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]

        public int OriginResponseTimeoutSecond { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdProfileName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ProfileName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.ResourceGroupName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter()]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.TagsDescription, ParameterSetName = FieldsParameterSet)]
        public Hashtable Tag { get; set; }

        public override void ExecuteCmdlet()
        {
            ConfirmAction(AfdResourceProcessMessage.AfdEndpointCreateMessage, this.EndpointName, this.CreateAfdEndpoint);
        }

        public void CreateAfdEndpoint()
        {
            try
            {
                AFDEndpoint afdEndpoint = new AFDEndpoint
                {
                    Location = AfdResourceConstants.AfdResourceLocation,

                    OriginResponseTimeoutSeconds = this.OriginResponseTimeoutSecond >= AfdResourceConstants.AfdEndpointOriginResponseTimeoutSecondsMin ? this.OriginResponseTimeoutSecond : 60,
<<<<<<< HEAD
                    Tags = TagsConversionHelper.CreateTagDictionary(this.Tag, true)
=======

                    Tags = TagsConversionHelper.CreateTagDictionary(this.Tags, true)
>>>>>>> e67fc76e04a2464605b55602e33da20092d952e5
                };

                PSAfdEndpoint psAfdEndpoint = this.CdnManagementClient.AFDEndpoints.Create(this.ResourceGroupName, this.ProfileName, this.EndpointName, afdEndpoint).ToPSAfdEndpoint();

                WriteObject(psAfdEndpoint);
            }
            catch (AfdErrorResponseException errorResponse)
            {
                throw new PSArgumentException(errorResponse.Response.Content);
            }
        }
    }
}
