# **Event System**

If you were a Flash developer, you will be familiar with the Event System of ActionScript 3.0(AS3). This Event System API for Unity engine is just the same as AS3.

### **EventDispatcher**

If you want to use class **EventDispatcher** to dispatch event or add listener for specific event in normal C# class. You need to create a custom **Event** class which is inherited from class **Event** under namespace **QuickUnity.Events** first. And also you need to define a event type. For example:

```c#
using QuickUnity.Events;

public class TestEvent : Event
{
    // Test event type.
    public const string Test = "Test";
  
  	public TestEvent(string eventType, object context = null)
      :base(eventType, context)
    {
    }
}
```

When you finished all things previous step, you can add a listener for this event type. For example:

```c#
IEventDispatcher dispatcher = new EventDispatcher();
dispatcher.AddEventListener<TestEvent>(TestEvent.Test, (testEvent) => 
{
  Debug.Log("TestEvent.Test was fired!");
});
```

Finnally, you can dispatch this event like this:

```c#
dispatcher.DispatchEvent<TestEvent>(new TestEvent(TestEvent.Test));
```

Then you can see debug message will be shown in console window of Unity.