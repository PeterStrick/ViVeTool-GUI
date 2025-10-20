> [!CAUTION]
> # DEPRECATION NOTICE
>
> This Project is unfortunately deprecated due to number of colliding factors, like me having less Motivation and Time to work on ViVeTool GUI; and the State of Windows deterioating on a day to day basis, which made me switch away from Windows to Linux full time and permanently
>
> While it hurts me to stop working on a Project that garnered this much attention and Stars, it is better to officially declare it deprecated and unmaintained, instead of just denying the facts
>
> It was really fun working and gaining experience while developing ViVeTool GUI, if someone else wants to carry on with the Project feel free to create a GitHub Issue to discuss it

![GitHub all releases](https://img.shields.io/github/downloads/peterstrick/vivetool-gui/total)
![GitHub License](https://img.shields.io/github/license/peterstrick/vivetool-gui)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/peterstrick/vivetool-gui)
[![](https://dcbadge.vercel.app/api/server/8vDFXEucp2?style=flat)](https://discord.gg/8vDFXEucp2)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PeterStrick_ViVeTool-GUI&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=PeterStrick_ViVeTool-GUI)
[![Translation](https://weblate.rawrr.dev/widgets/vivetool-gui/-/svg-badge.svg)](https://weblate.rawrr.dev/engage/vivetool-gui)

# ViVeTool GUI <img src="/images/icons8-advertisement-page-96.png" alt="ViVeTool GUI Logo" width="32"/> 
### Windows Feature Control GUI based on ViVeTool

## What is ViVeTool GUI?
ViVeTool GUI lets you easily enable, disable and search for new hidden Features in Windows Insider Builds, with the use of a Button and a pretty UI.

## Disclaimer.
### No one, including me / [PeterStrick](https://github.com/PeterStrick), [the creators of ViVe and ViVeTool](https://github.com/thebookisclosed/ViVe) or [the creators of mach2](https://github.com/riverar/mach2) are responsible for any damage or unintended side effects, this program might cause to your computer, by changing the Windows Feature Store. Use this program at your own risk.

## How to use it?
Using it is simple, 
Either:

1. Select the Build for which you want to enable or disable features for.
2. Wait for it to load in, open one of the Groups by pressing the Arrow, and select the Feature that you are looking for.
3. Press on Perform Action and perform your desired action for the entered feature ID.

<img width="511" height="355" src="/images/Method1.gif" alt="Image showing you how to perform Method 1" />


---

Or:
1. Press on "Manually change a Feature" (F12)
2. Enter a Feature ID
3. Press on Perform Action and perform your desired action for the selected feature.

<img width="511" height="355" src="/images/Method2.gif" alt="Image showing you how to perform Method 2" />

---

## What are the additional features?
Apart from being able to manage features, ViVeTool GUI let´s you also:
- Load in a Feature List of other Builds
- Search for Features 
- Sort Features by Feature Name, Feature ID or Feature State
- Group Features by: Always Enabled, Always Disabled, Enabled by Default, Disabled by Default and Modifiable
- Copy Feature Names and IDs by right-clicking them
- Switch between Dark and Light Mode (Setting get´s saved and applied on Start)
- Automatically load the latest Feature List when starting ViVeTool GUI
- Scan a Windows Build for Hidden Features to create your own Feature List
- Use ViVeTool GUI in multiple translated Languages
- Fix the LastKnownGood Store, as well as the A/B Testing Priorities for ViVeTool Features
- and at last, view the About Box by either pressing on the About Icon, or selecting the "About..." Item in the Application System Menu.

<img width="511" height="175" src="/images/Searching.gif" alt="Image showing you how to search" />

## What are the System Requirements?
Since ViVeTool GUI uses the ViVe API, Windows 10 Build 18963 (Version 2004) and newer is the only OS Requirement.

Apart from that, the only Requirement is .Net Framework 4.8

## Why not just use ViVeTool?
Using ViVeTool GUI is more easier and user-friendly, besides it lets you also search for features and enable them with a few clicks.

# Licensing
ViVeTool GUI uses Icons from [icons8.com](https://icons8.com/)

ViVeTool GUI is inspired by [ViVeTool](https://github.com/thebookisclosed/ViVe) and uses the [ViVe API](https://github.com/thebookisclosed/ViVe/tree/master/ViVe)

ViVeTool GUI uses [files](https://github.com/riverar/mach2/tree/master/features) from [mach2](https://github.com/riverar/mach2) for the Build Combo Box.

ViVeTool GUI - Feature Scanner uses [mach2](https://github.com/riverar/mach2) to create it's Feature Lists

