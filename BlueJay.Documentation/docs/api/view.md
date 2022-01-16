# View
The view is a scoped container that represents what should be processed by the game at the moment

## ConfigureProvider
Lets the view configure what systems, event listeners, and entities that need to be added to this view

```csharp
  /// @param serviceProvider: The service provider that has been created from the collection
  protected abstract void ConfigureProvider(IServiceProvider serviceProvider);
```
## Initialize
Lifecycle hook that triggers once this view has been initialized

```csharp
  /// @param serviceProvider: The current service provider we are working with
  void Initialize(IServiceProvider serviceProvider);
```
## Enter
Lifecycle hook that triggers when the player enters this view

```csharp
  void Enter();
```
## Leave
Lifecycle hook that triggers when the player leaves this view

```csharp
  void Leave();
```
## Draw
Lifecycle hook that triggers when this view needs to be drawn

```csharp
  void Draw();
```
## Update
Lifecycle hook that triggers when this view needs to be updated

```csharp
  void Update();
```
## Activate
Lifecycle hook that triggers when a player clicks back into the window after clicking away

```csharp
  void Activate();
```
## Deactivate
Lifecycle hook that triggers when a player clicks away from the window

```csharp
  void Deactivate();
```
## Exit
Lifecycle hook that triggers right before the game exits the app itself

```csharp
  void Exit();
```