using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    /// <summary>
    /// 스크롤뷰 안에 들어갈 상점 아이템
    /// </summary>
    public class ShopItem : MonoBehaviour
    {
        private Button m_Button;
        private Image m_Image;

        private void Awake()
        {
            m_Button = transform.Find("Button").GetComponent<Button>();
            m_Image = transform.GetComponent<Image>();

            m_Button.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            Debug.Log("OnClickButton() called");
        }
    }
}