> **Note** This repository is developed in .netstandard2.0 with support for SOAP service results mainly used in the .net framework (current support 4.5, 4.6.1 - 4.8)

The goal of this repository is to offer the possibility to manage and agree on the answers received as a result of the execution of a method or a process.

In other words, it offers the possibility to use a single(general) response, structured and easier parsed model for the executed methods.
As a result, you can have control over the messages and types of messages that will be obtained.

By currently following, 6 general types of messages (`Info`, `Warning`, `Error`, `NotFound`, `AccessDenied`, `Exception`) are implemented that can be returned to the caller.
As you can see in the `MessageType` enum, there are 9 types of messages, for all 3 (`Info`, `Warning`, `Error`) types previously specified exists with new ends `Confirm`. The idea of all of them is to inform UI (or caller) that returned message will be parsed/used as a dialog box/popup/modal.

No additional components or packs are required for use. So, it only needs to be added/installed in the project and can be used instantly.

**In case you wish to use it in your project, u can install the package from <a href="https://www.nuget.org/packages/AggregatedGenericResultMessage" target="_blank">nuget.org</a>** or specify what version you want:

> `Install-Package AggregatedGenericResultMessage -Version x.x.x.x`

[![NuGet Version](https://img.shields.io/nuget/v/AggregatedGenericResultMessage.svg?style=flat)](https://www.nuget.org/packages/AggregatedGenericResultMessage/)

## Content
1. [USING](docs/usage.md)
1. [CHANGELOG](docs/CHANGELOG.md)
1. [BRANCH-GUIDE](docs/branch-guide.md)