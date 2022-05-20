using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KetchappTools.Core.Save.Examples
{

    public class TestScript : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _debugText;
        [SerializeField]
        private ExampleSaveData _saveExample;

        [SerializeField]
        private ScriptableSaveData _saveData;
        
        private void Start()
        {
            _saveExample = ExampleSaveData.Create("playerSave");
            bool loadSuccess = _saveExample.Load();
            if(!loadSuccess)
                _saveExample.CreateDefault();

            RefreshText();
        }

        public void OnClickModifyData()
        {
            _saveExample.ChangeData();
            RefreshText();
        }

        public void OnClickSave()
        {
            _saveExample.Save();
        }

        public void OnClickLoad()
        {
            _saveExample.Load();
            RefreshText();
        }

        public void OnClickResetData()
        {
            _saveExample.CreateDefault();
            RefreshText();
        }

        private void RefreshText()
        {
            _debugText.text = _saveExample.ToText();
        }
    }
}
