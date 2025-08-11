using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // 시간을 계속해서 더해줄 것 이기 때문에 , 계속해서 더해주는 값을 저장해줄 변수가 하나 필요하다

    // 그리고 뒤집은 카드가 같다면 파괴시키고 , 아니면 다시 뒤집어주는 기능을 구현하자 

    public static GameManager instance;
    public Card firstCard;
    public Card secondCard;

    // === (추가) UI 관련 ===
    public Text timeTxt;
    public GameObject EndTxt;

    // === (추가) 게임 진행 관련 ===
    public int cardCount = 0;
    float time = 0.0f;
    public float timeLimit = 30f;

    // === (추가) 오디오: Hierarchy에서 드래그드롭으로 연결 ===
    [Header("Audio Sources (드래그드롭)")]
    public AudioSource bgmSource;   // 루프용 BGM 재생기
    public AudioSource sfxSource;   // 효과음 재생기

    [Header("Clips (드래그드롭)")]
    public AudioClip bgmMain;       // 게임 시작 BGM
    public AudioClip sfxCardClick;  // 카드 클릭
    public AudioClip sfxMatch;      // 맞췄을 때
    public AudioClip sfxMismatch;   // 틀렸을 때

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1; // 게임이 시작되면 시간 흐르게 하기
        if (EndTxt != null) EndTxt.SetActive(false);

        // (추가) 시작 BGM 재생
        if (bgmSource != null && bgmMain != null)
        {
            bgmSource.clip = bgmMain;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    // Update is 
    void Update()
    {
        // (추가) 시간 표시
        time += Time.deltaTime;
        if (timeTxt != null)
        {
            timeTxt.text = time.ToString("N2");
            if (time >= timeLimit * 0.66f) timeTxt.color = Color.red;
        }

        // (추가) 타임오버 처리
        if (time >= timeLimit && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            if (EndTxt != null) EndTxt.SetActive(true);
            if (bgmSource != null && bgmSource.isPlaying) bgmSource.Stop(); // 종료 시 BGM 정지(선택)
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            // 맞다면 파괴시켜라!
            PlayMatch(); // (추가) 정답 효과음

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if (cardCount == 0)
            {
                // 게임이 끝나면
                Time.timeScale = 0;
                if (EndTxt != null) EndTxt.SetActive(true);
                if (bgmSource != null && bgmSource.isPlaying) bgmSource.Stop(); // (추가) 클리어 시 BGM 정지(선택)
            }
        }
        else
        {
            // 닫아라
            PlayMismatch(); // (추가) 오답 효과음

            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null; // 두 카드가 모두 뒤집힌 상태로 초기화
    }

    // ---------- (추가) Audio Helpers ----------
    public void PlayClick()
    {
        if (sfxSource != null && sfxCardClick != null)
            sfxSource.PlayOneShot(sfxCardClick);
    }

    public void PlayMatch()
    {
        if (sfxSource != null && sfxMatch != null)
            sfxSource.PlayOneShot(sfxMatch);
    }

    public void PlayMismatch()
    {
        if (sfxSource != null && sfxMismatch != null)
            sfxSource.PlayOneShot(sfxMismatch);
    }
}
