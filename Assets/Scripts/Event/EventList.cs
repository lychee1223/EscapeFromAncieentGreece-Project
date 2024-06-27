using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventList : MonoBehaviour
{
    [TooltipAttribute("�R���e�N�X�g���j���[�� AddEvent ����ǉ�")]
    [SerializeReference] List<IEvent> eventList = new List<IEvent>();

    /// <summary>
    /// eventList�Ɋi�[���ꂽ�S�C�x���g�����s
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public virtual void Play(CancellationToken ct = default)
    {
        foreach (IEvent @event in eventList)
        {
            @event.Play(ct);
        }
    }

    // �e�C�x���g���R���e�L�X�g���j���[����ǉ��ł���悤�Ɋ֐���ݒ�
    [ContextMenu("AddEvent/"+ nameof(ChangeCameraAngleEvent))] 
    void AddChangeCameraAngleEvent()
    {
        eventList.Add(new ChangeCameraAngleEvent());
    }

    [ContextMenu("AddEvent/" + nameof(GetItemEvent))] 
    void AddGetItemEvent()
    {
        eventList.Add(new GetItemEvent());
    }

    [ContextMenu("AddEvent/" + nameof(GetDrachmaEvent))]
    void AddGetDrachmaEvent()
    {
        eventList.Add(new GetDrachmaEvent());
    }

    [ContextMenu("AddEvent/" + nameof(MoveObjectEvent))]
    void AddMoveObjectEvent()
    {
        eventList.Add(new MoveObjectEvent());
    }

    [ContextMenu("AddEvent/" + nameof(InputKeyEvent))]
    void AddKeyInputEvent()
    {
        eventList.Add(new InputKeyEvent());
    }

    [ContextMenu("AddEvent/" + nameof(SetActiveEvent))]
    void AddSetActiveEvent()
    {
        eventList.Add(new SetActiveEvent());
    }

    [ContextMenu("AddEvent/" + nameof(OpenMessageWindowEvent))]
    void AddMessageEvent()
    {
        eventList.Add(new OpenMessageWindowEvent());
    }

    [ContextMenu("AddEvent/" + nameof(OpenResultPanelEvent))]
    void AddGameOverEvent()
    {
        eventList.Add(new OpenResultPanelEvent());
    }

    [ContextMenu("AddEvent/" + nameof(PlaySoundEvent))]
    void AddPlaySoundEvent()
    {
        eventList.Add(new PlaySoundEvent());
    }

    [ContextMenu("AddEvent/" + nameof(SetStatusTagEvent))]
    void AddSetStatusTagEventt()
    {
        eventList.Add(new SetStatusTagEvent());
    }
}
