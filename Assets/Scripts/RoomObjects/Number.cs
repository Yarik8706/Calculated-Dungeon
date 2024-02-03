using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    private ExpressionControl _expressionControl;
    private int _id;
    private int _subID;
    private ExpressionSymbol _expressionSymbol;

    private void Start()
    {
        _expressionSymbol = GetComponent<ExpressionSymbol>();
        _expressionControl = GetComponentInParent<ExpressionControl>();
    }

    public void SetNumber(ExpressionControl expressionControl, int i, int sub_i)
    {
        _expressionControl = expressionControl;
        _id = i;
        _subID = sub_i;
    }

    public void DestrMe()
    {
        if (!_expressionSymbol.canIBeDestroyed) return;

        _expressionControl.DeleteNumberAndCheckExpressionFailOrTrue(_id, _subID);
        _expressionSymbol.DestrMe();
    }
}
