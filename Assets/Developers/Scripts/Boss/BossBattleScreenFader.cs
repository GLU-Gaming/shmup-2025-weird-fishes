using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BossBattleScreenFader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private TMP_Text _textElement;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _newBackground;
    [SerializeField] private AudioSource _bossMusic;
    [SerializeField] private BossBattle _bossBattle;

    [Header("Timing")]
    [SerializeField] private float _fadeInDuration = 1f;
    [SerializeField] private float _displayDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 1f;
    [SerializeField] private float _musicDropTime = 3f;

    private Color _originalScreenColor;
    private Vector3 _originalTextScale;

    void Start()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        _originalScreenColor = Color.white;
        _fadeScreen.color = Color.clear;
        _textElement.color = new Color(_textElement.color.r, _textElement.color.g, _textElement.color.b, 0);
        _boss.SetActive(false);
        _newBackground.SetActive(false);
    }

    public void StartBossFade()
    {
        StartCoroutine(BossBattleSequence());
    }

    IEnumerator BossBattleSequence()
    {
        yield return new WaitForSeconds(_musicDropTime);

        yield return StartCoroutine(FadeScreen(0, 1, _fadeInDuration));
        yield return new WaitForSeconds(_displayDuration);

        _newBackground.SetActive(true);
        _boss.SetActive(true);
        //_bossBattle.StartBoss();

        yield return StartCoroutine(FadeScreen(1, 0, _fadeOutDuration));
    }

    IEnumerator FadeScreen(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        Color startColor = new Color(_originalScreenColor.r, _originalScreenColor.g, _originalScreenColor.b, startAlpha);
        Color endColor = new Color(_originalScreenColor.r, _originalScreenColor.g, _originalScreenColor.b, endAlpha);

        while (timer < duration)
        {
            float progress = timer / duration;
            _fadeScreen.color = Color.Lerp(startColor, endColor, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        _fadeScreen.color = endColor;
    }
}
