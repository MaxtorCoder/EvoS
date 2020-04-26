using EvoS.Framework.Assets;
using EvoS.Framework.Network.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Game
{
    class GameAssetsManager
    {
        private static AssetLoader Resources;

        private static void LoadResources()
        {
            if (Resources == null)
            {
                Resources = new AssetLoader();
                Resources.LoadAsset("resources.assets");
            }
        }
        
        public static GameObject GetCharacter(CharacterType characterType)
        {
            LoadResources();
            Resources.ConstructCaches();
            //Resources.ClearCache();

            return Resources.NetObjsByName[characterType.ToString()].Instantiate();
        }
    }
}
