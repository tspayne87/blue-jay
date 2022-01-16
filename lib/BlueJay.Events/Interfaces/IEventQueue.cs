using System;

namespace BlueJay.Events.Interfaces
{
  public interface IEventQueue
  {
    /// <summary>
    /// Helper method is meant to dispatch events, this will defer them to the next frame for the event queue and will not be processed
    /// in the same frame it is triggered
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="evt">The event that is being triggered</param>
    void DispatchEvent<T>(T evt, object target = null);

    /// <summary>
    /// Helper method is meant to add on event listeners into the system so they can interact with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="handler">The handler when the event is fired</param>
    IDisposable AddEventListener<T>(IEventListener<T> handler);

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    IDisposable AddEventListener<T>(Func<T, bool> callback);

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    IDisposable AddEventListener<T>(Func<T, object, bool> callback);

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    IDisposable AddEventListener<T>(Func<T, bool> callback, object target);

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    IDisposable AddEventListener<T>(Func<T, object, bool> callback, object target);

    /// <summary>
    /// Helper method to process the current queue
    /// </summary>
    void Update();

    /// <summary>
    /// Handle the draw event for the event queue
    /// </summary>
    void Draw();

    /// <summary>
    /// Handle the activate event for the event queue
    /// </summary>
    void Activate();

    /// <summary>
    /// Handle the deactivate event for the event queue
    /// </summary>
    void Deactivate();

    /// <summary>
    /// Handle the exit event for the event queue
    /// </summary>
    void Exit();

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
    void Tick(bool excludeUpdate = false);
  }
}
