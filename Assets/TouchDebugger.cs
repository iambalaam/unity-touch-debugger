using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchDebugger : MonoBehaviour
{
    [SerializeField] private Text text;
    private int _enhancedTouchUp;
    private int _eventTouchUp;
    private int _buttonPresses;

    public void SetFrameRate(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void EnableEnhancedTouch()
    {
        while (!EnhancedTouchSupport.enabled)
        {
            EnhancedTouchSupport.Enable();
        }
    }

    public void DisableEnhancedTouch()
    {
        while (EnhancedTouchSupport.enabled)
        {
            EnhancedTouchSupport.Disable();
        }
    }

    public void ButtonPress()
    {
        _buttonPresses++;
    }

    private void Start()
    {
        SetFrameRate(2);
        EnableEnhancedTouch();

        // Event based
        Touch.onFingerUp += _ => _eventTouchUp++;
    }

    private void Update()
    {
        UpdateInput();
        UpdateCanvas();
    }

    private void UpdateInput()
    {
        // Enhanced
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Ended) _enhancedTouchUp++;
        }
    }

    private void UpdateCanvas()
    {
        text.text =
            $"Enhanced Touch Up: {_enhancedTouchUp}\n" +
            $"EventUp: {_eventTouchUp}\n" +
            $"Button Presses: {_buttonPresses}\n";
    }
}
