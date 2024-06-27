using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchModePanel : MonoBehaviour
{
    public static SearchModePanel instance; // �V���O���g��

    Canvas canvas;
    Item.ItemKey sarchedItem = Item.ItemKey.NONE;   // �T�[�`���̃A�C�e��
    GameObject sarchedEntity;                       // �T�[�`���̃A�C�e���̃I�u�W�F�N�g

    [Header("SearchModePanel UI")]
    [SerializeField] CanvasGroup disableCanvasGroup;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text explaination;
    [SerializeField] Transform sarchedEntityParent; // sarchedEntity�̐e�I�u�W�F�N�g

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
        canvas = GetComponent<Canvas>();

        // BackButton��Close()���A�^�b�`
        backButton.onClick.AddListener(() => Close());
    }

    /// <summary>
    /// �T�[�`���[�h�̋N��
    /// </summary>
    /// <param name="sarchedItemData">�T�[�`����A�C�e��</param>
    /// <param name="shouldSyncRotation">����sarchedEntity��sarchedItemData��rotation�𓯊����邩</param>
    public void Open(Item sarchedItemData, bool shouldSyncRotation)
    {
        if (sarchedItemData.key == this.sarchedItem) { return; }

        // �O��sarchedEntity��rotation�𓯊����ăT�[�`���[�h�N��
        if (shouldSyncRotation && (sarchedEntity != null))
        {
            // �O��entity��rotation��ۑ�
            Quaternion rotation = sarchedEntity.transform.rotation;

            Destroy(sarchedEntity);

            // entity���C���X�^���X��
            sarchedEntity = Instantiate(sarchedItemData.entity);
            sarchedEntity.transform.SetParent(sarchedEntityParent, false);

            // rotation����
            sarchedEntity.transform.rotation = rotation;
        }

        // rotation�𓯊������T�[�`���[�h�N��
        else
        {
            if (sarchedEntity != null)
            {
                Destroy(sarchedEntity);
            }

            // entity���C���X�^���X��
            sarchedEntity = Instantiate(sarchedItemData.entity);
            sarchedEntity.transform.SetParent(sarchedEntityParent, false);
        }

        // UI�X�V
        itemName.text = sarchedItemData.name;
        explaination.text = sarchedItemData.explaination;

        canvas.enabled = true;
        disableCanvasGroup.alpha = 0.0f;
        disableCanvasGroup.interactable = false;

        sarchedItem = sarchedItemData.key;
    }

    /// <summary>
    /// �T�[�`���[�h�̉���
    /// </summary>
    void Close()
    {
        Destroy(sarchedEntity);

        // UI�X�V
        canvas.enabled = false;
        disableCanvasGroup.alpha = 1.0f;
        disableCanvasGroup.interactable = true;

        sarchedItem = Item.ItemKey.NONE;
    }
}
