using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

namespace KetchappTools.SimpleFeedbacks
{
    [System.Serializable]
    public class Feedback
    {
        public string name = "";
        public AudioClip[] audioClips;
        public float volume = 1f;
        public float spatialBlend = 1f;
        public Transform vfxPrefab = null;
        public HapticTypes hapticType = HapticTypes.None;
        public bool screenshake = false;
    }

    public class Feedbacks : MonoBehaviour
    {
        public static Feedbacks Instance { get; private set; }

        public Transform vfxContainer;
        public Transform audioSourcesContainer;
        public int nbAudioSources = 5;
        public Transform audioSourcePrefab;
        public float minSoundInterval = 0.1f;
        public Feedback[] fxParams;

        bool _hapticEnable = true;
        public bool HapticEnable
        {
            get { return _hapticEnable; }
            set { _hapticEnable = value; }
        }

        bool _soundEnable = true;
        public bool SoundEnable
        {
            get { return _soundEnable; }
            set { _soundEnable = value; }
        }

        AudioSource[] _audioSources;
        int _currentAudioSourceIndex = -1;



        /// <summary>
        /// Awake
        /// </summary>
        /// 
        void Awake()
        {
            Instance = this;
        }


        /// <summary>
        /// Start
        /// </summary>
        /// 
        void Start()
        {
            _audioSources = new AudioSource[nbAudioSources];

            for (int i = 0; i < nbAudioSources; i++)
            {
                Transform tempTransform = Instantiate(audioSourcePrefab) as Transform;
                tempTransform.SetParent(audioSourcesContainer);
                tempTransform.position = Vector3.zero;
                _audioSources[i] = tempTransform.GetComponent<AudioSource>();
            }
        }


        /// <summary>
        /// EnableSound
        /// </summary>
        /// <param name="value"></param>
        /// 
        public static void EnableSound(bool value)
        {
            Instance._soundEnable = value;
        }


        /// <summary>
        /// EnableSound
        /// </summary>
        /// <param name="value"></param>
        /// 
        public static void EnableHaptic(bool value)
        {
            Instance._hapticEnable = value;
        }


        /// <summary>
        /// Play
        /// </summary>
        /// <param name="type"></param>
        public static void Play(string name, Vector3 position, Vector3 scale, Transform parentTransform = null)
        {
            Feedback fxParams = Instance.ParamsForName(name);

            if (fxParams != null)
            {
                // Play audio
                if (Instance._soundEnable)
                {
                    if (fxParams.audioClips.Length > 0)
                    {
                        int initAudioSourceIndex = Instance._currentAudioSourceIndex;
                        Instance._currentAudioSourceIndex++;
                        if (Instance._currentAudioSourceIndex >= Instance.nbAudioSources - 1) Instance._currentAudioSourceIndex = 0;
                        AudioSource audioSource = Instance._audioSources[Instance._currentAudioSourceIndex];

                        if (!audioSource.isPlaying
                            || audioSource.time >= Instance.minSoundInterval)
                        {
                            audioSource.Stop();
                            audioSource.clip = fxParams.audioClips[Random.Range(0, fxParams.audioClips.Length)];
                            audioSource.volume = fxParams.volume;
                            audioSource.spatialBlend = fxParams.spatialBlend;
                            audioSource.transform.position = position;
                            audioSource.Play();
                        }
                        else
                        {
                            Instance._currentAudioSourceIndex = initAudioSourceIndex;
                        }
                    }
                }

                // Haptic
                if (Instance._hapticEnable)
                {
                    if (fxParams.hapticType != HapticTypes.None)
                    {
                        MMVibrationManager.Haptic(fxParams.hapticType);
                    }
                }

                // Play VFX
                if (fxParams.vfxPrefab != null)
                {
                    Transform tempTransform = Instantiate(fxParams.vfxPrefab) as Transform;
                    if (parentTransform == null) tempTransform.SetParent(Instance.vfxContainer);
                    else
                    {
                        tempTransform.SetParent(parentTransform);
                        tempTransform.localEulerAngles = Vector3.zero;
                    }
                    tempTransform.position = position;
                    if (scale != Vector3.zero) tempTransform.localScale = scale;
                }

                // Screenshake
                if (fxParams.screenshake) Screenshaker.Instance?.Play();
            }
        }


        /// <summary>
        /// GetClipForType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// 
        Feedback ParamsForName(string name)
        {
            for (int i = 0; i < fxParams.Length; i++) if (name == fxParams[i].name) return fxParams[i];
            return null;
        }
    }
}
