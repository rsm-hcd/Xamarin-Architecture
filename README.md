# Xamarin Architecture

This is a boilerplate solution using the onion architecture for Xamarin.iOS and Xamarin.Android mobile projects.

### Architecture Documentation
The solution is separated into 3 main categories:
1. **Core** - Includes core models and interfaces as well as business logic.
3. **Presentation** - Includes iOS and Android project types.
4. **Testing** - Includes testing projects across for core and presentation projects.

#### Core
##### Mobile.Core
* Includes:
  * Interfaces used throughout all projects
  * Concrete implementation of *most* entities (e.g. DTOs, models)
  * Shared enumerations

##### Mobile.Services.Http
Contains web service implementations (e.g. REST APIs)

##### Mobile.Services.Realm
Implementation of local Realm database access.
* Includes:
  * Concrete implementations of Realm entities. Implementing entity interfaces from Mobile.Core.
  * Implementation of CRUD operations from service interfaces defined in `Mobile.Core.`

##### Mobile.ViewModels
Uses [MVVMCross](https://www.mvvmcross.com/) to manage view models that are bound to views in native UI projects (e.g. iOS, Android). The properties defined in a view model are bound to properties on UI controls. The view models make calls to `Mobile.Services.Http` and `Mobile.Services.Realm` as needed. The view models _can_ also take on the responsibility of navigation and common UI tasks (alert modals and process dialogs).

#### Presentation
##### Mobile.iOS
UI implementation of the app for iOS devices. `MVVMCross` view models are bound to view controllers. Each view controller then binds UI controls to properties from the bound view model.
* Includes:
  * **ViewController** - A view controller represents a "screen" in the app. Each screen is broken into 3 partial class files. The `layout` and `presenter` partial class files are nested under the `view controller` class file.
    * **View Controller** - Calls intitialize methods in `presenter` and `layout` files. Handles controller events (e.g. view did load, view did appear, etc...). Includes concrete implementations of view event handling. Example file name: `MainViewController.cs`
    * **Presenter** - Responsible for creating and configuring all of the views (controls) that the screen requires. Wires up view events and binds data to UI elements via `MVVMCross`. Example file name: `MainViewController.presenter.cs`
    * **Layout** - Responsible for positioning UI elements on the screen (via auto layout constraints). Exanple file name: `MainViewController.layout.cs`
  * **Views** - Any custom UI elements that the application requires are managed here (e.g. custom textbox input).
##### Mobile.Android
The UI implementation of the app for Android devices. `MVVMCross` view models are bound to activities/fragments based on name. For example if there is a view model named `MainViewModel` then the activity/fragment that you want to bind to it should be named `MainView`. Controls are bound to `MVVMCross` using the attribute `local:MvxBind` directly on views in layout (`axml`) files. For example you if you want to bind the `Text` property of a `TextView` to a property on a view model called `Name` you would add an attribute to the `TextView` like this: `local:MvxBind="Text Name"`.
##### Mobile.Svg
Includes SVG resources that are used in both iOS and Android presentation projects.

#### Tests
##### Mobile.ViewModels.Tests.iOS
An iOS test runner application that executes the tests created in `Mobile.ViewModels.Tests.Shared`. 
##### Mobile.ViewModels.Tests.Android
An Android test runner application that executes the tests created in `Mobile.ViewModels.Tests.Shared`. 
##### Mobile.ViewModels.Tests.Shared
Includes all ViewModel test implementations. It is referenced by the platform-specific test runner projects.

### Current Dependencies
* [ACR User Dialogs](https://github.com/aritchie/userdialogs) - A cross platform library that allows you to call for standard user dialogs from a shared/portable library.
* [Automapper](http://automapper.org/) - Object mapper
* [MVVMCross](https://www.mvvmcross.com/) - Model View/View Model framework
* [NUnit](https://www.nunit.org/) - Unit testing framework
* [Realm](https://realm.io/) - Mobile database
* [Shouldly](https://github.com/shouldly/shouldly) - Testing assertion framework
* [XamSvg](https://components.xamarin.com/view/xamsvg) - Vector image support in iOS and Android

### Steps to namespace Xamarin Architecture
If you want to start your own app from the boilerplate solution follow the following steps to get up and running.
1. Create a project directory for your solution (or clone a git repo you've already created).
2. Clone the `Xamarin-Architecture` repo.
3. Copy all the folders and files (exluding the `.git` folder) from the `Xamarin-Architecture` folder to your project's root folder.
4. Rename `Xamarin-Architecture.sln` to match your desired namespace.
5. Rename project folders to match your namespace
    * 01-Core/`Mobile.Core`
    * 01-Core/`Mobile.Services.Http`
    * 01-Core/`Mobile.Services.Realm`
    * 01-Core/`Mobile.ViewModels`
    * 02-Presentation/`Mobile.Android`
    * 02-Presentation/`Mobile.iOS`
    * 02-Presentation/`Mobile.Svg`
    * 03-Tests/`Mobile.ViewModels.Tests.Android`
    * 03-Tests/`Mobile.ViewModels.Tests.iOS`
    * 03-Tests/`Mobile.ViewModels.Tests.Shared`
6. Rename project files (`.csproj`)
    * 01-Core/Mobile.Core/`Mobile.Core.csproj`
    * 01-Core/Mobile.Services.Http/`Mobile.Services.Http.csproj`
    * 01-Core/Mobile.Services.Realm/`Mobile.Services.Realm.csproj`
    * 01-Core/Mobile.ViewModels/`Mobile.ViewModels.csproj`
    * 02-Presentation//Mobile.Android/`Mobile.Android.csproj`
    * 02-Presentation/Mobile.iOS/`Mobile.iOS.csproj`
    * 02-Presentation/Mobile.Svg/`Mobile.Svg.csproj`
    * 03-Tests/Mobile.ViewModels.Tests.Android/`Mobile.ViewModels.Tests.Android.csproj`
    * 03-Tests/Mobile.ViewModels.Tests.iOS/`Mobile.ViewModels.Tests.iOS.csproj`
    * 03-Tests/Mobile.ViewModels.Tests.Shared/`Mobile.ViewModels.Tests.Shared.shproj`
    * 03-Tests/Mobile.ViewModels.Tests.Shared/`Mobile.ViewModels.Tests.Shared.projitems`
7. Edit all of the project files in a text editor and update:
    * The root namespace: `<RootNamespace></RootNamespace>` 
    * The assembly name: `<AssemblyName></AssemblyName>`
    * Project references (when applicable): `<ProjectReference></ProjectReference>`
8. Edit all the project paths in the solution file (`.sln`).
9. Open the solution in `Visual Studio`.
10. Edit the `info.plist` file in the iOS presentation project.
    * Change the `Application Name`
    * Change the `Bundle Identifier`
11. Change the value for the `<string name="app_name"></string>` element in the `Strings.xml` file in the Android presentation project (located at Resources/values).
12. Open the `AndroidManifest.xml` file in the Android presentation project and change the value for `Package name`.
13. Repeat step `10` for the iOS test runner project.
14. Repeat steps `11` and `12` for the Android test runner project.
15. Update the namespaces for **all** classes in **all** projects. Note: This can be made simpler by doing a find and replace in all solution files. For example you can do a find on "`namespace Mobile.Core.`", and replace with '`namespace [YourNameSpace].Core.`". _OR_ if you're just prepending a value to the root namespace you can do a find on "`namespace Mobile.`" and replace with "`namespace YouNameSpace.Mobile.`".
16. Update all the `using` statements in **all** classes in **all** projects. Note: This can be made simpler by doing a find and replace in all solution files. For example you can do a find on "`using Mobile.Core.`", and replace with '`using [YourNameSpace].Core.`". _OR_ if you're just prepending a value to the root namespace you can do a find on "`using Mobile.`" and replace with "`using YouNameSpace.Mobile.`".
17. Open the `App.cs` file in the `ViewModels` core project and change `.InNamespace("Mobile.Services.Realm")` to reflect the correct namespace.
18. Rebuild the solution & address any issues.
19. Run the presentation and test projects to make sure everything loads properly.