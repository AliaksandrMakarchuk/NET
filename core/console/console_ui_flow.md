# console_ui
## Console UI manager
### Models
#### Window
Is a container of [window areas][window_area]
##### Main (screen)
##### Pop-up
#### Window area
Can handle user input - base on specific Flag
Contains related InputManager
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
User press key -> **ConsoleUIManager** handles the input -> Gets an [active window][active_window] -> Resend key into related **InputManager** -> Handle input

## Dictionary
<a name="active_window">**Active window**</a> - A window wich should handle user input
<a name="window_area"></a>
<a name="input_manager"></a>

## UML
[wiki](https://github.com/jaime-olivares/yuml-diagram/wiki)

[active_window]: #active_window "A window wich should handle user input"
[window_area]: #window_area
[input_manager]: #input_manager