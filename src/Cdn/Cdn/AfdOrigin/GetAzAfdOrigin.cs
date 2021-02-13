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

using Microsoft.Azure.Commands.Cdn.AfdModels;
using Microsoft.Azure.Commands.Cdn.AfdHelpers;
using Microsoft.Azure.Commands.Cdn.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Cdn;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Cdn.AfdOrigin
{
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "AfdOrigin", DefaultParameterSetName = FieldsParameterSet), OutputType(typeof(PSAfdOrigin))]
    public  class GetAzAfdOrigin : AzureCdnCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = HelpMessageConstants.AfdOriginGroupObjectDescription, ParameterSetName = ObjectParameterSet)]
        public PSAfdOriginGroup OriginGroup { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdOriginGroupName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string OriginGroupName { get; set; }

        [Parameter(Mandatory = false, HelpMessage = HelpMessageConstants.AfdOriginName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string OriginName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.AfdProfileName, ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ProfileName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.ResourceGroupName, ParameterSetName = FieldsParameterSet)]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = HelpMessageConstants.ResourceId, ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        public override void ExecuteCmdlet()
        {
            try
            {
                switch (ParameterSetName)
                {
                    case FieldsParameterSet:
                        this.FieldsParameterSetCmdlet();
                        break;
                    case ObjectParameterSet:
                        // this.ObjectParameterSetCmdlet();
                        break;
                    case ResourceIdParameterSet:
                        // this.ResourceIdParameterSetCmdlet();
                        break;
                }
            }
            catch (Microsoft.Azure.Management.Cdn.Models.AfdErrorResponseException errorResponse)
            {
                throw new PSArgumentException(errorResponse.Response.Content);
            }
        }

        public void FieldsParameterSetCmdlet()
        {
            if(AfdUtilities.IsValuePresent(this.OriginName))
            {
                // all fields are present (mandatory + optional)

                PSAfdOrigin psAfdOrigin = this.CdnManagementClient.AFDOrigins.Get(this.ResourceGroupName, this.ProfileName, this.OriginGroupName, this.OriginName).ToPSAfdOrigin();

                WriteObject(psAfdOrigin);
            }
            else
            {
                // only the mandatory fields are present 

                List<PSAfdOrigin> psAfdOrigins = this.CdnManagementClient.AFDOrigins.ListByOriginGroup(this.ResourceGroupName, this.ProfileName, this.OriginGroupName)
                                                 .Select(afdOrigin => afdOrigin.ToPSAfdOrigin())
                                                 .ToList();

                WriteObject(psAfdOrigins);
            }
        }
    }
}
