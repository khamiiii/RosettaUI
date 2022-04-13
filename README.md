# RosettaUI

Code-based GUI library for development menus for Unity

![](Documentation~/2022-04-12-17-18-14.png)

## Installation

This package uses the [scoped registry] feature to resolve package
dependencies. 

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html


### Setting Scoped Registry

**Edit > ProjectSettings... > Package Manager > Scoped Registries**

Enter the following and click the Save button.

```
"name": "fuqunaga",
"url": "https://registry.npmjs.com",
"scopes": [ "ga.fuquna" ]
```
![](Documentation~/2022-04-12-17-29-38.png)


### Install via Package Manager

**Window > Package Manager**

Select `MyRegistries` in `Packages:`

![](Documentation~/2022-04-12-17-40-26.png)

Select `RosettaUI - UI ToolKit` and click the Install button
![](Documentation~/2022-04-12-18-04-29.png)


## How to use

1. Put the `Packages/RosettaUI - UIToolkit/RosettaUIRootUIToolkit.prefab` in the Hierarychy
1. Write code to generate `Element` instance
1. Call RosettaUIRoot.Build(Element) to generate the actual UI ( [Example] )

[Example]: https://github.com/fuqunaga/RosettaUI/blob/9fbdb5af94ed09d0e6f46253e9350a8672bafd15/Assets/Example/Common/RosettaUIExample.cs#L31

Examples are available in this repository.
I recommend downloading and checking it out.


## Functions

### UI.Field()
```csharp
UI.Field(() => value)
```
![](Documentation~/field.gif)


### UI.Slider()
```csharp
UI.Slider(() => value)
```
![](Documentation~/2022-04-12-18-46-17.png)

### UI.MinMaxSlider()
```csharp
UI.MinMaxSlider(() => value)
```

![](Documentation~/2022-04-12-18-49-48.png)


### Layout elements
![](Documentation~/2022-04-12-18-55-52.png)


### And more
Please check the [Examples]("/Assets/Example/Common")