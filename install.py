import sys
import os
import shutil
import subprocess

os.system('dotnet build')
shutil.copy2(
    'bin/Debug/net46/MyFirstPlugin.dll',
    'D:/Program Files (x86)/Steam/steamapps/common/NGU IDLE/BepInEx/plugins/')
if 'run' in sys.argv:  
    subprocess.run('D:/Program Files (x86)/Steam/steamapps/common/NGU IDLE/NGUIdle.exe')