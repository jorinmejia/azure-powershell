﻿// ----------------------------------------------------------------------------------
//
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

using System.Management.Automation;
using Microsoft.Azure.Commands.Cdn.Common;
using Microsoft.Azure.Commands.Cdn.Helpers;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Cdn;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Cdn.Origin
{
    [Cmdlet("Remove", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "CdnOrigin", SupportsShouldProcess = true, DefaultParameterSetName = FieldsParameterSet), OutputType(typeof(bool))]
    public class RemoveAzCdnOrigin : AzureCdnCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = "Azure CDN origin name.", ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string OriginName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Azure CDN endpoint name.", ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string EndpointName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Azure CDN profile name.", ParameterSetName = FieldsParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ProfileName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The resource group of the Azure CDN profile.", ParameterSetName = FieldsParameterSet)]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The resource id of the Azure CDN origin.", ParameterSetName = ResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Return object if specified.")]
        public SwitchParameter PassThru { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ParameterSetName == ResourceIdParameterSet)
            {
                var parsedResourceId = new ResourceIdentifier(ResourceId);
                ResourceGroupName = parsedResourceId.ResourceGroupName;
                ProfileName = parsedResourceId.GetProfileName();
                EndpointName = parsedResourceId.GetEndpointName();
                OriginName = parsedResourceId.ResourceName;

            }

            ConfirmAction(MyInvocation.InvocationName, OriginName, DeleteOrigin);

            if (PassThru)
            {
                WriteObject(true);
            }
        }

        public void DeleteOrigin()
        {
            CdnManagementClient.Origins.Delete(
                ResourceGroupName,
                ProfileName,
                EndpointName,
                OriginName);
        }
    }
}
