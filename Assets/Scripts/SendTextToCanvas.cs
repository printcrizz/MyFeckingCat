using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendTextToCanvas : MonoBehaviour
{
    public string textToSend;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SendTextAndActivateCanvas()
    {
        //string newtext = textToSend.Replace("\\n", "\n");
        text2.SetText(textToSend);
        anim.SetBool("IsActive",true);
        StartCoroutine("DeactivateCanvas");
    }

    private IEnumerator DeactivateCanvas()
    {
        yield return new WaitForSeconds(10f);
        anim.SetBool("IsActive", false);

    }
}
