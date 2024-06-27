using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventList : MonoBehaviour
{
    [TooltipAttribute("コンテクストメニューの AddEvent から追加")]
    [SerializeReference] List<IEvent> eventList = new List<IEvent>();

    /// <summary>
    /// eventListに格納された全イベントを実行
    /// </summary>
    /// <param name="ct">UniTaskキャンセルトークン</param>
    public virtual void Play(CancellationToken ct = default)
    {
        foreach (IEvent @event in eventList)
        {
            @event.Play(ct);
        }
    }

    // 各イベントをコンテキストメニューから追加できるように関数を設定
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
