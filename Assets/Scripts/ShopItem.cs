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
        private Text m_TextID;
        private Text m_TextName;
        private Text m_TextCoinToBuy;

        private ItemData m_Data;

        private void Awake()
        {
            m_Button = transform.Find("Button").GetComponent<Button>();
            m_Image = transform.GetComponent<Image>();
            m_TextID = transform.Find("Text_ID").GetComponent<Text>();
            m_TextName = transform.Find("Text_Name").GetComponent<Text>();
            m_TextCoinToBuy = transform.Find("Text_CoinToBuy").GetComponent<Text>();

            m_Button.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            Debug.Log("OnClickButton() called");
        }

        public void SetData(ItemData data)
        {
            m_Data = data;

            m_TextID.text = "ID : " + m_Data.id.ToString();
            m_TextName.text = m_Data.name_en;
            m_TextCoinToBuy.text = m_Data.coin_buy.ToString();
        }

        public void SetSprite(Sprite spriteItem)
        {
            m_Image.sprite = spriteItem;
        }
    }
}