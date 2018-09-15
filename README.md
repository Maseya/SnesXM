# SnesXM
A Super NES emulator in C#

This project is nowhere near done.

# Table of Contents
* [What is SnesXM?](#what-is-snesxm)
* [Installation](#installation)
    * [Visual Studio](#visual-studio)
* [History](#history)
* [Contributions](#contributions)
* [Credits](#credits)
* [License](#license)

# What is SnesXM?
SnesXM will be my attempt to write a super NES emulator in C#. The need has arisen following a lot of work in [MushROMs](https://github.com/bonimy/MushROMs).

# Installation
Presently, the [Visual Studio 2017 IDE](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes) is the only supported environment for SnesXM. Users are encouraged to suggest new environments in our [Issues](https://github.com/Maseya/SnesXM/issues) section.

## Visual Studio
* Get the [latest](https://www.visualstudio.com/downloads/) version of Visual Studio. At the time of writing this, it should be Visual Studio 2017. You have three options: [Community, Professional, and Enterprise](https://www.visualstudio.com/vs/compare/). Any of these three are fine. The collaborators presently build against community since it is free. See that you meet the [System Requirements](https://www.visualstudio.com/en-us/productinfo/vs2017-system-requirements-vs) for Visual Studio for best interaction.
* When installing Visual Studio (or if you've already installed but missed these components, go to the installer),
    - Under the Workloads tab, select **.NET desktop development**
    - Under the Individual Components tab, select .NET Framework 4.7 SDK and .NET Framework 4.7 targeting pack if they weren't already selected.
    - Under the _Code Tools_ section (still in Individual Components tab), select **Git for Windows** and **GitHub extension for Visual Studio**.
* Click Install and let the installer do it's thing.
* Clone our repository and open [src/SnesXM.sln](src/SnesXM.sln) in Visual Studio.
* Hit `F5` to Build and Run and you should be all set!

# Contributions
Do you want to add a feature, report a bug, or propose a change to MushROMs? That's awesome! First, please refer to our [Contributing](CONTRIBUTING.md) file. We use it in hopes having the best working environment we can.

# Credits
Major contributors to MushROMs
* [Nelson Garcia](https://github.com/bonimy) - Project lead and main programmer

# License
SnesXM: A Super NES Emulator in C#
Copyright (C) 2018 Nelson Garcia

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program. If not, see http://www.gnu.org/licenses/.
