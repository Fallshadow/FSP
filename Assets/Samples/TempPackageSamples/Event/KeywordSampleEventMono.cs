using System;
using fsp.evt;
using fsp.debug;
using UnityEngine;

namespace fsp.sample
{
    public class KeywordSampleEventMono : MonoBehaviour
    {
        private void OnEnable()
        {
            PrintSystem.SetOutPutLogger(PrintSystem.PrintBy.unknown);
        }

        private void Start()
        {
            EventManager.instance.Register(EventGroup.KEYWORD, (short)KeywordSampleEvent.KEYWORD_FIRE, logKeywordFireEvent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                EventManager.instance.Send(EventGroup.KEYWORD, (short)KeywordSampleEvent.KEYWORD_FIRE);
            }
        }

        private void OnDestroy()
        {
            EventManager.instance.Unregister(EventGroup.KEYWORD, (short)KeywordSampleEvent.KEYWORD_FIRE, logKeywordFireEvent);
        }

        private void logKeywordFireEvent()
        {
            PrintSystem.Log("按下了Fire键（Enter）");
        }
        
    }
}

