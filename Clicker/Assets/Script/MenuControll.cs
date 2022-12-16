using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuControll : MonoBehaviour
{

    //Variables
    [Header("UI Component")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] private Image brightnessPanel;


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene las resoluciones posibles de la pantalla                                                                  */
    /*********************************************************************************************************************************/
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetResolution                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: resolutionIndex (resolucion seleccionada del dropbox)                                                   */
    /*Descripción: Modifica la resolucion del juego                                                                                  */
    /*********************************************************************************************************************************/
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    /*********************************************************************************************************************************/
    /*Funcion: SetMasterVolume                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen master del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetMusicVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen musica del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }
    /*********************************************************************************************************************************/
    /*Funcion: SetSoundVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen de sonidos juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundsVolume", Mathf.Log10(volume) * 20);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetQuality                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: qualityIndex (index del dropbox)                                                                        */
    /*Descripción: Modifica la calidad de graficos con el valor recibido                                                             */
    /*********************************************************************************************************************************/
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    /*********************************************************************************************************************************/
    /*Funcion: SetFullScreen                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: isFullscreen (booleana para controlar si esta en fullscreen o no el juego)                              */
    /*Descripción: activa o desactiva la pantalla completa                                                                           */
    /*********************************************************************************************************************************/
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void SetBrightness(float value)
    {
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, value);
        
    }


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
