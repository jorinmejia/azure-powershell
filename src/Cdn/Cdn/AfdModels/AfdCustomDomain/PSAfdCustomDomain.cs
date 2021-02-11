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

using Microsoft.Azure.Commands.Cdn.AfdModels.Arm;

namespace Microsoft.Azure.Commands.Cdn.AfdModels.AfdCustomDomain
{
    public class PSAfdCustomDomain : PSArmResource
    {
        public string HostName { get; set; }

        public string CertificateType { get; set; }

        public string MinimumTlsVersion { get; set; }

        public string Secret { get; set; }

        public string ValidationToken { get; set; }

        public string ExpirationDate { get; set; }

        public string AzureDnsZone { get; set; }

        public string DomainValidationState { get; set; }
    }
}
