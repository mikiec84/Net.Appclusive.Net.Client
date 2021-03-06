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

extern alias Api;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using Api::Net.Appclusive.Api;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Testing.PowerShell;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Constants;

namespace Net.Appclusive.PS.Client.Tests
{
    [TestClass]
    public class EnterServerTest
    {
        private static string _username;
        private static string _password;
        private static string _apiBaseUri;

        private readonly Type sut = typeof(EnterServer);

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var fileInfo = ModuleConfiguration.ResolveConfigurationFileInfo(null);
            var moduleContextSection = ModuleConfiguration.GetModuleContextConfigurationSection(fileInfo);

            _username = moduleContextSection.Credential.UserName;
            _password = moduleContextSection.Credential.GetNetworkCredential().Password;
            _apiBaseUri = moduleContextSection.ApiBaseUri.ToString();
        }

        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"'ApiBaseUri'.+'System\.Uri'")]
        public void InvokeWithInvalidApiBaseUriParameterThrowsParameterBindingException()
        {
            var parameters = @"-ApiBaseUri ";
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"Username.+Password")]
        public void InvokeWithMissingParameterThrowsParameterBindingException()
        {
            var parameters = string.Format("-ApiBaseUri {0}", _apiBaseUri);
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestMethod]
        [ExpectParameterBindingValidationException(MessagePattern = @"Password")]
        public void InvokeWithEmptyPasswordParameterThrowsParameterBindingValidationException()
        {
            var parameters = string.Format("-ApiBaseUri {0} -Username {1} -Password ''", _apiBaseUri, _username);
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestMethod]
        [ExpectParameterBindingValidationException(MessagePattern = @"Credential")]
        public void InvokeWithNullCredentialParameterThrowsParameterBindingValidationException()
        {
            var parameters = string.Format("-ApiBaseUri {0} -Credential $null", _apiBaseUri);
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"'Credential'.+.System\.String.")]
        public void InvokeWithInvalidCredentialParameterThrowsParameterBindingException()
        {
            var parameters = string.Format("-ApiBaseUri {0} -Credential arbitrary-user-as-string", _apiBaseUri);
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"'TenantId'.+.System\.Guid.")]
        public void InvokeWithInvalidTenantIdParameterThrowsParameterBindingException()
        {
            var parameters = string.Format("-ApiBaseUri {0} -Username {1} -Password {2} -TenantId 123", _apiBaseUri, _username, _password);
            PsCmdletAssert.Invoke(sut, parameters);
        }

        [TestCategory("SkipOnTeamCity")]
        [TestMethod]
        public void InvokeWithParameterSetPlainSucceeds()
        {
            // Arrange
            var parameters = string.Format(@"-ApiBaseUri {0} -User '{1}' -Password '{2}'", _apiBaseUri, _username, _password);

            // Act
            var results = PsCmdletAssert.Invoke(sut, parameters);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            var svc = results[0].BaseObject as Dictionary<string, DataServiceContextBase>;
            Assert.IsNotNull(svc);
            Assert.AreEqual(2, svc.Count);
        }

        [TestCategory("SkipOnTeamCity")]
        [TestMethod]
        public void InvokeWithParameterSetPlainWithValidTenantIdSucceeds()
        {
            // Arrange
            var parameters = string.Format(@"-ApiBaseUri {0} -User '{1}' -Password '{2}' -TenantId {3}", _apiBaseUri, _username, _password, Identity.Tenant.SYSTEM_TID);

            // Act
            var results = PsCmdletAssert.Invoke(sut, parameters);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            var svc = results[0].BaseObject as Dictionary<string, DataServiceContextBase>;
            Assert.IsNotNull(svc);
            Assert.AreEqual(2, svc.Count);
        }

        [TestMethod]
        public void InvokeWithParameterSetCredSucceeds()
        {
            // Arrange
            var parameters = string.Format(@"-ApiBaseUri {0} -Credential $([pscredential]::new('{1}', (ConvertTo-SecureString -AsPlainText -String {2} -Force)))", _apiBaseUri, _username, _password);

            // Act
            var results = PsCmdletAssert.Invoke(sut, parameters);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            var svc = results[0].BaseObject as Dictionary<string, DataServiceContextBase>;
            Assert.IsNotNull(svc);
            Assert.AreEqual(2, svc.Count);
        }

        [TestMethod]
        public void InvokeWithParameterSetConfigSucceeds()
        {
            // Arrange
            var fileInfo = ModuleConfiguration.ResolveConfigurationFileInfo(null);
            var moduleContextSection = ModuleConfiguration.GetModuleContextConfigurationSection(fileInfo);
            ModuleConfiguration.SetModuleContext(moduleContextSection);

            var parameters = "-UseModuleContext";

            // Act
            var results = PsCmdletAssert.Invoke(sut, parameters);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);

            var svc = results[0].BaseObject as Dictionary<string, DataServiceContextBase>;
            Assert.IsNotNull(svc);
            Assert.AreEqual(2, svc.Count);

            ModuleConfiguration.SetModuleContext(new ModuleContextConfigurationSection());
        }

        // DFTODO - maybe this test should be a generic test inside the Testing package
        [TestMethod]
        [ExpectedException(typeof(IncompleteParseException))]
        public void InvokeWithInvalidStringThrowsIncompleteParseException()
        {
            // Arrange
            // missing string terminator
            var parameters = string.Format(@"-ApiBaseUri {0} -User '{1} -Password '{2}'", _apiBaseUri, _username, _password);

            // Act
            PsCmdletAssert.Invoke(sut, parameters);

            // Assert
        }
    }
}
