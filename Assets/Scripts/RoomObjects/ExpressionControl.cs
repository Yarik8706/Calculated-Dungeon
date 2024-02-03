using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExpressionControl : MonoBehaviour
{
    [SerializeField] private int minAnswerNumber = 1;
    [SerializeField] private int maxAnswerNumber = 99;
    [SerializeField] private string expressionOperation;
    [SerializeField] private int id = -1;
    [SerializeField] private int hard_lvl = 1;
    [SerializeField] private bool invertInts;
    [SerializeField] private GameObject number;
    [SerializeField] private GameObject operators;
    [SerializeField] private Vector2 offsetInst = Vector2.right;
    
    private readonly string[] _operationsType = { "+", "-", "*", "÷"};
    private readonly int[] _trueExpressionNumbers = new int[3];
    private readonly string[] _stringExpressionNumbers = new string[3];
    
    private readonly int[] _expressionNumbers = new int[3];
    private readonly List<ExpressionSymbol> _expressionSymbols = new();

    private void Start()
    {
        ExpressionsControl.Instance.UnsolvedExpressionCount++;
        GenerateExpressionData();
        SpawnAndSetNumbers();
    }

    private void SpawnAndSetNumbers()
    {
        int currentNumberIndex = 0;
        if (invertInts)
        {
            for (int i = _stringExpressionNumbers.Length - 1; i >= 0; i--)
            {
                char[] charArr = _stringExpressionNumbers[i].ToCharArray();

                switch (i)
                {
                    case 0:
                    {
                        SpawnOperator(currentNumberIndex, expressionOperation);
                        currentNumberIndex++;
                        break;
                    }
                    case 1:
                    {
                        SpawnOperator(currentNumberIndex, "=");
                        currentNumberIndex++;
                        break;
                    }
                }

                for (int j = charArr.Length-1; j >=0 ; j--)
                {
                    SpawnNumber(i, j, charArr[j].ToString(), currentNumberIndex);
                    currentNumberIndex++;
                }
            }
        }
        else
        {
            for (int i = 0; i < _stringExpressionNumbers.Length; i++)
            {
                char[] charArr = _stringExpressionNumbers[i].ToCharArray();
                for (int j = 0; j < charArr.Length; j++)
                {
                    SpawnNumber(i, j, charArr[j].ToString(), currentNumberIndex);
                    
                    currentNumberIndex++;
                }

                switch (i)
                {
                    case 0:
                    {
                        SpawnOperator(currentNumberIndex, expressionOperation);
                        currentNumberIndex++;
                        break;
                    }
                    case 1:
                    {
                        SpawnOperator(currentNumberIndex, "=");
                        currentNumberIndex++;
                        break;
                    }
                }
            }
        }
    }

    private void SpawnOperator(int currentNumberIndex, string operation)
    {
        var operatorSymbol = Instantiate(operators, transform).GetComponent<ExpressionSymbol>();
        Vector2 tr = transform.position;
        operatorSymbol.transform.position = tr + offsetInst * currentNumberIndex;
        operatorSymbol.Text.text = operation;
        
        _expressionSymbols.Add(operatorSymbol);
    }

    private void SpawnNumber(int i, int j, string text, int currentNumberIndex)
    {
        GameObject newNumber = Instantiate(number, transform);
        Vector2 tr = transform.position;
        newNumber.transform.position = tr + offsetInst * currentNumberIndex;
        newNumber.GetComponent<Number>().SetNumber(this, i, j);

        var numberSymbol = newNumber.GetComponent<ExpressionSymbol>();
        numberSymbol.Text.text = text;

        _expressionSymbols.Add(numberSymbol);
    }

    private void GenerateExpressionData()
    {
        id = id == -1 ? Random.Range(0, 3) : id;
        expressionOperation = string.IsNullOrEmpty(expressionOperation) 
            ? _operationsType[Random.Range(0, _operationsType.Length)] : expressionOperation;

        GenerateTrueExpression();
        
        Array.Copy(_trueExpressionNumbers, _expressionNumbers, 3);

        for (int j = 0; j < hard_lvl; j++)
        {
            _expressionNumbers[id] = AddNumber(_expressionNumbers[id]);
        }

        for (int i = 0; i < _trueExpressionNumbers.Length; i++)
        {
            _stringExpressionNumbers[i] = _expressionNumbers[i].ToString();
        }
    }

    public void GenerateTrueExpression()
    {
        while (true)
        {
            int maxNumber = 1;
            switch (expressionOperation)
            {
                case "+":
                    _trueExpressionNumbers[0] = Random.Range(1, 10);
                    _trueExpressionNumbers[1] = Random.Range(1, 10);
                    _trueExpressionNumbers[2] = _trueExpressionNumbers[0] + _trueExpressionNumbers[1];
                    maxNumber = _trueExpressionNumbers[2];
                    break;
                case "-":
                    _trueExpressionNumbers[1] = Random.Range(1, 10);
                    _trueExpressionNumbers[2] = Random.Range(1, 10);
                    _trueExpressionNumbers[0] = _trueExpressionNumbers[2] + _trueExpressionNumbers[1];
                    maxNumber = _trueExpressionNumbers[0];
                    break;
                case "*":
                    _trueExpressionNumbers[0] = Random.Range(2, 10);
                    _trueExpressionNumbers[1] = Random.Range(3, 10);
                    _trueExpressionNumbers[2] = _trueExpressionNumbers[0] * _trueExpressionNumbers[1];
                    maxNumber = _trueExpressionNumbers[2];
                    break;
                case "÷":
                    _trueExpressionNumbers[1] = Random.Range(2, 10);
                    _trueExpressionNumbers[2] = Random.Range(2, 10);
                    _trueExpressionNumbers[0] = _trueExpressionNumbers[2] * _trueExpressionNumbers[1];
                    maxNumber = _trueExpressionNumbers[0];
                    break;
            }

            if (maxNumber < minAnswerNumber || maxAnswerNumber < maxNumber) continue;
            break;
        }
    }

    private static int AddNumber(int expressionBlock)
    {
        string expressionBlockString = expressionBlock + " ";
        int length = Random.Range(0, expressionBlockString.Length);
        int newFailNumber = Random.Range(1, 10);
        string newExpressionBlockStringWithFail = expressionBlockString.Substring(0, length) + newFailNumber + expressionBlockString.Substring(length, expressionBlockString.Length - length);

        return Convert.ToInt32(newExpressionBlockStringWithFail);;
    }

    public void DeleteNumberAndCheckExpressionFailOrTrue(int i, int subID)
    {
        bool expressionIsFail = false;

        char[] expressionBlockChars = _stringExpressionNumbers[i].ToCharArray();
        expressionBlockChars[subID] = ' ';
        _stringExpressionNumbers[i] = "";
        string expressionBlock = "";
        foreach (char ch in expressionBlockChars)
        {
            _stringExpressionNumbers[i] += ch;
            if (ch != ' ') expressionBlock += ch;
        }

        if (expressionBlock == "") expressionIsFail = true;
        else
        {
            _expressionNumbers[i] = Convert.ToInt32(expressionBlock);
            if (!CheckMayBeExpressionTrue()) expressionIsFail = true;
        }

        if (expressionIsFail)
        {
            foreach (var symbol in _expressionSymbols)
            {    
                if(symbol == null) continue;
                symbol.canIBeDestroyed = false;
                symbol.StartDarness();
            }

            EndLevelControl.Instance.EndLvl(false, 
                GameTexts.errorMathTexts[Random.Range(0, GameTexts.errorMathTexts.Length)].GetText());
        }

        if (CheckExpressionTrue())
        {
            ExpressionsControl.Instance.AddSolvedExpressionCount();
            TimerBar.Instance.RestartTimer();
            StartCoroutine(DestroyExpressionCoroutine());
        }
    }

    private bool CheckMayBeExpressionTrue()
    {
        for (int i = 0; i < _trueExpressionNumbers.Length; i++)
        {
            var numberChars = _trueExpressionNumbers[i].ToString().ToCharArray();
            var expressionCheckNumberChars = _expressionNumbers[i].ToString().ToCharArray();
            var expressionCheckIndex = 0;
            for (int j = 0; j < numberChars.Length; )
            {
                if(j == numberChars.Length) break;
                if (expressionCheckIndex == expressionCheckNumberChars.Length)
                {
                    return false;
                }
                if (numberChars[j] == expressionCheckNumberChars[expressionCheckIndex])
                {
                    j++;
                }

                expressionCheckIndex++;
            }
        }

        return true;
    }

    private bool CheckExpressionTrue()
    {
        bool expressionTrue = true;

        for (int i = 0; i < _trueExpressionNumbers.Length; i++)
        {
            if (_trueExpressionNumbers[i] != _expressionNumbers[i]) expressionTrue = false;
        }

        return expressionTrue;
    }

    private IEnumerator DestroyExpressionCoroutine()
    {
        while (_expressionSymbols.Count != 0)
        {
            if(_expressionSymbols[0] != null) _expressionSymbols[0].DestrMe();
            _expressionSymbols.RemoveAt(0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
