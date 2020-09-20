using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    public class Panel_Top : MonoBehaviour
    {
        private Button m_ButtonShop;

        private void Awake() 
        {
            m_ButtonShop = transform.Find("Button_Shop").GetComponent<Button>();

            m_ButtonShop.onClick.AddListener(OnClickShop);
        }

        private void OnClickShop()
        {
            PopupManager.Instance.ShowShop();
        }
    }
}
