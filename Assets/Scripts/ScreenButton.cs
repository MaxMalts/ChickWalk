using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;



namespace UnityEngine.InputSystem.OnScreen {

	public class ScreenButton : OnScreenControl, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

		[InputControl(layout = "Button")]
		[SerializeField]
		private string m_ControlPath;

		protected override string controlPathInternal {
			get => m_ControlPath;
			set => m_ControlPath = value;
		}


		public void OnPointerDown(PointerEventData eventData) {
			Debug.Log("Pointer down.");
		}


		public void OnPointerUp(PointerEventData eventData) {
			Debug.Log("Pointer up.");
		}


		public void OnPointerEnter(PointerEventData eventData) {
			Debug.Log("Pointer enter.");
			SendValueToControl(1.0f);
		}


		public void OnPointerExit(PointerEventData eventData) {
			Debug.Log("Pointer exit.");
			SendValueToControl(0.0f);
		}
	}
}
