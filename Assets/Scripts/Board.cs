using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Board : MonoBehaviour
{
    public GameObject card;

    // 카드의 간격을 나눠서 배치할것을 생각해보자
    // 몫과 나머지를 구해 1.4를 곱해서 x 랑 y 값에 넣어주자 
    // 그리고 위치를 지정해주자
    // 그리고 랜덤하게 섞어주도록 Linq 와 배열을 사용해보자
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray(); // 배열을 랜덤하게 섞어준다.
        // OrderBy 가 배열이 아니기 때문에 .ToArray()로 한 번 더 배열로 바꿔준다고 선언해줘야 한다.


        for (int i = 0; i < 16; i++)
        {
           GameObject go = Instantiate(card,this.transform); // Unity 에서의 this 는 자기 자신을 의미 
            go.GetComponent<Card>().Setting(arr[i]); // Card 컴포넌트의 Setting 메소드를 호출해서 배열의 i번째 있는 값을 넣어준다.

            float x = (i % 4) * 1.4f - 2.1f; // 4로 나눈 나머지에서 1.4를 곱해주고 x 좌표가 된다. 그리고 -2.1f 정도 옮겨주면 화면 가운데로 오게 된다.
            float y = (i / 4) * 1.4f - 3.0f; // 몫을 구해서 1.4를 곱해주면 y 좌표가 된다  그리고 -3.0f 정도 옮겨주면 화면 가운데로 오게 된다.

            go.transform.position = new Vector2(x,y);
        }
        GameManager.instance.cardCount = arr.Length; // 
    }

 
  
}
