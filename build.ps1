$ErrorActionPreference = 'Stop'

dotnet publish "WinForms/WinForms.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
