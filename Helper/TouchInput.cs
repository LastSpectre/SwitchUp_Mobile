using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : Singleton<TouchInput>
{
    public float m_Deadzone;

    // Control checks
    [HideInInspector]
    public bool m_tap;
    [HideInInspector]
    public bool m_swipeUp;
    [HideInInspector]
    public bool m_swipeDown;
    [HideInInspector]
    public bool m_swipeRight;
    [HideInInspector]
    public bool m_swipeLeft;
    [HideInInspector]
    public bool m_isDraging;

    // saves the position of the touches from player
    private Vector2 m_startTouchPos;
    private Vector2 m_endTouchPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ResetStart();

        m_endTouchPos = Vector2.zero;

        MobileControl();
    }

    /// <summary>
    /// Resets all control variables
    /// </summary>
    public void ResetStart()
    {
        m_tap = false;
        m_swipeDown = false;
        m_swipeUp = false;
        m_swipeRight = false;
        m_swipeLeft = false;
    }

    /// <summary>
    /// Contains player controls for mobile device
    /// </summary>
    void MobileControl()
    {
        // If player touched the screen
        if (Input.touchCount > 0)
        {
            // if first touch
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                m_startTouchPos = Input.touches[0].position;
            }
            // if player lifted his finger up
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                m_endTouchPos = Input.touches[0].position;

                // if player is draging
                if (Mathf.Abs(m_endTouchPos.x - m_startTouchPos.x) > m_Deadzone || Mathf.Abs(m_endTouchPos.y - m_startTouchPos.y) > m_Deadzone)
                {
                    // if x movement is greater than y movement
                    if (Mathf.Abs(m_endTouchPos.x - m_startTouchPos.x) > Mathf.Abs(m_endTouchPos.y - m_startTouchPos.y))
                    {
                        // swipe to the right
                        if ((m_endTouchPos.x > m_startTouchPos.x))
                        {
                            m_swipeRight = true;
                        }
                        // swipe to the left
                        else
                        {
                            m_swipeLeft = true;
                        }
                    }
                    // y movement is greater than x movement
                    else
                    {
                        // swipe up
                        if (m_endTouchPos.y > m_startTouchPos.y)
                        {
                            m_swipeUp = true;
                        }
                        // swipe down
                        else
                        {
                            m_swipeDown = true;
                        }
                    }
                }
                // its only a tap | check if game is paused so player doesnt attack after he unpaused the game
                else
                {
                    m_tap = true;
                }
            }
        }
    }
}
