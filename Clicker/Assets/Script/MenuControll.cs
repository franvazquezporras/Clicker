using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
   


    public void ShowPanel(GameObject showImage)
    {
        showImage.gameObject.SetActive(true);
    }
    public void Hide(GameObject hideImage)
    {
        Animator anim = hideImage.GetComponent<Animator>();
        anim.SetBool("Hide", true);

        StartCoroutine(HideObject(hideImage));
    }
    public void QuitGame(GameObject quitPanel)
    {
        ShowPanel(quitPanel);
    }

    
    public void CloseGame()
    {
        Application.Quit();
    } 



    public IEnumerator HideObject(GameObject hideImage)
    {
        if(hideImage.name =="AlertPanel")
            yield return new WaitForSeconds(1);
        else
            yield return new WaitForSeconds(2);
        hideImage.SetActive(false);
    }
}
