using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuControll : MonoBehaviour
{

    //Variables    
    [Header("Resolution")]
    [SerializeField] private Dropdown resolutionDropdown;
    Resolution[] resolutions;    
    [SerializeField] private Toggle fullscreenCheck;

    [Header("Quality")]
    [SerializeField] private Dropdown qualityDropdown;
    
    [Header("Brightness")]
    [SerializeField] private Image brightnessPanel;
    [SerializeField] private Slider brightnessSlider;

    [Header("Sounds")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider soundVolume;

    private bool update = false;
    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Obtiene las resoluciones posibles de la pantalla para generarlas en el dropbox de resoluciones sin que se repitan */
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
            if(i>=2 && (resolutions[i].width != resolutions[i-1].width || resolutions[i].height != resolutions[i-1].height))
                options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)            
                currentResolutionIndex = i;            
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        LoadSetting();
    }   

    /*********************************************************************************************************************************/
    /*Funcion: SetResolution                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: resolutionIndex (resolucion seleccionada del dropbox)                                                   */
    /*Descripci�n: Modifica la resolucion del juego                                                                                  */
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
    /*Descripci�n: Modifica el volumen master del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetMusicVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripci�n: Modifica el volumen musica del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetSoundVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripci�n: Modifica el volumen de sonidos juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundsVolume", Mathf.Log10(volume) * 20);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetQuality                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: qualityIndex (index del dropbox)                                                                        */
    /*Descripci�n: Modifica la calidad de graficos con el valor recibido                                                             */
    /*********************************************************************************************************************************/
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetFullScreen                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: isFullscreen (booleana para controlar si esta en fullscreen o no el juego)                              */
    /*Descripci�n: activa o desactiva la pantalla completa                                                                           */
    /*********************************************************************************************************************************/
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetBrightness                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: value (valor del slider para aumentar o reducir el brillo)                                              */
    /*Descripci�n: activa o desactiva la pantalla completa                                                                           */
    /*********************************************************************************************************************************/
    public void SetBrightness(float value)
    {
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, value);       
    }

    /*********************************************************************************************************************************/
    /*Funcion: ShowPanel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: showImage (panel a mostrar)                                                                             */
    /*Descripci�n: Activa el objeto que recibe como parametro                                                                        */
    /*********************************************************************************************************************************/
    public void ShowPanel(GameObject showImage)
    {
        showImage.gameObject.SetActive(true);
    }

    /*********************************************************************************************************************************/
    /*Funcion: Hide                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: hideImage (panel a ocultar)                                                                             */
    /*Descripci�n: Desactiva el objeto que recibe como parametro                                                                     */
    /*********************************************************************************************************************************/
    public void Hide(GameObject hideImage)
    {
        Animator anim = hideImage.GetComponent<Animator>();
        anim.SetBool("Hide", true);

        StartCoroutine(HideObject(hideImage));
    }

    /*********************************************************************************************************************************/
    /*Funcion: QuitGame                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: quitPanel (panel a mostrar)                                                                             */
    /*Descripci�n: Activa el panel de confirmar si se desea salir del juego                                                          */
    /*********************************************************************************************************************************/
    public void QuitGame(GameObject quitPanel)
    {
        ShowPanel(quitPanel);
    }

    /*********************************************************************************************************************************/
    /*Funcion: CloseGame                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripci�n: Cierra la aplicacion por completo                                                                                 */
    /*********************************************************************************************************************************/
    public void CloseGame()
    {
        Application.Quit();
    }

    /*********************************************************************************************************************************/
    /*Funcion: LoadSetting                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Carga los parametros guardados en playerpref y asigna los valores a los componentes de la UI                      */
    /*********************************************************************************************************************************/
    private void LoadSetting()
    {
        if (PlayerPrefs.GetInt("fullScreen") == 1)
            fullscreenCheck.isOn = true;
        else
            fullscreenCheck.isOn = false;
        SetFullScreen(fullscreenCheck.isOn);
        qualityDropdown.value = PlayerPrefs.GetInt("quality", 0);
        SetQuality(qualityDropdown.value);
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", 0);
        SetResolution(resolutionDropdown.value);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0f);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSlider.value);
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        SetMasterVolume(masterVolume.value);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        SetMusicVolume(musicVolume.value);
        soundVolume.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        SetSoundVolume(soundVolume.value);

    }

    /*********************************************************************************************************************************/
    /*Funcion: Deny                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Cancela los cambios realizados en el menu de opciones si no se han guardado previamente                           */
    /*********************************************************************************************************************************/
    public void Deny()
    {
        LoadSetting();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Accept                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Acepta los cambios realizados en el menu de opciones y los envia a guardar en los playerpref                      */
    /*********************************************************************************************************************************/
    public void Accept()
    {
        SaveSetting();
    }

    /*********************************************************************************************************************************/
    /*Funcion: SaveSetting                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Guarda los valores de las opciones en los distintos playerpref                                                    */
    /*********************************************************************************************************************************/
    private void SaveSetting()
    {
        if (fullscreenCheck.isOn)
            PlayerPrefs.SetInt("fullScreen", 1);
        else
            PlayerPrefs.SetInt("fullScreen", 0);
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("soundVolume", soundVolume.value);
    }


    public void Resume(GameObject hideImage)
    {
        Time.timeScale = 1;        
        Hide(hideImage);
        
    }

    public void Pause(GameObject showImage)
    {
        Time.timeScale = 0;      
        ShowPanel(showImage);


    }

    public void LoadScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    /*********************************************************************************************************************************/
    /*Funcion: HideObject                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: hideImage (objeto a desactivar)                                                                         */
    /*Descripci�n: desactiva el objeto que recibe pasado un segundo                                                                  */
    /*********************************************************************************************************************************/
    public IEnumerator HideObject(GameObject hideImage)
    {
        yield return new WaitForSecondsRealtime(1);      
        hideImage.SetActive(false);
    }
}
