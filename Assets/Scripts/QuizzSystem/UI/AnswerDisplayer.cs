using System;
using System.Collections.Generic;
using DG.Tweening;
using QuizzSystem.Answers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem.UI
{
    public class AnswerDisplayer
    {
        private readonly RectTransform _layout;
        private readonly List<ChoiceDisplayer> _choices;
        private readonly RectTransform _answerParent;
        private readonly TMP_Text _singleLineText;
        private readonly Image _image;

        public AnswerDisplayer(InitParam initParam)
        {
            _choices = initParam.choices;
            _answerParent = initParam.answerParent.GetComponent<RectTransform>();
            _singleLineText = initParam.singleLineText;
            _image = initParam.image;
            _layout = initParam.layout;
            
            _answerParent.gameObject.SetActive(false);
        }
        
        public void DisplayAnswer(Answer answer)
        {
            Sequence sequence = DOTween.Sequence();
            if (answer.type.HasFlag(AnswerType.MultipleChoice))
                HandleMultipleChoice(answer, sequence);
            
            if (answer.type.HasFlag(AnswerType.Image))
                HandlePictureAnswer(answer, sequence);

            if (answer.type.HasFlag(AnswerType.SingleText))
                HandleSingleLineText(answer, sequence);
        }

        private void HandleMultipleChoice(Answer answer, Sequence sequence)
        {
            for (int i = 0; i < _choices.Count; i++)
            {
                if (i == answer.multipleChoiceIndexAnswer)
                    continue;

                sequence.Append(_choices[i].transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            }
        }

        private void HandlePictureAnswer(Answer answer, Sequence sequence)
        {
            _image.gameObject.SetActive(true);
            _image.sprite = answer.spriteAnswer;
            
            _answerParent.gameObject.SetActive(true);
            _answerParent.localScale = Vector3.zero;

            Vector2 target = _answerParent.sizeDelta;
            _answerParent.sizeDelta = new Vector2(target.x, 0);
            
            sequence.Append(_answerParent.DOSizeDelta(target, 0.4f).SetEase(Ease.OutBack)).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(_layout));
            sequence.Join(_answerParent.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack));
        }

        private void HandleSingleLineText(Answer answer, Sequence sequence)
        {
            _answerParent.gameObject.SetActive(true);
            _singleLineText.gameObject.SetActive(true);
            
            _singleLineText.text = answer.singleTextAnswer;
            _singleLineText.transform.localScale = Vector3.zero;
            sequence.Append(_singleLineText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack));
        }

        public struct InitParam
        {
            public List<ChoiceDisplayer> choices;
            public GameObject answerParent;
            public TMP_Text singleLineText;
            public Image image;
            public RectTransform layout;
        }
    }
}