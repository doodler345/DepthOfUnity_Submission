# DepthOfUnity_Submission

Hochschule Darmstadt - Animation & Game (b.a.)
DephtOfUnity-Elective Submission (02.2025)
Lukas Salewsky (Matr. Nr. 1117683)

-------------------------------------------------------------------------------------------------------------------------------------
Setup (Unity):

1. Add Naughty Attributes package (v2.1.4) (https://github.com/dbrizov/NaughtyAttributes.git#v2.1.4) (Dependency required especially for SaveSystem)
2. Add Submission package (https://github.com/doodler345/DepthOfUnity_Submission.git#upm)
3. Import Sample "SubmissionSamples" and have a first look into the SampleScene

-------------------------------------------------------------------------------------------------------------------------------------
SaveSystem:
	
- SaveSystem.cs (Static class) handles saving/reading
- SaveObject.cs (MonoBehaviour) represents a single SaveFile (multiple SaveObjects are possible). 
	- Here the user can:
		- Set the SaveFileName
		- Set saveable Data (Single Data/List Data) with individual ValueNames
		- Set corresponding default values (if values get reset)
	- Possible data-Types: bool, int, float, string

- If a SaveObject reads in data the first time and wasn't saved yet, a save-file will be automatically generated. 
	Reading happens in the SaveObjects Start-Callback.
- Savefiles are saved to %AppData%/LocalLow/<CompanyName>/<ProjectName>/<SaveFileName>.save

Used Editor-Scripts:

- SaveReaderOnSelect.cs 
	- Reads in the data into the SaveObject as soon as the holding GameObject gets selected in Editor
- SaveObjectEditor.cs 
	- Draws Inspector-Buttons for Methods (Read, Write, Reset All)
	- Draws Inspector-HelpBoxes if SaveFileName or ValueName is empty

-------------------------------------------------------------------------------------------------------------------------------------
HelperComponents:

- BlinkingCanvasGroup.cs
	- Makes a (required) CanvasGroups alpha-value go up-and-down over time to make UI-Objects blink
	- User can set the blinkSpeed.

- GizmoDebugger.cs
	- Draws the selected GizmoType (Cube, Sphere, Line, Ray, WireCube, WireSphere)
	- Properties like color, size and originPositionOffset can be set by the user

Used Editor-Scripts:

- BlinkingCanvasGroupEditor.cs
	- Draws a Inspector-Progressbar, which goes up-and-down like the CanvasGroup alpha-value to preview the blinking speed during runtime.
	- Unfortuanetly this made me realize that OnInspectorGUI only gets called when moving the mouse inside the Inspector. So you have to move the mouse to see the effect.

- GizmoDebuggerEditor.cs
	- Redraws the whole Inspector manually to make conditional drawing of PropertyFields possible if their needed. (E.g. "radius" is only needed for Sphere-Gizmos)
	
-------------------------------------------------------------------------------------------------------------------------------------
ExtentionMethods:

I made a little interpolation-library using extention-methods.
Parameters for the methods are usually
	- targetValue (to interpolate towards)
	- duration
	- EaseType (see description below)
	- callback (called when the interpolation finished)

- InterpolateExtentionMethods.cs 
	- Transform:
		- INTERPMoveTo moves the object  
		- INTERPRotateTo rotates the object 
	- float:
	- Vector3:
	- Color:
		- INTERPOverTime changes the value
		
- InterpolateEase.cs (Scriptable Singleton) stores globally accessible EaseTypes (AnimationCurves)
	- Linear
	- InSine
	- OutSine
	- InOutSine
	- More can be easily added by the user
	
	