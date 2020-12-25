# Console UI manager
## Contains
- Windows Manager
- Input Manager

## Responsibility
- Manages windows: main, prompts, etc.
- Manages user input, like send input to correct prompt, etc.
- Manages menu, prompts and other UI/UX elements
- Start/stop UI tasks

# Windows Manager
- Contains references to all available windows: main + prompts
- Contains id of an **Active** window

# Input Manager
- Contains EventHandlers for a required inputs
- Send user input to a corresponding window