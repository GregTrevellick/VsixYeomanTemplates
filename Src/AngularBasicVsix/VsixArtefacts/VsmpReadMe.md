[GitHubRepoPullRequestsURL]: https://github.com/GregTrevellick/VsixYeomanTemplates/pulls

A basic [Angular.io](https://angular.io/) / [TypeScript](https://www.typescriptlang.org/) web application project template for [Visual Studio](https://visualstudio.microsoft.com/vs/) and [VS Code](https://code.visualstudio.com/), using [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/) (C#), [WebPack](https://webpack.js.org/), [Gulp](https://gulpjs.com/), [Sass](https://sass-lang.com/) and [Yarn](https://yarnpkg.com) (optional).

>##### Based on the [Angular Basic](https://github.com/MattJeanes/AngularBasic) project from [Matt Jeanes](https://mattjeanes.com/).

The project is scaffolded out using [Yeoman](https://yeoman.io/) from within [Visual Studio](https://visualstudio.microsoft.com/vs/).

When you create a new project...

![](screen0.png)

...a dialog appears explaining what will happen...

![](screen1.png)

...a command prompt is opened to scaffold the new project using Yeoman...
 
 - Node will install the [Angular Basic Yeoman generator](https://github.com/MattJeanes/AngularBasic) via command `npm install -g yo angular-basic`
 - Yeoman will prompt you for personalisation and custom options for the new project via command `yo angular-basic`
 - Yeoman will create the new project on your machine
 - Yeoman will automatically download any dependancies (e.g. into `node_modules` folder)

![](screen2.gif)

...the new project will be opened in a new instance of Visual Studio (or your default .csproj app), and here's what you get...

![](screen3.png)

...and upon build/run ('F5' in Visual Studio) this is the resultant website application...

![](screen4.png)

> Note: A Visual Studio solution file (`.Sln`) is not created when the project is scaffolded. However when you close Visual Studio you will be prompted to create and save a solution file.

### If you like this free extension please take just a few seconds to give it a rating.

>##### Additional Yeoman templates can be installed using [this](https://marketplace.visualstudio.com/items?itemName=GregTrevellick.NewYeomanProject) extension.

Contributions welcome [here][GitHubRepoPullRequestsURL].

See the [change log](https://github.com/GregTrevellick/VsixYeomanTemplates/blob/master/CHANGELOG.md) for release history.
