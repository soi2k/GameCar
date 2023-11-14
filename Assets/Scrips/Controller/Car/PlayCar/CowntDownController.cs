using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowntDownController : MonoBehaviour,IStartCownDown
{
    [SerializeField] GameObject cowntdown;
    public void StartCowntDown()
    {
        Instantiate(cowntdown);
    }
}
