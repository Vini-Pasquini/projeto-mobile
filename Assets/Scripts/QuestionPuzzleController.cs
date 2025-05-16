using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum QuestionPuzzleMode
{
    None,
    PowerUpChest,
    BossFight,
}

public class QuestionPuzzleController : MonoBehaviour
{
    private LibrasSign _signAnswer;
    private List<LibrasSign> _signOptions = new List<LibrasSign>();

    [Header("Question Panel")]
    [SerializeField] private TextMeshProUGUI _questionText;
    [Header("Answer Panel")]
    [SerializeField] private GameObject _questionAnswerPanel;
    [SerializeField] private Image _answerSignImage;
    [Header("Options Panel")]
    [SerializeField] private GameObject _questionOptionsPanel;
    [SerializeField] private Image[] _optionSignImages;

    private GameObject _questionPuzzlePanel;

    private QuestionPuzzleMode _currentQuestionPuzzleMode = QuestionPuzzleMode.None;

    private void Start()
    {
        this._questionPuzzlePanel = this.transform.GetChild(0).gameObject;
    }

    private void InitPuzzle()
    {
        this._questionAnswerPanel.SetActive(false);
        this._questionOptionsPanel.SetActive(true);

        this._signOptions.Clear();

        for (int i = 0; i < this._optionSignImages.Length; i++)
        {
            LibrasSign newWord;

            do
            {
                newWord = GameManager.Instance.GetRandomLibrasWordSign();
            } while (this._signOptions.Contains(newWord) || newWord == this._signAnswer);

            this._signOptions.Add(newWord);
            this._optionSignImages[i].sprite = this._signOptions[i].SignSprite;
        }

        int answIndex = Random.Range(0, this._signOptions.Count);
        this._signOptions[answIndex] = this._signAnswer;
        this._answerSignImage.sprite = this._optionSignImages[answIndex].sprite = this._signAnswer.SignSprite;

        this._questionText.text = $"Libra para {this._signAnswer.SignText}";
        this._questionText.transform.parent.GetComponent<Image>().color = new Color(0f, .25f, 1f, .5f); ; // PH DEBUG
    }

    public void StartLibrasPuzzle(LibrasSign answer, QuestionPuzzleMode mode)
    {
        Debug.Log(mode.ToString());

        if (this._questionPuzzlePanel.activeSelf || mode == QuestionPuzzleMode.None) return;

        this._signAnswer = answer;
        this._currentQuestionPuzzleMode = mode;
        this.InitPuzzle();
        this._questionPuzzlePanel.SetActive(true);
    }

    private bool EndPuzzle()
    {
        this._questionPuzzlePanel.SetActive(false);
        return this._puzzlePassFlag;
    }

    private bool _puzzlePassFlag = false;

    /* Buttons */

    public void OnOptionButtonPress(int optIndex)
    {
        if (this._signOptions[optIndex] == this._signAnswer)
        {
            this._questionText.text = $"CORRETO! O Sinal para {this._signAnswer.SignText} é";
            this._questionText.transform.parent.GetComponent<Image>().color = new Color(0f, 1f, 0f, .5f); // PH DEBUG
            this._puzzlePassFlag = true;
        }
        else
        {
            this._questionText.text = $"INCORRETO! O Sinal para {this._signAnswer.SignText} era:";
            this._questionText.transform.parent.GetComponent<Image>().color = new Color(1f, 0f, 0f, .5f); // PH DEBUG
            this._puzzlePassFlag = false;
        }

        this._questionAnswerPanel.SetActive(true);
        this._questionOptionsPanel.SetActive(false);
    }

    public void OnClosePuzzleButtonPress()
    {
        bool passFlag = EndPuzzle();
        switch (this._currentQuestionPuzzleMode)
        {
            case QuestionPuzzleMode.PowerUpChest: GameManager.Instance.ChestController.DisableChestPuzzle(passFlag); break;
            case QuestionPuzzleMode.BossFight: /**/ break;
            default: break;
        }
        this._currentQuestionPuzzleMode = QuestionPuzzleMode.None;
    }
}
