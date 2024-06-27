using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickEventPlayer : MonoBehaviour
{
    [SerializeField] Collider triggerCollider;
    [SerializeField] EventList playedEventList;

    [Header("�g���K�[�̗L������")]   // �R���C�_�[�̗L���E������؂�ւ�������A���O���E�A�C�e��
    [SerializeField] CameraAngle.AngleKey enableCameraAngle;
    [SerializeField] Item.ItemKey enableSelectedItem;
    [SerializeField] Item.ItemKey disableSelectedItem;

    [Space(10)]
    [SerializeField] bool shouldConsumeSelectedItem;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        // �N���b�N�����t�^
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => OnClickCollider((PointerEventData)data));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// �R���C�_�[�̗L���E������؂�ւ�
    /// </summary>
    void Update()
    {
        // ���݂̃A���O����enableCameraAngle�łȂ��ꍇ�A�R���C�_�[������
        if (enableCameraAngle != CameraAngle.AngleKey.NONE){
            if(enableCameraAngle != CameraManagaer.instance.currentAngle)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // ���݂̑I�𒆃A�C�e����enableSelectedItem�łȂ��ꍇ�A�R���C�_�[������
        if (enableSelectedItem != Item.ItemKey.NONE)
        {
            if (enableSelectedItem != Inventory.instance.selectedItem)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // ���݂̑I�𒆃A�C�e����disableSelectedItem�ł���ꍇ�A�R���C�_�[������
        if (disableSelectedItem != Item.ItemKey.NONE)
        {
            if (disableSelectedItem == Inventory.instance.selectedItem)
            {
                triggerCollider.enabled = false;
                return;
            }
        }

        // ���ׂĂ̏����𖞂����ꍇ�A�R���C�_�[�L����
        triggerCollider.enabled = true;
    }

    /// <summary>
    /// �R���C�_�[�N���b�N���ɃC�x���g�����s
    /// </summary>
    /// <param name="pointerEventData">�C�x���g�f�[�^</param>
    void OnClickCollider(PointerEventData pointerEventData)
    {
        // ���N���b�N�ȊO�Ŏ��s���ꂽ�ꍇ�͉������Ȃ�
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        // �A�C�e��������
        if (shouldConsumeSelectedItem)
        {
            Inventory.instance.Consume(Inventory.instance.selectedItem);
        }
        
        playedEventList.Play(this.GetCancellationTokenOnDestroy());
    }
}
