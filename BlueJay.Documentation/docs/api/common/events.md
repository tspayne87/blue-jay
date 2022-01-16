# Events
This is a list of events that could be attached to process logic based on the event
triggers

## GamePadButtonDownEvent
The event that will trigger when a button is pressed down on the keypad

### Type
The type of button that was pressed

### Index
The current player index that triggered this down event

## GamePadButtonUpEvent
The event that will trigger when a button is pressed up on the keypad

### Type
The type of button that was pressed

### Index
The current player index that triggered this down event

## GamePadStickEvent
The event is triggered when the player moves either stick around on the game pad

### Value
The current value that the controllers stick is been changed

### PreviousValue
The previous value for this event to calculate a delta between the current and previous

### Type
The thumb stick that was used to trigger this event

### Index
The current player index that triggered this down event

## GamePadTriggerEvent
The event is triggered when a player starts to press on the back triggers on the game pad

### Value
The current value that the controllers back trigger is been changed

### PreviousValue
The previous value for this event to calculate a delta between the current and previous

### Type
The thumb stick that was used to trigger this event

### Index
The current player index that triggered this down event

## KeyboardDownEvent
The event is triggered when a key is pressed down on the keyboard

### Key
The current key that has been pressed

### CapsLock
Determines if the caps lock has been toggled or not

### NumLock
Determines if the num lock has been toggled or not

### Shift
Determines if a shift key is being held

### Ctrl
Determines if a control key is being held

### Alt
Determines if the alt key is being held

## KeyboardUpEvent
The event is triggered when a key is pressed up on the keyboard

### Key
The current key that has been pressed

### CapsLock
Determines if the caps lock has been toggled or not

### NumLock
Determines if the num lock has been toggled or not

### Shift
Determines if a shift key is being held

### Ctrl
Determines if a control key is being held

### Alt
Determines if the alt key is being held

## KeyboardPressEvent
The event is triggered when a key is pressed on the keyboard meaning a down and up have occured for the key

### Key
The current key that has been pressed

### CapsLock
Determines if the caps lock has been toggled or not

### NumLock
Determines if the num lock has been toggled or not

### Shift
Determines if a shift key is being held

### Ctrl
Determines if a control key is being held

### Alt
Determines if the alt key is being held

## MouseDownEvent
The event is triggered when the mouse button is pressed down

### Position
The current position of the mouse in the games window

### Button
If there is a button being pressed at the moment

## MouseUpEvent
The event is triggered when the mouse button is pressed up

### Position
The current position of the mouse in the games window

### Button
If there is a button being pressed at the moment

## MouseMoveEvent
The event is triggered when the mouse moves on the screen

### Position
The current position of the mouse in the games window

### Button
If there is a button being pressed at the moment

### PreviousPosition
The previous position of the mouse that the delta can be calculated from

## ScrollEvent
The event is triggered when the scroll happens from the mouse

### ScrollWheelValue
The current wheel value for the scroll wheel

### PreviousScrollWheelValue
The previous scroll wheel position for the mouse

## TouchDownEvent
The event is triggered when a user starts to touch the screen on a phone

### Position
The position of the touch on the screen

### Pressure
The current pressure that is being applied to the screen

## TouchUpEvent
The event is triggered when a user stops to touch the screen on a phone

### Position
The position of the touch on the screen

### Pressure
The current pressure that is being applied to the screen

## TouchMoveEvent
The event is triggered when a user starts to move around on the phone after touching it

### Position
The position of the touch on the screen

### Pressure
The current pressure that is being applied to the screen

## ViewportChangeEvent
The viewport change event occurs when the width/height changes for the viewport

### Current
The current size of the viewport

### Previous
The previous size of the viewport to calculate the delta from
