# NGUnidle

A quick mod to make the game NGU Idle a little more interactive.

## Developing

I was lazy and didn't set any scripts up to make dev faster. Sorry! Here's
how to get started:

1. Copy `Assembly-CSharp.dll` and `UnityEngine.UI.dll` from the game folder into
   the `lib/` folder
2. Download bepinex here, and unzip it to the root NGU Idle directory:
   https://github.com/BepInEx/BepInEx/releases/download/v5.4.21/BepInEx_x64_5.4.21.0.zip
3. Run the game to create the BepInEx plugin directory
4. Update `install.py` to point to the newly generated plugin directory
5. Run `python install.py run` to test out the mod.

## Installation

Just unzip the release file into your game directory. That's it!
