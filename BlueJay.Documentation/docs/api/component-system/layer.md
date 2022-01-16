# ILayer interface
Layers are meant to handle the problem of what entities should be processed or drawn before other entities.
For instance, you could have a layer that is the background layer or the foreground layer that will draw
entities either behind or in front of other entities in an easier way.

## Entities
The [entity collection](/api/component-system/entity-collection), that has all the entities for this layer

## Id
The name of the layer

## Weight
The current weight of the layer, layers are processed in ascending order