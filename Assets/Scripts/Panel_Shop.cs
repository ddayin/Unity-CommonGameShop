using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    public class Panel_Shop : MonoBehaviour
    {
        private Text m_TextTitle;
        private Button m_ButtonClose;

        private void Awake()
        {
            m_TextTitle = transform.Find("Text_Title").GetComponent<Text>();
            m_ButtonClose = transform.Find("Button_Close").GetComponent<Button>();

            m_TextTitle.text = "Welcome to SHOP";
            m_ButtonClose.onClick.AddListener(OnClickClose);
        }

        private void OnClickClose()
        {
            //Destroy(this.gameObject);
            PopupManager.Instance.CloseShop();
        }
    }
}