using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPStudios.Tools;

namespace CommonShop
{
    /// <summary>
    /// Resources 폴더 내에 있는 에셋 파일들을 불러옵니다.
    /// </summary>
    public class ResourcesLoader : MonoSingleton<ResourcesLoader>
    {
        public List<Sprite> m_ShopSpritesList = new List<Sprite>();

        public ShopData m_ShopData;

        private void Awake()
        {
            LoadShopItemSprites();
        }

        /// <summary>
        /// 상점 아이템 스프라이트들을 불러옵니다.
        /// </summary>
        public void LoadShopItemSprites()
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/ShopItem") as Sprite[];

            foreach (Sprite sp in sprites)
            {
                m_ShopSpritesList.Add(sp);
            }
        }
    }
}