using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // シングルトン

    ItemDB itemDB;
    public List<Item> items { get; private set; } = new List<Item>();    // インベントリ内の所持アイテム
    public Item.ItemKey selectedItem { get; private set; } = Item.ItemKey.NONE; // 選択中アイテム

    [SerializeField] GameObject[] slots;        // スロットのUI
    [SerializeField] Color defaultSlotColor;    // 通常時スロット背景色
    [SerializeField] Color selectedSlotColor;   // 選択中のスロット背景色
    SearchModePanel searchModePanel;

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
        itemDB = DBManager.instance.itemDB;         // DB取得
        searchModePanel = SearchModePanel.instance; // サーチモードパネル取得

        // 各SlotのbuttonにSelect()をアタッチ
        for (int i = 0; i < slots.Length; i++)
        {
            var j = i;
            slots[i].GetComponent<Button>().onClick.AddListener(() => Select(j));
        }

        UpdateSlot();   // UI初期化
    }

    /// <summary>
    /// アイテムを選択
    /// </summary>
    /// <param name="index">選択するアイテムのitemsのindex</param>
    public void Select(int index)
    {
        // 既に選択されたアイテムを選択した場合はサーチモード起動
        if (selectedItem == items[index].key)
        {
            searchModePanel.Open(items[index], false);
            Unselect();
        }

        // items[index]を選択
        else
        {
            selectedItem = items[index].key;
            UpdateSlot();
        }
    }

    /// <summary>
    /// アイテムの選択を解除
    /// </summary>
    public void Unselect()
    {
        selectedItem = Item.ItemKey.NONE;
        UpdateSlot();
    }

    /// <summary>
    /// アイテムを入手
    /// </summary>
    /// <param name="gottenItem">入手するアイテムのkey</param>
    public void Get(Item.ItemKey gottenItem)
    {
        if (gottenItem == Item.ItemKey.NONE) { return; }

        // アイテムを追加
        Item gottenItemData = itemDB.Get(gottenItem);
        if(gottenItemData == null) { return; }
        items.Add(gottenItemData);

        // UI更新
        searchModePanel.Open(gottenItemData, false);
        UpdateSlot();
    }

    /// <summary>
    /// インベントリにあるアイテムと置換してアイテムを入手
    /// </summary>
    /// <param name="replacedItem">入手するアイテムと置換するアイテムのkey</param>
    /// <param name="gottenItem">入手するアイテムのkey</param>
    /// <param name="shouldSyncRotation">replacedItemとgottenItemのrotationを同期するか</param>
    public void Replace(Item.ItemKey replacedItem, Item.ItemKey gottenItem, bool shouldSyncRotation)
    {
        if (replacedItem == Item.ItemKey.NONE) { return; }
        if (gottenItem == Item.ItemKey.NONE) { return; }

        // 置換するitemsのindexを取得
        int index = items.IndexOf(itemDB.Get(replacedItem));
        if (index < 0) { return; }

        // アイテムを置換
        Item gottenItemData = itemDB.Get(gottenItem);
        if (gottenItemData == null) { return; }
        items[index] = gottenItemData;

        // UI更新
        searchModePanel.Open(gottenItemData, shouldSyncRotation);
        UpdateSlot();
    }

    /// <summary>
    /// アイテムを消費
    /// </summary>
    /// <param name="consumedItem">消費するアイテムのkey</param>
    public void Consume(Item.ItemKey consumedItem)
    {
        if (consumedItem == Item.ItemKey.NONE) { return; }

        items.Remove(itemDB.Get(consumedItem));

        Unselect();
    }

    /// <summary>
    /// スロットの表示・アイコン・背景色を更新
    /// </summary>
    void UpdateSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // スロットにアイテムがある場合スロットを表示
            if (i < items.Count)
            {
                slots[i].SetActive(true);

                // アイコンを更新
                slots[i].transform.Find("Icon").GetComponent<Image>().sprite = items[i].icon;

                // スロットの背景色を更新
                if (items[i].key == selectedItem)
                {
                    slots[i].GetComponent<Image>().color = selectedSlotColor;
                }
                else
                {
                    slots[i].GetComponent<Image>().color = defaultSlotColor;
                }
            }
            // スロットにアイテムがない場合スロットを非表示
            else
            {
                slots[i].SetActive(false);
            }
        }
    }
}
