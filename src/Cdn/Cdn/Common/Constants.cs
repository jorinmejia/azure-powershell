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

namespace Microsoft.Azure.Commands.Cdn.Common
{
    public class Constants
    {
        public const int PurgeLoadMinimumCollectionCount = 1;
        public const int PurgeLoadMaximumCollectionCount = 50;
    }

    public static class HelpMessageConstants
    {
        public const string AfdCustomDomainName = "The Azure Front Door custom domain name.";
        public const string AfdEndpointName = "The Azure Front Door endpoint name.";
        public const string AfdProfileName = "The Azure Front Door profile name.";
        public const string AfdProfileObjectDescription = "The Azure Front Door profile object.";
        public const string ResourceId = "The Azure resource id.";
        public const string ResourceGroupName = "The Azure resource group name.";
    }

    public static class AfdSkuConstants
    {
        public const string PremiumAzureFrontDoor = "Premium_AzureFrontDoor";
        public const string StandardAzureFrontDoor = "Standard_AzureFrontDoor"; 
    }
}
