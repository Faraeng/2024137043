using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//수집 가능한 아이템 스크립트 
public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;                   //아이템 종류 (예 : 크리스탈, 식물, 수풀 ,나무)
    public string itemName;                     //아이템 이름
    public float respawnTime = 30.0f;           //리스폰 시간(아이템이 다시 생성 될 때 까지의 대기 시간)
    public bool canCollect = true;              //수집 가능 여부(수집할 수 있는지 여부를 나타냄)

    //아이템을 수집하는 메서드 , playerInventory를 통해 인벤토리에 추가 
    public void CollectItem(PlayerInventory inventory)
    {
        //수집 가능 여부를 체크 
        if (!canCollect) return;

        inventory.AddItem(itemType);            //아이템을 인벤토리에 추가

        if(FloatingTextManager.instance != null )
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;
            FloatingTextManager.Instance.Show($"+{itemName}", textPosition);
        }
        Debug.Log($"{itemName} 수집 완료");     //아이템 수집 완료 메세지 출력
        StartCoroutine(RespawnRoutine());       //아이템 리스폰 코루틴 실행 
    }

    //아이템 리스폰을 처리하는 코루틴
    private IEnumerator RespawnRoutine()
    {
        canCollect = false;         //수집 불가능 상태로 변경
        GetComponent<MeshRenderer>().enabled = false;               //아티메의 MeshRenderer를 꺼서 보이지 않게 함
        GetComponent<MeshCollider>().enabled = false;

        //설정된 리스폰 시간 만큼 대기
        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;                //아이템을 다시 보이게 합
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;                                          //수집 가능 상태로 변경
    }

}
