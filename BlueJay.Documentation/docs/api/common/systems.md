# Systems
A list of systems that can be used by games to help bootstrap the process of making games

### ClearSystem
The system will clear the screen to a specific color, this color should be pushed into the arguments whe
adding the system to the service provider.

```csharp
  serviceProvider.AddSystem<ClearSystem>(Color.White); // Every frame the screen will be cleared with white
```

### DebugSystem
The debug system will render debug information onto the screen for each entity that has the [DebugAddon](/api/common/addons) attach to them

```csharp
  serviceProvider.AddSystem<DebugSystem>("Default"); // The name of the sprite font that has been added to the font collection
```

### FPSSystem
The fps system will render the current fps that the system is currently doing

```csharp
  serviceProvider.AddSystem<FPSSystem>("Default"); // The name of the sprite font that has been added to the font collection
```

### GamepadSystem
The gamepad system that needs to be added before gamepad events will be triggered in the system.  Its main job is to watch the
gamepad and send out events to be processed by the system.

```csharp
  serviceProvider.AddSystem<GamepadSystem>();
```

### KeyboardSystem
The keyboard system that needs to be added before keyboard events will be triggered in the system.  Its main job is to watch the
keyboard and send out events to be processed by the system.

```csharp
  serviceProvider.AddSystem<KeyboardSystem>();
```

### MouseSystem
The mouse system that needs to be added before mouse events will be triggered in the system.  Its main job is to watch the mouse
and send out events to be processed by the system.

```csharp
  serviceProvider.AddSystem<MouseSystem>();
```

### RenderingSystem
The basic rendering system that will watch for entities that have [PositionAddon](/api/common/addons) and a [TextureAddon](/api/common/addons)
and render those entities to the screen

```csharp
  serviceProvider.AddSystem<RenderingSystem>();
```

### TouchSystem
The touch system that needs to be added before touch events will be triggered in the system.  Its main job is to watch the touch
and send out events to be processed by the system.

```csharp
  serviceProvider.AddSystem<TouchSystem>();
```

### VelocitySystem
The velocity system is meant to move entities around on the screen based on the velocity, it will take all entities that have a
[PositionAddon](/api/common/addons) and a [VelocityAddon](/api/common/addons) and move the position based on the velocity.

```csharp
  serviceProvider.AddSystem<VelocitySystem>();
```

### ViewportSystem
The viewport system that needs to be added before viewport events will be triggered in the system.  Its main job is to watch the window
and send out events to be processed by the system.

```csharp
  serviceProvider.AddSystem<ViewportSystem>();
```