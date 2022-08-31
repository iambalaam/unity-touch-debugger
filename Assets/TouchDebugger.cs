using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchDebugger : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] public int touchFrames;
    [SerializeField] public int enhancedTouchFrames;
    [SerializeField] public int buttonPresses;

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
        buttonPresses++;
    }

    private void Start()
    {
        SetFrameRate(2);
        EnableEnhancedTouch();
    }

    private void Update()
    {
        UpdateInput();
        UpdateCanvas();
    }

    private void UpdateInput()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.isInProgress) enhancedTouchFrames++;
        }
        
        if (Touchscreen.current == null) return;
        foreach (var touch in Touchscreen.current.touches)
        {
            var phase = touch.phase.ReadValue();
            if (phase == TouchPhase.Began || phase == TouchPhase.Moved)
            {
                touchFrames++;
            }
        }
    }
    
    private void UpdateCanvas()
    {
        text.text =
            $"Touch Frames: {touchFrames}\n" +
            $"Enhanced Touch Frames: {enhancedTouchFrames}\n" +
            $"Button Presses: {buttonPresses}";
    }
}
