# https://docs.microsoft.com/en-us/visualstudio/extensibility/walkthrough-publishing-a-visual-studio-extension-via-command-line?view=vs-2017

cd C:\_git\OpenInApp.Launcher\src\packages\Microsoft.VSSDK.BuildTools.15.8.3252\tools\vssdk\bin

.\VsixPublisher.exe publish  -payload "..\..\..\..\..\..\..\VsixYeomanTemplates\src\AngularBasicVsix\bin\debug\AngularBasicVsix.vsix"  -publishManifest "..\..\..\..\..\..\..\VsixYeomanTemplates\src\AngularBasicVsix\VsixArtefacts\VsmpPublish.json"  -personalAccessToken "vsmp_pat"
.\VsixPublisher.exe publish  -payload "..\..\..\..\..\..\..\VsixYeomanTemplates\src\OmniSharpAspNetVsix\bin\debug\OmniSharpAspNetVsix.vsix"  -publishManifest "..\..\..\..\..\..\..\VsixYeomanTemplates\src\OmniSharpAspNetVsix\VsixArtefacts\VsmpPublish.json"  -personalAccessToken "vsmp_pat"
