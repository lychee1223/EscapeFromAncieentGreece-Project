using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //�V���O���g��

    // �X�R�A
    public int risk { get; private set; } = 0;      // �������X�N(�N���b�N��)
    public float second { get; private set; } = 0;  // �o�߂����b��
    public int fundage { get; private set; } = 0;   // �擾�����h���N�}�̋��z

    [SerializeField] int maxRisk;

    [SerializeField] Image infectionEffect;

    [SerializeField] CameraAngle.AngleKey gameClearAngleKey;
    [SerializeField] EventList gameClearEventList;
    [SerializeField] EventList gameOverEventList;

    public bool isPlayable { get; private set; } = false;

    Canvas resulutPanelCanvas;
    Canvas settingMenuCanvas;
    Canvas howToPageCanvas;

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
    async void Start()
    {
        resulutPanelCanvas = ResultPanel.instance.gameObject.GetComponent<Canvas>();
        settingMenuCanvas = SettingMenu.instance.gameObject.GetComponent<Canvas>();
        howToPageCanvas = HowToPage.instance.gameObject.GetComponent<Canvas>();


        Color color = infectionEffect.color;
        color.a = 0.0f;
        infectionEffect.color = color;

        Time.timeScale = 1.0f;

        try
        {
            // �v�����[�O���Đ�
            await OpeningEvent.instance.Play(this.GetCancellationTokenOnDestroy());
        }
        // UniTask�̃L�����Z����catch
        catch (OperationCanceledException e)
        {
#if UNITY_EDITOR
            Debug.Log(nameof(GameManager) + ":" + e);
#endif

            SceneManager.LoadScene("TitleScene");    // �^�C�g���ɖ߂�
            return;
        }

        isPlayable = true;
    }

    /// <summary>
    /// �Q�[�����̊e�L�[���͂��󂯕t��, �Q�[���I�[�o�[������s��
    /// </summary>
    void Update()
    {
        // ESC�L�[�Ń��j���[�̕\����؂�ւ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!resulutPanelCanvas.enabled)
            {
                if ((Time.timeScale != 0.0f))
                {
                    PauseMenu.instance.Open();
                }
                else if (!settingMenuCanvas.enabled && !howToPageCanvas.enabled)
                {
                    PauseMenu.instance.Close();
                }
            }
        }

        if (!isPlayable || Time.timeScale == 0.0f) { return; }    // �ȍ~�̏����̓|�[�Y���s��Ȃ�
        
        // �p�����[�^�X�V    
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseRisk(); // ���N���b�N�Ŋ������X�N����
        }

        IncreaseTime(); // �o�ߎ��Ԃ𑝉�

        // �Q�[���I�[�o�[����
        if (CameraManagaer.instance.currentAngle == gameClearAngleKey)
        {
            GameOver(true, this.GetCancellationTokenOnDestroy());  // �S�[�����B�ŃQ�[���N���A
        }

        if (risk / maxRisk == 1.0f)
        {
            GameOver(false, this.GetCancellationTokenOnDestroy());  // ������100%�ŃQ�[���I�[�o�[
        }
    }

    /// <summary>
    /// �Q�[���I�[�o�[���̏���
    /// </summary>
    /// <param name="playedEventList">�Đ�����C�x���g���X�g</param>
    void GameOver(bool isClear, CancellationToken ct = default)
    {
        isPlayable = false;

        float rate = (float)risk / maxRisk;
        ResultPanel.instance.SetStatus(rate, fundage, second, isClear);
        ScoreManager.instance.CalculateScore(rate, fundage, second, isClear);

        if (isClear)
        {
            gameClearEventList.Play(ct);
        }
        else
        {
            gameOverEventList.Play(ct);
        }
    }

    /// <summary>
    /// �������X�N�𑝉�������
    /// </summary>
    /// <param name="increaseAmount">�������X�N�̑�����</param>
    void IncreaseRisk()
    {
        risk = Math.Min(risk + 1, maxRisk);
        float rate = (float)risk / maxRisk;
        RiskGauge.instance.Set(rate);

        // �G�t�F�N�g�̓��ߗ����X�V
        Color color = infectionEffect.color;
        color.a = ((float)Math.Pow(2, rate) - 1.0f) * 0.2f;
        infectionEffect.color = color;
    }

    /// <summary>
    /// �o�ߎ��Ԃ𑝉�������
    /// </summary>
    void IncreaseTime()
    {
        second += Time.deltaTime;
        TimeCounter.instance.Set(second);
    }

    /// <summary>
    /// �������𑝉�������
    /// </summary>
    /// <param name="increaseAmount">����</param>
    public void IncreaseDrachma(int income)
    {
        fundage += income;
        DrachmaCounter.instance.Set(fundage);
    }


}
