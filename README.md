A development tool with voice control. Initially geared toward running commands via a VS14 command prompt.

###### Example Usage
Click Listen, then say: *"computer, build notepad"*

###### Configuration
Add new commands via the commands.json file that is created in the app directory of the .exe

```
[
  {
    "Key": "Notepad",
    "Utterance": "Notepad",
    "ConfirmationText": "Running the test command that will launch notepad.",
    "Executable": "notepad.exe",
    "InitialDirectory": "C:\\Windows"
  }
]

```

- Key: unique key among the commands
- Utterance: The word identifying the command.
- Confirmation Text: Read back before the command is executed.
- Executable: The command to run within InitialDirectory. Could be an executable, script etc... For example: 'msbuild SomeSolution.sln'
- InitialDirectory: Iniitial directory for the command


###### Wishlist

- _ Nice UI
- _ Live in and activate via the system tray
- _ Display loaded commands and descriptions.
- _ Enable continuous listen mode
- _ Enable different execution contexts beyond cmd window
- - powershell, cmder, alternate VS environments
- - User configurable
- _ Expand range of commands. e
- - .g. build, run, launch etc...
- - User configurable
- _ Distributed execution across machines (akka)
- _ Kick off CI builds
- ++x++ Enable test mode.


###### Icons

Icons made by [freepik](http://www.flaticon.com/authors/freepik) from www.flaticon.com 

[Mic1](http://www.flaticon.com/free-icon/microphone_189821#term=voice&page;=1&position;=87)
[Mic2](http://www.flaticon.com/free-icon/microphone_176567#term=voice&page;=3&position;=58)



