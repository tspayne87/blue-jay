# IEvent&lt;T&gt; Interface
Event object that is pasted to the event listeners to be processed it houses the event data that
triggered the event in the first place, if the event was completed, the target that triggered the
event if one exists, and a way to stop processing the event for any listeners pasted this point

## Name
This is the current name of the event that was triggered, this will usually be the name of the class

## IsComplete
A boolean determining if this event has been completed or not and weather it should keep processing
event listeners

## Target
The current target object that triggered this event

## Data
The data that triggered this event

## StopPropagation
It will stop processing any event listeners that were registered after this current event listener

```csharp
    void StopPropagation();
```