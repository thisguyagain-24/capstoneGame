I am too lazy to butcher the gitignore to track these properly, so these commands need to be run in the root folder of the repo in order to properly track these two files. 
Open the project in Unity first, then run these commands. Not sure if a pull will be necessary after the third command. 

git add -f "Library/PackageCache/com.unity.inputsystem@1.14.2/InputSystem/Plugins/PlayerInput/PlayerInputManagerEditor.cs"

git add -f "Library/PackageCache/com.unity.inputsystem@1.14.2/InputSystem/Plugins/PlayerInput/PlayerInputManager.cs"

git restore
