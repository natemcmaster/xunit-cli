# xunit-cli

A global .NET Core command line tool for running xunit tests.

This project is the result of https://github.com/xunit/xunit/issues/1684. The xUnit team might add a global console runner in the next major version, xUnit 3, but might not. In the meantime, this project is a wrapper around [xunit.runner.console](https://nuget.org/packages/xunit.runner.console) which is designed for use with `dotnet tool install`.

## Usage

Installation:
```
dotnet tool install -g xunit-cli --add-source https://myget.org/F/natemcmaster/api/v3/index.json
```

Usage:
```
xunit <assemblyPath> [additionalArgs]
```

Run `xunit` without arguments to see full help output.
