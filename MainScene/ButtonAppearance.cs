using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAppearance : MonoBehaviour
{
    public Sprite m_Locked;
    public Sprite m_Unlocked;

    // set button to be activated or not
    public void ChangeAppearance(bool _lockStatus)
    {
        // if is locked, button cant be clicked || if not locked, can be clicked
        GetComponent<Button>().interactable = !_lockStatus;

        if (_lockStatus)
        {
            GetComponent<Image>().sprite = m_Locked;
        }
        else
        {
            GetComponent<Image>().sprite = m_Unlocked;
        }
    }
}
