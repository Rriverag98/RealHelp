using System;

[System.Serializable]
public class Questions {

    public string fact;
    public bool isTrue;

    public static implicit operator string(Questions v)
    {
        throw new NotImplementedException();
    }
}
