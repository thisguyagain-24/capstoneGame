I am too lazy to butcher the gitignore to track these properly, so you gotta run these in the repo in order to properly track these two files. 
Probably do it AFTER opening the project in Unity once, because I assume the PackageManager will overwrite these files when installing the InputSystem package. 

git add -f "Library/PackageCache/com.unity.inputsystem@1.14.2/InputSystem/Plugins/PlayerInput/PlayerInputManagerEditor.cs"

git add -f "Library/PackageCache/com.unity.inputsystem@1.14.2/InputSystem/Plugins/PlayerInput/PlayerInputManager.cs"
