using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    public class Panel_Top : MonoBehaviour
    {
        private Button m_ButtonShop;
        //public Button m_Button;

        private void Awake() 
        {
            m_ButtonShop = transform.Find("Button_Shop").GetComponent<Button>();

            m_ButtonShop.onClick.AddListener(OnClickShop);
        }

        private void OnClickShop()
        {
            //Debug.LogWarning("OnClickShop() called");
            PopupManager.Instance.ShowShop();
        }
    }
}
