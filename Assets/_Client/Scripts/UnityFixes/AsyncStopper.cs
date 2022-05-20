using System;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace CrowdT.Utils
{
    public class AsyncStopper : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            var constructor = SynchronizationContext
                              .Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                                                                new Type[] {typeof(int)}, null);
            var newContext = constructor.Invoke(new object[] {Thread.CurrentThread.ManagedThreadId});
            SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);
#endif
        }
    }
}