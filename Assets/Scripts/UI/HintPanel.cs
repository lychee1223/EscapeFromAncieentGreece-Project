using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour
{
    public static HintPanel instance;

    public Hint.Tag statusTag;
    HintDB hintDB;

    [SerializeField] CanvasGroup canvas;
    [SerializeField] Button button;
    [SerializeField] TMP_Text text;
    [SerializeField] float aliveTime;
    [SerializeField] float closeTime;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        instance = this;
        hintDB = DBManager.instance.hintDB; // DB�擾
        button.onClick.AddListener(() => Open());
    }

    /// <summary>
    /// MessageWindow��\��
    /// </summary>
    /// <param name="ct">UniTask�L�����Z���g�[�N��</param>
    public async void Open(CancellationToken ct = default)
    {
        try
        {
            if (canvas.alpha != 0.0f) { return; }

            // �e�L�X�g���X�V
            Hint hint = hintDB.Get();
            if (hint != null)
            {
                text.text = hint.hintText;
            }
            
            // canvas�̕\��
            canvas.alpha = 1.0f;

            // aliveTime�b��ACanvas���\���ɂ���
            await UniTask.Delay(System.TimeSpan.FromSeconds(aliveTime), cancellationToken: ct);
            await canvas.MakeTransparent(closeTime, ct);
        }
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(MessageWindow) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }
    }
}
