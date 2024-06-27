using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // �V���O���g��

    ItemDB itemDB;
    public List<Item> items { get; private set; } = new List<Item>();    // �C���x���g�����̏����A�C�e��
    public Item.ItemKey selectedItem { get; private set; } = Item.ItemKey.NONE; // �I�𒆃A�C�e��

    [SerializeField] GameObject[] slots;        // �X���b�g��UI
    [SerializeField] Color defaultSlotColor;    // �ʏ펞�X���b�g�w�i�F
    [SerializeField] Color selectedSlotColor;   // �I�𒆂̃X���b�g�w�i�F
    SearchModePanel searchModePanel;

    /// <summary>
    /// �V���O���g��������
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        itemDB = DBManager.instance.itemDB;         // DB�擾
        searchModePanel = SearchModePanel.instance; // �T�[�`���[�h�p�l���擾

        // �eSlot��button��Select()���A�^�b�`
        for (int i = 0; i < slots.Length; i++)
        {
            var j = i;
            slots[i].GetComponent<Button>().onClick.AddListener(() => Select(j));
        }

        UpdateSlot();   // UI������
    }

    /// <summary>
    /// �A�C�e����I��
    /// </summary>
    /// <param name="index">�I������A�C�e����items��index</param>
    public void Select(int index)
    {
        // ���ɑI�����ꂽ�A�C�e����I�������ꍇ�̓T�[�`���[�h�N��
        if (selectedItem == items[index].key)
        {
            searchModePanel.Open(items[index], false);
            Unselect();
        }

        // items[index]��I��
        else
        {
            selectedItem = items[index].key;
            UpdateSlot();
        }
    }

    /// <summary>
    /// �A�C�e���̑I��������
    /// </summary>
    public void Unselect()
    {
        selectedItem = Item.ItemKey.NONE;
        UpdateSlot();
    }

    /// <summary>
    /// �A�C�e�������
    /// </summary>
    /// <param name="gottenItem">���肷��A�C�e����key</param>
    public void Get(Item.ItemKey gottenItem)
    {
        if (gottenItem == Item.ItemKey.NONE) { return; }

        // �A�C�e����ǉ�
        Item gottenItemData = itemDB.Get(gottenItem);
        if(gottenItemData == null) { return; }
        items.Add(gottenItemData);

        // UI�X�V
        searchModePanel.Open(gottenItemData, false);
        UpdateSlot();
    }

    /// <summary>
    /// �C���x���g���ɂ���A�C�e���ƒu�����ăA�C�e�������
    /// </summary>
    /// <param name="replacedItem">���肷��A�C�e���ƒu������A�C�e����key</param>
    /// <param name="gottenItem">���肷��A�C�e����key</param>
    /// <param name="shouldSyncRotation">replacedItem��gottenItem��rotation�𓯊����邩</param>
    public void Replace(Item.ItemKey replacedItem, Item.ItemKey gottenItem, bool shouldSyncRotation)
    {
        if (replacedItem == Item.ItemKey.NONE) { return; }
        if (gottenItem == Item.ItemKey.NONE) { return; }

        // �u������items��index���擾
        int index = items.IndexOf(itemDB.Get(replacedItem));
        if (index < 0) { return; }

        // �A�C�e����u��
        Item gottenItemData = itemDB.Get(gottenItem);
        if (gottenItemData == null) { return; }
        items[index] = gottenItemData;

        // UI�X�V
        searchModePanel.Open(gottenItemData, shouldSyncRotation);
        UpdateSlot();
    }

    /// <summary>
    /// �A�C�e��������
    /// </summary>
    /// <param name="consumedItem">�����A�C�e����key</param>
    public void Consume(Item.ItemKey consumedItem)
    {
        if (consumedItem == Item.ItemKey.NONE) { return; }

        items.Remove(itemDB.Get(consumedItem));

        Unselect();
    }

    /// <summary>
    /// �X���b�g�̕\���E�A�C�R���E�w�i�F���X�V
    /// </summary>
    void UpdateSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // �X���b�g�ɃA�C�e��������ꍇ�X���b�g��\��
            if (i < items.Count)
            {
                slots[i].SetActive(true);

                // �A�C�R�����X�V
                slots[i].transform.Find("Icon").GetComponent<Image>().sprite = items[i].icon;

                // �X���b�g�̔w�i�F���X�V
                if (items[i].key == selectedItem)
                {
                    slots[i].GetComponent<Image>().color = selectedSlotColor;
                }
                else
                {
                    slots[i].GetComponent<Image>().color = defaultSlotColor;
                }
            }
            // �X���b�g�ɃA�C�e�����Ȃ��ꍇ�X���b�g���\��
            else
            {
                slots[i].SetActive(false);
            }
        }
    }
}
