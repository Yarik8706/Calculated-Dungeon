
using System;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public string levelPassed = "";
        public float[] records = new float[3];
        public bool isIntroWas;
        
        public SavesYG()
        {
            
        }
    }
}
