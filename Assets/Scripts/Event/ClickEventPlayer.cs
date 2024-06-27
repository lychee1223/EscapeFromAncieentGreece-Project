using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickEventPlayer : MonoBehaviour
{
    [SerializeField] Collider triggerCollider;
    [SerializeField] EventList playedEventList;

    [Header("トリガーの有効条件")]   // コライダーの有効・無効を切り替える条件アングル・アイテム
    [SerializeField] CameraAngle.AngleKey enableCameraAngle;
    [SerializeField] Item.ItemKey enableSelectedItem;
    [SerializeField] Item.ItemKey disableSelectedItem;

    [Space(10)]
    [SerializeField] bool shouldConsumeSelectedItem;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // クリック判定を付与
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => OnClickCollider((PointerEventData)data));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// コライダーの有効・無効を切り替え
    /// </summary>
    void Update()
    {
        // 現在のアングルがenableCameraAngleでない場合、コライダー無効化
        if (enableCameraAngle != CameraAngle.AngleKey.NONE){
            if(enableCameraAngle != CameraManagaer.instance.currentAngle)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // 現在の選択中アイテムがenableSelectedItemでない場合、コライダー無効化
        if (enableSelectedItem != Item.ItemKey.NONE)
        {
            if (enableSelectedItem != Inventory.instance.selectedItem)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // 現在の選択中アイテムがdisableSelectedItemである場合、コライダー無効化
        if (disableSelectedItem != Item.ItemKey.NONE)
        {
            if (disableSelectedItem == Inventory.instance.selectedItem)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // すべての条件を満たす場合、コライダー有効化
        triggerCollider.enabled = true;
    }

    /// <summary>
    /// コライダークリック時にイベントを実行
    /// </summary>
    /// <param name="pointerEventData">イベントデータ</param>
    void OnClickCollider(PointerEventData pointerEventData)
    {
        // 左クリック以外で実行された場合は何もしない
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        // アイテムを消費
        if (shouldConsumeSelectedItem)
        {
            Inventory.instance.Consume(Inventory.instance.selectedItem);
        }
        
        playedEventList.Play(this.GetCancellationTokenOnDestroy());
    }
}
