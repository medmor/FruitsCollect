using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputEventListener : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the InputSystem.onEvent callback
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (device.name != " f51a)") { return; }
        if (eventPtr.IsA<StateEvent>() || eventPtr.IsA<DeltaStateEvent>())
        {
            // Iterate through all controls on the device to find which control was affected by this event
            foreach (var control in device.allControls)
            {
                // Check if the control's state was updated by this event
                if (control.HasValueChangeInEvent(eventPtr))
                {
                    Debug.Log($"Device: {device.name}, Control: {control.name}, Value: {control.ReadValueAsObject()}");
                }
            }
        }
    }
}
