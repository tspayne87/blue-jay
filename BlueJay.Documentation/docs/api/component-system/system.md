# Systems
Systems are the main implmenentation of the game, these objects are where entities go to be processed and
drawn to the screen

## ISystem interface
The system interface is the based interface that all other systems will implement from

### Key
The key is the main way the system knows what entities it should be processing, meaning it is a type of filter.  Using the
[KeyHelper.Create<...>](/api/component-system/key-helper) will help with merging addons together to create a key to filter
out entities that should not be processed by this system.
### Layers
The [layers](/api/component-system/layer) act as a way to filter the system out even further based on the layer this system
should be processing on.

### IUpdateSystem
Will be called before any entities are processed by this system, this acts as a starting point before any of the entities
are processed by this system.  Can only be called once each frame.

### IUpdateEntitySystem
Will be called for each entity that meets the systems criteria and can be called multiple times in one frame.

### IUpdateEndSystem
Will be called after all the entities are processed and should handle the cleanup after each entity is processed.  Can only
be called once each frame

### IDrawSystem
Will be called before any entities are processed, in a draw system that is mainly reserved for starting a batch sprite batch so
your not processing one entity at a time. Can only be called once each frame.

### IDrawEntitySystem
Will be called for each entity that meets the systems criteria and can be called multiple times in one frame.  This would be where
drawing each entity to the screen would happen.

### IDrawEndSystem
Will be called after all entities are processed, this would be where you could end the sprite batch and send the data along to be
renderered.  Can only be called once each frame.


