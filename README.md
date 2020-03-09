# xunit-cli

[![Build Status](https://dev.azure.com/natemcmaster/github/_apis/build/status/xunit-cli?branchName=master)](https://dev.azure.com/natemcmaster/github/_build/latest?definitionId=8&branchName=master)

[![NuGet][nuget-badge] ![NuGet Downloads][nuget-download-badge]][nuget]

[nuget]: https://www.nuget.org/packages/xunit-cli/
[nuget-badge]: https://img.shields.io/nuget/v/xunit-cli.svg?style=flat-square
[nuget-download-badge]: https://img.shields.io/nuget/dt/xunit-cli?style=flat-square


A global .NET Core command line tool for running xunit tests.

This project is the result of https://github.com/xunit/xunit/issues/1684. The xUnit team might add a global console runner in the next major version, xUnit 3, but might not. In the meantime, this project is a wrapper around [xunit.runner.console](https://nuget.org/packages/xunit.runner.console) which is designed for use with `dotnet tool install`.

## Usage

Installation:
```
dotnet tool install -g xunit-cli
```

Usage:
```
xunit <assemblyPath> [additionalArgs]
```

Run `xunit` without arguments to see full help output.
