using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KetchappTools.TouchInput
{
    public class InputExample : BaseTouchInput
    {
        public Transform inputPanel_prefab;
        public Transform container;
        public GameObject tapdislay;

        private Dictionary<int, Transform> panels = new Dictionary<int, Transform>();


        protected override void OnDown(FingerTouch finger)
        {
            Transform instance = Instantiate(inputPanel_prefab, container);
            panels.Add(finger.id, instance);
            LogInputInfo(finger, instance);
        }

        protected override void OnHeld(FingerTouch finger)
        {
            LogInputInfo(finger, panels[finger.id]);
        }

        protected override void OnUp(FingerTouch finger)
        {
            Destroy(panels[finger.id].gameObject);
            panels.Remove(finger.id);

        }

        protected override void OnTap(FingerTouch finger)
        {
            //Debug.Log("Detect tap");
            GameObject tap = Instantiate(tapdislay, transform);
            Destroy(tap, 0.1f);

        }


        private void LogInputInfo(FingerTouch finger, Transform target)
        {
            target.GetChild(0).GetComponent<TMP_Text>().text = finger.id.ToString();
            target.GetChild(1).GetComponent<TMP_Text>().text = finger.isOverUI.ToString();
            target.GetChild(2).GetComponent<TMP_Text>().text = finger.diff.magnitude.ToString("0.00");
            target.GetChild(3).GetComponent<TMP_Text>().text = finger.holdDuration.ToString("0.00");
        }


    }
}