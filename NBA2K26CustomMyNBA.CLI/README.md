# NBA 2K26 Custom MyNBA

This is a small application to help people create custom MyNBA teams without needing to upload images to the MyNBA website.  It utilises the `mods` capability of NBA 2K26 to replace existing textures to allow customisation/relocation of teams in NBA 2K26 to create unique teams for something different.

The aim of this app is to make it easy for people to go from creating PNG files to having them usable inside the NBA 2K26 game.

## Prerequisites

This app does not contain _everything_ it needs to run successfully.  It is dependent on two external tools to run:

+ **Texconv** - A free Microsoft command-line tool for converting images to specific DDS formats.  Download/installation instructions can be found here: https://github.com/microsoft/DirectXTex/wiki/texconv
+ **7zip** - A free universal tool for managing archives.  Needed by the app to package converted graphics into NBA2K IFF files.  7zip can be downloaded and installed from here: https://www.7-zip.org/download.html

## Using the App

### Installation

Simply download the latest published EXE from https://github.com/OzWolf/nba2k26-custom-mynba/releases and place in a folder you will be working in.

### Directory Information

When the application runs for the first time, it will create a number of directories it will need.  These are:

+ `mods` - Created alongside the exe.  This will be where the app will place generated IFF files.
+ `work` - Created alongside the exe.  This will be where the app places its converted DDS files before packing them in IFF files.
+ `teams` - Created alongside the exe.  This is the directory for you to create team folders containing source PNG images for the app to convert into usable IFF mod files.
+ `%APPDATA%\NBA2K26CustomMyNBA` - A shared directory for the app to place it's storage JSON file containing your custom mappings.  This allows you to close down the app and restart it while working and for the app to remember your choices.

### 