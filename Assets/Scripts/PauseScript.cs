using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI = null;
    
        public void PushPauseButton()
        {            
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;                
            }
            else
            {
            //　ポーズUIが表示されてなければ通常通り進行
                Time.timeScale = 1f;
            }
        }
}
