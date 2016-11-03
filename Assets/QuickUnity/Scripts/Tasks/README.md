# **Task System**

## **Coroutine Task**

Unity provide a coroutine system that allow you can do something asynchronously. But you just can use it in the class who is inherited from class **MonoBehaviour**. So **Coroutine Task** can help you using coroutine anywhere. Simple usage like this:

```c#
using QuickUnity.Tasks;
using System.Collections;

public class CoroutineTaskSample
{
	public CoroutineTaskSample()
    {
    	ICoroutineTask task = new CoroutineTask(CoroutineFunc());
    }
  
  	private IEnumerator CoroutineFunc()
    {
      // Your logic codes here.
    }
}
```