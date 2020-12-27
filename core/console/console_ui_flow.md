# console_ui
## Console UI manager
### Models
#### Window
Contains:
- several [window areas][window_area]
- zero/one [input manager][]
##### Main (screen)
##### Pop-up
#### Window area
#### InputManager
#### Command

### Contains
- Windows Manager
- Input Manager

### Responsibility
- Manages windows: main, prompts, etc.
- Manages user input, like send input to correct prompt, etc.
- Manages menu, prompts and other UI/UX elements
- Start/stop UI tasks

## Windows Manager
- Contains references to all available windows: main + prompts
- Contains id of an [**Active**][active_window] window

## Input Manager
- Contains EventHandlers for a required inputs
- Send user input to a corresponding window

## Flow
User press key -> **ConsoleUIManager** handles the input -> Gets an [active window][active_window] -> Fire event into inside **InputManager**

## Dictionary
<a name="active_window">**Active window**</a> - A window wich should handle user input

[active_window]: #active_window "A window wich should handle user input"