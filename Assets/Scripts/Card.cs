using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{ // 카드의 정보를 담고 있는 클래스
    // 카드 뒤집기 기능도 애니메이션 적용 

    public int idx = 0;
    public GameObject front;
    public GameObject back;

    public Animator anim;
    public SpriteRenderer frontImage;

    // Start is called before the first frame update
    void Start() { }

    void Update() { }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        // (추가) 카드 클릭 효과음
        if (GameManager.instance != null) GameManager.instance.PlayClick();

        anim.SetBool("IsOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
        Destroy(gameObject);
    }

    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard() // 뒤집는것도 확인할 겨를도 없이 파괴되니 Invoke 를 사용해서 0.5초 후에 뒤집도록 한다.
    {
        Invoke("CloseCardInvoke", 0.5f);

    }
    public void CloseCardInvoke()
    {

        anim.SetBool("IsOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
