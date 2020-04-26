using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CentralServer.LobbyServer.Character
{
    class SkinHelper
    {
        Dictionary<CharacterType, Dictionary<int, Dictionary<int, List<int>>>> CharacterSkins;

        const string FileName = "SkinData.json";

        public SkinHelper() 
        {
            if (File.Exists(FileName))
            {
                string jsonData = File.ReadAllText(FileName);
                CharacterSkins = JsonConvert.DeserializeObject<Dictionary<CharacterType, Dictionary<int, Dictionary<int, List<int>>>>>(jsonData);
            }
            else
            {
                CharacterSkins = new Dictionary<CharacterType, Dictionary<int, Dictionary<int, List<int>>>>();
            }
        }

        public void Save()
        {
            File.WriteAllText(FileName, JsonConvert.SerializeObject(CharacterSkins, Formatting.Indented));
        }

        public bool HasSkins(CharacterType character)
        {
            if (CharacterSkins.ContainsKey(character)) {
                return true;
            }
            return false;
        }

        public Dictionary<int, Dictionary<int, List<int>>> GetSkins(CharacterType character)
        {
            return CharacterSkins[character];
        }

        void GetSkinDictionaryForCharacter(CharacterType character)
        {
            if (!CharacterSkins.ContainsKey(character))
            {
                CharacterSkins.Add(character, new Dictionary<int, Dictionary<int, List<int>>>());
            }
        }
        void GetPatternForSkinID(CharacterType character, int skinID)
        {
            GetSkinDictionaryForCharacter(character);

            if (!CharacterSkins[character].ContainsKey(skinID))
            {
                CharacterSkins[character].Add(skinID, new Dictionary<int, List<int>>());
            }
        }

        void GetColorForPatternID(CharacterType character, int skinID, int patternID)
        {
            GetPatternForSkinID(character, skinID);
            if (!CharacterSkins[character][skinID].ContainsKey(patternID))
            {
                CharacterSkins[character][skinID].Add(patternID, new List<int>());
            }
            
        }

        public void AddSkin(CharacterType character, int skinID, int patternID, int colorID)
        {
            GetColorForPatternID(character, skinID, patternID);

            if (!CharacterSkins[character][skinID][patternID].Contains(colorID))
            {
                CharacterSkins[character][skinID][patternID].Add(colorID);
            }
        }
    }
}
