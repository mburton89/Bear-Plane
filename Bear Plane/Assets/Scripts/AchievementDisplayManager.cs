using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementDisplayManager : MonoBehaviour
{
    public static AchievementDisplayManager Instance;

    [SerializeField] private float _secondsToShowText;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _text2;

    private const string INTRO_TEXT = "Get 'em \nSport";
    private const string PILOT_SWAT_TEXT = "Pilot \nSwat";
    private const string DOUBLE_KILL_TEXT = "Double \nKill";
    private const string TRIPLE_KILL_TEXT = "Triple \nKill";
    private const string BLOODY_MESS_TEXT = "Bloody \nMess";
    private const string UNFRIENDLY_FIRE_TEXT = "Unfriendly \nFire";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pilotSwatClip;
    [SerializeField] private AudioClip _doubleKillClip;
    [SerializeField] private AudioClip _tripleKillClip;
    [SerializeField] private AudioClip _bloodyMessClip;
    [SerializeField] private AudioClip _unfriendlyFireClip;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowIntroText();
    }

    void ShowIntroText()
    {
        _text.SetText(INTRO_TEXT);
        _text2.SetText(INTRO_TEXT);
        _audioSource.clip = _doubleKillClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    public void ShowPilotSwat()
    {
        _text.SetText(PILOT_SWAT_TEXT);
        _text2.SetText(PILOT_SWAT_TEXT);
        _audioSource.clip = _pilotSwatClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    public void ShowDoubleKill()
    {
        _text.SetText(DOUBLE_KILL_TEXT);
        _text2.SetText(DOUBLE_KILL_TEXT);
        _audioSource.clip = _doubleKillClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    public void ShowTripleKill()
    {
        _text.SetText(TRIPLE_KILL_TEXT);
        _text2.SetText(TRIPLE_KILL_TEXT);
        _audioSource.clip = _tripleKillClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    public void ShowBloodyMess()
    {
        _text.SetText(BLOODY_MESS_TEXT);
        _text2.SetText(BLOODY_MESS_TEXT);
        _audioSource.clip = _bloodyMessClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    public void ShowUnfriendlyFire()
    {
        _text.SetText(UNFRIENDLY_FIRE_TEXT);
        _text2.SetText(UNFRIENDLY_FIRE_TEXT);
        _audioSource.clip = _unfriendlyFireClip;
        _audioSource.Play();
        StartCoroutine(ShowTextCo());
    }

    private IEnumerator ShowTextCo()
    {
        _text.gameObject.SetActive(true);
        _text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(_secondsToShowText);
        _text.gameObject.SetActive(false);
        _text2.gameObject.SetActive(false);
    }
}
