[GitHubRepoPullRequestsURL]: https://github.com/GregTrevellick/VsixYeomanTemplates/pulls

A collection of [Yeoman](https://yeoman.io/) scaffolded web / console/ class library / unit test projects using [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/). Optionally also using MVC, WebAPI, [Nancy](http://nancyfx.org/), MS Test, [Xunit](https://xunit.github.io/), [C#](https://docs.microsoft.com/en-us/dotnet/csharp/), [F#](https://fsharp.org/), [bootstrap](https://getbootstrap.com/), [semantic](https://semantic-ui.com/).

>##### Based on the [AspNet](https://github.com/omnisharp/generator-aspnet) project from [OmniSharp](http://www.omnisharp.net/).

The project is scaffolded out using [Yeoman](https://yeoman.io/) from within [Visual Studio](https://visualstudio.microsoft.com/vs/).

When you create a new project...

![](screen0.png)

...a dialog appears informing you what will happen and prompting your confirmation...

![](screen1.png)

...when you click 'OK' a command prompt is opened to scaffold the new project using Yeoman...
 
 - Node will install the angular-basic Yeoman generator via command `npm install -g yo aspnet`
 - Yeoman will prompt you for personalisation and custom options for the new project (red border) via command `yo aspnet`
 - Within seconds Yeoman will create the new project on your hard drive (purple border)
 - Yeoman will automatically download any necessary dependancies (e.g. into `node_modules` folder)

![](screen2.gif)

...the new project will be opened in a new instance of Visual Studio, and here's what you get...

![](screen3.png)

...and upon build/run ('F5' in Visual Studio) this is the resultant website application...

![](screen4.png)

> Note: A Visual Studio solution file (`.Sln`) is not created when the project is scaffolded. However when you close Visual Studio you will be prompted to create and save a solution file.

If you like this **free** extension please take just a few seconds to give it a rating.

Contributions welcome [here][GitHubRepoPullRequestsURL].

See the [change log](https://github.com/GregTrevellick/VsixYeomanTemplates/blob/master/CHANGELOG.md) for release history.

### Road map

- [ ] VS2019 support - yay !