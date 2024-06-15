using UnityEngine;

namespace Assets.Scripts.Tetris
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSystem : MonoBehaviour, ISound
    {
        [Header("Effect")]
        [SerializeField] private AudioSource _effectSource;
        [Header("Main themes")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip[] _mainThemes;

        private ISaveController _settings;
        private IScore _score;

        public void Initialize(IScore score, ISaveController settings)
        {
            _score = score;
            _settings = settings;

            _score.ChangeLinesEvent += (count) =>
            {
                if (count > 0)
                {
                    PlayClearLines();
                }
            };

            _settings.OnChangeEffectEvent += OnMuteEffect;
            _settings.OnChangeMusicEvent += OnMuteMusic;
        }

        public void PlayMainTheme()
        {
            _musicSource.clip = _mainThemes[0];
            _musicSource.Play();
        }

        public void PlayClearLines()
        {
            _effectSource.Play();
        }

        private void OnMuteEffect(bool isMute)
        {
            _effectSource.mute = !isMute;
        }

        private void OnMuteMusic(bool isMute)
        {
            _musicSource.mute = !isMute;
        }
    }
}