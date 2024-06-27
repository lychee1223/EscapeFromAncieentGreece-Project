using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchModePanel : MonoBehaviour
{
    public static SearchModePanel instance; // シングルトン

    Canvas canvas;
    Item.ItemKey sarchedItem = Item.ItemKey.NONE;   // サーチ中のアイテム
    GameObject sarchedEntity;                       // サーチ中のアイテムのオブジェクト

    [Header("SearchModePanel UI")]
    [SerializeField] CanvasGroup disableCanvasGroup;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text explaination;
    [SerializeField] Transform sarchedEntityParent; // sarchedEntityの親オブジェクト

    /// <summary>
    /// シングルトン初期化
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        canvas = GetComponent<Canvas>();

        // BackButtonにClose()をアタッチ
        backButton.onClick.AddListener(() => Close());
    }

    /// <summary>
    /// サーチモードの起動
    /// </summary>
    /// <param name="sarchedItemData">サーチするアイテム</param>
    /// <param name="shouldSyncRotation">元のsarchedEntityとsarchedItemDataのrotationを同期するか</param>
    public void Open(Item sarchedItemData, bool shouldSyncRotation)
    {
        if (sarchedItemData.key == this.sarchedItem) { return; }

        // 前のsarchedEntityのrotationを同期してサーチモード起動
        if (shouldSyncRotation && (sarchedEntity != null))
        {
            // 前のentityのrotationを保存
            Quaternion rotation = sarchedEntity.transform.rotation;

            Destroy(sarchedEntity);

            // entityをインスタンス化
            sarchedEntity = Instantiate(sarchedItemData.entity);
            sarchedEntity.transform.SetParent(sarchedEntityParent, false);

            // rotation同期
            sarchedEntity.transform.rotation = rotation;
        }

        // rotationを同期せずサーチモード起動
        else
        {
            if (sarchedEntity != null)
            {
                Destroy(sarchedEntity);
            }

            // entityをインスタンス化
            sarchedEntity = Instantiate(sarchedItemData.entity);
            sarchedEntity.transform.SetParent(sarchedEntityParent, false);
        }

        // UI更新
        itemName.text = sarchedItemData.name;
        explaination.text = sarchedItemData.explaination;

        canvas.enabled = true;
        disableCanvasGroup.alpha = 0.0f;
        disableCanvasGroup.interactable = false;

        sarchedItem = sarchedItemData.key;
    }

    /// <summary>
    /// サーチモードの解除
    /// </summary>
    void Close()
    {
        Destroy(sarchedEntity);

        // UI更新
        canvas.enabled = false;
        disableCanvasGroup.alpha = 1.0f;
        disableCanvasGroup.interactable = true;

        sarchedItem = Item.ItemKey.NONE;
    }
}
