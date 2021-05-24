
using UnityEngine;
using UnityEngine.UI;
 
namespace Tank
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Health3 pHealth;
 
        
        void Update(){
            slider.maxValue = pHealth.MaxHealth;
            slider.value = pHealth.Health;
        }
    }
}