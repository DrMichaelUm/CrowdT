﻿#if CrossPromotion
using System;
#if MEDIATION_FairBid
using Fyber;
#endif
using Ketchapp.Internal;
using KetchappCrossPromotion.NativeSDK;
using UnityEngine;

namespace Ketchapp.Internal.CrossPromo
{
    public class CrossPromoCallbacks : KetchappCallbackBehaviour
    {
        public Action PromotionClosed { get; set; }
        public Action GdprClosed { get; set; }

        private string AttSavedKey => "att_optin";
        private string AttSentStatusKey => "att_sent";

        public override void OnGDPRResult(KetchappPromo.OnGDPRResultArgs args)
        {
            base.OnGDPRResult(args);
            Debug.Log("gdpr callback");
            if (args.HasBeenAccepted)
            {
                Debug.Log("Intialize Analytics after gdpr");
                KetchappInternal.Analytics.Initialize();
#if MEDIATION_MAX
                MaxSdk.SetHasUserConsent(args.HasBeenAccepted);
#elif MEDIATION_FairBid
                UserInfo.SetGdprConsent(args.HasBeenAccepted);
#elif MEDIATION_IronSource
                IronSource.Agent.setConsent(args.HasBeenAccepted);
#endif
            }

            CheckOnAttEvent();
            GdprClosed?.Invoke();
            GdprClosed = null;
        }

        public override void OnPromotionClosed()
        {
            Debug.Log("Promotion closed");
            base.OnPromotionClosed();
            CheckOnAttEvent();
            PromotionClosed?.Invoke();
            PromotionClosed = null;
        }

        private void CheckOnAttEvent()
        {
#if UNITY_IOS
            if (PlayerPrefs.GetInt(AttSentStatusKey, 0) == 0)
            {
                Debug.Log("Sending att value to analytics");
                var attValue = PlayerPrefs.GetString(AttSavedKey);
                if (!string.IsNullOrEmpty(attValue))
                {
                    KetchappInternal.Analytics.SendAttResult(attValue);
                }

                PlayerPrefs.SetInt(AttSentStatusKey, 1);
            }
#endif
        }
#if UNITY_IOS
        public override void OnTrackingResult(KetchappPromo.OnTrackingResultsArgs args)
        {
            Debug.Log($"ATTResult > ATT result is {args.SkAdNetworkResult}");

            if (args.SkAdNetworkResult != KetchappPromo.OnTrackingResultsArgs.TrackingResultEnum.ATT_DISABLED)
            {
                switch (args.SkAdNetworkResult)
                {
                    case KetchappPromo.OnTrackingResultsArgs.TrackingResultEnum.FIST_FLOW_ACCEPTED:
                        PlayerPrefs.SetString(AttSavedKey, "true");
                        break;
                    case KetchappPromo.OnTrackingResultsArgs.TrackingResultEnum.FIST_FLOW_REFUSED:
                        PlayerPrefs.SetString(AttSavedKey, "false");
                        break;
                    default:
                        {
                            PlayerPrefs.SetString(AttSavedKey, "false");
                            break;
                        }
                }
            }
        }
#endif
    }
}
#endif